using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnBattleSpeed : LoadBehaviour
{
    public Transform imageCover;
    public float speed;

    public int lvUnlock = 5;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadImageCover();
    }

    protected void LoadImageCover(){
        this.imageCover = transform.Find("ImageCover");
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        this.UpdateStatus();
        
    }

    protected void UpdateStatus(){
        if(PlayerManager.instance.lv < this.lvUnlock){
            return;
        }
        if(PlayerManager.instance.battleSpeed == this.speed){
            this.imageCover.gameObject.SetActive(false);
        }else{
            this.imageCover.gameObject.SetActive(true);
        }
    }

    public void OnClick(){
        if(PlayerManager.instance.lv < this.lvUnlock){
            WarningUI warningUI = BattleUIManager.instance.GetWarningUIByName(WarningName.unlockBattleSpeed2);
            warningUI.OnUI();
            return;
        }
        if(PlayerManager.instance.battleSpeed == this.speed){
            PlayerManager.instance.battleSpeed = 1;
        }else{
            PlayerManager.instance.battleSpeed = speed;
        }
        
        BattleManager.instance.BattleTimeSpeed(PlayerManager.instance.battleSpeed);
    }
}
