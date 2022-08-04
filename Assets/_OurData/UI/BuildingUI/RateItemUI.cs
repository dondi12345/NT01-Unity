using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RateItemUI : LoadBehaviour
{
    public ItemName itemName;
    public TextMeshProUGUI textRate;
    public TextMeshProUGUI textNumber;

    public ImageItemCtr imageItemCtr;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadImageItemCtr();
        this.LoadText();
        try
        {
            this.itemName = ItemNameParser.FromString(transform.name);
            this.imageItemCtr.SetImage(this.itemName);
        }
        catch (System.Exception)
        {
            this.itemName = ItemName.noItem;
            Debug.LogWarning("Can't LoadItemName");
        }
    }

    public void LoadImageItemCtr(){
        this.imageItemCtr = transform.Find("ImageItemCtr").GetComponent<ImageItemCtr>();
    }

    public void LoadText(){
        this.textRate = transform.Find("TextRate").GetComponent<TextMeshProUGUI>();
        this.textNumber = transform.Find("TextNumber").GetComponent<TextMeshProUGUI>();
    }

    public void UpdateData(ItemRate itemRate){
        this.textRate.text = itemRate.rate +"%";
        this.textNumber.text = NumberForm.ToString(itemRate.number);
        this.imageItemCtr.SetImage(this.itemName);
    }
}