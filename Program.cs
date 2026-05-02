using System;
using System.Text;

class Product
{
    public int Id;
    public string Name;
    public string Category;
    public double Price;
    public int RemainingStock;

    public void DisplayProduct()
    {
        Console.WriteLine($"{Id}. {Name} [{Category}] - ₱{Price:F2} (Stock: {RemainingStock})");
    }
}

class CartItem
{
    public Product Product;
    public int Quantity;
    public double Subtotal;

    public void UpdateSubtotal()
    {
        Subtotal = Product.Price * Quantity;
    }
}

class OrderHistory
{
    public string ReceiptNumber;
    public DateTime OrderDate;
    public double FinalTotal;
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        Product[] products = new Product[]
        {
            new Product { Id = 1, Name = "Coffee", Category = "Food", Price = 120, RemainingStock = 10 },
            new Product { Id = 2, Name = "Pasta", Category = "Food", Price = 250, RemainingStock = 5 },
            new Product { Id = 3, Name = "Mouse", Category = "Electronics", Price = 500, RemainingStock = 8 },
            new Product { Id = 4, Name = "Keyboard", Category = "Electronics", Price = 1200, RemainingStock = 3 },
            new Product { Id = 5, Name = "Shirt", Category = "Clothing", Price = 350, RemainingStock = 7 }
        };

        CartItem[] cart = new CartItem[20];
        int cartCount = 0;

        OrderHistory[] history = new OrderHistory[20];
        int historyCount = 0;

        int receiptCounter = 1;

