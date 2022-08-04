using System;

public class WorkerNameParser
{
    public static WorkerName FromString(string name)
    {
        //name = name.ToLower();
        name = name.Substring(0,1).ToLower() + name.Substring(1);
        return (WorkerName)Enum.Parse(typeof(WorkerName), name);
    }
}

public enum WorkerName
{
    noWorker = 0,

    defaultWorker = 1,

    farmer = 201,
    cook = 202,
    smith = 203,
    sailor = 204,

    haonEarthArcher = 301,
    haonFireWizard = 302,
    haonWaterWarrior = 303,
    haonWoodPriest = 304,
    

}
