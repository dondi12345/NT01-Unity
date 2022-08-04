using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierMovement : LoadBehaviour
{
    public float walkLimit = 0.1f;
    [SerializeField] protected float targetDistance = 0f;

    public float runSpeed = 4;
    public bool canMove = true;

    public Transform target;


    public SoldierCtrl soldierCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSoldierCtrl();
    }

    protected virtual void LoadSoldierCtrl()
    {
        if (this.soldierCtrl != null) return;
        this.soldierCtrl = transform.parent.GetComponent<SoldierCtrl>();
        Debug.Log(transform.name + ": LoadSoldierCtrl", gameObject);
    }

    protected override void FixedUpdate()
    {   
        this.Moving();
        this.Animating();
    }

    public void UpdateData(){
        this.UpdateRunSpeed();
    }

    public void UpdateRunSpeed(){
        this.soldierCtrl.navMeshAgent.speed = this.runSpeed;    
    }

    public virtual bool IsCloseTarget()
    {
        if (this.target == null) return false;

        Vector3 targetPos = this.target.position;
        targetPos.y = transform.position.y;

        this.targetDistance = Vector3.Distance(transform.position, targetPos);
        if(this.targetDistance < this.walkLimit){
            return true;
        }
        return false;
    }

    public virtual void Moving()
    {
        if(!this.canMove){
            this.soldierCtrl.navMeshAgent.isStopped = true;
            this.soldierCtrl.soldier.Run = false;
            return;
        }
        if (this.target == null || this.IsCloseTarget())
        {
            this.soldierCtrl.navMeshAgent.isStopped = true;
            this.soldierCtrl.soldier.Run = false;
        
            return;
        }

        this.soldierCtrl.soldier.Run = true;
        this.soldierCtrl.navMeshAgent.isStopped = false;
        this.soldierCtrl.navMeshAgent.SetDestination(this.target.position);
    }

    protected virtual void Animating()
    {
        try
        {
            this.soldierCtrl.animator.SetBool("Run", this.soldierCtrl.soldier.Run);         
        }
        catch (System.Exception)
        {
            
        }
    }
}
