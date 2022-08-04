using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaonEarthArcherPowAttack : SoldierAttack
{
    //Attack random 3 targets
    public override void Attack(Soldier soldier, float dmg){
        dmg *= this.percentDmg;
        Soldier target1 = soldier;
        Soldier target2 = soldier;
        Soldier target3 = soldier;
        try
        {   
            List<Soldier> opponents = null;

            if(this.soldierCtrl.soldierBattle.soldierTeamName == SoldierTeamName.ally){
                opponents = BattleManager.instance.enemiesAlive;
            }
            if(this.soldierCtrl.soldierBattle.soldierTeamName == SoldierTeamName.enemy){
                opponents = BattleManager.instance.alliesAlive;
            }

            target2 = opponents[Random.Range(0, opponents.Count)];
            target3 = opponents[Random.Range(0, opponents.Count)];
        }
        catch (System.Exception){}

        Bullet bullet1 = Instantiate<GameObject>(this.bulletG).GetComponent<Bullet>();
        bullet1.transform.position = this.bulletG.transform.position;
        bullet1.gameObject.SetActive(true);
        bullet1.SetData(target1, dmg);

        Bullet bullet2 = Instantiate<GameObject>(this.bulletG).GetComponent<Bullet>();
        bullet2.transform.position = this.bulletG.transform.position;
        bullet2.waitToStart += 0.15f;
        bullet2.gameObject.SetActive(true);
        bullet2.SetData(target2, dmg);

        Bullet bullet3 = Instantiate<GameObject>(this.bulletG).GetComponent<Bullet>();
        bullet3.transform.position = this.bulletG.transform.position;
        bullet3.waitToStart += 0.3f;
        bullet3.gameObject.SetActive(true);
        bullet3.SetData(target3, dmg);
    }
}
