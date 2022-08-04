using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position : LoadBehaviour
{
    public int number;
    public Soldier soldier;

    public Transform transUnlock;
    public Transform transLock;
    public Transform transInto;

    public int lvUnlock = -1;
    public bool lockPosition;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTransStatusPosition();
    }

    private void LoadTransStatusPosition(){
        this.transUnlock = transform.Find("Unlock");
        this.transLock = transform.Find("Lock");
        this.transInto = transform.Find("Into");
    }

    public void UpdateData(){
        if(this.lvUnlock == -1){
            this.lvUnlock = this.number;
        }
        
        if(this.lvUnlock <= PlayerManager.instance.lv){
            this.lockPosition = false;
        }else{
            this.lockPosition = true;
        }

        this.OffAllStatus();
        try
        {
            if(transform.parent.name.Equals("Enemy")){
                this.transInto.gameObject.SetActive(true);
                return;
            }
            if(this.soldier != null){
                this.transInto.gameObject.SetActive(true);
                return;
            }
  
            
        }
        catch (System.Exception){}
        
        if(lockPosition){
            this.transLock.gameObject.SetActive(true);
            return;
        }
        this.transUnlock.gameObject.SetActive(true);
    }

    public void OffAllStatus(){
        this.transLock.gameObject.SetActive(false);
        this.transUnlock.gameObject.SetActive(false);
        this.transInto.gameObject.SetActive(false);
    }

    public void PutSoldier(Soldier soldier){
        if(this.soldier != null){
            this.soldier.ComeToBag();
            this.soldier = soldier;
        }
        soldier.gameObject.SetActive(true);
        this.soldier = soldier;
        soldier.numberPosition = this.number;
        soldier.position = this;
        soldier.transform.position = transform.position;
        soldier.transform.localRotation = Quaternion.Euler(0,0,0);
        this.UpdateData();
        BattleManager.instance.UpdatePower();
    }

    public void OutSoldier(){
        this.soldier = null;
        this.UpdateData();
        BattleManager.instance.UpdatePower();
    }

    //Onclick
    private void OnMouseDown()
    {
        if(soldier == null) return;
        if(!BattleManager.instance.barracks) return;
        
        this.soldier.ComeToBag();
    }
}
