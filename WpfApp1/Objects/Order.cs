using System;
using System.Collections.Generic;

namespace WpfApp1;

public class Order
{
    public List<Pizza> pizzas { get; set; }
    public List<Drink> drinks { get; set; } = new List<Drink>();
    public int orderNumber { get; set; }
    public DateTime orderTime { get; set; }
    public DateTime orderDate { get; set; }
    public string customerName { get; set; }
    public string clerkName { get; set; }
    public Address address { get; set; }
    private static int counter;
    
    public Order(List<Pizza> pizzas, List<Drink> drinks, string customerName, string clerkName, Address address)
    {
        this.pizzas = pizzas;
        this.drinks = drinks;
        orderNumber = counter;
        orderTime = DateTime.Now;
        orderDate = DateTime.Today;
        this.customerName = customerName;
        this.clerkName = clerkName;
        this.address = address;
        counter++;
    }
    
    public Order(List<Pizza> pizzas, string customerName, string clerkName, Address address)
    {
        this.pizzas = pizzas;
        this.orderNumber = orderNumber;
        this.orderTime = orderTime;
        this.orderDate = orderDate;
        this.customerName = customerName;
        this.clerkName = clerkName;
        this.address = address;
    }
}