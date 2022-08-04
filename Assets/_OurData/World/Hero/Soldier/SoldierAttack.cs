using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAttack : LoadBehaviour
{
    public GameObject bulletG;

    public Soldier target;

    public float percentDmg = 1;

    public SoldierCtrl soldierCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBullet();
        this.LoadSodierCtrl();
    }

    protected virtual void LoadBullet(){
        foreach (Transform trans in transform)
        {
            Bullet bullet = trans.GetComponent<Bullet>();
            if(bullet == null) continue;
            this.bulletG = bullet.gameObject;
            return;
        }
    }

    protected virtual void LoadSodierCtrl()
    {
        if (this.soldierCtrl != null) return;
        this.soldierCtrl = transform.parent.parent.GetComponent<SoldierCtrl>();
        Debug.Log(transform.name + ": SodierCtrl", gameObject);
    }

    public virtual void Attack(Soldier soldier, float dmg){
        dmg *= this.percentDmg;
        if(soldier == null) return;
        Bullet bullet = Instantiate<GameObject>(this.bulletG).GetComponent<Bullet>();
        bullet.transform.position = this.bulletG.transform.position;
        bullet.gameObject.SetActive(true);
        bullet.SetData(soldier, dmg);
    }

}
