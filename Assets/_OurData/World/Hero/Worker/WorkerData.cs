using System;

[Serializable]
public class WorkerData
{
    //base status
    public int lv = 1;
    public int evolveLv = 1;
    public int numberPosition = 0;

    public WorkerName workerName = WorkerName.noWorker;

    public float timeWorking = 0;

    //Position
    public float x = 0;
    public float y = 0;
    public float z = 0;

    public float atk;
    public float def;
    public float hp;
}
