using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harbor : Building
{
    protected override void OnMouseDown()
    {
        this.UpdateData();
        if(this.lv == 0) return;
        Debug.Log(TownUIManager.instance.isUIActive());
        if(!TownUIManager.instance.isUIActive()) return;
        this.buildingCtrl.buildingUpgrade.kindUpgradeLv = 0;
        TownUIManager.instance.OnHarborUI();
    }
}
