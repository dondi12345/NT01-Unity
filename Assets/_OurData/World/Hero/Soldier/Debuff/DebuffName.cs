using System;

public class DebuffNameParser
{
    public static DebuffName FromString(string name)
    {
        //name = name.ToLower();
        name = name.Substring(0,1).ToLower() + name.Substring(1);
        return (DebuffName)Enum.Parse(typeof(DebuffName), name);
    }
}

public enum DebuffName
{
    noDebuff = 0,

    freeze = 101,
    petrify = 102,
    
}
