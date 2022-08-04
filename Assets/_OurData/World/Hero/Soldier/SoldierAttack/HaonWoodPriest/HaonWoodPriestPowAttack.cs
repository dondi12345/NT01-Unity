using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaonWoodPriestPowAttack : SoldierAttack
{
    //Heal ally hp min;
    public override void Attack(Soldier soldier, float dmg){
        dmg *= this.percentDmg;
        Soldier target = soldierCtrl.soldier;

        // try
        // {   
            List<Soldier> allies = null;

            if(this.soldierCtrl.soldierBattle.soldierTeamName == SoldierTeamName.ally){
                allies = BattleManager.instance.alliesAlive;
            }
            if(this.soldierCtrl.soldierBattle.soldierTeamName == SoldierTeamName.enemy){
                allies = BattleManager.instance.enemiesAlive;
            }

            float hpMin = target.soldierCtrl.soldierBattle.currentHp;
            for (int i = 0; i < allies.Count; i++)
            {
                if(hpMin > allies[i].soldierCtrl.soldierBattle.currentHp){
                    hpMin = allies[i].soldierCtrl.soldierBattle.currentHp;
                    target = allies[i];
                }
            }
            // foreach (Soldier ally in allies)
            // {
            //     Debug.LogWarning(ally.workerName+""+ally.soldierCtrl.soldierBattle.currentHp);
            //     if(hpMin > ally.soldierCtrl.soldierBattle.currentHp){
            //         hpMin = ally.soldierCtrl.soldierBattle.currentHp;
            //         target = ally;
            //     }
            // }
        // }catch (System.Exception){}

        Bullet bullet = Instantiate<GameObject>(this.bulletG).GetComponent<Bullet>();
        bullet.transform.position = this.bulletG.transform.position;
        bullet.gameObject.SetActive(true);
        bullet.SetData(target, dmg);
    }
}
