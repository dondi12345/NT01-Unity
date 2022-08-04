using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : LoadBehaviour
{
    public Bullet bullet;
    public BulletMovement bulletMovement;
    public Transform explodeEffect;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBullet();
        this.LoadBulletMove();
        this.LoadExplodeEffect();
    }

    protected virtual void LoadBullet(){
        this.bullet = this.transform.GetComponent<Bullet>();
    }
    protected virtual void LoadBulletMove(){
        this.bulletMovement = this.transform.Find("BulletMovement").GetComponent<BulletMovement>();
    }
    protected virtual void LoadExplodeEffect(){
        this.explodeEffect = this.transform.Find("ExplodeEffect");
    }
}
