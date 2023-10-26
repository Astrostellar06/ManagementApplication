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
        List<Customer> listCustomers = new List<Customer>();
        List<Clerk> listClerks = new List<Clerk>();
        List<Deliverer> listDeliverers = new List<Deliverer>();

        //Workforce module

        private void affCustomerClick(object sender, RoutedEventArgs e)
        {
            Panel1.Visibility = Visibility.Collapsed;
            Panel2.Visibility = Visibility.Visible;
            if (sender.Equals(Add1))
            {
                AddCustomerTitle.Visibility = Visibility.Visible;
                addCustomerButton.Visibility = Visibility.Visible;
            }
            else if (sender.Equals(Modify1))
            {
                ModifyCustomerTitle.Visibility = Visibility.Visible;
                modifyCustomerButton.Visibility = Visibility.Visible;
                nameCustomer.Text = listCustomers[lstCustomer.SelectedIndex].name;
                surnameCustomer.Text = listCustomers[lstCustomer.SelectedIndex].surname;
                phoneCustomer.Text = listCustomers[lstCustomer.SelectedIndex].phone;
                postcodeCustomer.Text = listCustomers[lstCustomer.SelectedIndex].address.postalCode.ToString();
                cityCustomer.Text = listCustomers[lstCustomer.SelectedIndex].address.city;
                streetCustomer.Text = listCustomers[lstCustomer.SelectedIndex].address.street;
                numberCustomer.Text = listCustomers[lstCustomer.SelectedIndex].address.number.ToString();
                if (listCustomers[lstCustomer.SelectedIndex].address.floor != 0)
                    floorCustomer.Text = listCustomers[lstCustomer.SelectedIndex].address.floor.ToString();
                if (listCustomers[lstCustomer.SelectedIndex].address.apartment != "")
                    apartmentCustomer.Text = listCustomers[lstCustomer.SelectedIndex].address.apartment;
            }
        }

        private void affClerkClick(object sender, RoutedEventArgs e)
        {
            Panel1.Visibility = Visibility.Collapsed;
            Panel3.Visibility = Visibility.Visible;
            if (sender.Equals(Add2))
            {
                AddClerkTitle.Visibility = Visibility.Visible;
                addClerkButton.Visibility = Visibility.Visible;
            }
            else if (sender.Equals(Modify2))
            {
                ModifyClerkTitle.Visibility = Visibility.Visible;
                modifyClerkButton.Visibility = Visibility.Visible;
                name.Text = listClerks[lstClerk.SelectedIndex].name;
                surname.Text = listClerks[lstClerk.SelectedIndex].surname;
            }

        }

        private void affDelivererClick(object sender, RoutedEventArgs e)
        {
            Panel1.Visibility = Visibility.Collapsed;
            Panel3.Visibility = Visibility.Visible;
            if (sender.Equals(Add3))
            {
                AddDelivererTitle.Visibility = Visibility.Visible;
                addDelivererButton.Visibility = Visibility.Visible;
            }
            else if (sender.Equals(Modify3))
            {
                ModifyDelivererTitle.Visibility = Visibility.Visible;
                modifyDelivererButton.Visibility = Visibility.Visible;
                name.Text = listDeliverers[lstDeliverer.SelectedIndex].name;
                surname.Text = listDeliverers[lstDeliverer.SelectedIndex].surname;
            }
        }

        private void addCustomerClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(nameCustomer.Text) && !string.IsNullOrWhiteSpace(surnameCustomer.Text) &&
                !string.IsNullOrWhiteSpace(phoneCustomer.Text) && !string.IsNullOrWhiteSpace(postcodeCustomer.Text) &&
                !string.IsNullOrWhiteSpace(cityCustomer.Text) && !string.IsNullOrWhiteSpace(streetCustomer.Text) &&
                !string.IsNullOrWhiteSpace(numberCustomer.Text))
            {
                Address address;
                if (!string.IsNullOrWhiteSpace(floorCustomer.Text) && string.IsNullOrWhiteSpace(apartmentCustomer.Text))
                {
                    address = new Address(int.Parse(postcodeCustomer.Text), cityCustomer.Text,
                        streetCustomer.Text, int.Parse(numberCustomer.Text), int.Parse(floorCustomer.Text));
                }
                else if (!string.IsNullOrWhiteSpace(floorCustomer.Text) &&
                         !string.IsNullOrWhiteSpace(apartmentCustomer.Text))
                {
                    address = new Address(int.Parse(postcodeCustomer.Text), cityCustomer.Text,
                        streetCustomer.Text, int.Parse(numberCustomer.Text), int.Parse(floorCustomer.Text),
                        apartmentCustomer.Text);
                }
                else if (string.IsNullOrWhiteSpace(floorCustomer.Text) &&
                         !string.IsNullOrWhiteSpace(apartmentCustomer.Text))
                {
                    address = new Address(int.Parse(postcodeCustomer.Text), cityCustomer.Text,
                        streetCustomer.Text, int.Parse(numberCustomer.Text), apartmentCustomer.Text);
                }
                else
                {
                    address = new Address(int.Parse(postcodeCustomer.Text), cityCustomer.Text,
                        streetCustomer.Text, int.Parse(numberCustomer.Text));
                }

                if (sender.Equals(modifyCustomerButton))
                {
                    Customer customerToModify = listCustomers[lstCustomer.SelectedIndex];
                    customerToModify.address = address;
                    customerToModify.name = nameCustomer.Text;
                    customerToModify.surname = surnameCustomer.Text;
                    customerToModify.phone = phoneCustomer.Text;
                    listCustomers[lstCustomer.SelectedIndex] = customerToModify;
                    lstCustomer.Items[lstCustomer.SelectedIndex] =
                        customerToModify.name + " " + customerToModify.surname;
                    Console.Write(lstCustomer.SelectedIndex);
                }
                else
                {
                    listCustomers.Add(
                        new Customer(nameCustomer.Text, surnameCustomer.Text, address, phoneCustomer.Text));
                    lstCustomer.Items.Add(nameCustomer.Text + " " + surnameCustomer.Text);
                }

                clear();
            }
        }

        private void addClerkClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(name.Text) && !string.IsNullOrWhiteSpace(surname.Text))
            {
                if (sender.Equals(modifyClerkButton))
                {
                    Clerk clerk = listClerks[lstClerk.SelectedIndex];
                    clerk.name = name.Text;
                    clerk.surname = surname.Text;
                    listClerks[lstClerk.SelectedIndex] = clerk;
                    lstClerk.Items[lstClerk.SelectedIndex] = clerk.name + " " + clerk.surname;
                }
                else
                {
                    listClerks.Add(new Clerk(name.Text, surname.Text));
                    lstClerk.Items.Add(name.Text + " " + surname.Text);
                }

                clear();
            }
        }

        private void addDelivererClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(name.Text) && !string.IsNullOrWhiteSpace(surname.Text))
            {
                if (sender.Equals(modifyDelivererButton))
                {
                    Deliverer deliverer = listDeliverers[lstDeliverer.SelectedIndex];
                    deliverer.name = name.Text;
                    deliverer.surname = surname.Text;
                    listDeliverers[lstDeliverer.SelectedIndex] = deliverer;
                    lstDeliverer.Items[lstDeliverer.SelectedIndex] = deliverer.name + " " + deliverer.surname;
                }
                else
                {
                    listDeliverers.Add(new Deliverer(name.Text, surname.Text));
                    lstDeliverer.Items.Add(name.Text + " " + surname.Text);
                }

                clear();
            }
        }

        private void cancelClick(object sender, RoutedEventArgs e)
        {
            clear();
        }

        private void SelectionChanged1(object sender, SelectionChangedEventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            String selectedItem = (String)listBox.SelectedItem;

            if (selectedItem != null)
            {
                lstClerk.UnselectAll();
                lstDeliverer.UnselectAll();
                Modify2.Visibility = Visibility.Collapsed;
                Delete2.Visibility = Visibility.Collapsed;
                Modify3.Visibility = Visibility.Collapsed;
                Delete3.Visibility = Visibility.Collapsed;
                AddressInfo.Visibility = Visibility.Visible;
                clearInfo();
                Customer customer = listCustomers[lstCustomer.SelectedIndex];
                Modify1.Visibility = Visibility.Visible;
                Delete1.Visibility = Visibility.Visible;
                InfoPanel.Visibility = Visibility.Visible;
                NameInfo.Text = "Name: " + customer.name;
                SurnameInfo.Text = "Surname: " + customer.surname;
                PhoneInfo.Text = "Phone: " + customer.phone;
                FirstOrderInfo.Text = "First order: " + customer.dateFirstOrder.ToString("dd/MM/yyyy");
                NumOrderInfo.Text = "Number of order: " + customer.numberOfOrder;
                CityInfo.Text = "City: " + customer.address.city;
                PostcodeInfo.Text = "Postcode: " + customer.address.postalCode;
                StreetInfo.Text = "Street: " + customer.address.street;
                NumberInfo.Text = "Number: " + customer.address.number;
                if (customer.address.floor != 0)
                    FloorInfo.Text = "Floor: " + customer.address.floor;
                if (customer.address.apartment != "")
                    ApartmentInfo.Text = "Apartment: " + customer.address.apartment;
            }
            else if (lstClerk.SelectedItem == null && lstDeliverer.SelectedItem == null)
            {
                Modify1.Visibility = Visibility.Collapsed;
                Delete1.Visibility = Visibility.Collapsed;
                InfoPanel.Visibility = Visibility.Collapsed;
            }
        }

        private void SelectionChanged2(object sender, SelectionChangedEventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            String selectedItem = (String)listBox.SelectedItem;

            if (selectedItem != null)
            {
                lstCustomer.UnselectAll();
                lstDeliverer.UnselectAll();
                Modify1.Visibility = Visibility.Collapsed;
                Delete1.Visibility = Visibility.Collapsed;
                Modify3.Visibility = Visibility.Collapsed;
                Delete3.Visibility = Visibility.Collapsed;
                AddressInfo.Visibility = Visibility.Collapsed;
                clearInfo();
                Clerk clerk = listClerks[lstClerk.SelectedIndex];
                Modify2.Visibility = Visibility.Visible;
                Delete2.Visibility = Visibility.Visible;
                InfoPanel.Visibility = Visibility.Visible;
                NameInfo.Text = "Name: " + clerk.name;
                SurnameInfo.Text = "Surname: " + clerk.surname;
                IdInfo.Text = "ID: " + clerk.id;
            }
            else if (lstCustomer.SelectedItem == null && lstDeliverer.SelectedItem == null)
            {
                Modify2.Visibility = Visibility.Collapsed;
                Delete2.Visibility = Visibility.Collapsed;
                InfoPanel.Visibility = Visibility.Collapsed;
            }
        }

        private void SelectionChanged3(object sender, SelectionChangedEventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            String selectedItem = (String)listBox.SelectedItem;

            if (selectedItem != null)
            {
                lstCustomer.UnselectAll();
                lstClerk.UnselectAll();
                Modify1.Visibility = Visibility.Collapsed;
                Delete1.Visibility = Visibility.Collapsed;
                Modify2.Visibility = Visibility.Collapsed;
                Delete2.Visibility = Visibility.Collapsed;
                AddressInfo.Visibility = Visibility.Collapsed;
                clearInfo();
                Deliverer deliverer = listDeliverers[lstDeliverer.SelectedIndex];
                Modify3.Visibility = Visibility.Visible;
                Delete3.Visibility = Visibility.Visible;
                InfoPanel.Visibility = Visibility.Visible;
                NameInfo.Text = "Name: " + deliverer.name;
                SurnameInfo.Text = "Surname: " + deliverer.surname;
                IdInfo.Text = "Deliverer's number: " + deliverer.number;
            }
            else if (lstCustomer.SelectedItem == null && lstClerk.SelectedItem == null)
            {
                Modify3.Visibility = Visibility.Collapsed;
                Delete3.Visibility = Visibility.Collapsed;
                InfoPanel.Visibility = Visibility.Collapsed;
            }
        }

        private void deleteCustomer(object sender, RoutedEventArgs e)
        {
            if (lstCustomer.SelectedItem != null)
            {
                listCustomers.RemoveAt(lstCustomer.SelectedIndex);
                lstCustomer.Items.RemoveAt(lstCustomer.SelectedIndex);
            }
        }

        private void deleteClerk(object sender, RoutedEventArgs e)
        {
            if (lstClerk.SelectedItem != null)
            {
                listClerks.RemoveAt(lstClerk.SelectedIndex);
                lstClerk.Items.RemoveAt(lstClerk.SelectedIndex);
            }
        }

        private void deleteDeliverer(object sender, RoutedEventArgs e)
        {
            if (lstDeliverer.SelectedItem != null)
            {
                listDeliverers.RemoveAt(lstDeliverer.SelectedIndex);
                lstDeliverer.Items.RemoveAt(lstDeliverer.SelectedIndex);
            }
        }

        private void clear()
        {
            Panel1.Visibility = Visibility.Visible;
            Panel2.Visibility = Visibility.Collapsed;
            Panel3.Visibility = Visibility.Collapsed;
            findCustomerPanel.Visibility = Visibility.Collapsed;
            phoneFindCustomer.Text = "";
            AddClerkTitle.Visibility = Visibility.Collapsed;
            ModifyClerkTitle.Visibility = Visibility.Collapsed;
            addClerkButton.Visibility = Visibility.Collapsed;
            modifyClerkButton.Visibility = Visibility.Collapsed;
            AddDelivererTitle.Visibility = Visibility.Collapsed;
            ModifyDelivererTitle.Visibility = Visibility.Collapsed;
            addDelivererButton.Visibility = Visibility.Collapsed;
            modifyDelivererButton.Visibility = Visibility.Collapsed;
            AddCustomerTitle.Visibility = Visibility.Collapsed;
            addCustomerButton.Visibility = Visibility.Collapsed;
            ModifyCustomerTitle.Visibility = Visibility.Collapsed;
            modifyCustomerButton.Visibility = Visibility.Collapsed;
            noMatch.Visibility = Visibility.Collapsed;
            nameCustomer.Text = "";
            surnameCustomer.Text = "";
            phoneCustomer.Text = "";
            postcodeCustomer.Text = "";
            cityCustomer.Text = "";
            streetCustomer.Text = "";
            numberCustomer.Text = "";
            floorCustomer.Text = "";
            apartmentCustomer.Text = "";
            name.Text = "";
            surname.Text = "";
        }

        public void findCustomer(object sender, RoutedEventArgs e)
        {
            Panel1.Visibility = Visibility.Collapsed;
            findCustomerPanel.Visibility = Visibility.Visible;
        }

        public void findCustomerClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(phoneFindCustomer.Text))
                return;
            noMatch.Visibility = Visibility.Collapsed;
            bool found = false;
            for (int i = 0; i < listCustomers.Count; i++)
            {
                if (listCustomers[i].phone == phoneFindCustomer.Text)
                {
                    found = true;
                    lstCustomer.SelectedIndex = i;
                    break;
                }
            }

            if (!found || listCustomers.Count == 0)
            {
                InfoPanel.Visibility = Visibility.Collapsed;
                noMatch.Visibility = Visibility.Visible;
            }
        }

        public void clearInfo()
        {
            NameInfo.Text = "";
            SurnameInfo.Text = "";
            PhoneInfo.Text = "";
            FirstOrderInfo.Text = "";
            NumOrderInfo.Text = "";
            CityInfo.Text = "";
            PostcodeInfo.Text = "";
            StreetInfo.Text = "";
            NumberInfo.Text = "";
            FloorInfo.Text = "";
            ApartmentInfo.Text = "";
            IdInfo.Text = "";
        }

        private void OrderAlpha(object sender, RoutedEventArgs e)
        {
            listCustomers.Sort((x, y) => string.Compare(x.name, y.name));
            lstCustomer.Items.Clear();
            foreach (Customer customer in listCustomers)
            {
                lstCustomer.Items.Add(customer.name + " " + customer.surname);
            }

            listClerks.Sort((x, y) => string.Compare(x.name, y.name));
            lstClerk.Items.Clear();
            foreach (Clerk clerk in listClerks)
            {
                lstClerk.Items.Add(clerk.name + " " + clerk.surname);
            }

            listDeliverers.Sort((x, y) => string.Compare(x.name, y.name));
            lstDeliverer.Items.Clear();
            foreach (Deliverer deliverer in listDeliverers)
            {
                lstDeliverer.Items.Add(deliverer.name + " " + deliverer.surname);
            }
        }

        private void OrderCity(object sender, RoutedEventArgs e)
        {
            listCustomers.Sort((x, y) => string.Compare(x.address.city, y.address.city));
            lstCustomer.Items.Clear();
            foreach (Customer customer in listCustomers)
            {
                lstCustomer.Items.Add(customer.name + " " + customer.surname);
            }
        }

        private void OrderByOrder(object sender, RoutedEventArgs e)
        {
            listCustomers.Sort((x, y) => x.numberOfOrder.CompareTo(y.numberOfOrder));
            lstCustomer.Items.Clear();
            foreach (Customer customer in listCustomers)
            {
                lstCustomer.Items.Add(customer.name + " " + customer.surname);
            }
        }
    }
}