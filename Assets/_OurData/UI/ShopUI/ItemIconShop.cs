using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemIconShop : LoadBehaviour
{
    public ItemName itemName;

    public ImageItemCtr imageItemCtr;
    public Text textNumberItem;
    public Text textCost;

    public int numberItem;

    public float costItem;
    public ProductName productName;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        
        this.itemName = ItemNameParser.FromString(transform.name);

        this.LoadImageItemCtr();
        this.LoadText();
    }

    protected void LoadImageItemCtr(){
        this.imageItemCtr = transform.Find("ImageItemCtr").GetComponent<ImageItemCtr>();
        this.imageItemCtr.SetImage(this.itemName);
    }

    protected void LoadText(){
        this.textNumberItem = transform.Find("TextNumberItem").GetComponent<Text>();
        this.textCost = transform.Find("Button").Find("TextCost").GetComponent<Text>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        this.textNumberItem.text = "x"+this.numberItem.ToString();
        this.textCost.text = this.costItem.ToString();

    }

    public virtual void BuyItem(){
        if(!ResourcesManager.instance.GetProductStorageByName(this.productName).TakeOut(this.costItem)) return;

        ItemManager.instance.TakeItem(this.itemName, this.numberItem);
    }

    public void OnClick(){
        TownUIManager.instance.buyItemWarning.OnUI(this);
    }

    public void OnView(){
        TownUIManager.instance.itemShopInfoUI.OnUI(this.itemName);
    }
}
