using System;
using System.Text;

class Product
{
    public int Id;
    public string Name;
    public double Price;
    public int RemainingStock;

    public void DisplayProduct()
    {
        Console.WriteLine($"{Id}. {Name} - ₱{Price:F2} (Stock: {RemainingStock})");
    }

    public double GetItemTotal(int quantity)
    {
        return Price * quantity;
    }

    public bool HasEnoughStock(int quantity)
    {
        return quantity <= RemainingStock;
    }

    public void DeductStock(int quantity)
    {
        RemainingStock -= quantity;
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

class Program
{
    static void Main()
    {
        // Fix for peso sign display
        Console.OutputEncoding = Encoding.UTF8;

        Product[] products = new Product[]
        {
            new Product { Id = 1, Name = "Coffee", Price = 120, RemainingStock = 10 },
            new Product { Id = 2, Name = "Pasta", Price = 250, RemainingStock = 5 },
            new Product { Id = 3, Name = "Salad", Price = 180, RemainingStock = 8 },
            new Product { Id = 4, Name = "Cake", Price = 150, RemainingStock = 0 }
        };

        CartItem[] cart = new CartItem[5];
        int cartCount = 0;

        while (true)
        {
            Console.WriteLine("\n=== STORE MENU ===");

            foreach (var p in products)
            {
                p.DisplayProduct();
            }

            Console.Write("\nEnter product number: ");
            string prodInput = Console.ReadLine();

            if (!int.TryParse(prodInput, out int prodChoice) ||
                prodChoice < 1 ||
                prodChoice > products.Length)
            {
                Console.WriteLine("Invalid product number.");
                continue;
            }

            Product selectedProduct = products[prodChoice - 1];

            if (selectedProduct.RemainingStock == 0)
            {
                Console.WriteLine("Product is out of stock.");
                continue;
            }

            Console.Write("Enter quantity: ");
            string qtyInput = Console.ReadLine();

            if (!int.TryParse(qtyInput, out int quantity) || quantity <= 0)
            {
                Console.WriteLine("Invalid quantity.");
                continue;
            }

            if (!selectedProduct.HasEnoughStock(quantity))
            {
                Console.WriteLine("Not enough stock available.");
                continue;
            }

            // Check for duplicate product in cart
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

            // Add new product to cart
            if (!found)
            {
                if (cartCount >= cart.Length)
                {
                    Console.WriteLine("Cart is full.");
                    continue;
                }

                cart[cartCount] = new CartItem
                {
                    Product = selectedProduct,
                    Quantity = quantity
                };

                cart[cartCount].UpdateSubtotal();
                cartCount++;
            }

            // Deduct stock
            selectedProduct.DeductStock(quantity);

            Console.WriteLine("Item added to cart.");

            // Fixed Y/N validation
            string choice;

            while (true)
            {
                Console.Write("Add another item? (Y/N): ");
                choice = Console.ReadLine().ToUpper();

                if (choice == "Y" || choice == "N")
                {
                    break;
                }

                Console.WriteLine("Invalid input. Please enter Y or N only.");
            }

            if (choice == "N")
            {
                break;
            }
        }

        // Receipt
        Console.WriteLine("\n=== RECEIPT ===");

        double grandTotal = 0;

        for (int i = 0; i < cartCount; i++)
        {
            Console.WriteLine(
                $"{cart[i].Product.Name} x{cart[i].Quantity} = ₱{cart[i].Subtotal:F2}"
            );

            grandTotal += cart[i].Subtotal;
        }

        Console.WriteLine($"Grand Total: ₱{grandTotal:F2}");

        // Discount
        double discount = 0;

        if (grandTotal >= 5000)
        {
            discount = grandTotal * 0.10;
            Console.WriteLine($"Discount (10%): ₱{discount:F2}");
        }

        double finalTotal = grandTotal - discount;

        Console.WriteLine($"Final Total: ₱{finalTotal:F2}");

        // Updated stock
        Console.WriteLine("\n=== UPDATED STOCK ===");

        foreach (var p in products)
        {
            Console.WriteLine($"{p.Name}: {p.RemainingStock}");
        }

        Console.WriteLine("\nThank you for shopping!");
    }
}
