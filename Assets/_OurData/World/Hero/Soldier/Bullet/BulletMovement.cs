using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : LoadBehaviour
{
    public Transform target;
    public float speed = 10f;

    public float targetDistance;
    public float walkLimit = 0.1f;

    public bool canMove = false;
    public bool teleport = false;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        this.Move();
    }

    private void Move(){
        if(!canMove) return;
        if(this.target == null) return;

        if(this.teleport){
            transform.parent.position = target.transform.position;
        }

        transform.parent.position = Vector3.MoveTowards(transform.parent.position, target.position, this.speed * Time.fixedDeltaTime);
        transform.parent.forward = -(transform.parent.position - target.position);
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
}
