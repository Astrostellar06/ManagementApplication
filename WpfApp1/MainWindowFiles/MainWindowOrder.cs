﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace WpfApp1
{
    public partial class MainWindow
    {
        public Order orderBeingModified;
        public bool remove1;
        public bool remove2;
        public delegate void CookingDelegate();
        public delegate void DelivererMessageDelegate(string msg);
        List<Pizza> pizzas = new List<Pizza>();
        List<Drink> drinks = new List<Drink>();
        public static List<Order> orders = new List<Order>();
        public static List<Order> deliveryOrders = new List<Order>();
        public static List<Order> currentDeliveryOrders = new List<Order>();
        public static List<Order> pastOrders = new List<Order>();
        
        private void AddPizza(object sender, RoutedEventArgs e)
        {
            if (PizzaSizeList.SelectedItem.Equals(default2) || PizzaList.SelectedItem.Equals(default1))
                MessageBox.Show("Please select a type and a size of pizza.");
            else
            {
                ComboBoxItem selectedPizza = (ComboBoxItem)PizzaList.SelectedItem;
                ComboBoxItem selectedPizzaSize = (ComboBoxItem)PizzaSizeList.SelectedItem;
                Pizza.Size size = (Pizza.Size)Enum.Parse(typeof(Pizza.Size), selectedPizzaSize.Content.ToString());
                Pizza.Type type = (Pizza.Type)Enum.Parse(typeof(Pizza.Type), selectedPizza.Content.ToString());
                Pizza pizza = new Pizza(size, type);
                pizzas.Add(pizza);
                PizzaOrderList.Items.Add(selectedPizzaSize.Content + " " + selectedPizza.Content);
                PizzaOrderList.Visibility = Visibility.Visible;
                PizzaOrderListLabel.Visibility = Visibility.Visible;
            }
        }

        private void AddDrink(object sender, RoutedEventArgs e)
        {
            if (DrinkList.SelectedItem.Equals(default3) || DrinkList.SelectedItem.Equals(default4))
                MessageBox.Show("Please select a drink and a size.");
            else
            {
                ComboBoxItem selectedDrink = (ComboBoxItem)DrinkList.SelectedItem;
                ComboBoxItem selectedDrinkSize = (ComboBoxItem)DrinkSizeList.SelectedItem;
                Drink.Type type = (Drink.Type)Enum.Parse(typeof(Drink.Type), selectedDrink.Content.ToString());
                Drink.Size size = (Drink.Size)Enum.Parse(typeof(Drink.Size), selectedDrinkSize.Content.ToString());
                Drink drink = new Drink(type, size);
                drinks.Add(drink);
                DrinkOrderList.Items.Add(selectedDrinkSize.Content + " of " + selectedDrink.Content);
                DrinkOrderList.Visibility = Visibility.Visible;
                DrinkOrderListLabel.Visibility = Visibility.Visible;
            }
        }

        private void PlaceOrder(object sender, RoutedEventArgs e)
        {
            if (PizzaOrderList.Items.IsEmpty)
                MessageBox.Show("Please add at least one pizza to your order.");
            else if (string.IsNullOrWhiteSpace(phoneOrder.Text) || string.IsNullOrWhiteSpace(orderClerk.Text))
                MessageBox.Show("At least one field was left empty.");
            else
            {
                int customerIndex = -1;
                for (int i = 0 ; i < listCustomers.Count ; i++)
                    if (listCustomers[i].phone == phoneOrder.Text)
                        customerIndex = i;
                if (customerIndex == -1)
                {
                    MessageBox.Show("Customer not found.");
                    return;
                }
                int clerkIndex = -1;
                for (int i = 0 ; i < listClerks.Count ; i++)
                    if (listClerks[i].id == int.Parse(orderClerk.Text))
                        clerkIndex = i;
                if (clerkIndex == -1)
                {
                    MessageBox.Show("Clerk not found.");
                    return;
                }

                if (drinks.Count == 0)
                {
                    if (sender.Equals(PlaceOrderButton))
                    {
                        orders.Add(new Order(new List<Pizza>(pizzas), listCustomers[customerIndex], listClerks[clerkIndex]));
                    }
                    else
                    {
                        Order order = orders[CurrentOrderList.SelectedIndex];
                        order.clerk.numberOfOrders--;
                        order.customer.numberOfOrder--;
                        order.pizzas = new List<Pizza>(pizzas);
                        order.customer = listCustomers[customerIndex];
                        order.clerk = listClerks[clerkIndex];
                        orders[CurrentOrderList.SelectedIndex] = order;
                        order.clerk.numberOfOrders++;
                        order.customer.numberOfOrder++;
                        
                        string originalStringLeft = "Order #" + order.orderNumber + " ";
                        string originalStringRight = " Price: " + order.calculateTotalPrice() + "€";
                        string formattedString = originalStringLeft.PadRight(35, '.') + originalStringRight.PadLeft(35,'.');

                        CurrentOrderList.Items[CurrentOrderList.SelectedIndex] = formattedString;
                        
                        ClerkMessages.Items.Add(
                            "Successfuly modified the order n°" + orders[orders.Count - 1].orderNumber);
                        KitchenMessages.Items.Add("The order n°" + orders[orders.Count - 1].orderNumber +
                                                  " was re-transmitted.");
                    }
                }
                else
                {
                    if (sender.Equals(PlaceOrderButton))
                    {
                        orders.Add(new Order(new List<Pizza>(pizzas), new List<Drink>(drinks), listCustomers[customerIndex], listClerks[clerkIndex]));
                    }
                    else
                    {
                        Order order = orders[CurrentOrderList.SelectedIndex];
                        order.clerk.numberOfOrders--;
                        order.customer.numberOfOrder--;
                        order.pizzas = new List<Pizza>(pizzas);
                        order.drinks = new List<Drink>(drinks);
                        order.customer = listCustomers[customerIndex];
                        order.clerk = listClerks[clerkIndex];
                        orders[CurrentOrderList.SelectedIndex] = order;
                        order.clerk.numberOfOrders++;
                        order.customer.numberOfOrder++;
                        
                        string originalStringLeft = "Order #" + order.orderNumber + " ";
                        string originalStringRight = " Price: " + order.calculateTotalPrice() + "€";
                        string formattedString = originalStringLeft.PadRight(35, '.') + originalStringRight.PadLeft(35,'.');

                        CurrentOrderList.Items[CurrentOrderList.SelectedIndex] = formattedString;
                        
                        ClerkMessages.Items.Add(
                            "Successfuly modified the order n°" + orders[orders.Count - 1].orderNumber);
                        KitchenMessages.Items.Add("The order n°" + orders[orders.Count - 1].orderNumber +
                                                  " was re-transmitted.");
                    }
                }

                if (sender.Equals(PlaceOrderButton))
                {
                    listClerks[clerkIndex].numberOfOrders++;
                    listCustomers[customerIndex].numberOfOrder++;
                        
                    string originalStringLeft = "Order #" + orders[orders.Count - 1].orderNumber + " ";
                    string originalStringRight = " Price: " + orders[orders.Count - 1].calculateTotalPrice() + "€";
                    string formattedString = originalStringLeft.PadRight(35, '.') + originalStringRight.PadLeft(35,'.');
                        
                    CurrentOrderList.Items.Add(formattedString);

                    if (!remove1)
                    {
                        KitchenMessages.Items.Clear();
                        ClerkMessages.Items.Clear();
                        CustomerMessages.Items.Clear();
                        remove1 = true;
                    }

                    CustomerMessages.Items.Add("To customer " + orders[orders.Count - 1].customer.name + " - Your order n°" + orders[orders.Count - 1].orderNumber +
                                               " has been placed.");
                    ClerkMessages.Items.Add(
                        "To clerk n°" + orders[orders.Count - 1].clerk.id + " - Successfuly opened the order n°" + orders[orders.Count - 1].orderNumber);
                    KitchenMessages.Items.Add("The order n°" + orders[orders.Count - 1].orderNumber +
                                              " has been transmitted.");

                    DelivererMessageDelegate delivererMessageDelegate = SendDelivererMessage;
                    delivererMessageDelegate("To all - The order n°" + orders[orders.Count - 1].orderNumber + " was placed 5 minutes ago");
                    CookingDelegate cookingDelegate = Cooking;
                    cookingDelegate();
                }
                checkMessages();
                Clear();
            }
        }

        private async void SendDelivererMessage(string msg)
        {
            await Task.Delay(10000);
            DelivererMessages.SelectedIndex = 0;
            if (!remove2)
            {
                DelivererMessages.Items.Clear();
                remove2 = true;
            }
            DelivererMessages.Items.Add(msg);
            DelivererMessages.SelectedIndex = -1;
            checkMessages();
        } 

        private void SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (sender.Equals(PizzaOrderList))
                DeletePizza.Visibility = Visibility.Visible;
            else if (sender.Equals(DrinkOrderList))
                DeleteDrink.Visibility = Visibility.Visible;
            else if (sender.Equals(CurrentOrderList))
            {
                ModifyButton.Visibility = Visibility.Visible;
                LossButton1.Visibility = Visibility.Visible;
            }
            else if (sender.Equals(DeliveryOrderList))
                LossButton2.Visibility = Visibility.Visible;
            else if (sender.Equals(PrepareOrderList))
            {
                if (pastOrders.Contains(orderBeingPrepared))
                {
                    LossText.Visibility = Visibility.Visible;
                }
                else
                {
                    LossButton0.Visibility = Visibility.Visible;
                }
            }
            else if (sender.Equals(CurrentDeliveryOrderList))
            {
                if (CurrentDeliveryOrderList.SelectedIndex == -1)
                    return;
                if (pastOrders.Contains(currentDeliveryOrders[CurrentDeliveryOrderList.SelectedIndex]))
                {
                    LossButton3.Visibility = Visibility.Collapsed;
                    LossText2.Visibility = Visibility.Visible;
                }
                else
                {
                    LossButton3.Visibility = Visibility.Visible;
                    LossText2.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void DeletePizzaClick(object sender, RoutedEventArgs e)
        {
            pizzas.RemoveAt(PizzaOrderList.SelectedIndex);
            PizzaOrderList.Items.RemoveAt(PizzaOrderList.SelectedIndex);
            if (PizzaOrderList.Items.IsEmpty)
            {
                PizzaOrderList.Visibility = Visibility.Collapsed;
                PizzaOrderListLabel.Visibility = Visibility.Collapsed;
            }
            DeletePizza.Visibility = Visibility.Collapsed;
        }
        
        private void DeleteDrinkClick(object sender, RoutedEventArgs e)
        {
            drinks.RemoveAt(DrinkOrderList.SelectedIndex);
            DrinkOrderList.Items.RemoveAt(DrinkOrderList.SelectedIndex);
            if (DrinkOrderList.Items.IsEmpty)
            {
                DrinkOrderList.Visibility = Visibility.Collapsed;
                DrinkOrderListLabel.Visibility = Visibility.Collapsed;
            }
            DeleteDrink.Visibility = Visibility.Collapsed;
        }

        private void CancelOrder(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            pizzas.Clear();
            drinks.Clear();
            PizzaOrderList.Items.Clear();
            DrinkOrderList.Items.Clear();
            PizzaOrderList.Visibility = Visibility.Collapsed;
            DrinkOrderList.Visibility = Visibility.Collapsed;
            PizzaOrderListLabel.Visibility = Visibility.Collapsed;
            DrinkOrderListLabel.Visibility = Visibility.Collapsed;
            OrderPanel.Visibility = Visibility.Collapsed;
            OrderListPanel.Visibility = Visibility.Visible;
            phoneOrder.Text = "";
            orderClerk.Text = "";
            PizzaList.SelectedIndex = 0;
            PizzaSizeList.SelectedIndex = 0;
            DrinkList.SelectedIndex = 0;
            DrinkSizeList.SelectedIndex = 0;
            ModifyButton.Visibility = Visibility.Collapsed;
            CurrentOrderList.SelectedIndex = -1;
            DeliveryOrderList.SelectedIndex = -1;
            PrepareOrderList.SelectedIndex = -1;
            PastOrderList.SelectedIndex = -1;
            CurrentDeliveryOrderList.SelectedIndex = -1;
            DeletePizza.Visibility = Visibility.Collapsed;
            DeleteDrink.Visibility = Visibility.Collapsed;
            ModifyOrderButton.Visibility = Visibility.Collapsed;
            PlaceOrderButton.Visibility = Visibility.Collapsed;
            ModifyButton.Visibility = Visibility.Collapsed;
            LossButton1.Visibility = Visibility.Collapsed;
            LossButton2.Visibility = Visibility.Collapsed;
            LossButton0.Visibility = Visibility.Collapsed;
            LossText.Visibility = Visibility.Collapsed;
            LossButton3.Visibility = Visibility.Collapsed;
            LossText2.Visibility = Visibility.Collapsed;
        }

        private void AddOrder(object sender, RoutedEventArgs e)
        {
            OrderListPanel.Visibility = Visibility.Collapsed;
            OrderPanel.Visibility = Visibility.Visible;
            if (sender.Equals(ModifyButton))
            {
                orderBeingModified = orders[CurrentOrderList.SelectedIndex];
                ModifyOrderButton.Visibility = Visibility.Visible;
                for (int i = 0 ; i < orderBeingModified.pizzas.Count ; i++)
                {
                    PizzaOrderList.Items.Add(orderBeingModified.pizzas[i].size + " " + orderBeingModified.pizzas[i].type);
                    PizzaOrderList.Visibility = Visibility.Visible;
                    PizzaOrderListLabel.Visibility = Visibility.Visible;
                }
                for (int i = 0 ; i < orderBeingModified.drinks.Count ; i++)
                {
                    DrinkOrderList.Items.Add(orderBeingModified.drinks[i].volume + " of " + orderBeingModified.drinks[i].type);
                    DrinkOrderList.Visibility = Visibility.Visible;
                    DrinkOrderListLabel.Visibility = Visibility.Visible;
                }
                phoneOrder.Text = orderBeingModified.customer.phone;
                orderClerk.Text = orderBeingModified.clerk.id.ToString();
                pizzas = new List<Pizza>(orderBeingModified.pizzas);
                if (orderBeingModified.drinks.Count > 0)
                    drinks = new List<Drink>(orderBeingModified.drinks);
            }
            else
            {
                PlaceOrderButton.Visibility = Visibility.Visible;
            }
        }
        
        public void LossOrder(object sender, RoutedEventArgs e)
        {
            Order order;
            if (sender.Equals(LossButton1))
            {
                order = orders[CurrentOrderList.SelectedIndex];
                orders.RemoveAt(CurrentOrderList.SelectedIndex);
                CurrentOrderList.Items.RemoveAt(CurrentOrderList.SelectedIndex);
                ModifyButton.Visibility = Visibility.Collapsed;
                LossButton1.Visibility = Visibility.Collapsed;
                
                string originalStringLeft = "Order #" + order.orderNumber + " ";
                string originalStringRight = " At loss";
                string formattedString = originalStringLeft.PadRight(37, '.') + originalStringRight.PadLeft(37,'.');

                PastOrderList.Items.Add(formattedString);
                pastOrders.Add(order);
            }
            else if (sender.Equals(LossButton2))
            {
                order = deliveryOrders[DeliveryOrderList.SelectedIndex];
                deliveryOrders.RemoveAt(DeliveryOrderList.SelectedIndex);
                DeliveryOrderList.Items.RemoveAt(DeliveryOrderList.SelectedIndex);
                LossButton2.Visibility = Visibility.Collapsed;
                
                string originalStringLeft = "Order #" + order.orderNumber + " ";
                string originalStringRight = " At loss";
                string formattedString = originalStringLeft.PadRight(37, '.') + originalStringRight.PadLeft(37,'.');
                
                PastOrderList.Items.Add(formattedString);
                pastOrders.Add(order);
            }
            else if (sender.Equals(LossButton3))
            {
                LossButton3.Visibility = Visibility.Collapsed;
                order = currentDeliveryOrders[CurrentDeliveryOrderList.SelectedIndex];
                string originalStringLeft = "Order #" + order.orderNumber + " ";
                string originalStringRight = " At loss";
                string formattedString = originalStringLeft.PadRight(37, '.') + originalStringRight.PadLeft(37,'.');
                CurrentDeliveryOrderList.Items[CurrentDeliveryOrderList.SelectedIndex] = formattedString;
                pastOrders.Add(order);
                currentDeliveryOrders.Remove(order);
            }
            else
            {
                PrepareOrderList.SelectedValue = null;
                order = orderBeingPrepared;
                LossButton0.Visibility = Visibility.Collapsed;
                string originalStringLeft = "Order #" + order.orderNumber + " ";
                string originalStringRight = " At loss";
                string formattedString = originalStringLeft.PadRight(37, '.') + originalStringRight.PadLeft(37,'.');
                
                PrepareOrderList.Items.Clear();
                PrepareOrderList.Items.Add(formattedString);
                pastOrders.Add(order);
            }
        }

        public void checkMessages()
        {
            if (KitchenMessages.Items.Count == 11)
                KitchenMessages.Items.RemoveAt(0);
            if (ClerkMessages.Items.Count == 6)
                ClerkMessages.Items.RemoveAt(0);
            if (CustomerMessages.Items.Count == 6)
                CustomerMessages.Items.RemoveAt(0);
            if (DelivererMessages.Items.Count == 6)
                DelivererMessages.Items.RemoveAt(0);
        }

        public void BrowseButton(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files (*.txt)|*.txt";
            
            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFilePath = openFileDialog.FileName;
                string[] lines = System.IO.File.ReadAllLines(selectedFilePath);
                phoneOrder.Text = lines[0];
                orderClerk.Text = lines[1];
                for (int i = 2 ; i < lines.Length ; i++)
                {
                    if (lines[i].Contains("Pizza"))
                    {
                        string[] pizza = lines[i].Split(' ');
                        PizzaOrderList.Items.Add(pizza[1] + " " + pizza[2]);
                        PizzaOrderList.Visibility = Visibility.Visible;
                        PizzaOrderListLabel.Visibility = Visibility.Visible;
                        Pizza.Size size = (Pizza.Size)Enum.Parse(typeof(Pizza.Size), pizza[1]);
                        Pizza.Type type = (Pizza.Type)Enum.Parse(typeof(Pizza.Type), pizza[2]);
                        Pizza pizza1 = new Pizza(size, type);
                        pizzas.Add(pizza1);
                    }
                    else if (lines[i].Contains("Drink"))
                    {
                        string[] drink = lines[i].Split(' ');
                        DrinkOrderList.Items.Add(drink[1] + " of " + drink[3]);
                        DrinkOrderList.Visibility = Visibility.Visible;
                        DrinkOrderListLabel.Visibility = Visibility.Visible;
                        Drink.Type type = (Drink.Type)Enum.Parse(typeof(Drink.Type), drink[3]);
                        Drink.Size size = (Drink.Size)Enum.Parse(typeof(Drink.Size), drink[1]);
                        Drink drink1 = new Drink(type, size);
                        drinks.Add(drink1);
                    }
                }
            }
            
        }
    }
}