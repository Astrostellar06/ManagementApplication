namespace WpfApp1;

public class Address
{
    public int postalCode { get; set; }
    public string city { get; set; }
    public string street { get; set; }
    public int number { get; set; }
    public int floor { get; set; }
    public string apartment { get; set; } = "";
    
    public Address(int postalCode, string city, string street, int number)
    {
        this.postalCode = postalCode;
        this.city = city;
        this.street = street;
        this.number = number;
    }
    
    public Address(int postalCode, string city, string street, int number, int floor)
    {
        this.postalCode = postalCode;
        this.city = city;
        this.street = street;
        this.number = number;
        this.floor = floor;
    }
    
    public Address(int postalCode, string city, string street, int number, int floor, string apartment)
    {
        this.postalCode = postalCode;
        this.city = city;
        this.street = street;
        this.number = number;
        this.floor = floor;
        this.apartment = apartment;
    }
    
    public Address(int postalCode, string city, string street, int number, string apartment)
    {
        this.postalCode = postalCode;
        this.city = city;
        this.street = street;
        this.number = number;
        this.apartment = apartment;
    }
}