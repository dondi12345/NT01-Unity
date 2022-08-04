using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerMaterialUI : LoadBehaviour
{
    public Transform contentWorkerIcons;
    public GameObject defaultWorkerMaterialIcon;

    public WorkerIcon workerIcon;

    public List<WorkerMaterialIcon> workerMaterialIcons;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadDefaultWorkerMaterialIcon();
        this.LoadContentWorkerIcon();
    }

    public void LoadDefaultWorkerMaterialIcon(){
        Transform trans = transform.parent.parent.Find("WorkerUI").Find("Collections").Find("WorkerMaterialIcon");
        if(trans == null){
            Debug.LogWarning(transform.name + ": Not LoadDefaultWorkerMaterialIcon", gameObject);
            return;
        }
        this.defaultWorkerMaterialIcon = trans.gameObject;
        Debug.Log(transform.name + ": LoadDefaultWorkerMaterialIcon", gameObject);
    }

    public void LoadContentWorkerIcon(){
        Transform trans = transform.Find("Panel").Find("Workers").Find("ScrollView").Find("Viewport").Find("Content");
        if(trans == null) return;

        this.contentWorkerIcons = trans;
        Debug.Log(transform.name + ": LoadContentWorkerIcon", gameObject);
    }

    public void ReloadWorkerIconByWorker(List<WorkerIcon> workerIcons, int meterial){
        this.ClearWorkerIcon();
        this.workerMaterialIcons.Clear();
        foreach (WorkerIcon workerIcon in workerIcons)
        {
            WorkerMaterialIcon workerMaterialIcon = Instantiate<GameObject>(this.defaultWorkerMaterialIcon).transform.GetComponent<WorkerMaterialIcon>();
            workerMaterialIcon.transform.SetParent(this.contentWorkerIcons);
            workerMaterialIcon.workerIcon = workerIcon;
            workerMaterialIcon.transform.localScale = new Vector3(1,1,1);
            workerMaterialIcon.UpdateData();
            workerMaterialIcon.material = meterial;
            workerMaterialIcon.gameObject.SetActive(true);
            this.workerMaterialIcons.Add(workerMaterialIcon);
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

    public void OnUIMaterial_1(List<WorkerIcon> workerIcons){
        
        this.ReloadWorkerIconByWorker(workerIcons, 1);
        transform.gameObject.SetActive(true);
    }

    public void OnUIMaterial_2(List<WorkerIcon> workerIcons){
        
        this.ReloadWorkerIconByWorker(workerIcons, 2);
        transform.gameObject.SetActive(true);
    }

    public void OffUI(){
        transform.gameObject.SetActive(false);
    }
}
