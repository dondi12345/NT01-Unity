using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageWorker : LoadBehaviour
{
    public WorkerName workerName = WorkerName.noWorker;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if(this.workerName == WorkerName.noWorker){
            this.workerName = WorkerNameParser.FromString(transform.name);
        }
    }

}
