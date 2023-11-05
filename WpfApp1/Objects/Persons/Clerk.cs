namespace WpfApp1;

public class Clerk : Person
{
    public int id { get; set; }
    public static int counterClerk;
    public int numberOfOrders { get; set; }
    
    public Clerk(string name, string surname)
    {
        this.name = name;
        this.surname = surname;
        id = counterClerk;
        counterClerk++;
        numberOfOrders = 0;
    }   
}