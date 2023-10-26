namespace WpfApp1;

public class Drink : Products
{
    public enum Type
    {
        CocaCola,
        Fanta,
        Sprite,
        Water
    }
    
    private Type type { get; set; }
    private int volume { get; set; }
    
    public Drink(Type type, int volume)
    {
        this.type = type;
        this.volume = volume;
    }
}