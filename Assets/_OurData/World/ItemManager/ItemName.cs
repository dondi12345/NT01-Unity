using System;

public class ItemNameParser
{
    public static ItemName FromString(string name)
    {
        //name = name.ToLower();
        name = name.Substring(0,1).ToLower() + name.Substring(1);
        return (ItemName)Enum.Parse(typeof(ItemName), name);
    }
}

public enum ItemName
{
    noItem= 0,

    //Money Item
    moneyOneHourItem = 101,
    moneyThreeHourItem = 102,
    moneySixHourItem = 103,
    moneyTwelveHourItem = 104,
    moneyOneDayItem = 105,

    //Building Item
    blueprintWarehouse = 201,
    blueprintBuilding = 202,

    //ExperienceItem
    oneExperience = 301,
    itemOneExperience = 302,

    //DiceItem
    diceItem = 401,

    //SoulItem
    oneSoul = 501, 

    //Diamond
    oneDiamond = 601,


}
