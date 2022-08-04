using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemIcon : LoadBehaviour
{
    public Item item;

    public Text textNumber;

    public ImageItemCtr imageItemCtr;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadText();
        this.LoadImageItemCtr();

    }

    public void LoadText(){
        Transform transTextMulti = transform.Find("TextNumber");
        if(transTextMulti == null) return;
        this.textNumber = transTextMulti.GetComponent<Text>();
    }

    public void LoadImageItemCtr(){
        Transform transImageIconCtr = transform.Find("ImageItemCtr");
        if(transImageIconCtr == null) return;
        this.imageItemCtr = transImageIconCtr.GetComponent<ImageItemCtr>();
    }

    public void UpdateData(){
        this.textNumber.text = NumberForm.ToString(item.number);
        if(item.number <= 0){
            gameObject.SetActive(false);
        }
        this.imageItemCtr.SetImage(item.itemName);
    }

    public void OnClick(){
        TownUIManager.instance.OnUseItemUI(this);
        
    }
}