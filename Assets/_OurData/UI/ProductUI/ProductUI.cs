using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProductUI : LoadBehaviour
{
    public ProductName productName = ProductName.noProduct;
    public TextMeshProUGUI number;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if(this.productName == ProductName.noProduct){
            this.productName = ProductNameParser.FromString(transform.name);
        }
        this.LoadText();
    }

    public void LoadText(){
        this.number = transform.Find("Number").GetComponent<TextMeshProUGUI>();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        this.UpdateData();
    }

    public void UpdateData(){
        this.number.text = NumberForm.ToString(ResourcesManager.instance.GetProductStorageByName(this.productName).number);
    }
}
