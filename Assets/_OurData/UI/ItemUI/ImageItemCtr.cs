using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageItemCtr :LoadBehaviour
{
    public List<ImageItem> imageItems;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadImageItem();
    }

    public void LoadImageItem(){
        this.imageItems.Clear();
        foreach (Transform trans in transform)
        {
            foreach (Transform tran in trans)
            {
                ImageItem imageItem = tran.GetComponent<ImageItem>();
                if(imageItem == null) continue;
                this.imageItems.Add(imageItem);
            }
        }
    }

    public void SetImage(ItemName itemName){
        this.OffAllImage();
        this.GetImageItemByName(itemName).gameObject.SetActive(true);
    }

    public void OffAllImage(){
        foreach (ImageItem imageItem in this.imageItems)
        {
            imageItem.gameObject.SetActive(false);
        }
    }

    public ImageItem GetImageItemByName(ItemName itemName){
        return this.imageItems.Find((imageItem) => imageItem.itemName == itemName);
    }
}
