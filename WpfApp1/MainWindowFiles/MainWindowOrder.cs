using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        List<Pizza> pizzas = new List<Pizza>();
        List<Drink> drinks = new List<Drink>();
        public static List<Order> orders = new List<Order>();
        public static List<Order> deliveryOrders = new List<Order>();
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
                        listClerks[clerkIndex].numberOfOrders++;
                        
                        string originalStringLeft = "Order #" + orders[orders.Count - 1].orderNumber + " ";
                        string originalStringRight = " Price: " + orders[orders.Count - 1].calculateTotalPrice() + "€";
                        string formattedString = originalStringLeft.PadRight(35, '.') + originalStringRight.PadLeft(35,'.');
                        
                        CurrentOrderList.Items.Add(formattedString);
                        
                        Cooking();
                    }
                    else
                    {
                        Order order = orders[CurrentOrderList.SelectedIndex];
                        order.clerk.numberOfOrders--;
                        order.pizzas = new List<Pizza>(pizzas);
                        order.customer = listCustomers[customerIndex];
                        order.clerk = listClerks[clerkIndex];
                        orders[CurrentOrderList.SelectedIndex] = order;
                        order.clerk.numberOfOrders++;
                        
                        string originalStringLeft = "Order #" + order.orderNumber + " ";
                        string originalStringRight = " Price: " + order.calculateTotalPrice() + "€";
                        string formattedString = originalStringLeft.PadRight(35, '.') + originalStringRight.PadLeft(35,'.');

                        CurrentOrderList.Items[CurrentOrderList.SelectedIndex] = formattedString;
                    }
                }
                else
                {
                    if (sender.Equals(PlaceOrderButton))
                    {
                        orders.Add(new Order(new List<Pizza>(pizzas), new List<Drink>(drinks), listCustomers[customerIndex], listClerks[clerkIndex]));
                        listClerks[clerkIndex].numberOfOrders++;
                        
                        string originalStringLeft = "Order #" + orders[orders.Count - 1].orderNumber + " ";
                        string originalStringRight = " Price: " + orders[orders.Count - 1].calculateTotalPrice() + "€";
                        string formattedString = originalStringLeft.PadRight(35, '.') + originalStringRight.PadLeft(35,'.');
                        
                        CurrentOrderList.Items.Add(formattedString);
                        
                        Cooking();
                    }
                    else
                    {
                        Order order = orders[CurrentOrderList.SelectedIndex];
                        order.clerk.numberOfOrders--;
                        order.pizzas = new List<Pizza>(pizzas);
                        order.drinks = new List<Drink>(drinks);
                        order.customer = listCustomers[customerIndex];
                        order.clerk = listClerks[clerkIndex];
                        orders[CurrentOrderList.SelectedIndex] = order;
                        order.clerk.numberOfOrders++;
                        
                        string originalStringLeft = "Order #" + order.orderNumber + " ";
                        string originalStringRight = " Price: " + order.calculateTotalPrice() + "€";
                        string formattedString = originalStringLeft.PadRight(35, '.') + originalStringRight.PadLeft(35,'.');

                        CurrentOrderList.Items[CurrentOrderList.SelectedIndex] = formattedString;
                    }
                }
                Clear();
            }
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
            DeletePizza.Visibility = Visibility.Collapsed;
            DeleteDrink.Visibility = Visibility.Collapsed;
            ModifyOrderButton.Visibility = Visibility.Collapsed;
            PlaceOrderButton.Visibility = Visibility.Collapsed;
            ModifyButton.Visibility = Visibility.Collapsed;
            LossButton1.Visibility = Visibility.Collapsed;
            LossButton2.Visibility = Visibility.Collapsed;
            LossButton0.Visibility = Visibility.Collapsed;
            LossText.Visibility = Visibility.Collapsed;
        }

        private void AddOrder(object sender, RoutedEventArgs e)
        {
            OrderListPanel.Visibility = Visibility.Collapsed;
            OrderPanel.Visibility = Visibility.Visible;
            if (sender.Equals(ModifyButton))
            {
                Order order = orders[CurrentOrderList.SelectedIndex];
                ModifyOrderButton.Visibility = Visibility.Visible;
                for (int i = 0 ; i < order.pizzas.Count ; i++)
                {
                    PizzaOrderList.Items.Add(order.pizzas[i].size + " " + order.pizzas[i].type);
                    PizzaOrderList.Visibility = Visibility.Visible;
                    PizzaOrderListLabel.Visibility = Visibility.Visible;
                }
                for (int i = 0 ; i < order.drinks.Count ; i++)
                {
                    DrinkOrderList.Items.Add(order.drinks[i].volume + " of " + order.drinks[i].type);
                    DrinkOrderList.Visibility = Visibility.Visible;
                    DrinkOrderListLabel.Visibility = Visibility.Visible;
                }
                phoneOrder.Text = order.customer.phone;
                orderClerk.Text = order.clerk.id.ToString();
                pizzas = new List<Pizza>(order.pizzas);
                if (order.drinks.Count > 0)
                    drinks = new List<Drink>(order.drinks);
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
            else if(sender.Equals(LossButton2))
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
    }
}