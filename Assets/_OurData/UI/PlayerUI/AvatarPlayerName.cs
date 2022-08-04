using System;

public class AvatarPlayerNameParser
{
    public static AvatarPlayerName FromString(string name)
    {
        //name = name.ToLower();
        name = name.Substring(0,1).ToLower() + name.Substring(1);
        return (AvatarPlayerName)Enum.Parse(typeof(AvatarPlayerName), name);
    }
}

public enum AvatarPlayerName
{
    noAvata = -1,
    skull = 0,

    bear = 101,

    buffalo = 102,
    chick = 103,

}

