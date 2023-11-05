using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace WpfApp1
{
    public partial class MainWindow
    {
        public bool darkTheme = false;
        public void GenerateFile(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "Text Files (*.txt)|*.txt";
            saveFileDialog.Title = "Save a scenario JSON file";
            
            bool? result = saveFileDialog.ShowDialog();

            if (result ==  true)
            {
                string filePath = saveFileDialog.FileName;
                String content = JsonConvert.SerializeObject(listCustomers) + Environment.NewLine;
                content += JsonConvert.SerializeObject(listClerks) + Environment.NewLine;
                content += JsonConvert.SerializeObject(listDeliverers) + Environment.NewLine;
                content += JsonConvert.SerializeObject(pastOrders) + Environment.NewLine;
                content += JsonConvert.SerializeObject(currentDeliveryOrders) + Environment.NewLine;
                content += JsonConvert.SerializeObject(deliveryOrders) + Environment.NewLine;
                content += JsonConvert.SerializeObject(orders) + Environment.NewLine;
                if (isBusy)
                    content += JsonConvert.SerializeObject(orderBeingPrepared) + Environment.NewLine;
                
                System.IO.File.WriteAllText(filePath, content);
                MessageBox.Show("File saved successfuly: " + filePath);
            }
        }

        public void LoadFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files (*.txt)|*.txt";

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFilePath = openFileDialog.FileName;
                String[] content = System.IO.File.ReadAllLines(selectedFilePath);
                listCustomers = JsonConvert.DeserializeObject<List<Customer>>(content[0]);
                listClerks = JsonConvert.DeserializeObject<List<Clerk>>(content[1]);
                listDeliverers = JsonConvert.DeserializeObject<List<Deliverer>>(content[2]);
                List<Order> orders1 = JsonConvert.DeserializeObject<List<Order>>(content[3]);
                List<Order> orders2 = JsonConvert.DeserializeObject<List<Order>>(content[4]);
                List<Order> orders3 = JsonConvert.DeserializeObject<List<Order>>(content[5]);
                List<Order> orders4 = JsonConvert.DeserializeObject<List<Order>>(content[6]);
                orders.Clear();
                currentDeliveryOrders.Clear();
                deliveryOrders.Clear();
                pastOrders.Clear();
                foreach(Order order in orders1)
                    orders.Add(order);
                foreach(Order order in orders2)
                    orders.Add(order);
                foreach(Order order in orders3)
                    orders.Add(order);
                foreach(Order order in orders4)
                    orders.Add(order);
                if (content.Length > 7)
                    orders.Add(JsonConvert.DeserializeObject<Order>(content[7]));
                lstCustomer.Items.Clear();
                lstClerk.Items.Clear();
                lstDeliverer.Items.Clear();
                CurrentOrderList.Items.Clear();
                foreach (Customer customer in listCustomers)
                    lstCustomer.Items.Add(customer.name + " " + customer.surname);
                foreach (Clerk clerk in listClerks)
                    lstClerk.Items.Add(clerk.name + " " + clerk.surname);
                foreach (Deliverer deliverer in listDeliverers)
                    lstDeliverer.Items.Add(deliverer.name + " " + deliverer.surname);
                foreach (Order order in orders)
                {
                    string originalStringLeft = "Order #" + order.orderNumber + " ";
                    string originalStringRight = " Price: " + order.calculateTotalPrice() + "€";
                    string formattedString = originalStringLeft.PadRight(35, '.') + originalStringRight.PadLeft(35,'.');
                    CurrentOrderList.Items.Add(formattedString);
                }
                Order.counterOrder = orders.Count;
                Deliverer.counterDeliverer = listDeliverers.Count;
                Clerk.counterClerk = listClerks.Count;
                Cooking();
                MessageBox.Show("File loaded successfuly: " + selectedFilePath);
            }
        }

        public void switchTheme(object sender, RoutedEventArgs e)
        {
            darkTheme = !darkTheme;
            if (darkTheme)
            {
                SolidColorBrush currentBrush1 = (SolidColorBrush)Resources["Theme1"];
                currentBrush1.Color = (Color)ColorConverter.ConvertFromString("#262626");
                SolidColorBrush currentBrush2 = (SolidColorBrush)Resources["Theme2"];
                currentBrush2.Color = Colors.White;
                SolidColorBrush currentBrush3 = (SolidColorBrush)Resources["Theme3"];
                currentBrush3.Color = Colors.Gray;
                switchButton.Content = "Switch to Light Theme";
            }
            else
            {
                SolidColorBrush currentBrush = (SolidColorBrush)Resources["Theme1"];
                currentBrush.Color = Colors.White;
                SolidColorBrush currentBrush2 = (SolidColorBrush)Resources["Theme2"];
                currentBrush2.Color = Colors.Black;
                SolidColorBrush currentBrush3 = (SolidColorBrush)Resources["Theme3"];
                currentBrush3.Color = Colors.Black;
                switchButton.Content = "Switch to Dark Theme";
            }
            this.InvalidateVisual();
        }
    }
}