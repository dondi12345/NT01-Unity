using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoldierIcon : LoadBehaviour
{
    public Soldier soldier;
    public CardWorker cardWorker;
    public Transform statusBattle;

    protected override void Start()
    {
        this.UpdateData();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        this.UpdateStatusBattle();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTransStatusWoking();
        this.LoadCardWorker();
    }

    protected void LoadCardWorker(){
        this.cardWorker = transform.Find("CardWorker").GetComponent<CardWorker>();
    }

    public void LoadTransStatusWoking(){
        Transform trans = transform.Find("Status");
        if(trans == null) {
            Debug.LogWarning("Can't LoadTransStatusWoking");
            return;
        }
        this.statusBattle = trans;
    }

    public void UpdateData(){
        if(this.soldier == null) return;
        this.UpdateStatusBattle();
        this.cardWorker.UpdateDataSoldier(this.soldier);
    }

    public void UpdateStatusBattle(){
        if(this.soldier.isBattle()){
            this.statusBattle.gameObject.SetActive(true);
        }else{
            this.statusBattle.gameObject.SetActive(false);
        }
    }

    public void PutPosition(){
        
        if(this.soldier.isBattle()){
            soldier.ComeToBag();
            this.UpdateData();
            return;
        }
        
        Position position = PositionManager.instance.GetEmptyAllyPosition();
        if(position == null) return;
        position.PutSoldier(this.soldier);
        this.UpdateData();
    }
}
