using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarborUI : LoadBehaviour
{
    public BuildingMoneyUI harborBuildingUI;
    public MiningBuildingUI miningBuildingUI;

    public List<BtnTab> btnBuildings;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWarehouseBuildingUI();
        this.LoadSummonBuildingUI();
        this.LoadBtnBuilding();
    }

    protected void LoadWarehouseBuildingUI(){
        this.harborBuildingUI = transform.Find("HarborUI").GetComponent<BuildingMoneyUI>();
    }
    protected void LoadSummonBuildingUI(){
        this.miningBuildingUI = transform.Find("MiningBuildingUI").GetComponent<MiningBuildingUI>();
    }

    protected void LoadBtnBuilding(){
        this.btnBuildings.Clear();
        foreach (Transform trans in transform.Find("Btn"))
        {
            BtnTab btnBuilding = trans.GetComponent<BtnTab>();
            this.btnBuildings.Add(btnBuilding);
        }
    }

    public void ChangeHarborBuildingUI(){
        this.OffAllBtnBuilding();
        this.harborBuildingUI.OnUI(BuildingName.harbor);
        this.miningBuildingUI.OffUI();
        this.btnBuildings[0].OnButton();
    }
    public void ChangeMiningBuildingUI(){
        this.OffAllBtnBuilding();
        this.miningBuildingUI.OnUI();
        this.harborBuildingUI.OffUI();
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
        this.ChangeHarborBuildingUI();
        this.btnBuildings[0].OnButton();
    }

    public void OffUI()
    {
        gameObject.SetActive(false);
    }
}
