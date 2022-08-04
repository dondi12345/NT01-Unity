using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : LoadBehaviour
{
    public int lv = 1;
    public int evolveLv = 1;
    public ElementName elementName;

    public int numberPosition = 0;
    public Position position;

    public WorkerName workerName = WorkerName.noWorker;

    //For nav mesh
    public float runSpeed = 1;

    //For animation
    public bool Run = false;

    //For Save
    public WorkerData workerData;

    //Released
    public bool isReleased = false;

    public SoldierCtrl soldierCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSodierCtrl();
        if(this.workerName == WorkerName.noWorker){
            this.workerName = WorkerNameParser.FromString(transform.name);
        }
    }

    protected virtual void LoadSodierCtrl()
    {
        if (this.soldierCtrl != null) return;
        this.soldierCtrl = GetComponent<SoldierCtrl>();
        Debug.Log(transform.name + ": SodierCtrl", gameObject);
    }

    //Function
    public void UpdateData(){
        this.soldierCtrl.soldierMovement.UpdateData();
        this.soldierCtrl.soldierBattle.UpdateData();
    }

    public void ParseFromData(WorkerData workerData){
        this.workerData = workerData;
        this.lv = workerData.lv;
        this.evolveLv = workerData.evolveLv;
        this.workerName = workerData.workerName;
        this.numberPosition = workerData.numberPosition;
    }

    public WorkerData ParseToData(){
        this.UpdateData();
        this.workerData.numberPosition = this.numberPosition;
        return this.workerData;
    }

    public void ComeToBag(){
        this.numberPosition = 0;
        if(this.position != null){
            this.position.OutSoldier();
        }
        this.position = null;
        this.UpdateData();
        gameObject.SetActive(false);
    }
    
    //Get
    public float GetPowerSoldier(){
        float number = 0;
        this.UpdateData();
        number = this.GetAtk()/2 + this.GetDef() + this.GetHp()/5;
        return number;
    }

    public float GetAtk(){
        float atk = this.soldierCtrl.soldierStats.GetAtk()*this.lv;
        atk = atk * ((this.evolveLv-1) * 0.2f +1);
        //Count buff
        return atk;
    }
    public float GetDef(){
        float def = this.soldierCtrl.soldierStats.GetDef()*this.lv;
        def = def * ((this.evolveLv-1) * 0.2f +1);
        //Count buff
        return def;
    }
    public float GetHp(){
        float hp = this.soldierCtrl.soldierStats.GetHp()*this.lv;
        hp = hp * ((this.evolveLv-1) * 0.2f +1);
        //Count buff
        return hp;
    }
    public float GetAttackDelay(){
        float delay = this.soldierCtrl.soldierStats.GetAttackDelay();
        //Count buff
        return delay;
    }
    public float GetAttackRange(){
        return this.soldierCtrl.soldierStats.GetAttackRange();
    }
    
    public bool isBattle(){
        if(this.numberPosition == 0) return false;
        return true;
    }
}
