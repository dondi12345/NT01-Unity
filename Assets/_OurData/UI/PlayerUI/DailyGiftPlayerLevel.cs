using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyGiftPlayerLevel : LoadBehaviour
{
    public List<ReceiveItemIcon> receiveItemIcons;
    protected ProfilePlayerUI profilePlayerUI;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadReceiveItemIcon();
    }

    public void LoadReceiveItemIcon(){
        this.receiveItemIcons.Clear();
        Transform transformReceiveItemIcon = transform.Find("Panel").Find("ReceiveItem");
        foreach (Transform trans in transformReceiveItemIcon)
        {
            ReceiveItemIcon receiveItemIcon = trans.GetComponent<ReceiveItemIcon>();
            if(receiveItemIcon == null) return;
            this.receiveItemIcons.Add(receiveItemIcon);
        }
    }

    public void OnUI(ProfilePlayerUI profilePlayerUI){
        
        this.profilePlayerUI = profilePlayerUI;
        if(PlayerManager.instance.isDailyGiftPlayerLevel) return;
        gameObject.SetActive(true);
        this.GetReceiveItemIconByName(ItemName.oneDiamond).number = PlayerManager.instance.lv;
        foreach (ReceiveItemIcon receiveItemIcon in this.receiveItemIcons)
        {
            receiveItemIcon.UpdateData();
        }
    }

    public void Close(){
        
        this.OffUI();
    }

    public void OffUI(){
        foreach (ReceiveItemIcon receiveItemIcon in this.receiveItemIcons)
        {
            receiveItemIcon.TakeItem();
        }
        PlayerManager.instance.isDailyGiftPlayerLevel = true;
        this.profilePlayerUI.UpdatData();
        gameObject.SetActive(false);
    }

    public ReceiveItemIcon GetReceiveItemIconByName(ItemName itemName){
        return this.receiveItemIcons.Find((receiveItemIcon) => (receiveItemIcon.itemName == itemName));
    }
}
