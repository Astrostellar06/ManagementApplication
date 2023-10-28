﻿using System;
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
        DateTime startDate;
        
        private void OrdersByClerk(object sender, RoutedEventArgs e)
        {
            ClearStats();
            listStatsLabel.Text = "Orders by clerk";
            if (listClerks.Count == 0)
                listStats.Items.Add("No clerk in the database");
            else
            {
                foreach (Clerk clerk in listClerks)
                {
                    string originalStringLeft = "Clerk ID: " + clerk.id + " ";
                    string originalStringRight = " Number of orders: " + clerk.numberOfOrders;
                    string formattedString = originalStringLeft.PadRight(30, '.') + originalStringRight.PadLeft(30, '.');
                    listStats.Items.Add(formattedString);
                }
            }
            listStatsLabel.Visibility = Visibility.Visible;
            listStats.Visibility = Visibility.Visible;
        }

        private void OrdersByDeliverer(object sender, RoutedEventArgs e)
        {
            ClearStats();
            listStatsLabel.Text = "Orders by deliverer";
            if (listDeliverers.Count == 0)
                listStats.Items.Add("No deliverer in the database");
            else
            {
                foreach (Deliverer deliverer in listDeliverers)
                {
                    string originalStringLeft = "Deliverer's number: " + deliverer.number + " ";
                    string originalStringRight = " Number of orders: " + deliverer.numberOfOrders;
                    string formattedString = originalStringLeft.PadRight(30, '.') + originalStringRight.PadLeft(30, '.');
                    listStats.Items.Add(formattedString);
                }
            }
            listStatsLabel.Visibility = Visibility.Visible;
            listStats.Visibility = Visibility.Visible;
        }

        private void AverageOrderPrice(object sender, RoutedEventArgs e)
        {
            ClearStats();
            listStatsLabel.Text = "Average order price";
            if (orders.Count + deliveryOrders.Count + pastOrders.Count == 0)
                AveragePrice.Text = "No orders in the database";
            else
            {
                double totalPrice = 0;
                foreach (Order order in orders)
                {
                    totalPrice += order.calculateTotalPrice();
                }
                foreach (Order order in deliveryOrders)
                {
                    totalPrice += order.calculateTotalPrice();
                }
                foreach (Order order in pastOrders)
                {
                    totalPrice += order.calculateTotalPrice();
                }
                AveragePrice.Text = "Average order price: " + (totalPrice / (orders.Count + deliveryOrders.Count + pastOrders.Count)) + "€";
            }
            listStatsLabel.Visibility = Visibility.Visible;
            AveragePrice.Visibility = Visibility.Visible;
        }

        private void OrdersByTime(object sender, RoutedEventArgs e)
        {
            ClearStats();
            CalendarText.Text = "Select a starting date:";
            Calendar.Visibility = Visibility.Visible;
            CalendarSave1.Visibility = Visibility.Visible;
        }
        
        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            Mouse.Capture(null); // To prevent the calendar from being stuck on the screen
        }


        private void SaveCalendar1(object sender, RoutedEventArgs e)
        {
            DateTime? selectedDate = ActualCalendar.SelectedDate;

            if (selectedDate.HasValue)
            {
                StartingDate.Text = "Starting date: " + selectedDate.Value.ToShortDateString();
                StartingDate.Visibility = Visibility.Visible;
                CalendarText.Text = "Select an ending date:";
                CalendarSave1.Visibility = Visibility.Collapsed;
                CalendarSave2.Visibility = Visibility.Visible;
                startDate = selectedDate.Value;
            }
            else
            {
                MessageBox.Show("Aucune date sélectionnée.");
            }
        }
        
        private void SaveCalendar2(object sender, RoutedEventArgs e)
        {
            DateTime? selectedDate = ActualCalendar.SelectedDate;

            if (selectedDate.HasValue)
            {
                if (selectedDate.Value < startDate)
                {
                    MessageBox.Show("The ending date must be after the starting date.");
                    return;
                }
                ClearStats();
                DateTime endDate = selectedDate.Value;
                listStatsLabel.Text = "Orders by time period";
                DateRange.Text = "Starting date: " + startDate.ToShortDateString() + "\nEnding date: " + endDate.ToShortDateString();
                DateRange.Visibility = Visibility.Visible;
                if (orders.Count + deliveryOrders.Count + pastOrders.Count == 0)
                    listStats.Items.Add("No orders in the database");
                else
                {
                    bool noOrders = true;
                    foreach (Order order in orders)
                    {
                        if (order.orderDate >= startDate && order.orderDate <= endDate)
                        {
                            noOrders = false;
                            string originalStringLeft = "Order number: " + order.orderNumber + " ";
                            string originalStringRight = " Order date: " + order.orderDate.ToShortDateString();
                            string formattedString = originalStringLeft.PadRight(25, '.') + originalStringRight.PadLeft(25, '.');
                            listStats.Items.Add(formattedString);
                        }
                    }
                    foreach (Order order in deliveryOrders)
                    {
                        if (order.orderDate >= startDate && order.orderDate <= endDate)
                        {
                            noOrders = false;
                            string originalStringLeft = "Order number: " + order.orderNumber + " ";
                            string originalStringRight = " Order date: " + order.orderDate.ToShortDateString();
                            string formattedString = originalStringLeft.PadRight(25, '.') + originalStringRight.PadLeft(25, '.');
                            listStats.Items.Add(formattedString);
                        }
                    }
                    foreach (Order order in pastOrders)
                    {
                        if (order.orderDate >= startDate && order.orderDate <= endDate)
                        {
                            noOrders = false;
                            string originalStringLeft = "Order number: " + order.orderNumber + " ";
                            string originalStringRight = " Order date: " + order.orderDate.ToShortDateString();
                            string formattedString = originalStringLeft.PadRight(25, '.') + originalStringRight.PadLeft(25, '.');
                            listStats.Items.Add(formattedString);
                        }
                    }
                    if (noOrders)
                        listStats.Items.Add("No orders in the database for this time period");
                }
                listStatsLabel.Visibility = Visibility.Visible;
                listStats.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Aucune date sélectionnée.");
            }
        }
        
        private void ClearStats()
        {
            listStats.Items.Clear();
            listStatsLabel.Visibility = Visibility.Collapsed;
            listStats.Visibility = Visibility.Collapsed;
            AveragePrice.Visibility = Visibility.Collapsed;
            Calendar.Visibility = Visibility.Collapsed;
            CalendarSave1.Visibility = Visibility.Collapsed;
            CalendarSave2.Visibility = Visibility.Collapsed;
            StartingDate.Visibility = Visibility.Collapsed;
            DateRange.Visibility = Visibility.Collapsed;
        }
    }
}