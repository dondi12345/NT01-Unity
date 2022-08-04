using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemShopInfoUI : LoadBehaviour
{
    public ItemName itemName;
    public Item item;
    public MultiLanguageText textDescription;
    public MultiLanguageText textItemName;

    public ImageItemCtr imageItemCtr;
    public GameObject defaultItemIcon;


    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadImageItem();
        this.LoadDefaultItemIcon();
        this.LoadText();
    }

    public void LoadImageItem(){
        this.imageItemCtr = transform.Find("Panel").Find("ImageItemCtr").GetComponent<ImageItemCtr>();
    }

    public void LoadDefaultItemIcon(){
        Transform trans = transform.parent.parent.Find("ItemUI").Find("Collections").Find("ItemIcon");
        if(trans == null){
            Debug.LogWarning(transform.name + ": Not LoadItemIcon", gameObject);
            return;
        }
        this.defaultItemIcon = trans.gameObject;
        Debug.Log(transform.name + ": LoadItemIcon", gameObject);
    }

    public void LoadText(){   
        this.textDescription = transform.Find("Panel").Find("TextDescription").GetComponent<MultiLanguageText>();    
        this.textItemName = transform.Find("Panel").Find("TitleBuilding").Find("TextItemName").GetComponent<MultiLanguageText>();    
    }


    //Function
    public void LoadData(){
        this.UpdateIconItemCtr();

        this.UpdateText();
    }

    public void UpdateText(){
        this.textItemName.SetLocalizationKey(this.item.LocalizationKeyOfName);
        this.textDescription.SetLocalizationKey(this.item.LocalizationKeyOfDescription);
    }

    public void UpdateIconItemCtr(){
        this.imageItemCtr.SetImage(this.itemName);
    }

    public void Close(){
        
        this.OffUI();
    }
    
    public void OnUI(ItemName itemName){
        
        this.itemName = itemName;
        this.item = ItemManager.instance.GetItemByName(this.itemName);
        gameObject.SetActive(true);
        this.LoadData();
    }
    public void OffUI(){
        gameObject.SetActive(false);
    }
}
