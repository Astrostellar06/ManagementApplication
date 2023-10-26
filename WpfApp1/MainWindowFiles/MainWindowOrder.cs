using System;
using System.Collections.Generic;
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
        List<Order> orders = new List<Order>();
        
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
                DrinkOrderList.Items.Add(selectedDrink.Content);
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
                    orders.Add(new Order(pizzas, listCustomers[customerIndex].name, listClerks[clerkIndex].name, listCustomers[customerIndex].address));
                else
                    orders.Add(new Order(pizzas, drinks, listCustomers[customerIndex].name, listClerks[clerkIndex].name, listCustomers[customerIndex].address));
                pizzas.Clear();
                drinks.Clear();
                PizzaOrderList.Items.Clear();
                DrinkOrderList.Items.Clear();
                PizzaOrderList.Visibility = Visibility.Collapsed;
                DrinkOrderList.Visibility = Visibility.Collapsed;
                PizzaOrderListLabel.Visibility = Visibility.Collapsed;
                DrinkOrderListLabel.Visibility = Visibility.Collapsed;
                MessageBox.Show("Your order has been placed.");
            }
        }

        private void SelectionChanged4(object sender, RoutedEventArgs e)
        {
            DeletePizza.Visibility = Visibility.Visible;
        }
        
        private void SelectionChanged5(object sender, RoutedEventArgs e)
        {
            DeleteDrink.Visibility = Visibility.Visible;
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

        private void CancelOrder(object Sender, RoutedEventArgs e)
        {
            pizzas.Clear();
            drinks.Clear();
            PizzaOrderList.Items.Clear();
            DrinkOrderList.Items.Clear();
            PizzaOrderList.Visibility = Visibility.Collapsed;
            DrinkOrderList.Visibility = Visibility.Collapsed;
            PizzaOrderListLabel.Visibility = Visibility.Collapsed;
            DrinkOrderListLabel.Visibility = Visibility.Collapsed;
        }
    }
}