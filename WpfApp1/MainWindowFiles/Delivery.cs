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
        public async void Deliver()
        {
            Deliverer currentDeliverer = null;
            //Test to check if a deliverer is available
            foreach (Deliverer del in listDeliverers)
            {
                if (!del.inDelivery)
                {
                    currentDeliverer = del;
                    break;
                }
            }
            if (currentDeliverer == null)
                return;
            while (deliveryOrders.Count != 0)
            {
                CurrentDeliveryOrderListLabel.Visibility = Visibility.Visible;
                CurrentDeliveryOrderList.Visibility = Visibility.Visible;
                currentDeliverer.inDelivery = true;
                Order currentOrder = deliveryOrders[0];
                currentDeliveryOrders.Add(deliveryOrders[0]);
                CurrentDeliveryOrderList.Items.Add(DeliveryOrderList.Items[0]);
                deliveryOrders.Remove(deliveryOrders[0]);
                DeliveryOrderList.Items.RemoveAt(0);
                DelivererMessages.Items.Add("To Deliverer n°" + currentDeliverer.number + " - Picked up order #" + currentDeliveryOrders[0].orderNumber);
                checkMessages();
                Random random = new Random();
                await Task.Delay(random.Next(15, 50) * 1000);
                ClerkMessages.Items.Add("To Clerk n°" + currentOrder.clerk.id + " - The Adress of Order #" + currentOrder.orderNumber + " was found");
                checkMessages();
                await Task.Delay(random.Next(15, 50) * 1000);
                DelivererMessages.Items.Add("To Deliverer n°" + currentDeliverer.number + " - Order #" + currentOrder.orderNumber + " delivered - You got paid " + ((currentDeliveryOrders[0].calculateTotalPrice())/10).ToString("0.00") + "€");
                ClerkMessages.Items.Add("To Clerk n°" + currentOrder.clerk.id + " - Order #" + currentOrder.orderNumber + " delivered - Order closed");
                checkMessages();
                currentDeliverer.numberOfOrders++;
                currentDeliverer.inDelivery = false;
                for (int i = 0; i < CurrentDeliveryOrderList.Items.Count; i++)
                {
                    if (CurrentDeliveryOrderList.Items[i].ToString().Contains("Order #" + currentOrder.orderNumber.ToString()))
                    {
                        if (!pastOrders.Contains(currentOrder))
                        {
                            pastOrders.Add(currentOrder);
                            currentDeliveryOrders.Remove(currentOrder);
                        }
                        PastOrderList.Items.Add(CurrentDeliveryOrderList.Items[i]);
                        CurrentDeliveryOrderList.Items.RemoveAt(i); ;
                    }
                }
            }
            if (CurrentDeliveryOrderList.Items.Count == 0)
            {
                CurrentDeliveryOrderListLabel.Visibility = Visibility.Collapsed;
                CurrentDeliveryOrderList.Visibility = Visibility.Collapsed;
            }
        }
    }
}