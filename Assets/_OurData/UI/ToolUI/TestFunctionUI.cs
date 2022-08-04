using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFunctionUI : LoadBehaviour
{
    public void AlliesBatle(){
        foreach (Soldier soldier in BattleManager.instance.alliesAlive)
        {
            soldier.soldierCtrl.soldierBattle.isBattle = true;
        }
    }
}
