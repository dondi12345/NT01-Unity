using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.SimpleLocalization;

public class UseItemUI : LoadBehaviour
{
    public ItemIcon itemIcon;
    public TextMeshProUGUI textNumber;
    public MultiLanguageText textDescription;
    public MultiLanguageText textItemName;
    public int numberUse = 1;

    public ImageItemCtr imageItemCtr;
    public GameObject defaultItemIcon;

    public Transform immediatelyUsing;
    public Transform btnConfirm;


    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadImageItem();
        this.LoadDefaultItemIcon();
        this.LoadText();
        this.LoadImmediatelyUsing();
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
        this.textNumber = transform.Find("Panel").Find("ImmediatelyUsing").Find("TextNumber").GetComponent<TextMeshProUGUI>();    
        this.textDescription = transform.Find("Panel").Find("TextDescription").GetComponent<MultiLanguageText>();    
        this.textItemName = transform.Find("Panel").Find("TitleBuilding").Find("TextItemName").GetComponent<MultiLanguageText>();    
    }
    public void LoadImmediatelyUsing(){
        this.immediatelyUsing = transform.Find("Panel").Find("ImmediatelyUsing");    
        this.btnConfirm = transform.Find("Panel").Find("BtnConfirm");    
    }


    //Function
    public void LoadData(){
        this.UpdateIconItemCtr();

        if(this.itemIcon.item.immediatelyUsing){
            immediatelyUsing.gameObject.SetActive(true);
            btnConfirm.gameObject.SetActive(false);
        }
        else{
            immediatelyUsing.gameObject.SetActive(false);
            btnConfirm.gameObject.SetActive(true);
        }

        this.UpdateText();
    }

    public void UpdateText(){
        this.textNumber.text = NumberForm.ToString(this.numberUse);
        this.textItemName.SetLocalizationKey(this.itemIcon.item.LocalizationKeyOfName);
        this.textDescription.SetLocalizationKey(this.itemIcon.item.LocalizationKeyOfDescription);
    }

    public void UpdateIconItemCtr(){
        this.imageItemCtr.SetImage(this.itemIcon.item.itemName);
    }

    public void Use(){
        
        this.itemIcon.item.UsingItem(this.numberUse);
        this.itemIcon.UpdateData();
        this.OffUI();
    }

    public void AddOne(){
        
        if(this.itemIcon.item.number <= this.numberUse) return;
        this.numberUse ++;
        this.UpdateText();
    }

    public void MinusOne(){
        
        if(this.numberUse <= 1) return;
        this.numberUse --;
        this.UpdateText();
    }
    public void AddMax(){
        
        this.numberUse = this.itemIcon.item.number;
        this.UpdateText();
    }

    public void MinusMin(){
        
        this.numberUse = 1;
        this.UpdateText();
    }

    public void Close(){
        
        this.OffUI();
    }
    
    public void OnUI(ItemIcon itemIcon){
        
        this.itemIcon = itemIcon;
        this.numberUse = 1;
        gameObject.SetActive(true);
        this.LoadData();
    }
    public void OffUI(){
        gameObject.SetActive(false);
    }
}
