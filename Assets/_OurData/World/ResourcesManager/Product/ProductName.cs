using System;

public class ProductNameParser
{
    public static ProductName FromString(string name)
    {
        //name = name.ToLower();
        name = name.Substring(0,1).ToLower() + name.Substring(1);
        return (ProductName)Enum.Parse(typeof(ProductName), name);
    }
}

public enum ProductName
{
    noProduct = 0,

    dolla = 1,

    //Money
    diamond = 100,
    money = 101,
    soul = 102,


}
