using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerAI : LoadBehaviour
{
    public WorkerCtrl workerCtrl;

    public float minTimeWorking = 3;
    public float maxTimeWorking = 6;

    public float currentTimeWorking = 0;
    public float randTimeWorking = 5;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWorkerCtrl();
    }

    protected virtual void LoadWorkerCtrl()
    {
        if (this.workerCtrl != null) return;
        this.workerCtrl = transform.parent.GetComponent<WorkerCtrl>();
        Debug.Log(transform.name + ": LoadWorkerCtrl", gameObject);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        this.GoToRandomBuilding();
    }

    public void ComeBackToWarehouse(){
        this.workerCtrl.workerMovement.target = BuildingManager.instance.warehouse.transform;
        if(!this.workerCtrl.workerMovement.IsCloseTarget()) return;
        this.workerCtrl.worker.OffGameObject();
    }

    public void GoToRandomBuilding(){
        if(this.currentTimeWorking <= this.randTimeWorking){
            this.currentTimeWorking+= Time.fixedDeltaTime;
            this.workerCtrl.worker.Walk = false;
            return;
        }

        if(this.workerCtrl.workerMovement.target == null){
            this.currentTimeWorking = 0;
            
            this.randTimeWorking = Random.Range(this.minTimeWorking, this.maxTimeWorking);
            List<Transform> transPoints = this.GetRandomBuilding();
            transPoints.AddRange(this.GetPoint());
            this.workerCtrl.workerMovement.target = transPoints[Random.Range(0,transPoints.Count)];
        }

        if(!this.workerCtrl.workerMovement.IsCloseTarget()) return;

        this.currentTimeWorking+= Time.fixedDeltaTime;

        if(this.currentTimeWorking >= this.randTimeWorking){
            this.workerCtrl.workerMovement.target = null;
            this.currentTimeWorking = 0;
        }
    }

    public List<Transform> GetRandomBuilding(){
        List<Transform> transBuildings = new List<Transform>();

        foreach (Building building in BuildingManager.instance.buildings)
        {
            if(building.lv > 0){
                transBuildings.Add(building.transform);
            }
        }
        transBuildings.Add(BuildingManager.instance.warehouse.transform);
        return transBuildings;
    }

    public List<Transform> GetPoint(){
        List<Transform> transPoint = new List<Transform>();
        foreach (Transform trans in WorkerManager.instance.transform.Find("RandomPoint"))
        {
            transPoint.Add(trans);
        }
        return transPoint;
    }
}
