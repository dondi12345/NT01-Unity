using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffCtrl : LoadBehaviour
{
    public List<Debuff> debuffs;
    
    public SoldierCtrl soldierCtrl; 

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadDebuffs();
        this.LoadSoldierCtrl();
    }

    protected virtual void LoadDebuffs(){
        this.debuffs.Clear();
        foreach (Transform trans in transform)
        {
            Debuff debuff = trans.GetComponent<Debuff>();
            if(debuff == null) continue;
            this.debuffs.Add(debuff);
        }
    }

    protected virtual void LoadSoldierCtrl(){
        SoldierCtrl soldierCtrl = transform.parent.GetComponent<SoldierCtrl>();
        if(soldierCtrl == null){
            Debug.LogWarning(transform.name +": "+ "Can't LoadSoldierCtrl");
            return;
        }
        this.soldierCtrl = soldierCtrl;
        Debug.Log(transform.name + ": LoadSoldierCtrl");
    }

    public void Freeze(float time){
        Debuff freeze = this.GetDebuffByName(DebuffName.freeze);
        if(freeze.time < time) freeze.time = time;
        freeze.gameObject.SetActive(true);
        this.soldierCtrl.soldierBattle.canAttack = false;
        this.soldierCtrl.soldierMovement.canMove = false;

    }

    public void UnFreeze(){
        this.soldierCtrl.soldierBattle.canAttack = true;
        this.soldierCtrl.soldierMovement.canMove = true;
    }
    public void Petrify(float time){
        Debuff petrify = this.GetDebuffByName(DebuffName.petrify);
        if(petrify.time < time) petrify.time = time;
        petrify.gameObject.SetActive(true);
        this.soldierCtrl.soldierBattle.canAttack = false;
        this.soldierCtrl.soldierMovement.canMove = false;

    }

    public void UnPetrify(){
        this.soldierCtrl.soldierBattle.canAttack = true;
        this.soldierCtrl.soldierMovement.canMove = true;
    }

    public Debuff GetDebuffByName(DebuffName debuffName){
        return this.debuffs.Find((debuff) => (debuff.debuffName == debuffName));
    }
}
