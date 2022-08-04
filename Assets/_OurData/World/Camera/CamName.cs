using System;

public class CamNameParser
{
    public static CamName FromString(string name)
    {
        //name = name.ToLower();
        name = name.Substring(0,1).ToLower() + name.Substring(1);
        return (CamName)Enum.Parse(typeof(CamName), name);
    }
}

public enum CamName
{
    noCam = 0,

    mainCam = 1,

    barracksCam = 2
}
