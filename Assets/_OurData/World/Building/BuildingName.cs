using System;

public class BuildingNameParser
{
    public static BuildingName FromString(string name)
    {
        //name = name.ToLower();
        name = name.Substring(0,1).ToLower() + name.Substring(1);
        return (BuildingName)Enum.Parse(typeof(BuildingName), name);
    }
}

public enum BuildingName
{
    noBuilding = 0,

    defaultBuilding = 1,

    farm = 201,
    restaurant = 202,
    smith = 203,
    harbor = 204,

}
