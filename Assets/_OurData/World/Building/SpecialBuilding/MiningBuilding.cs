using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningBuilding : LoadBehaviour
{
    public ProductStorage productCostMiningOneTime;
    public ProductStorage productCostMiningTenTime;
    public float baseProductCostMining = 100;

    public List<ItemRate> itemRates;
    
    public int time = 0;
    public int growthRate = 10;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadItemRate();
        this.LoadProductCostMiningOneTime();
        this.LoadProductCostMiningTenTime();
        this.UpdateItemRate();
    }

    public void LoadItemRate(){
        this.itemRates.Clear();
        Transform transItemRates = transform.Find("RandomItem");
        foreach (Transform trans in transItemRates)
        {
            ItemRate itemRate = trans.GetComponent<ItemRate>();
            if(itemRate == null) continue;
            this.itemRates.Add(itemRate);
        }
    }

    public virtual void LoadProductCostMiningOneTime()
    {
        try
        {
            Transform transProductCostMining = transform.Find("ProductCostMiningOneTime").Find("Money");
            ProductStorage productCostMining = transProductCostMining.GetComponent<ProductStorage>();
            if(productCostMining != null){
                this.productCostMiningOneTime = productCostMining;
            }
        }
        catch (System.Exception)
        {
            Debug.LogWarning(transform.name + ": Not find LoadProductCostMiningOneTime", gameObject);
        }

        Debug.Log(transform.name + ": LoadProductCostMiningOneTime", gameObject);
    }
    public virtual void LoadProductCostMiningTenTime()
    {
        try
        {
            Transform transProductCostMiningTenTime = transform.Find("ProductCostMiningTenTime").Find("Money");
            ProductStorage productCostMiningTenTime = transProductCostMiningTenTime.GetComponent<ProductStorage>();
            if(productCostMiningTenTime != null){
                this.productCostMiningTenTime = productCostMiningTenTime;
            }
        }
        catch (System.Exception)
        {
            Debug.LogWarning(transform.name + ": Not find LoadProductCostMiningTenTime", gameObject);
        }

        Debug.Log(transform.name + ": LoadProductCostMiningTenTime", gameObject);
    }

    public void UpdateData(){
        
        this.UpdateProductCostMining();
        this.UpdateItemRate();
    }

    //Function
    public void UpdateItemRate(){
        this.GetItemRateByName(ItemName.diceItem).rate = 5;
        this.GetItemRateByName(ItemName.diceItem).number = 1;

        this.GetItemRateByName(ItemName.blueprintWarehouse).rate = 10;
        this.GetItemRateByName(ItemName.blueprintWarehouse).number = 1;

        this.GetItemRateByName(ItemName.blueprintBuilding).rate = 25;
        this.GetItemRateByName(ItemName.blueprintBuilding).number = 1;

        // this.GetItemRateByName(ItemName.oneExperience).rate = 20;
        // this.GetItemRateByName(ItemName.oneExperience).number = 1;

        this.GetItemRateByName(ItemName.oneSoul).rate = 100-5-10-25;
        try
        {
            this.GetItemRateByName(ItemName.oneSoul).number = 5+PlayerManager.instance.waveLv/10;
        }
        catch (System.Exception){}
        

        // this.GetItemRateByName(ItemName.moneyOneHourItem).rate = 100-1-5-15-25;
        // this.GetItemRateByName(ItemName.moneyOneHourItem).number = 1;
        
    }

    public void ResetTimeMining(){
        this.time = 0;
    }

    public void UpdateProductCostMining(){
        float exponential = Mathf.Pow(1.3f, time);
        if(exponential < 1){
            exponential = 0;
        }
        this.productCostMiningOneTime.number = this.baseProductCostMining * exponential;

        this.productCostMiningTenTime.number = 0;
        float productCostMining = this.baseProductCostMining;
        for (int i = 0; i < 10; i++)
        {
            float exponential1 = Mathf.Pow(1.3f, time+i);
            if(exponential1 < 1){
                exponential1 = 0;
            }
            this.productCostMiningTenTime.number += productCostMining * exponential1;
        }
    }

    public bool MiningOneTime(){
        this.UpdateProductCostMining();
        if(!this.CanMining(this.productCostMiningOneTime)) return false;
        ResourcesManager.instance.GetProductStorageByName(this.productCostMiningOneTime.productName).TakeOut(this.productCostMiningOneTime);
        this.time+=1;
        this.UpdateData();
        return true;
    }
    public bool MiningTenTime(){
        this.UpdateProductCostMining();
        if(!this.CanMining(this.productCostMiningTenTime)) return false;
        ResourcesManager.instance.GetProductStorageByName(this.productCostMiningTenTime.productName).TakeOut(this.productCostMiningTenTime);
        this.time+=10;
        this.UpdateData();
        return true;
    }

    public List<ItemData> InstantiateRandomItem(int times){
        List<ItemData> itemDatas = new List<ItemData>();
        for (int i =0; i< times; i++)
        {
            float numberRand = Random.Range(1f,99f);
            foreach (ItemRate itemRate in itemRates)
            {
                if(numberRand <= itemRate.rate){
                    itemDatas.Add(itemRate.ParseToData());
                    break;
                }
                numberRand-=itemRate.rate;
            }
        }
        return itemDatas;
        
    }

    public bool CanMining(ProductStorage productStorage){
        if(ResourcesManager.instance.GetProductStorageByName(productStorage.productName).number < productStorage.number) {
            return false;
        }
        return true;
    }

    public MiningBuildingData ParseToData(){
        this.UpdateData();
        MiningBuildingData miningBuildingData = new MiningBuildingData();
        miningBuildingData.time = this.time;
        return miningBuildingData;
    }
    public void ParseFromData(MiningBuildingData miningBuildingData){
        this.time = miningBuildingData.time;
        this.UpdateData();
    }

    //Get
    public ItemRate GetItemRateByName(ItemName itemName){
        return this.itemRates.Find((itemRate)=> (itemRate.itemName == itemName));
    }

}
