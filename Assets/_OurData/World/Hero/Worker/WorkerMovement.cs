using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerMovement : LoadBehaviour
{
    [SerializeField] protected float walkLimit = 0.7f;
    [SerializeField] protected float targetDistance = 0f;

    public Transform target;


    public WorkerCtrl workerCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWorkerCtrl();
    }

    protected virtual void LoadWorkerCtrl()
    {
        if (this.workerCtrl != null) return;
        this.workerCtrl = GetComponent<WorkerCtrl>();
        if(this.workerCtrl == null){
            this.workerCtrl = transform.parent.GetComponent<WorkerCtrl>();
        }
        Debug.Log(transform.name + ": LoadWorkerCtrl", gameObject);
    }

    protected override void FixedUpdate()
    {   
        this.Moving();
        this.Animating();
        
    }

    public virtual bool IsCloseTarget()
    {
        if (this.target == null) return false;

        Vector3 targetPos = this.target.position;
        targetPos.y = transform.position.y;

        this.targetDistance = Vector3.Distance(transform.position, targetPos);
        if(this.targetDistance < this.walkLimit){
            this.workerCtrl.navMeshAgent.radius = 0f;
            this.workerCtrl.transSkin.gameObject.SetActive(false);
            return true;
        }
        this.workerCtrl.navMeshAgent.radius = 0.5f;
        this.workerCtrl.transSkin.gameObject.SetActive(true);
        return false;
    }

    public virtual void Moving()
    {
        //Don't know Error
        if (this.target == null || this.IsCloseTarget())
        {
            this.workerCtrl.navMeshAgent.isStopped = true;
            this.workerCtrl.worker.Walk = false;
            
            return;
        }

        this.workerCtrl.worker.Walk = true;
        this.workerCtrl.navMeshAgent.isStopped = false;
        this.workerCtrl.navMeshAgent.SetDestination(this.target.position);
        
    }

    protected virtual void Animating()
    {
        try
        {
            this.workerCtrl.animator.SetBool("Walk", this.workerCtrl.worker.Walk);         
        }
        catch (System.Exception)
        {
            
        }
    }
}
