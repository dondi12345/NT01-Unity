using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiningItemUI : LoadBehaviour
{
    public GameObject receiveItemIcon;
    public Transform contentItemIcons;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadReceiveItemIcon();
        this.LoadContentItemIcon();
    }

    public void LoadReceiveItemIcon(){
        Transform trans = transform.parent.parent.Find("ItemUI").Find("Collections").Find("ReceiveItemIcon");
        if(trans == null){
            Debug.LogWarning(transform.name + ": Not LoadItemIcon", gameObject);
            return;
        }
        this.receiveItemIcon = trans.gameObject;
        Debug.Log(transform.name + ": LoadItemIcon", gameObject);
    }

    public void LoadContentItemIcon(){
        Transform trans = transform.Find("Panel").Find("Content");
        if(trans == null) return;

        this.contentItemIcons = trans;
        Debug.Log(transform.name + ": LoadContentItemIcon", gameObject);
    }

    public void ReloadItemIconByItemData(List<ItemData> itemDatas){
        this.ClearItemIcon();
        foreach (ItemData item in itemDatas)
        {
            ReceiveItemIcon receiveItemIcon = Instantiate<GameObject>(this.receiveItemIcon).transform.GetComponent<ReceiveItemIcon>();
            receiveItemIcon.transform.SetParent(this.contentItemIcons);
            receiveItemIcon.itemName = item.itemName;
            receiveItemIcon.number = item.number;
            receiveItemIcon.transform.localScale = new Vector3(1,1,1);
            receiveItemIcon.UpdateData();
            receiveItemIcon.gameObject.SetActive(true);
            receiveItemIcon.TakeItem();
        }
    }

    public void ClearItemIcon(){
        foreach (Transform trans in contentItemIcons)
        {
            trans.gameObject.SetActive(false);
            Destroy(trans.gameObject);
        }
    }

    public void Close(){
        
        this.OffUI();
    }

    public void OnUI(List<ItemData> itemDatas){
        
        if(itemDatas.Count == 0) return;
        this.gameObject.SetActive(true);
        this.ReloadItemIconByItemData(itemDatas);
    }

    public void OffUI(){
        this.gameObject.SetActive(false);
    }
}
