using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationDailyGift : NotificationUI
{

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        this.CheckDailyGift();
    }

    protected void CheckDailyGift(){
        if(PlayerManager.instance.isDailyGiftPlayerLevel){
            this.transImage.gameObject.SetActive(false);
        }else{
            this.transImage.gameObject.SetActive(true);
        }
        
    }
}
