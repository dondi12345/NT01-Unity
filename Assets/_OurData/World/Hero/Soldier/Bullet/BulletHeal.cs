using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHeal : Bullet
{
    protected override void DamageTarget(Soldier soldier){
        soldier.soldierCtrl.soldierBattle.GetHeal(this.dmg);
    }
}
