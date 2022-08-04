using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : LoadBehaviour
{
    public int lv = 0;
    public int slot = 1;

    public int expandLv = 0;

    public float multiExpand = 0.1f;
    public float multiTotal =1;

    public BuildingName buildingName = BuildingName.noBuilding;
    public string LocalizationKey = "";

    public BuildingCtrl buildingCtrl;
    public ProductStorage productStorage;

    public int nextLv = 1;

    public int lvUnlock = 2;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        try
        {
            this.buildingName = BuildingNameParser.FromString(transform.name);
        }
        catch (System.Exception)
        {
            this.buildingName = BuildingName.noBuilding;
            Debug.LogWarning("Can't Load BuildingName");
        }
        this.LoadProductStorage();
        this.LoadBuildingCtrl();
    }

    protected virtual void LoadBuildingCtrl()
    {
        if (this.buildingCtrl != null) return;
        this.buildingCtrl = transform.GetComponent<BuildingCtrl>();
        Debug.Log(transform.name + ": LoadBuildingCtrl", gameObject);
    }

    public virtual void LoadProductStorage()
    {
        try
        {
            Transform transProductStorage = transform.Find("ProductStorage");
            foreach(Transform trans in transProductStorage){
                ProductStorage productStorage = trans.GetComponent<ProductStorage>();
                if(productStorage != null){
                    this.productStorage = productStorage;
                    break;
                }
            }
        }
        catch (System.Exception)
        {
            Debug.LogWarning(transform.name + ": Not find LoadProductStorage", gameObject);
        }

        Debug.Log(transform.name + ": LoadProductStorage", gameObject);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        this.CheckUnlock();
    }

    protected override void Start()
    {
        base.Start();
        this.UpdateData();
    }

    protected void CheckUnlock(){
        if(this.lv != 0) return;
        if(BuildingManager.instance.warehouse.expandLv < this.lvUnlock) return;
        this.lv = 1;
        this.expandLv = 1;
        this.UpdateData();
    }

    //Function

    public void UpdateData(){
        this.slot = expandLv;
        this.LoadMulti();
        this.buildingCtrl.buildingProduce.UpdateGainProduction();
        this.buildingCtrl.buildingUpgrade.UpdateProductCostUpgrade();
        this.buildingCtrl.buildingUpgrade.UpdateProductCostExpand();

        if(this.lv == 0){
            this.buildingCtrl.nameBuildingUI.Lock();
        }else{
            this.buildingCtrl.nameBuildingUI.Unlock();
        }

        this.buildingCtrl.buildingModel.SetModelLv(this.expandLv);
    }

    public void LoadMulti(){
        this.multiTotal = 1;
        
        this.multiExpand = (float)this.expandLv/10;

        BuildingManager.instance.warehouse.UpdateData();
        float multiWarehouse = BuildingManager.instance.warehouse.multi;

        float multiWave = PlayerManager.instance.GetMultiMoneyByWave();
        
        this.multiTotal += multiWarehouse + multiWave + multiExpand;
    }

    public void UpLvOneTime(){
        this.buildingCtrl.buildingUpgrade.kindUpgradeLv = 0;
        this.buildingCtrl.buildingUpgrade.UpdateProductCostUpgrade();
    }
    public void UpLvTenTime(){
        this.buildingCtrl.buildingUpgrade.kindUpgradeLv = 1;
        this.buildingCtrl.buildingUpgrade.UpdateProductCostUpgrade();
    }
    public void UpLvHundredTime(){
        this.buildingCtrl.buildingUpgrade.kindUpgradeLv = 2;
        this.buildingCtrl.buildingUpgrade.UpdateProductCostUpgrade();
    }

    public bool UpgradeLv(){
        return this.buildingCtrl.buildingUpgrade.UpgradeLv();
    }
    public bool ExpandLv(){
        return this.buildingCtrl.buildingUpgrade.ExpandLv();
    }

    public BuildingData ParseToData(){
        this.UpdateData();
        BuildingData buildingData = new BuildingData();
        buildingData.lv = this.lv;
        buildingData.expandLv = this.expandLv;
        buildingData.buildingName = this.buildingName;
        buildingData.productStorageData = this.productStorage.ParseToData();
        return buildingData;
    }
    public void ParseFromData(BuildingData buildingData){
        this.lv = buildingData.lv;
        this.expandLv = buildingData.expandLv;
        this.productStorage.ParseFromData(buildingData.productStorageData);
        this.UpdateData();
    }

    public float GetMoneyGainPerS(){
        float number = 0;
        if(this.lv == 0) return 0;

        number += this.buildingCtrl.buildingProduce.productCreated.number;
        return number/BuildingManager.instance.createDelay;
    }

    public void CountMoneyOffline(float time){
        this.UpdateData();
        float number = time*this.GetMoneyGainPerS();
        this.productStorage.number += number;
    }


    //Click building
    protected virtual void OnMouseDown()
    {

        this.UpdateData();
        if(this.lv == 0) return;
        if(!TownUIManager.instance.isUIActive()) return;
        this.buildingCtrl.buildingUpgrade.kindUpgradeLv = 0;
        TownUIManager.instance.OnBuildingUI(this.buildingName);
    }
}
