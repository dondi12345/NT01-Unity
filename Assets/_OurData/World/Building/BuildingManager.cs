using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : LoadBehaviour
{
    public static BuildingManager instance;
    //Special Building
    public Warehouse warehouse;
    public MiningBuilding miningBuilding;
    public SummonBuilding summonBuilding;

    //MoneyBuilding
    public List<Building> buildings;
    
    //For building create money
    public float createDelay = 2f;

    public string lastTime;
    public float maxTimeOffline = 28800;

    protected override void Awake()
    {
        base.Awake();
        if (BuildingManager.instance != null) Debug.LogError("Only 1 BuildingManager allow");
        BuildingManager.instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWarehouse();
        this.LoadMiningBuilding();
        this.LoadSummonBuilding();
        this.LoadBuilding();
    }

    public virtual void LoadWarehouse(){
        if (this.warehouse != null) return;
   
        Warehouse warehouse = transform.Find("SpecialBuilding").Find("Warehouse").GetComponent<Warehouse>();
        if(warehouse == null) {
            Debug.Log(transform.name + ": Can't LoadWarehouse", gameObject);
            return;
        };

        this.warehouse = warehouse;

        Debug.Log(transform.name + ": LoadWarehouse", gameObject);
    }

    public virtual void LoadMiningBuilding(){
        if (this.miningBuilding != null) return;
   
        MiningBuilding miningBuilding = transform.Find("SpecialBuilding").Find("MiningBuilding").GetComponent<MiningBuilding>();
        if(miningBuilding == null) {
            Debug.Log(transform.name + ": Can't LoadMiningBuilding", gameObject);
            return;
        };

        this.miningBuilding = miningBuilding;

        Debug.Log(transform.name + ": LoadMiningBuilding", gameObject);
    }

    public virtual void LoadSummonBuilding(){
        if (this.summonBuilding != null) return;
   
        SummonBuilding summonBuilding = transform.Find("SpecialBuilding").Find("SummonBuilding").GetComponent<SummonBuilding>();
        if(summonBuilding == null) {
            Debug.Log(transform.name + ": Can't LoadSummonBuilding", gameObject);
            return;
        };

        this.summonBuilding = summonBuilding;

        Debug.Log(transform.name + ": LoadSummonBuilding", gameObject);
    }

    public virtual void LoadBuilding(){
        if (this.buildings.Count > 0) buildings.Clear();

        foreach (Transform tras in transform.Find("MoneyBuilding"))
        {
            Building building = tras.GetComponent<Building>();
            if (building == null) continue;
            this.buildings.Add(building);
        }

        Debug.Log(transform.name + ": LoadBuilding", gameObject);
    }

    public void UpdateData(){
        foreach (Building building in this.buildings)
        {
            building.UpdateData();
        }

        this.warehouse.UpdateData();
        this.miningBuilding.UpdateData();
    }

    public BuildingManagerData ParseToData(){
        this.BuildingResetNewDay();
        this.UpdateData();
        BuildingManagerData buildingManagerData = new BuildingManagerData();
        buildingManagerData.buildingDatas = new List<BuildingData>();
        this.LoadBuilding();
        foreach (Building building in this.buildings)
        {
            buildingManagerData.buildingDatas.Add(building.ParseToData());
        }

        buildingManagerData.warehouseData = this.warehouse.ParseToData();
        buildingManagerData.miningBuildingData = this.miningBuilding.ParseToData();
        return buildingManagerData;
    }

    public void ParseFromData(BuildingManagerData buildingManagerData){
        if(buildingManagerData == null) return;

        foreach (BuildingData buildingData in buildingManagerData.buildingDatas)
        {
            Building building = this.GetBuildingByName(buildingData.buildingName);
            building.ParseFromData(buildingData);
        }

        this.warehouse.ParseFromData(buildingManagerData.warehouseData);
        this.miningBuilding.ParseFromData(buildingManagerData.miningBuildingData);

        if(PlayerManager.instance.lastTimeInTown == null) return;

        try
        {
            TimeSpan timeSpan = DateTime.Now.Subtract(DateTime.Parse(PlayerManager.instance.lastTimeInTown));
            this.CountMoneyOffline((float)timeSpan.TotalSeconds);
        }
        catch (System.Exception)
        {
            Debug.LogWarning("Can't CountMoneyOffline");
        }

        this.BuildingResetNewDay();
        this.UpdateData();
    }

    public void CountMoneyOffline(float time){
        if(time < 0) return;
        if(time > this.maxTimeOffline) time = this.maxTimeOffline;

        float number = time * this.GetMoneyGainPerS();
        foreach (Building building in buildings)
        {
            building.CountMoneyOffline(time);
        }
    }

    public void BuildingResetNewDay(){
        if(!PlayerManager.instance.CheckNewDay(PlayerManager.instance.lastTimeInTown)) return;

        this.miningBuilding.ResetTimeMining();
    }

    //Get

    public Building GetBuildingByName(BuildingName buildingName){
        return this.buildings.Find((building) => building.buildingName == buildingName);
    }

    public float GetMoneyGainPerS(){
        float number = 0;
        foreach (Building building in BuildingManager.instance.buildings)
        {
            building.UpdateData();
            if(building.lv == 0) continue;

            number += building.GetMoneyGainPerS();
        }
        return number;
    }
}
