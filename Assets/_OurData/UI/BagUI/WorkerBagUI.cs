using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WorkerBagUI : LoadBehaviour
{
    public int filterWorker = 0;

    public List<WorkerIcon> workerIcons;
    public List<WorkerIcon> workerIconShow;


    public GameObject defaultWorkerIcon;
    public Transform contentWorkerIcons;

    public TextMeshProUGUI textNumberWorker;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadDefaultWorkerIcon();
        this.LoadContentWorkerIcon();
        this.LoadText();
    }

    public void LoadDefaultWorkerIcon(){
        Transform trans = transform.parent.parent.Find("WorkerUI").Find("Collections").Find("WorkerIcon");
        if(trans == null){
            Debug.LogWarning(transform.name + ": Not LoadDefaultWorkerIcon", gameObject);
            return;
        }
        this.defaultWorkerIcon = trans.gameObject;
        Debug.Log(transform.name + ": LoadDefaultWorkerIcon", gameObject);
    }

    public void LoadContentWorkerIcon(){
        Transform trans = transform.Find("Panel").Find("Workers").Find("ScrollView").Find("Viewport").Find("Content");
        if(trans == null) return;

        this.contentWorkerIcons = trans;
        Debug.Log(transform.name + ": LoadContentWorkerIcon", gameObject);
    }

    public void LoadWokerIcon(){
        this.workerIcons.Clear();
        foreach (Transform transWokerIcon in this.contentWorkerIcons)
        {
            WorkerIcon workerIcon = transWokerIcon.GetComponent<WorkerIcon>();
            if(workerIcon == null || workerIcon.isGone) continue;
            this.workerIcons.Add(transWokerIcon.GetComponent<WorkerIcon>());
        }
    }

    public void LoadText(){
        this.textNumberWorker = transform.Find("Panel").Find("NumberWorker").GetComponent<TextMeshProUGUI>();
    }

    public void UpdatData(){
        WorkerManager.instance.UpdateData();
        this.ReloadWorkerIconByWorker();
        this.LoadDataText();
        this.UpdateWorkerIconShow();
    }

    public void LoadDataText(){
        this.textNumberWorker.text = WorkerManager.instance.workers.Count.ToString() +"/"+ WorkerManager.instance.GetSlot();
    }

    public void UpdateWorkerIconShow(){
        this.workerIconShow.Clear();
        foreach (WorkerIcon workerIcon in this.workerIcons)
        {
            if(workerIcon == null || workerIcon.isGone) continue;
            switch (this.filterWorker)
            {
                case 0:
                    workerIcon.gameObject.SetActive(true);
                    this.workerIconShow.Add(workerIcon);
                    break;
                case 1:
                    if(workerIcon.worker.isBattle()){
                        workerIcon.gameObject.SetActive(true);
                        this.workerIconShow.Add(workerIcon);
                    }else{
                        workerIcon.gameObject.SetActive(false);
                    }
                    break;
                case 2:
                    if(!workerIcon.worker.isBattle()){
                        workerIcon.gameObject.SetActive(true);
                        this.workerIconShow.Add(workerIcon);
                    }else{
                        workerIcon.gameObject.SetActive(false);
                    }
                    break;
            }
        }
    }

    public void ReloadWorkerIconByWorker(){
        this.ClearWorkerIcon();
        foreach (Worker worker in WorkerManager.instance.workers)
        {
            WorkerIcon workerIcon = Instantiate<GameObject>(this.defaultWorkerIcon).transform.GetComponent<WorkerIcon>();
            workerIcon.transform.SetParent(this.contentWorkerIcons);
            workerIcon.worker = worker;
            workerIcon.transform.localScale = new Vector3(1,1,1);
            workerIcon.UpdateData();
            workerIcon.gameObject.SetActive(true);
        }
        this.LoadWokerIcon();
    }

    //Function

    public void FilterWoker(){
        this.filterWorker++;
        if(this.filterWorker == 3) this.filterWorker = 0;
        this.UpdateWorkerIconShow();
    }

    public void ClearWorkerIcon(){
        foreach (Transform trans in contentWorkerIcons)
        {
            trans.GetComponent<WorkerIcon>().isGone = true;
            trans.gameObject.SetActive(false);
            Destroy(trans.gameObject);
        }
    }

    public void AutoLvUpWorker(){
        for (int i = 0; i < 50; i++){
            WorkerIcon workerIcon = null;

            if(this.filterWorker == 0) workerIcon = this.GetLowestLvWorkerIconWorking();
            if(this.filterWorker != 0) workerIcon = this.GetLowestLvWorkerIconShow();

            if(workerIcon == null) continue;
            if(!workerIcon.worker.CanUpLv()) continue;

            workerIcon.worker.UpLv();
            workerIcon.UpdateData();
        }
    }

    public WorkerIcon GetLowestLvWorkerIconWorking(){
        if(this.workerIcons.Count == 0) return null;
        WorkerIcon workerIconLowest = this.workerIcons[0];
        foreach (WorkerIcon workerIcon in this.workerIcons)
        {
            if(!workerIcon.worker.isBattle()) continue;
            if(workerIconLowest.worker.lv > workerIcon.worker.lv){
                workerIconLowest = workerIcon;
            }
        }
        return workerIconLowest;
    }

    public WorkerIcon GetLowestLvWorkerIconShow(){
        if(this.workerIconShow.Count == 0) return null;
        WorkerIcon workerIconLowest = this.workerIconShow[0];
        foreach (WorkerIcon workerIconShow in this.workerIconShow)
        {
            if(workerIconLowest.worker.lv > workerIconShow.worker.lv){
                workerIconLowest = workerIconShow;
            }
        }
        return workerIconLowest;
    }

    public List<WorkerIcon> GetWorkersMaterial_1(WorkerName workerName){
        List<WorkerIcon> newWorkerIcons = new List<WorkerIcon>();
        for (int i = this.workerIcons.Count-1; i >= 0; i--)
        {
            if(workerIcons[i].worker.isBattle()){
                continue;
            }
            if(this.workerIcons[i].worker.workerName == workerName && this.workerIcons[i].worker.evolveLv == 1){
                newWorkerIcons.Add(this.workerIcons[i]);
            }
        }
        return newWorkerIcons;
    }
    public List<WorkerIcon> GetWorkersMaterial_2(int evolveLv){
        List<WorkerIcon> newWorkerIcons = new List<WorkerIcon>();
        for (int i = this.workerIcons.Count-1; i >= 0; i--)
        {
            if(workerIcons[i].worker.isBattle()){
                continue;
            }
            if(this.workerIcons[i].worker.evolveLv == evolveLv){
                newWorkerIcons.Add(this.workerIcons[i]);
            }
        }
        return newWorkerIcons;
    }

    public void Close(){
        
        this.OffUI();
    }

    public void OnUI(){
        
        this.transform.gameObject.SetActive(true);
        this.filterWorker = 0;
        this.UpdatData();

        ProductUIManager.instance.OnSoul();
    }
    public void OffUI(){
        this.transform.gameObject.SetActive(false);

        ProductUIManager.instance.OffSoul();
    }
}
