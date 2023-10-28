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
    public Customer customer { get; set; }
    public Clerk clerk { get; set; }
    private static int counter;
    
    public Order(List<Pizza> pizzas, List<Drink> drinks, Customer customer, Clerk clerk)
    {
        this.pizzas = pizzas;
        this.drinks = drinks;
        orderNumber = counter;
        orderTime = DateTime.Now;
        orderDate = DateTime.Today;
        this.customer = customer;
        this.clerk = clerk;
        counter++;
    }
    
    public Order(List<Pizza> pizzas, Customer customer, Clerk clerk)
    {
        this.pizzas = pizzas;
        orderNumber = counter;
        orderTime = DateTime.Now;
        orderDate = DateTime.Today;
        this.customer = customer;
        this.clerk = clerk;
        counter++;
    }
    
    public double calculateTotalPrice()
    {
        double totalPrice = 0;
        foreach (Pizza pizza in pizzas)
        {
            totalPrice += pizza.calculatePrice();
        }
        foreach (Drink drink in drinks)
        {
            totalPrice += drink.calculatePrice();
        }
        return totalPrice;
    }
}