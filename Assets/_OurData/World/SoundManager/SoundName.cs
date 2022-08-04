using System;

public class SoundNameParser
{
    public static SoundName FromString(string name)
    {
        //name = name.ToLower();
        name = name.Substring(0,1).ToLower() + name.Substring(1);
        return (SoundName)Enum.Parse(typeof(SoundName), name);
    }
}

public enum SoundName
{
    noSound = 0,
    
    soundClick = 101,

    soundWin = 201,
    soundLose = 202,

    //Bullet
    bowShoot = 1001,
    spellShoot = 1002,
    swordShoot = 1003,

    healShoot = 2001,

    fireExplore = 3001,
}