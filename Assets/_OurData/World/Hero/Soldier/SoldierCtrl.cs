using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SoldierCtrl : LoadBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Soldier soldier;
    public SoldierMovement soldierMovement;
    public SoldierStats soldierStats;
    public SoldierBattle soldierBattle;
    public DebuffCtrl debuffCtrl;

    public Animator animator;

    public ProcessBar healBar;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAgent();
        this.LoadSoldier();
        this.LoadSoldierMovement();
        this.LoadSoldierStats();
        this.LoadSoldierBattle();
        this.LoadDebuffCtrl();
        this.LoadAnimator();
        this.LoadHealBar();
    }

    protected virtual void LoadAgent()
    {
        if (this.navMeshAgent != null) return;
        this.navMeshAgent = GetComponent<NavMeshAgent>();
        Debug.Log(transform.name + ": LoadAgent", gameObject);
    }

    public virtual void LoadSoldier()
    {
        if (this.soldier != null) return;
        this.soldier = GetComponent<Soldier>();
        Debug.Log(transform.name + ": LoadSoldier", gameObject);
    }

    public virtual void LoadSoldierMovement()
    {
        if (this.soldierMovement != null) return;
        this.soldierMovement = transform.Find("SoldierMovement").GetComponent<SoldierMovement>();
        Debug.Log(transform.name + " LoadSoldierLevel", gameObject);
    }
    public virtual void LoadSoldierBattle()
    {
        if (this.soldierBattle != null) return;
        this.soldierBattle = transform.Find("SoldierBattle").GetComponent<SoldierBattle>();
        Debug.Log(transform.name + " LoadSoldierBattle", gameObject);
    }
    public virtual void LoadSoldierStats()
    {
        if (this.soldierStats != null) return;

        SoldierStats soldierStats = transform.Find("SoldierStats").GetComponent<SoldierStats>();
        if(soldierStats != null){
            this.soldierStats = soldierStats;
            Debug.Log(transform.name + " LoadSoldierStats", gameObject);
            return;
        }
        Debug.LogWarning(transform.name + "Can't LoadSoldierStats", gameObject);
    }

    public virtual void LoadDebuffCtrl()
    {
        DebuffCtrl debuffCtrl = transform.Find("DebuffCtrl").GetComponent<DebuffCtrl>();
        if(debuffCtrl == null){
            Debug.LogWarning(transform.name + ": Can't LoadDebuffCtrl", gameObject);
            return;
        }

        this.debuffCtrl = debuffCtrl;
        Debug.Log(transform.name + " LoadDebuffCtrl", gameObject);
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

    public virtual void LoadHealBar()
    {
        if (this.healBar != null) return;
        Transform transformHealBar = transform.Find("Canvas").Find("HealBar");
        if(transformHealBar == null) {
            Debug.Log(transform.name + ": Can't LoadHealBar", gameObject);
            return;
        }

        this.healBar = transformHealBar.GetComponent<ProcessBar>();
       
        Debug.Log(transform.name + ": LoadHealBar", gameObject);
    }
}
