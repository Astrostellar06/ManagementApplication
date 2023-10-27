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
    
    public Type type { get; set; }
    public Size volume { get; set; }
    
    public Drink(Type type, Size volume)
    {
        this.type = type;
        this.volume = volume;
    }

    public double calculatePrice()
    {
        double price = 0;
        switch (this.type)
        {
            case Type.Coke:
                price = 1.5;
                break;
            case Type.Fanta:
                price = 1.5;
                break;
            case Type.Sprite:
                price = 1.25;
                break;
            case Type.Orangina:
                price = 1.5;
                break;
            case Type.IceTea:
                price = 1.75;
                break;
            case Type.Water:
                price = 0.75;
                break;
        }
        
        switch (this.volume)
        {
            case Size.Can:
                price *= 1;
                break;
            case Size.Bottle:
                price *= 2;
                break;
        }
        
        return price;
    }
}