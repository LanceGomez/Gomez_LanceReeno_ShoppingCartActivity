# Gomez_LanceReeno_ShoppingCartActivity
This project is a console-based Shopping Cart System developed in C#. It simulates a simple café/store where users can browse products, select items, and add them to a cart while considering available stock.

The system allows users to input product choices and quantities, automatically calculates totals, and updates inventory in real time. It also generates a receipt with the overall cost and applies a discount for qualifying purchases.

This program demonstrates the use of basic C# concepts such as classes, objects, arrays, loops, and input validation in creating a functional and interactive shopping experience.

The process began by designing a Product class to store item details such as ID, name, price, and remaining stock, along with methods for displaying products and handling stock operations.

An array of product objects was then created to serve as the store menu. A separate CartItem class was implemented to manage selected items, including their quantity and subtotal.

The main logic was built using loops and conditional statements to handle user input, validate entries, and ensure stock availability. Features such as preventing duplicate cart entries, updating stock in real time, and limiting cart size were also added.

Finally, the program computes the total cost, applies a discount when applicable, and displays a receipt along with the updated inventory after checkout.

AI Usage in This Project

AI was used as a guide while developing this program, especially for debugging and fixing errors. I used it to help identify issues in my code, such as input validation problems and logical errors in handling the shopping cart and stock updates.

It also helped me understand why certain errors were happening and how to fix them properly. One specific example was when the peso sign (₱) was showing as a “?” in the console. I asked for help, and it suggested using UTF-8 encoding, which solved the problem.

Aside from debugging, I also used AI to improve parts of my code and make sure it followed the given requirements. All suggestions were reviewed and adjusted to match my understanding before applying them to the final program.
