using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WarehouseUI : LoadBehaviour
{
    public WarehouseBuildingUI warehouseBuildingUI;
    public SummonBuildingUI summonBuildingUI;

    public List<BtnTab> btnBuildings;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWarehouseBuildingUI();
        this.LoadSummonBuildingUI();
        this.LoadBtnBuilding();
    }

    protected void LoadWarehouseBuildingUI(){
        this.warehouseBuildingUI = transform.Find("WarehouseBuildingUI").GetComponent<WarehouseBuildingUI>();
    }
    protected void LoadSummonBuildingUI(){
        this.summonBuildingUI = transform.Find("SummonBuildingUI").GetComponent<SummonBuildingUI>();
    }

    protected void LoadBtnBuilding(){
        this.btnBuildings.Clear();
        foreach (Transform trans in transform.Find("Btn"))
        {
            BtnTab btnBuilding = trans.GetComponent<BtnTab>();
            this.btnBuildings.Add(btnBuilding);
        }
    }

    public void ChangeWarehouseBuildingUI(){
        this.OffAllBtnBuilding();
        this.warehouseBuildingUI.OnUI();
        this.summonBuildingUI.OffUI();
        this.btnBuildings[0].OnButton();
    }
    public void ChangeSummonBuildingUI(){
        this.OffAllBtnBuilding();
        this.summonBuildingUI.OnUI();
        this.warehouseBuildingUI.OffUI();
        this.btnBuildings[1].OnButton();
    }

    public void OffAllBtnBuilding(){
        foreach (BtnTab btnBuilding in btnBuildings)
        {
            btnBuilding.OffButton();
        }
    }

    public void Close(){
        
        this.OffUI();
    }

    public void OnUI(){
        
        transform.gameObject.SetActive(true);
        this.ChangeWarehouseBuildingUI();
        this.btnBuildings[0].OnButton();
    }

    public void OffUI()
    {
        gameObject.SetActive(false);
        ProductUIManager.instance.OnMoney();
    }
}
