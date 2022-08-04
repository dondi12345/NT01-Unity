using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkerCtrl : LoadBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Worker worker;
    public WorkerMovement workerMovement;
    public WorkerAI workerAI;
    public SoldierStats soldierStats;
    public Transform transSkin;

    public Animator animator;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAgent();
        this.LoadWorker();
        this.LoadWorkerMovement();
        this.LoadSoldierStats();
        this.LoadWorkerAI();
        this.LoadTransSkin();
        this.LoadAnimator();
    }

    protected virtual void LoadAgent()
    {
        if (this.navMeshAgent != null) return;
        this.navMeshAgent = GetComponent<NavMeshAgent>();
        Debug.Log(transform.name + ": LoadAgent", gameObject);
    }

    public virtual void LoadWorker()
    {
        if (this.worker != null) return;
        this.worker = GetComponent<Worker>();
        Debug.Log(transform.name + ": LoadWorker", gameObject);
    }

    public virtual void LoadWorkerMovement()
    {
        if (this.workerMovement != null) return;
        this.workerMovement = transform.Find("WorkerMovement").GetComponent<WorkerMovement>();
        Debug.Log(transform.name + " LoadWorkerLevel", gameObject);
    }

    public virtual void LoadSoldierStats()
    {
        if (this.soldierStats != null) return;
        foreach (Transform trans in transform)
        {
            SoldierStats soldierStats = trans.GetComponent<SoldierStats>();
            if(soldierStats != null){
                this.soldierStats = soldierStats;
                Debug.Log(transform.name + " LoadSoldierStats", gameObject);
                return;
            }
        }
        Debug.LogWarning(transform.name + "Can't LoadSoldierStats", gameObject);
    }

    public virtual void LoadWorkerAI()
    {
        if (this.workerAI != null) return;
        this.workerAI = transform.Find("WorkerAI").GetComponent<WorkerAI>();
        Debug.Log(transform.name + " LoadWorkerAI", gameObject);
    }
    public virtual void LoadTransSkin()
    {
        if (this.transSkin != null) return;
        this.transSkin = transform.Find("Skin");
        Debug.Log(transform.name + " LoadTransSkin", gameObject);
    }

    public virtual void LoadAnimator()
    {
        if (this.animator != null) return;
        Transform transformSkin = transform.Find("Skin");
        foreach (Transform trans in transformSkin)
        {
            if(trans.gameObject.activeSelf){
                this.animator = trans.GetComponentInChildren<Animator>();
            }
            break;
        }
        Debug.Log(transform.name + ": LoadAnimator", gameObject);
    }
}
