using System;

public class ElementNameParser
{
    public static ElementName FromString(string name)
    {
        //name = name.ToLower();
        name = name.Substring(0,1).ToLower() + name.Substring(1);
        return (ElementName)Enum.Parse(typeof(ElementName), name);
    }
}

public enum ElementName
{
    noElement = 0,

    water = 1,
    fire = 2,
    wood = 3,
    earth = 4,
    
}
