using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : LoadBehaviour
{
    public ItemName itemName = ItemName.noItem;
    public int number = 0;

    public bool immediatelyUsing = false;

    public string LocalizationKeyOfDescription = "";

    public string LocalizationKeyOfName ="";

    protected override void LoadComponents()
    {
        base.LoadComponents();

        if(this.itemName == ItemName.noItem){
            this.itemName = ItemNameParser.FromString(transform.name);
        }
    }

    //Function

    public void UsingItem(int number){
        if(number <= 0) return;
        if(number > this.number){
            number = this.number;
        }
        this.number -= number;
        ItemManager.instance.UsingItem(this.itemName, number);
    }

    public ItemData ParseToData(){
        ItemData itemData = new ItemData();
        itemData.number = this.number;
        itemData.itemName = this.itemName;
        return itemData;
    }
    public void ParseFromData(ItemData itemData){
        this.number = itemData.number;
    }

    
}
