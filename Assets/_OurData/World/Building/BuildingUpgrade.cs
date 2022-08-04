using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingUpgrade : LoadBehaviour
{
    public float multi =1;

    public ProductStorage productCostUpgrade;
    public float multiCostUpgrade = 25f;
    public float baseDownCostUpgrade = 10f;

    public int costExpand = 1;
    public int baseCostExpand = 1;

    public BuildingCtrl buildingCtrl;

    public int kindUpgradeLv = 0;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBuildingCtrl();
        this.LoadProductCostUpgrade();
    }

    public virtual void LoadProductCostUpgrade()
    {
        try
        {
            Transform transProductCostUpgrade = transform.Find("ProductCostUpgrade");
            foreach(Transform trans in transProductCostUpgrade){
                ProductStorage productCostUpgrade = trans.GetComponent<ProductStorage>();
                if(productCostUpgrade != null){
                    this.productCostUpgrade = productCostUpgrade;
                    break;
                }
            }
        }
        catch (System.Exception)
        {
            Debug.LogWarning(transform.name + ": Not find LoadProductCostUpgrade", gameObject);
        }

        Debug.Log(transform.name + ": LoadProductCostUpgrade", gameObject);
    }

    protected virtual void LoadBuildingCtrl()
    {
        if (this.buildingCtrl != null) return;
        this.buildingCtrl = transform.parent.GetComponent<BuildingCtrl>();
        Debug.Log(transform.name + ": LoadBuildingCtrl", gameObject);
    }

    public void UpdateProductCostUpgrade(){
        if(this.kindUpgradeLv == 0){
            this.buildingCtrl.building.nextLv = 1;
            float number = ((buildingCtrl.building.lv-1)*buildingCtrl.building.lv/2+1) * this.multiCostUpgrade -this.baseDownCostUpgrade;
            this.productCostUpgrade.number = number;
        }
        if(this.kindUpgradeLv == 1){
            this.buildingCtrl.building.nextLv = 10;
            float nextNumber2 = buildingCtrl.building.lv -1 +10;
            float number2 = ((nextNumber2-1)*nextNumber2*(nextNumber2+1)/6 + nextNumber2) * this.multiCostUpgrade -this.baseDownCostUpgrade*nextNumber2;
            float nextNumber1 = buildingCtrl.building.lv-1;
            float number1 = ((nextNumber1-1)*nextNumber1*(nextNumber1+1)/6 + nextNumber1) * this.multiCostUpgrade -this.baseDownCostUpgrade*nextNumber1;
            this.productCostUpgrade.number = number2-number1;
        }
        if(this.kindUpgradeLv == 2){
            this.UpdateCostUpgradeMax();
        }
    }

    public void UpdateCostUpgradeMax(){
        float nextNumber1 = buildingCtrl.building.lv-1;
        float number1 = ((nextNumber1-1)*nextNumber1*(nextNumber1+1)/6 + nextNumber1) * this.multiCostUpgrade -this.baseDownCostUpgrade*nextNumber1;
        this.buildingCtrl.building.nextLv = 1;
        for (int i = 1; i <= 50; i++)
        {
            float nextNumber2 = buildingCtrl.building.lv -1 +i;
            float number2 = ((nextNumber2-1)*nextNumber2*(nextNumber2+1)/6 + nextNumber2) * this.multiCostUpgrade -this.baseDownCostUpgrade*nextNumber2;
            this.productCostUpgrade.number = number2-number1;
            if(!this.CanUpgrade()){
                break;
            }
            this.buildingCtrl.building.nextLv = i;
        }
        float nextNumber3 = buildingCtrl.building.lv -1 + this.buildingCtrl.building.nextLv;
        float number3 = ((nextNumber3-1)*nextNumber3*(nextNumber3+1)/6 + nextNumber3) * this.multiCostUpgrade -this.baseDownCostUpgrade*nextNumber3;
        this.productCostUpgrade.number = number3-number1;
    }

    public void UpdateProductCostExpand(){
        float number = ((buildingCtrl.building.expandLv-1)*buildingCtrl.building.expandLv/2 +1) * this.baseCostExpand;
        this.costExpand = (int)number;
    }

    public bool UpgradeLv(){
        this.UpdateProductCostUpgrade();
        if(!this.CanUpgrade()) return false;
        
        ResourcesManager.instance.GetProductStorageByName(this.productCostUpgrade.productName).TakeOut(this.productCostUpgrade);

        this.buildingCtrl.building.lv += buildingCtrl.building.nextLv;
        this.UpdateProductCostUpgrade();
        return true;
    }

    public bool ExpandLv(){
        this.UpdateProductCostExpand();
        if(!this.CanExpand()) return false;
        
        ItemManager.instance.GetItemByName(ItemName.blueprintBuilding).number -= this.costExpand;

        this.buildingCtrl.building.expandLv ++;
        this.UpdateProductCostExpand();
        this.buildingCtrl.building.UpdateData();
        return true;
    }

    public bool CanUpgrade(){
        if(ResourcesManager.instance.GetProductStorageByName(this.productCostUpgrade.productName).number < this.productCostUpgrade.number) {
            return false;
        }
        return true;
    }
    public bool CanExpand(){
        if(ItemManager.instance.GetItemByName(ItemName.blueprintBuilding).number < this.costExpand) {
            return false;
        }
        return true;
    }


}
