using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WorkerIcon : LoadBehaviour
{
    public Worker worker;
    public CardWorker cardWorker;

    public bool isGone = false;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCardWorker();
    }

    protected void LoadCardWorker(){
        this.cardWorker = transform.Find("CardWorker").GetComponent<CardWorker>();
    }

    public void UpdateData(){
        if(this.worker != null){
            this.worker.UpdateData();
            this.cardWorker.UpdateDataByWorker(this.worker);
        }
    }
    
    public void ConsumeWorkerIcon(){
        this.worker.DestroyWorker();
        this.isGone = true;
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public void OnClick(){
        TownUIManager.instance.OnWorkerInfoUI(this);
    }

}
