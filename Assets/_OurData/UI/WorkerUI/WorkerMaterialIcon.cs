using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WorkerMaterialIcon : LoadBehaviour
{
    public WorkerIcon workerIcon;

    public int material = 1;
    public CardWorker cardWorker;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCardWorker();
    }

    protected void LoadCardWorker(){
        this.cardWorker = transform.Find("CardWorker").GetComponent<CardWorker>();
    }


    public void UpdateData(){
        if(workerIcon == null) return; 
        this.workerIcon.worker.UpdateData();
        if(this.workerIcon.worker == null) return;
        this.cardWorker.UpdateDataByWorker(workerIcon.worker);
    }

    public void Choose(){
        if(this.material == 1){
            TownUIManager.instance.workerInfoUI.workerEvolveUI.material_1.workerIcon = this.workerIcon;
            TownUIManager.instance.workerInfoUI.workerEvolveUI.material_1.UpdateData();
            TownUIManager.instance.workerInfoUI.workerEvolveUI.UpdateMaterial();
        }
        if(this.material == 2){
            TownUIManager.instance.workerInfoUI.workerEvolveUI.material_2.workerIcon = this.workerIcon;
            TownUIManager.instance.workerInfoUI.workerEvolveUI.material_2.UpdateData();
            TownUIManager.instance.workerInfoUI.workerEvolveUI.UpdateMaterial();
        }
        TownUIManager.instance.workerMaterialUI.OffUI();
    }
}
