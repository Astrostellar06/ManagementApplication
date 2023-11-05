using System;

namespace WpfApp1;

public class Customer : Person
{
    public Address address { get; set; }
    public string phone { get; set; }
    public DateTime dateFirstOrder { get; set; }
    public int numberOfOrder { get; set; }
    
    public Customer(string name, string surname, Address address, string phone)
    {
        this.name = name;
        this.surname = surname;
        this.address = address;
        this.phone = phone;
        dateFirstOrder = DateTime.Today;
        numberOfOrder = 0;
    }
}