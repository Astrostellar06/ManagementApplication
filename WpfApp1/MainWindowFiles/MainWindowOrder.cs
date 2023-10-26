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
            {
                MessageBox.Show("Please select a type and a size of pizza.");
            }
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
            {
                MessageBox.Show("Please select a drink and a size.");
            }
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
    }
}