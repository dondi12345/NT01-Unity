using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductStorage : LoadBehaviour
{
    public ProductName productName;

    public float number;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if(this.productName == ProductName.noProduct){
            this.productName = ProductNameParser.FromString(transform.name);
        }
    }

    public void UpdateData(){}

    public void AddProduct(ProductStorage productStorage){
        if(this.productName != productStorage.productName) return;

        this.number += productStorage.number;
    }

    //move the all product to this
    public void RecallProduct(ProductStorage productStorage){
        if(this.productName != productStorage.productName) return;

        this.number += productStorage.number;
        productStorage.number = 0;
    }

    public void TakeOut(ProductStorage productStorage){
        if(this.productName != productStorage.productName) return;
        if(this.number < productStorage.number) return;

        this.number -= productStorage.number;
    }
    public bool TakeOut(float number){
        if(this.number < number){
            TownUIManager.instance.GetWarningUIByName(WarningName.dontEnoughResource).OnUI();
            return false;
        }

        this.number -= number;
        return true;
    }

    public ProductStorageData ParseToData(){
        this.UpdateData();
        ProductStorageData productStorageData = new ProductStorageData();
        productStorageData.productName = this.productName;
        productStorageData.number = this.number;
        return productStorageData;
    }
    public void ParseFromData(ProductStorageData productStorageData){
        this.number = productStorageData.number;
        this.UpdateData();
    }

    //Get

}
