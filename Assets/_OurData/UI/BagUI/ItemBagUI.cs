using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBagUI : LoadBehaviour
{
    public List<Item> items;
    public List<ItemIcon> itemIcons;

    public GameObject itemIcon;
    public Transform contentItemIcon;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadItemIcon();
        this.LoadContentItemIcon();
    }

    public void LoadItemIcon(){
        Transform trans = transform.parent.parent.Find("ItemUI").Find("Collections").Find("ItemIcon");
        if(trans == null){
            Debug.LogWarning(transform.name + ": Not LoadItemIcon", gameObject);
            return;
        }
        this.itemIcon = trans.gameObject;
        Debug.Log(transform.name + ": LoadItemIcon", gameObject);
    }

    public void LoadContentItemIcon(){
        Transform trans = transform.Find("Panel").Find("Items").Find("ScrollView").Find("Viewport").Find("Content");
        if(trans == null) return;

        this.contentItemIcon = trans;
        Debug.Log(transform.name + ": LoadContentItemIcon", gameObject);
    }

    public void LoadData(){
        this.LoadItem();
        this.ReloadItemIconByItem();
    }

    public void LoadItem(){
        this.items = ItemManager.instance.GetItemHave();
    }

    public void ReloadItemIconByItem(){
        this.ClearItemIcon();
        this.itemIcons.Clear();
        foreach (Item item in this.items)
        {
            ItemIcon itemIcon = Instantiate<GameObject>(this.itemIcon).transform.GetComponent<ItemIcon>();
            itemIcon.transform.SetParent(this.contentItemIcon);
            itemIcon.item = item;
            itemIcon.UpdateData();
            itemIcon.transform.localScale = new Vector3(1,1,1);
            itemIcon.gameObject.SetActive(true);
            this.itemIcons.Add(itemIcon);
        }
    }

    //Function

    public void ClearItemIcon(){
        foreach (Transform trans in contentItemIcon)
        {
            trans.gameObject.SetActive(false);
            Destroy(trans.gameObject);
        }
    }

    public void Close(){
        
        this.OffUI();
    }

    public void OnUI(){
        

        this.transform.gameObject.SetActive(true);
        this.LoadData();
    }
    public void OffUI(){
        this.transform.gameObject.SetActive(false);
    }

    public ItemIcon GetItemIconByName(ItemName itemName){
        return this.itemIcons.Find((itemIcon) => (itemIcon.item.itemName == itemName));
    }
}
