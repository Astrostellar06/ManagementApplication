using System;
using System.Threading.Tasks;
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
        private bool isBusy;
        private Order orderBeingPrepared;

        public async void Cooking()
        {
            if (!isBusy)
            {
                while (orders.Count != 0)
                {
                    PrepareOrder.Visibility = Visibility.Visible;
                    PrepareOrderList.Visibility = Visibility.Visible;
                    PrepareOrderList.Items.Clear();
                    LossButton0.Visibility = Visibility.Collapsed;
                    LossText.Visibility = Visibility.Collapsed;
                    isBusy = true;
                    orderBeingPrepared = orders[0];
                    orders.Remove(orderBeingPrepared);
                    PrepareOrderList.Items.Add(CurrentOrderList.Items[0]);
                    CurrentOrderList.Items.RemoveAt(0);
                    Console.WriteLine("Cooking order " + orderBeingPrepared.orderNumber);
                    await Task.Delay(calculateTime(orderBeingPrepared) * 1000);
                    Console.WriteLine("Order " + orderBeingPrepared.orderNumber + " is ready");
                    if (pastOrders.Contains(orderBeingPrepared))
                    {
                        PastOrderList.Items.Add(PrepareOrderList.Items[0]);
                    }
                    else
                    {
                        deliveryOrders.Add(orderBeingPrepared);
                        DeliveryOrderList.Items.Add(PrepareOrderList.Items[0]);
                    }
                }
                isBusy = false;
                PrepareOrder.Visibility = Visibility.Collapsed;
                PrepareOrderList.Visibility = Visibility.Collapsed;
                LossButton0.Visibility = Visibility.Collapsed;
                LossText.Visibility = Visibility.Collapsed;
            }
        }

        public int calculateTime(Order order)
        {
            int totalTime = 0;
            foreach (Pizza pizza in order.pizzas)
            {
                int time;
                switch (pizza.size)
                {
                    case Pizza.Size.Small:
                        time = 30;
                        break;
                    case Pizza.Size.Medium:
                        time = 50;
                        break;
                    case Pizza.Size.Large:
                        time = 70;
                        break;
                    default:
                        time = 0;
                        break;
                }
                totalTime += time;
            }
            
            foreach (Drink drink in order.drinks)
            {
                int time;
                switch (drink.volume)
                {
                    case Drink.Size.Can:
                        time = 10;
                        break;
                    case Drink.Size.Bottle:
                        time = 20;
                        break;
                    default:
                        time = 0;
                        break;
                }
                totalTime += time;
            }

            return totalTime;
        }
    }
}