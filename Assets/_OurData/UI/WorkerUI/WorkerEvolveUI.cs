using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkerEvolveUI : LoadBehaviour
{
    public WorkerInfoUI workerInfoUI;

    public WorkerMaterialIcon material_1;
    public WorkerMaterialIcon material_2;
    public Image imageCoverMaterial_1;
    public Image imageCoverMaterial_2;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWorkerInfoUI();
        this.LoadWorkerMaterialIcon();
    }

    protected void LoadWorkerInfoUI(){
        this.workerInfoUI = transform.parent.GetComponent<WorkerInfoUI>();
    }

    public void LoadWorkerMaterialIcon(){
        this.material_1 = transform.Find("Panel").Find("Material_1").Find("WorkerMaterialIcon").GetComponent<WorkerMaterialIcon>();
        this.material_2 = transform.Find("Panel").Find("Material_2").Find("WorkerMaterialIcon").GetComponent<WorkerMaterialIcon>();
        this.imageCoverMaterial_1 = transform.Find("Panel").Find("Material_1").Find("ImageCover").GetComponent<Image>();
        this.imageCoverMaterial_2 = transform.Find("Panel").Find("Material_2").Find("ImageCover").GetComponent<Image>();
    }

    public void UpdateData(){
        this.UpdateMaterial();
    }

    public void UpdateMaterial(){
        if(this.material_1.workerIcon == null){
            this.imageCoverMaterial_1.color = 
            new Color(this.imageCoverMaterial_1.color.r, this.imageCoverMaterial_1.color.g, this.imageCoverMaterial_1.color.b,255f);
        }else{
            this.imageCoverMaterial_1.color =
            new Color(this.imageCoverMaterial_1.color.r, this.imageCoverMaterial_1.color.g, this.imageCoverMaterial_1.color.b,0f);
        }

        if(this.material_2.workerIcon == null){
            this.imageCoverMaterial_2.color =
            new Color(this.imageCoverMaterial_2.color.r, this.imageCoverMaterial_2.color.g, this.imageCoverMaterial_2.color.b,255f);
        }else{
            this.imageCoverMaterial_2.color =
            new Color(this.imageCoverMaterial_2.color.r, this.imageCoverMaterial_2.color.g, this.imageCoverMaterial_2.color.b,0f);
        }
    }

    public void ReSetMaterial(){
        this.material_1.workerIcon = null;
        this.material_2.workerIcon = null;
        this.UpdateData();
    }

    public void SelectMaterial_1(){
        // this.material_1.workerIcon = null;
        List<WorkerIcon> workerIcons = TownUIManager.instance.workerBagUI.GetWorkersMaterial_1(this.workerInfoUI.workerIcon.worker.workerName);
        workerIcons.Remove(this.workerInfoUI.workerIcon);

        if(this.material_2.workerIcon != null){
            workerIcons.Remove(this.material_2.workerIcon);
        }
        TownUIManager.instance.workerMaterialUI.OnUIMaterial_1(workerIcons);
    }
    public void SelectMaterial_2(){
        // this.material_1.workerIcon = null;
        List<WorkerIcon> workerIcons = TownUIManager.instance.workerBagUI.GetWorkersMaterial_2(this.workerInfoUI.workerIcon.worker.evolveLv);
        workerIcons.Remove(this.workerInfoUI.workerIcon);

        if(this.material_1.workerIcon != null){
            workerIcons.Remove(this.material_1.workerIcon);
        }
        TownUIManager.instance.workerMaterialUI.OnUIMaterial_2(workerIcons);
    }

    public void EvolveLv(){
        if(this.material_1.workerIcon == null) return;
        if(this.material_2.workerIcon == null) return;
        this.workerInfoUI.workerIcon.worker.evolveLv ++;
        this.material_1.workerIcon.ConsumeWorkerIcon();
        this.material_2.workerIcon.ConsumeWorkerIcon();
        this.material_1.workerIcon = null;
        this.material_2.workerIcon = null;
        this.UpdateData();
        this.workerInfoUI.UpdateData();
        TownUIManager.instance.workerBagUI.LoadWokerIcon();
        TownUIManager.instance.workerBagUI.UpdateWorkerIconShow();
    }

    public void Close(){
        
        this.OffUI();
    }

    public void OnUI(){
        
        this.material_1.workerIcon = null;
        this.material_2.workerIcon = null;
        gameObject.SetActive(true);
        this.UpdateData();
    }

    public void OffUI(){
        gameObject.SetActive(false);
    }
}
