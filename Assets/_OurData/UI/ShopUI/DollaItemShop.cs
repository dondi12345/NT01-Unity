using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollaItemShop : ItemIconShop
{
    public override void BuyItem(){
        ItemManager.instance.TakeItem(this.itemName, this.numberItem);
    }
}
