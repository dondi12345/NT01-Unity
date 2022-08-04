using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftTutorial : LoadBehaviour
{
    public List<ReceiveItemIcon> receiveItemIcons;

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

    public void Close(){
        
        this.OffUI();
    }

    public void OnUI(){
        
        gameObject.SetActive(true);
        this.GetReceiveItemIconByName(ItemName.oneDiamond).number = 10;
        foreach (ReceiveItemIcon receiveItemIcon in this.receiveItemIcons)
        {
            receiveItemIcon.UpdateData();
        }
    }

    public void OffUI(){
        foreach (ReceiveItemIcon receiveItemIcon in this.receiveItemIcons)
        {
            receiveItemIcon.TakeItem();
        }
        PlayerManager.instance.isDailyGiftPlayerLevel = true;
        gameObject.SetActive(false);
    }

    public ReceiveItemIcon GetReceiveItemIconByName(ItemName itemName){
        return this.receiveItemIcons.Find((receiveItemIcon) => (receiveItemIcon.itemName == itemName));
    }
}
