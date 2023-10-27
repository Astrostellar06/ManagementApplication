using System.Collections.ObjectModel;

namespace WpfApp1;

public class Pizza : Products
{
    public enum Size
    {
        Small,
        Medium,
        Large
    }
    
    public enum Type
    {
        Margherita,
        Pepperoni,
        Regina,
        Napoletana,
        Calzone,
        Hawaiian,
        Vegetarian,
        Vegan,
        Sicilian,
        Tartiflette,
        Chorizo,
        Salmon
    }
    public Size size { get; set; }
    public Type type { get; set; }
    
    public Pizza(Size size, Type type)
    {
        this.size = size;
        this.type = type;
    }

    public double calculatePrice()
    {
        double price = 0;
        switch (this.type)
        {
            case Type.Margherita:
                price = 5;
                break;
            case Type.Pepperoni:
                price = 6;
                break;
            case Type.Regina:
                price = 6;
                break;
            case Type.Napoletana:
                price = 6;
                break;
            case Type.Calzone:
                price = 7;
                break;
            case Type.Hawaiian:
                price = 7.5;
                break;
            case Type.Vegetarian:
                price = 5.5;
                break;
            case Type.Vegan:
                price = 5;
                break;
            case Type.Sicilian:
                price = 6;
                break;
            case Type.Tartiflette:
                price = 8;
                break;
            case Type.Chorizo:
                price = 6.5;
                break;
            case Type.Salmon:
                price = 8.5;
                break;
        }
        
        switch (this.size)
        {
            case Size.Small:
                price *= 1;
                break;
            case Size.Medium:
                price *= 1.5;
                break;
            case Size.Large:
                price *= 2;
                break;
        }
        
        return price;
    }
}