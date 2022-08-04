using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageWorkerCtr : LoadBehaviour
{
    public List<ImageWorker> imageWorkers;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadImageWorker();
    }

    public void LoadImageWorker(){
        this.imageWorkers.Clear();
        foreach (Transform trans in transform)
        {
            foreach (Transform tran in trans)
            {
                ImageWorker imageWorker = tran.GetComponent<ImageWorker>();
                if(imageWorker == null) continue;
                this.imageWorkers.Add(imageWorker);
            }
        }
    }

    public void SetImage(WorkerName workerName){
        this.OffAllImage();
        this.GetImageWorkerByName(workerName).gameObject.SetActive(true);
    }

    public void OffAllImage(){
        foreach (ImageWorker imageWorker in this.imageWorkers)
        {
            imageWorker.gameObject.SetActive(false);
        }
    }

    public ImageWorker GetImageWorkerByName(WorkerName workerName){
        ImageWorker imageWorker = this.imageWorkers.Find((imageWorker) => imageWorker.workerName == workerName);
        if(imageWorker != null) return imageWorker;
        return this.imageWorkers.Find((imageWorker) => imageWorker.workerName == WorkerName.noWorker);
    }
}
