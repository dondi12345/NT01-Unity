using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : LoadBehaviour
{
    public float dmg = 0;
    public float speed = 10f;
    public Soldier target;

    public float walkLimit = 0.1f;

    public bool canDmg = false;

    public bool immediatelyDmg = false;
    public bool teleport = false;

    public float waitToStart = 0f;
    public float waitForDestroy = 0f;

    [Header("Control")]
    public float rateFreeze = 0;
    public float timeFreeze = 0;
    public float ratePetrify = 0;
    public float timePetrify = 0;

    [Header("Explode")]
    public bool isExplode = false;
    public float rangeExplode = 3.5f;
    public int numberAffect = 1;

    [Header("Sound")]
    public SoundName soundEnable = SoundName.noSound;
    public SoundName soundStart = SoundName.noSound;
    public SoundName soundDamage = SoundName.noSound;

    public BulletCtrl bulletCtrl;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBulletCtrl();
    }

    protected virtual void LoadBulletCtrl(){
        this.bulletCtrl = this.transform.GetComponent<BulletCtrl>();
    }

    protected override void FixedUpdate() {
        if(this.target == null){
            StartCoroutine(this.DestroyBullet());
            return;
        }

        if(!this.bulletCtrl.bulletMovement.IsCloseTarget() && !this.immediatelyDmg) return;
        this.Damage();
    }

    public void SetData(Soldier soldier, float dmg){
        this.target = soldier;
        this.dmg = dmg;

        this.bulletCtrl.bulletMovement.teleport = this.teleport;
        this.bulletCtrl.bulletMovement.target = soldier.transform;
        this.bulletCtrl.bulletMovement.speed = this.speed;
        this.bulletCtrl.bulletMovement.walkLimit = this.walkLimit;

        SoundManager.instance.OnSoundByName(this.soundEnable);

        StartCoroutine(this.WaiteStart());
    }

    public IEnumerator WaiteStart(){
        yield return new WaitForSeconds(this.waitToStart);
        
        SoundManager.instance.OnSoundByName(this.soundStart);

        this.canDmg = true;
        if(!this.immediatelyDmg){
            this.bulletCtrl.bulletMovement.canMove = true;
        }
    }

    protected IEnumerator DestroyBullet(){
        yield return new WaitForSeconds(this.waitForDestroy);
        transform.gameObject.SetActive(false);
        Destroy(transform.gameObject);
    }

    protected void Damage(){
        if(this.target == null) return;
        if(!this.canDmg) return;
        this.canDmg = false;

        if(isExplode){
            this.Explode();
        }else{
            this.DamageTarget(target);
        }


        SoundManager.instance.OnSoundByName(this.soundDamage);
        StartCoroutine(this.DestroyBullet());
    }

    protected void Explode(){
        this.bulletCtrl.explodeEffect.gameObject.SetActive(true);
        List<Soldier> targets = new List<Soldier>();
        targets.Add(this.target);
        int count = 0;

        try
        {
            List<Soldier> opponents = new List<Soldier>();

            if(this.target.soldierCtrl.soldierBattle.soldierTeamName == SoldierTeamName.ally){
                opponents.AddRange(BattleManager.instance.alliesAlive);
                opponents.Remove(this.target);
            }
            if(this.target.soldierCtrl.soldierBattle.soldierTeamName == SoldierTeamName.enemy){
                opponents.AddRange(BattleManager.instance.enemiesAlive);
                opponents.Remove(this.target);
            }

            
            foreach (Soldier opponent in opponents)
            {
                if(Vector3.Distance(this.target.transform.position, opponent.transform.position) <= this.rangeExplode){
                    targets.Add(opponent);
                    count ++;
                    if(count >= this.numberAffect) break;
                }
            }
        }
        catch (System.Exception)
        {
            
        }

        this.dmg = this.dmg / targets.Count;
        foreach (Soldier target in targets)
        {
            DamageTarget(target);
        }
    }

    protected virtual void DamageTarget(Soldier soldier){
        float randEffect = Random.Range(1f, 100f);
        if(randEffect <= this.rateFreeze){
            soldier.soldierCtrl.debuffCtrl.Freeze(this.timeFreeze);
        }
        if(randEffect <= this.ratePetrify){
            soldier.soldierCtrl.debuffCtrl.Petrify(this.timePetrify);
        }
        soldier.soldierCtrl.soldierBattle.GetDamage(this.dmg);
    }
}
