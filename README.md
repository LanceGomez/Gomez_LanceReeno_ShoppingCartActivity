# Gomez_LanceReeno_EnhancedShoppingCartActivity
# Enhanced Shopping Cart System

A console-based shopping cart application developed in C#. This program simulates a realistic shopping and checkout process with features such as cart handling, inventory tracking, payment verification, receipt printing, and transaction history recording.

## Overview

This project showcases the use of object-oriented programming principles through the implementation of classes, objects, arrays, loops, methods, and input validation. The system is designed to provide an organized shopping workflow while monitoring product inventory and customer transactions.

---

# Key Features

## Product Handling
- Displays available products with corresponding prices and stock
- Prevents users from purchasing unavailable quantities
- Automatically decreases stock after successful checkout

## Cart Features
- Add products to cart
- Display cart contents and totals
- Modify product quantities
- Remove products from cart
- Empty the cart

## Checkout Process
- Produces a complete receipt
- Applies automatic discounts for purchases above ₱5000
- Verifies customer payment input
- Computes the customer’s change

## Receipt Details
- Generates receipt numbers
- Displays transaction date and time
- Shows purchased products and total costs
- Includes payment amount and change

## Inventory Monitoring
- Updates product inventory instantly
- Displays low-stock notifications for products with stock less than or equal to 5

## Transaction History
- Saves completed orders during runtime
- Displays receipt number, transaction date, and total amount

## Input Validation
- Prevents invalid numeric entries
- Re-prompts invalid Y/N responses
- Maintains smooth and error-free execution


---

# Sample Output

```text
=== STORE MENU ===

1. Coffee - ₱120.00 (Stock: 10)
2. Pasta - ₱250.00 (Stock: 5)
3. Salad - ₱180.00 (Stock: 8)
4. Cake - ₱150.00 (Stock: 3)

Enter product number: 2
Enter quantity: 3

Item added to cart.

Add another item? (Y/N): Y

Enter product number: 1
Enter quantity: 2

Item added to cart.

Add another item? (Y/N): N


=== CART MENU ===
1. View Cart
2. Update Quantity
3. Remove Item
4. Clear Cart
5. Checkout

Enter choice: 5


=== RECEIPT ===

Receipt No: 0002
Date: May 02, 2026 10:45 PM

Pasta x3 = ₱750.00
Coffee x2 = ₱240.00

Grand Total: ₱990.00
Discount: ₱0.00
Final Total: ₱990.00

Enter payment: 1200

Payment: ₱1200.00
Change: ₱210.00


=== ORDER HISTORY ===

Receipt #0002 - Final Total: ₱990.00 - May 02, 2026 10:45 PM


=== LOW STOCK ALERT ===

Pasta has only 2 stocks left.

Thank you for shopping!
```

---

# AI Usage

AI tools were utilized to:
- Troubleshoot and enhance program logic
- Improve validation and cart operations
- Refine code readability and organization
- Assist with receipt generation and transaction history features
