using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReceiveItemIcon : LoadBehaviour
{
    public ItemName itemName;

    public Text textNumber;

    public int number;

    public ImageItemCtr imageItemCtr;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        try
        {
            if(itemName == ItemName.noItem){
                this.itemName = ItemNameParser.FromString(transform.name);
            }
        }
        catch (System.Exception)
        {
            Debug.LogWarning("Can't Load Item");
            return;
        }
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
        this.textNumber.text = NumberForm.ToString(number);
        if(number <= 0){
            gameObject.SetActive(false);
        }
        this.imageItemCtr.SetImage(this.itemName);
    }

    public void TakeItem(){
        ItemManager.instance.TakeItem(this.itemName, this.number);
    }
}
