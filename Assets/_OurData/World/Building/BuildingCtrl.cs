using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCtrl : LoadBehaviour
{
    public Building building;
    public BuildingProduce buildingProduce;
    public BuildingUpgrade buildingUpgrade;
    public BuildingModel buildingModel;

    public NameBuildingUI nameBuildingUI;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBuilding();
        this.LoadBuildingProduce();
        this.LoadBuildingUpgrade();
        this.LoadNameBuildingUI();
        this.LoadModelLv();
    }

    //Load

    protected virtual void LoadBuilding()
    {
        if (this.building != null) return;
        this.building = GetComponent<Building>();
        Debug.Log(transform.name + " LoadBuilding", gameObject);
    }

    protected virtual void LoadBuildingProduce()
    {
        if (this.buildingProduce != null) return;
        this.buildingProduce = transform.Find("BuildingProduce").GetComponent<BuildingProduce  >();
        Debug.Log(transform.name + " LoadBuildingProduce", gameObject);
    }
    protected virtual void LoadBuildingUpgrade()
    {
        if (this.buildingUpgrade != null) return;
        this.buildingUpgrade = transform.Find("BuildingUpgrade").GetComponent<BuildingUpgrade>();
        Debug.Log(transform.name + " LoadBuildingUpgrade", gameObject);
    }
    protected virtual void LoadNameBuildingUI(){
        this.nameBuildingUI = transform.Find("NameBuildingUI").GetComponent<NameBuildingUI>();
    }
    protected virtual void LoadModelLv(){
        this.buildingModel = transform.Find("Model").GetComponent<BuildingModel>();
        Debug.Log(transform.name + " LoadModelLv", gameObject);
    }

}
