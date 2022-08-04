using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationTreasure : NotificationUI
{

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        this.CheckTreasure();
    }

    protected void CheckTreasure(){
        if(BuildingManager.instance.GetBuildingByName(BuildingName.harbor).expandLv < 2) {
            this.transImage.gameObject.SetActive(false);
            return;
        }

        if(BuildingManager.instance.miningBuilding.productCostMiningTenTime.number >
            ResourcesManager.instance.GetProductStorageByName(ProductName.money).number){
            this.transImage.gameObject.SetActive(false);
        }else{
            this.transImage.gameObject.SetActive(true);
        }
        
    }
}
