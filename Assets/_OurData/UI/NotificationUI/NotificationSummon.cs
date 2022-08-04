using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationSummon : NotificationUI
{
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        this.CheckSummon();
    }

    protected void CheckSummon(){
        if(ItemManager.instance.GetItemByName(ItemName.diceItem).number > 0){
            this.transImage.gameObject.SetActive(true);
        }else{
            this.transImage.gameObject.SetActive(false);
        }
    }
}
