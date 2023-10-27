namespace WpfApp1;

public class Drink : Products
{
    public enum Type
    {
        Coke,
        Fanta,
        Sprite,
        Orangina,
        IceTea,
        Water
    }
    
    public enum Size
    {
        Can,
        Bottle
    }
    
    private Type type { get; set; }
    private Size volume { get; set; }
    
    public Drink(Type type, Size volume)
    {
        this.type = type;
        this.volume = volume;
    }
}