using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SummonWorkerUI : LoadBehaviour
{
    public GameObject workerIcon;
    public Transform contentWorkerIcons;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWorkerIcon();
        this.LoadContentWorkerIcon();
    }

    public void LoadWorkerIcon(){
        Transform trans = transform.parent.parent.Find("WorkerUI").Find("Collections").Find("WorkerIcon");
        if(trans == null){
            Debug.LogWarning(transform.name + ": Not LoadWorkerIcon", gameObject);
            return;
        }
        this.workerIcon = trans.gameObject;
        Debug.Log(transform.name + ": LoadWorkerIcon", gameObject);
    }

    public void LoadContentWorkerIcon(){
        Transform trans = transform.Find("Panel").Find("Content");
        if(trans == null) return;

        this.contentWorkerIcons = trans;
        Debug.Log(transform.name + ": LoadContentWorkerIcon", gameObject);
    }

    public void ReloadWorkerIconByWorker(List<Worker> workers){
        this.ClearWorkerIcon();
        foreach (Worker worker in workers)
        {
            WorkerIcon workerIcon = Instantiate<GameObject>(this.workerIcon).transform.GetComponent<WorkerIcon>();
            workerIcon.transform.SetParent(this.contentWorkerIcons);
            workerIcon.worker = worker;
            workerIcon.transform.localScale = new Vector3(1,1,1);
            workerIcon.UpdateData();
            workerIcon.gameObject.SetActive(true);
        }
    }

    public void ClearWorkerIcon(){
        foreach (Transform trans in contentWorkerIcons)
        {
            trans.gameObject.SetActive(false);
            Destroy(trans.gameObject);
        }
    }

    public void Close(){
        
        this.OffUI();
    }

    public void OnUI(List<Worker> workers){
        
        if(workers.Count == 0) return;
        this.gameObject.SetActive(true);
        this.ReloadWorkerIconByWorker(workers);
    }

    public void OffUI(){
        this.gameObject.SetActive(false);
    }
}
