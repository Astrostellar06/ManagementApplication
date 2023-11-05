namespace WpfApp1;

public class Deliverer : Person
{
    public int number { get; set; }
    public static int counterDeliverer;
    public int numberOfOrders { get; set; }
    public bool inDelivery;
    
    public Deliverer(string name, string surname)
    {
        this.name = name;
        this.surname = surname;
        number = counterDeliverer;
        counterDeliverer++;
        numberOfOrders = 0;
    }
}