        while (true)
        {
            Console.WriteLine("\n=== SHOPPING CART SYSTEM ===");
            Console.WriteLine("1. View Products");
            Console.WriteLine("2. Search Product");
            Console.WriteLine("3. Filter by Category");
            Console.WriteLine("4. Add to Cart");
            Console.WriteLine("5. Manage Cart");
            Console.WriteLine("6. View Order History");
            Console.WriteLine("7. Exit");

            int mainChoice = GetValidatedNumber("Enter choice: ");

            switch (mainChoice)
            {
                case 1:
                    DisplayProducts(products);
                    break;

                case 2:
                    SearchProduct(products);
                    break;

                case 3:
                    FilterByCategory(products);
                    break;

                case 4:
                    AddToCart(products, cart, ref cartCount);
                    break;

                case 5:
                    ManageCart(products, cart, ref cartCount, history,
                        ref historyCount, ref receiptCounter);
                    break;

                case 6:
                    ViewOrderHistory(history, historyCount);
                    break;

                case 7:
                    Console.WriteLine("Thank you for shopping!");
                    return;

                default:
                    Console.WriteLine("Invalid menu choice.");
                    break;
            }
        }
    }

    static void DisplayProducts(Product[] products)
    {
        Console.WriteLine("\n=== PRODUCT LIST ===");

        foreach (var p in products)
        {
            p.DisplayProduct();
        }
    }

    static void SearchProduct(Product[] products)
    {
        Console.Write("\nEnter product name to search: ");
        string keyword = Console.ReadLine().ToLower();

        bool found = false;

        foreach (var p in products)
        {
            if (p.Name.ToLower().Contains(keyword))
            {
                p.DisplayProduct();
                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine("No product found.");
        }
    }

    static void FilterByCategory(Product[] products)
    {
        Console.WriteLine("\n1. Food");
        Console.WriteLine("2. Electronics");
        Console.WriteLine("3. Clothing");

        int categoryChoice = GetValidatedNumber("Choose category: ");

        string selectedCategory = "";

        switch (categoryChoice)
        {
            case 1:
                selectedCategory = "Food";
                break;

            case 2:
                selectedCategory = "Electronics";
                break;

            case 3:
                selectedCategory = "Clothing";
                break;

            default:
                Console.WriteLine("Invalid category.");
                return;
        }

        Console.WriteLine($"\n=== {selectedCategory.ToUpper()} ===");

        foreach (var p in products)
        {
            if (p.Category == selectedCategory)
            {
                p.DisplayProduct();
            }
        }
    }

    static void AddToCart(Product[] products, CartItem[] cart, ref int cartCount)
    {
        DisplayProducts(products);

        int productChoice = GetValidatedNumber("\nEnter product number: ");

        if (productChoice < 1 || productChoice > products.Length)
        {
            Console.WriteLine("Invalid product.");
            return;
        }

        Product selectedProduct = products[productChoice - 1];

        if (selectedProduct.RemainingStock <= 0)
        {
            Console.WriteLine("Product is out of stock.");
            return;
        }

        int quantity = GetValidatedNumber("Enter quantity: ");

        if (quantity <= 0)
        {
            Console.WriteLine("Invalid quantity.");
            return;
        }

        if (quantity > selectedProduct.RemainingStock)
        {
            Console.WriteLine("Not enough stock.");
            return;
        }

        bool found = false;

        for (int i = 0; i < cartCount; i++)
        {
            if (cart[i].Product.Id == selectedProduct.Id)
            {
                cart[i].Quantity += quantity;
                cart[i].UpdateSubtotal();
                found = true;
                break;
            }
        }

        if (!found)
        {
            cart[cartCount] = new CartItem
            {
                Product = selectedProduct,
                Quantity = quantity
            };

            cart[cartCount].UpdateSubtotal();
            cartCount++;
        }

        selectedProduct.RemainingStock -= quantity;

        Console.WriteLine("Item added to cart.");
    }

    static void ManageCart(Product[] products, CartItem[] cart,
        ref int cartCount, OrderHistory[] history,
        ref int historyCount, ref int receiptCounter)
    {
        while (true)
        {
            Console.WriteLine("\n=== CART MENU ===");
            Console.WriteLine("1. View Cart");
            Console.WriteLine("2. Update Quantity");
            Console.WriteLine("3. Remove Item");
            Console.WriteLine("4. Clear Cart");
            Console.WriteLine("5. Checkout");
            Console.WriteLine("6. Back");

            int choice = GetValidatedNumber("Enter choice: ");

            switch (choice)
            {
                case 1:
                    ViewCart(cart, cartCount);
                    break;

                case 2:
                    UpdateCart(cart, cartCount);
                    break;

                case 3:
                    RemoveItem(cart, ref cartCount);
                    break;

                case 4:
                    ClearCart(cart, ref cartCount);
                    break;

                case 5:
                    Checkout(products, cart, ref cartCount,
                        history, ref historyCount,
                        ref receiptCounter);
                    return;

                case 6:
                    return;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }

    static void ViewCart(CartItem[] cart, int cartCount)
    {
        Console.WriteLine("\n=== CART ===");

        if (cartCount == 0)
        {
            Console.WriteLine("Cart is empty.");
            return;
        }

        double total = 0;

        for (int i = 0; i < cartCount; i++)
        {
            Console.WriteLine(
                $"{i + 1}. {cart[i].Product.Name} x{cart[i].Quantity} = ₱{cart[i].Subtotal:F2}");

            total += cart[i].Subtotal;
        }

        Console.WriteLine($"Total: ₱{total:F2}");
    }

    static void UpdateCart(CartItem[] cart, int cartCount)
    {
        if (cartCount == 0)
        {
            Console.WriteLine("Cart is empty.");
            return;
        }

        ViewCart(cart, cartCount);

        int item = GetValidatedNumber("Select item number: ");

        if (item < 1 || item > cartCount)
        {
            Console.WriteLine("Invalid item.");
            return;
        }

        int newQty = GetValidatedNumber("Enter new quantity: ");

        if (newQty <= 0)
        {
            Console.WriteLine("Invalid quantity.");
            return;
        }

        cart[item - 1].Quantity = newQty;
        cart[item - 1].UpdateSubtotal();

        Console.WriteLine("Cart updated.");
    }

    static void RemoveItem(CartItem[] cart, ref int cartCount)
    {
        if (cartCount == 0)
        {
            Console.WriteLine("Cart is empty.");
            return;
        }

        ViewCart(cart, cartCount);

        int item = GetValidatedNumber("Select item to remove: ");

        if (item < 1 || item > cartCount)
        {
            Console.WriteLine("Invalid item.");
            return;
        }

        for (int i = item - 1; i < cartCount - 1; i++)
        {
            cart[i] = cart[i + 1];
        }

        cartCount--;

        Console.WriteLine("Item removed.");
    }

    static void ClearCart(CartItem[] cart, ref int cartCount)
    {
        cartCount = 0;
        Console.WriteLine("Cart cleared.");
    }

    static void Checkout(Product[] products, CartItem[] cart,
        ref int cartCount, OrderHistory[] history,
        ref int historyCount, ref int receiptCounter)
    {
        if (cartCount == 0)
        {
            Console.WriteLine("Cart is empty.");
            return;
        }

        Console.WriteLine("\n=== RECEIPT ===");

        string receiptNo = receiptCounter.ToString("D4");

        Console.WriteLine($"Receipt No: {receiptNo}");
        Console.WriteLine($"Date: {DateTime.Now}");

        double grandTotal = 0;

        for (int i = 0; i < cartCount; i++)
        {
            Console.WriteLine(
                $"{cart[i].Product.Name} x{cart[i].Quantity} = ₱{cart[i].Subtotal:F2}");

            grandTotal += cart[i].Subtotal;
        }

        Console.WriteLine($"Grand Total: ₱{grandTotal:F2}");

        double discount = 0;

        if (grandTotal >= 5000)
        {
            discount = grandTotal * 0.10;
        }

        Console.WriteLine($"Discount: ₱{discount:F2}");

        double finalTotal = grandTotal - discount;

        Console.WriteLine($"Final Total: ₱{finalTotal:F2}");

        double payment;

        while (true)
        {
            Console.Write("Enter payment: ");

            if (!double.TryParse(Console.ReadLine(), out payment))
            {
                Console.WriteLine("Invalid payment.");
                continue;
            }

            if (payment < finalTotal)
            {
                Console.WriteLine("Insufficient payment.");
                continue;
            }

            break;
        }

        double change = payment - finalTotal;

        Console.WriteLine($"Payment: ₱{payment:F2}");
        Console.WriteLine($"Change: ₱{change:F2}");

        history[historyCount] = new OrderHistory
        {
            ReceiptNumber = receiptNo,
            OrderDate = DateTime.Now,
            FinalTotal = finalTotal
        };

        historyCount++;
        receiptCounter++;

        cartCount = 0;

        Console.WriteLine("\nLOW STOCK ALERT:");

        foreach (var p in products)
        {
            if (p.RemainingStock <= 5)
            {
                Console.WriteLine(
                    $"{p.Name} has only {p.RemainingStock} stocks left.");
            }
        }
    }

    static void ViewOrderHistory(OrderHistory[] history, int historyCount)
    {
        Console.WriteLine("\n=== ORDER HISTORY ===");

        if (historyCount == 0)
        {
            Console.WriteLine("No orders yet.");
            return;
        }

        for (int i = 0; i < historyCount; i++)
        {
            Console.WriteLine(
                $"Receipt #{history[i].ReceiptNumber} - " +
                $"Final Total: ₱{history[i].FinalTotal:F2} - " +
                $"{history[i].OrderDate}");
        }
    }

    static int GetValidatedNumber(string message)
    {
        int number;

        while (true)
        {
            Console.Write(message);

            if (int.TryParse(Console.ReadLine(), out number))
            {
                return number;
            }

            Console.WriteLine("Invalid input. Enter numbers only.");
        }
    }

    static string GetYesNo(string message)
    {
        string choice;

        while (true)
        {
            Console.Write(message);

            choice = Console.ReadLine().ToUpper();

            if (choice == "Y" || choice == "N")
            {
                return choice;
            }

            Console.WriteLine("Invalid input. Please enter Y or N only.");
        }
    }
}
