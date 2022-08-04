using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyItemWarning : LoadBehaviour
{
    public ItemIconShop itemIconShop;

    public void BuyItem(){
        
        this.itemIconShop.BuyItem();
        this.OffUI();
    }

    public void Close(){
        
        this.OffUI();
    }

    public void OnUI(ItemIconShop itemIconShop){
        
        gameObject.SetActive(true);
        this.itemIconShop = itemIconShop;
    }

    public void OffUI(){
        gameObject.SetActive(false);
    }
}
