using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warehouse : LoadBehaviour
{
    
    public int costExpand = 1;
    public int baseCostExpand = 1;
    public int expandLv = 1;
    public int slot = 10;

    public float multi;

    float timeCount = 0;

    public BuildingModel buildingModel;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        this.timeCount += Time.fixedDeltaTime;

        //Colection money every 1s
        if(this.timeCount >= 1){
            this.CollectMoney();
            this.timeCount = 0;
        }
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadModelLv();
    }

    protected virtual void LoadModelLv(){
        this.buildingModel = transform.Find("Model").GetComponent<BuildingModel>();
        Debug.Log(transform.name + " LoadModelLv", gameObject);
    }

    protected override void Start()
    {
        base.Start();
        this.UpdateData();
        this.buildingModel.SetModelLv(this.expandLv);
    }

    //Function

    public void UpdateData(){
        this.slot = expandLv + 9;
        this.UpdateCostExpand();
        this.LoadMulti();
    }

    public void LoadMulti(){
        this.multi = (float)this.expandLv/10;
    }

    public bool ExpandLv(){
        this.UpdateData();
        if(!this.CanExpand()) return false;
        
        ItemManager.instance.GetItemByName(ItemName.blueprintWarehouse).number -= this.costExpand;
        this.expandLv ++;

        this.UpdateData();
        return true;
    }

    public bool CanExpand(){
        if( ItemManager.instance.GetItemByName(ItemName.blueprintWarehouse).number < this.costExpand) {
            TownUIManager.instance.OnWarningUI(WarningName.dontEnoughResource);
            return false;
        }
        return true;
    }

    public void UpdateCostExpand(){
        float exponential = Mathf.Pow(2f, this.expandLv - 1);
        float number = ((this.expandLv-1)*this.expandLv/2 +1) * this.baseCostExpand;
        this.costExpand = (int)number;
    }

    public void CollectMoney(){
        foreach (Building building in BuildingManager.instance.buildings)
        {
            ResourcesManager.instance.GetProductStorageByName(building.productStorage.productName).RecallProduct(building.productStorage);
        }
    }
    
    public WarehouseData ParseToData(){
        this.UpdateData();
        WarehouseData warehouseData = new WarehouseData();
        warehouseData.expandLv = this.expandLv;
        return warehouseData;
    }
    public void ParseFromData(WarehouseData warehouseData){
        this.expandLv = warehouseData.expandLv;
        this.UpdateData();
    }
     

    //Get

    //Click
    private void OnMouseDown()
    {
        if(!TownUIManager.instance.isUIActive()) return;

        TownUIManager.instance.OnWarehouseUI();
    }
}
