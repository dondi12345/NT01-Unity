using System;

public class WarningNameParser
{
    public static WarningName FromString(string name)
    {
        //name = name.ToLower();
        name = name.Substring(0,1).ToLower() + name.Substring(1);
        return (WarningName)Enum.Parse(typeof(WarningName), name);
    }
}

public enum WarningName
{
    noWarning = 0,

    defaultWarning = 1,

    dontEnoughResource = 101,

    bagFull = 201,

    unlockBattleSpeed2 = 301,

}
