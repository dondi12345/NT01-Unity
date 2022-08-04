using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageItem : LoadBehaviour
{
    public ItemName itemName = ItemName.noItem;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if(this.itemName == ItemName.noItem){
            this.itemName = ItemNameParser.FromString(transform.name);
        }
    }
}
