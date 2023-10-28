namespace WpfApp1;

public class Deliverer : Person
{
    public int number { get; set; }
    private static int counter;
    public int numberOfOrders { get; set; }
    
    public Deliverer(string name, string surname)
    {
        this.name = name;
        this.surname = surname;
        number = counter;
        counter++;
        numberOfOrders = 0;
    }
}