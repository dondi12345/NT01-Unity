using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierBattle : LoadBehaviour
{
    public SoldierCtrl soldierCtrl;
    public Soldier opponent;

    //damage = skill_attack * (base_attack^2/(base_attack + defense))
    public float atk = 10;
    public float def = 5;
    public float hp = 50;

    public float currentHp = 50;

    [SerializeField] protected float attackTimer = 0f;
    public float attackDelay = 2f;

    public bool isBattle = false;

    public bool canAttack = true;
    public SoldierAttack normalAttack;
    public SoldierAttack powAttack;
    public float currentEnergy = 0;
    public float powEnergy = 3;

    public SoldierTeamName soldierTeamName = SoldierTeamName.noTeam;


    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSoldierCtrl();
        this.LoadNormalAttack();
        this.LoadPowAttack();
    }

    protected virtual void LoadSoldierCtrl()
    {
        if (this.soldierCtrl != null) return;
        this.soldierCtrl = transform.parent.GetComponent<SoldierCtrl>();
        Debug.Log(transform.name + ": LoadSoldierCtrl", gameObject);
    }

    protected virtual void LoadNormalAttack(){
        if(this.normalAttack != null) return;
        try
        {
            this.normalAttack = transform.parent.Find("Attack").Find("NormalAttack").GetComponent<SoldierAttack>();
        }
        catch (System.Exception)
        {
            Debug.LogWarning(transform.name + ": Can't LoadNormalAttack", gameObject);
        }
        
        Debug.Log(transform.name + ": LoadNormalAttack", gameObject);
    }
    protected virtual void LoadPowAttack(){
        if(this.powAttack != null) return;
        try
        {
            this.powAttack = transform.parent.Find("Attack").Find("PowAttack").GetComponent<SoldierAttack>();
        }
        catch (System.Exception)
        {
            Debug.LogWarning(transform.name + ": Can't LoadPowAttack", gameObject);
        }
        
        Debug.Log(transform.name + ": LoadPowAttack", gameObject);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if(!this.isBattle) return;
        if(this.opponent == null || !this.opponent.gameObject.activeSelf) this.FindOpponent();

        this.Battle();
    }

    public void StartBattle(){
        this.soldierCtrl.soldier.UpdateData();
        this.UpdateData();
        this.isBattle = true;
        this.currentHp = this.hp;

        this.soldierCtrl.soldierMovement.runSpeed = SoldierManager.instance.soldierWalkSpeed;
        this.soldierCtrl.soldierMovement.walkLimit = this.soldierCtrl.soldier.GetAttackRange();
        this.soldierCtrl.soldierMovement.UpdateData();
    }

    public void UpdateData(){
        this.UpdateStat();
    }

    public void UpdateStat(){
        this.atk = this.soldierCtrl.soldier.GetAtk();
        this.def = this.soldierCtrl.soldier.GetDef();
        this.hp = this.soldierCtrl.soldier.GetHp();
        this.attackDelay = this.soldierCtrl.soldier.GetAttackDelay();
    }

    protected void Battle(){
        this.attackTimer += Time.fixedDeltaTime;
        if(this.opponent == null || !this.opponent.gameObject.activeSelf) {
            this.soldierCtrl.soldierMovement.target = null;
            return;
        }
        this.soldierCtrl.soldierMovement.target = this.opponent.transform;
        if(!this.soldierCtrl.soldierMovement.IsCloseTarget()) return;

        if (this.attackTimer < this.attackDelay) return;
        this.attackTimer = 0;

        this.Attack();
    }

    protected void Attack(){
        if(!this.canAttack) return;

        if(this.opponent == null) return;
        float dmg =  this.atk;
        this.soldierCtrl.animator.SetTrigger("Attack");

        if(this.currentEnergy >= this.powEnergy && this.powAttack != null){
            this.powAttack.Attack(this.opponent, dmg);
            currentEnergy -= powEnergy;
            return;
        }

        if(this.normalAttack == null){
            this.opponent.soldierCtrl.soldierBattle.GetDamage(dmg);
            this.currentEnergy += 1;
            return;
        }

        this.normalAttack.Attack(this.opponent, dmg);
        this.currentEnergy += 1;
    }

    public void FindOpponent(){
        if(transform.parent.parent.name.Equals("Allies")){
            this.opponent = BattleManager.instance.GetRandEnemy();
            for (int i = 0; i < 5; i++)
            {
                Soldier soldier = BattleManager.instance.GetRandEnemy();
                if(soldier == null) return;
                if(Vector3.Distance(transform.position, soldier.transform.position) < Vector3.Distance(transform.position, opponent.transform.position)){
                    this.opponent = soldier;
                }
            }
        }

        if(transform.parent.parent.name.Equals("Enemies")){
            this.opponent = BattleManager.instance.GetRandAlly();
            for (int i = 0; i < 5; i++)
            {
                Soldier soldier = BattleManager.instance.GetRandAlly();
                if(soldier == null) return;
                if(Vector3.Distance(transform.position, soldier.transform.position) < Vector3.Distance(transform.position, opponent.transform.position)){
                    this.opponent = soldier;
                }
            }
        }
    }

    public void GetDamage(float dmg){
        this.UpdateStat();
        dmg = dmg*dmg/(dmg + this.def);
        this.currentHp -= dmg;
        if(this.currentHp <= 0){
            transform.parent.gameObject.SetActive(false);
            if(transform.parent.parent.name.Equals("Allies")){
                BattleManager.instance.AllyDead(this.soldierCtrl.soldier);
            }

            if(transform.parent.parent.name.Equals("Enemies")){
                BattleManager.instance.EnemyDead(this.soldierCtrl.soldier);
            }
        }
        this.soldierCtrl.healBar.SetValue(this.currentHp/this.hp*100);
    }

    public void GetHeal(float dmg){
        this.UpdateStat();
        this.currentHp += dmg;
        if(this.currentHp > this.hp) this.currentHp = this.hp;
        this.soldierCtrl.healBar.SetValue(this.currentHp/this.hp*100);
    }
}
