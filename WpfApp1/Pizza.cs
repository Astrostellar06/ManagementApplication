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
        Hawaiian,
        Vegetarian,
        Vegan
    }
    private Size size { get; set; }
    private Type type { get; set; }
    
    public Pizza(Size size, Type type)
    {
        this.size = size;
        this.type = type;
    }
}