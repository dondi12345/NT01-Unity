using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRate : LoadBehaviour
{
    public ItemName itemName;
    public int number;
    public int rate;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        try
        {
            this.itemName = ItemNameParser.FromString(transform.name);
        }
        catch (System.Exception)
        {
            this.itemName = ItemName.noItem;
            Debug.LogWarning("Can't LoadItemName");
        }
    }

    public ItemData ParseToData(){
        ItemData itemData = new ItemData();
        itemData.number = this.number;
        itemData.itemName = this.itemName;
        return itemData;
    }
}
