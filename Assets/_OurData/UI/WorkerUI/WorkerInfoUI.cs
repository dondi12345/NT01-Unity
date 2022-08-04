using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WorkerInfoUI : LoadBehaviour
{
    public WorkerIcon workerIcon;

    public Text textName;

    public WorkerLvUI workerLvUI;
    public WorkerEvolveUI workerEvolveUI;

    public List<BtnTab> btnTabs;

    public Image imageWorker;
    public EvolveCtrl evolveCtrl;

    public GameObject skillWorkerGO;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSkillWorker();
        this.LoadText();
        this.LoadImageWorker();
        this.LoadWorkerLvUI();
        this.LoadWorkerEvolveUI();
        this.LoadBtnBuilding();
        this.LoadEvolveCtrl();
    }

    public void LoadText(){
        this.textName = transform.Find("TitleBuilding").Find("TextNameHero").GetComponent<Text>();        
    }

    public void LoadImageWorker(){
        Transform imageSoldier = transform.Find("ImageWorker");
        if(imageSoldier == null) return;
        this.imageWorker = imageSoldier.GetComponent<Image>();
    }
    public void LoadEvolveCtrl(){
        Transform transEvolveCtrl = transform.Find("EvolveCtrl");
        if(transEvolveCtrl == null) return;
        this.evolveCtrl = transEvolveCtrl.GetComponent<EvolveCtrl>();
    }

    public void LoadWorkerLvUI(){
        Transform transWorkerLvUI = transform.Find("WorkerLvUI");
        if(transWorkerLvUI == null) return;
        this.workerLvUI = transWorkerLvUI.GetComponent<WorkerLvUI>();
    }
    public void LoadWorkerEvolveUI(){
        Transform transWorkerEvolveUI = transform.Find("WorkerEvolveUI");
        if(transWorkerEvolveUI == null) return;
        this.workerEvolveUI = transWorkerEvolveUI.GetComponent<WorkerEvolveUI>();
    }

    protected void LoadBtnBuilding(){
        this.btnTabs.Clear();
        foreach (Transform trans in transform.Find("Btn"))
        {
            BtnTab btnBuilding = trans.GetComponent<BtnTab>();
            this.btnTabs.Add(btnBuilding);
        }
    }

    protected void LoadSkillWorker(){
        this.skillWorkerGO = transform.parent.parent.Find("WorkerUI").Find("Collections").Find("SkillWorkerIcon").gameObject;
    }

    //Function
    public void UpdateData(){
        this.UpdatImage();
        this.UpdateText();
        this.workerIcon.UpdateData();
        this.evolveCtrl.SetEvolveLv(this.workerIcon.worker.evolveLv);
    }

    public void ChangeWorkerLvUI(){
        this.OffAllBtnTab();
        this.workerLvUI.OnUI();
        this.workerEvolveUI.OffUI();
        this.btnTabs[0].OnButton();
    }
    public void ChangeWorkerEvolveUI(){
        this.OffAllBtnTab();
        this.workerLvUI.OffUI();
        this.workerEvolveUI.OnUI();
        this.btnTabs[1].OnButton();
    }

    public void OffAllBtnTab(){
        foreach (BtnTab btnTab in this.btnTabs)
        {
            btnTab.OffButton();
        }
    }
    
    public void UpdateText(){
        this.textName.text = this.workerIcon.worker.GetWorkerName();
    }

    public void UpdatImage(){
        if(this.workerIcon.worker.workerCtrl.soldierStats.GetImage() == null) return;
        this.imageWorker.gameObject.SetActive(false);
        this.imageWorker.sprite = this.workerIcon.worker.workerCtrl.soldierStats.GetImage();
        this.imageWorker.gameObject.SetActive(true);
    }

    public void Close(){
        
        this.OffUI();
    }
    
    public void OnUI(WorkerIcon workerIcon){
        
        this.workerIcon = workerIcon;
        this.workerIcon.worker.UpdateData();
        gameObject.SetActive(true);
        this.UpdateData();
        this.ChangeWorkerLvUI();
    }
    public void OffUI(){
        gameObject.SetActive(false);
    }

    public void NextWorker(){
        int index = TownUIManager.instance.workerBagUI.workerIconShow.IndexOf(this.workerIcon);
        if(index < 0) return;
        index ++;
        if(index >= TownUIManager.instance.workerBagUI.workerIconShow.Count){
            index = 0;
        }
        this.workerIcon = TownUIManager.instance.workerBagUI.workerIconShow[index];
        this.UpdateData();
        this.workerLvUI.UpdateData();
        this.workerEvolveUI.ReSetMaterial();
    }

    public void BackWorker(){
        int index = TownUIManager.instance.workerBagUI.workerIconShow.IndexOf(this.workerIcon);
        if(index < 0) return;
        index --;
        if(index < 0){
            index = TownUIManager.instance.workerBagUI.workerIconShow.Count-1;
        }
        this.workerIcon = TownUIManager.instance.workerBagUI.workerIconShow[index];
        this.UpdateData();
        this.workerLvUI.UpdateData();
        this.workerEvolveUI.ReSetMaterial();
    }
}
