namespace WpfApp1;

public class Clerk : Person
{
    public int id { get; set; }
    private static int counter;
    public int numberOfOrders { get; set; }
    
    public Clerk(string name, string surname)
    {
        this.name = name;
        this.surname = surname;
        id = counter;
        counter++;
        numberOfOrders = 0;
    }   
}