using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MiningBuildingUI : LoadBehaviour
{
    public MiningBuilding miningBuilding;

    public TextMeshProUGUI textOneTimeCost;
    public TextMeshProUGUI textTenTimeCost;

    public List<RateItemUI> rateItemUIs;
    

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadText();
        this.LoadRateItemUI();
    }

    public void LoadText(){
        this.textOneTimeCost = transform.Find("Panel").Find("TextOneTimeCost").GetComponent<TextMeshProUGUI>();
        this.textTenTimeCost = transform.Find("Panel").Find("TextTenTimeCost").GetComponent<TextMeshProUGUI>();
    }

    public void LoadRateItemUI(){
        this.rateItemUIs.Clear();
        Transform transRateItem = transform.Find("Panel").Find("RateItem").Find("Content");
        foreach (Transform trans in transRateItem)
        {
            RateItemUI rateItemUI = trans.GetComponent<RateItemUI>();
            if(rateItemUI == null) continue;
            this.rateItemUIs.Add(rateItemUI);
        }
    }
    
    //Function
    public void UpdateData(){
        this.miningBuilding = BuildingManager.instance.miningBuilding;
        this.miningBuilding.UpdateData();
        this.UpdateText();
        this.UpdateItemRate();
    }

    public void UpdateText(){
        this.textOneTimeCost.text = NumberForm.ToString(this.miningBuilding.productCostMiningOneTime.number)+"<sprite=0>";
        this.textTenTimeCost.text = NumberForm.ToString(this.miningBuilding.productCostMiningTenTime.number)+"<sprite=0>";
    }

    public void UpdateItemRate(){
        foreach (RateItemUI rateItemUI in this.rateItemUIs)
        {
            rateItemUI.UpdateData(this.miningBuilding.GetItemRateByName(rateItemUI.itemName));
        }
    }

    public void MiningOneTime(){
        
        this.UpdateData();
        if(this.miningBuilding.MiningOneTime()){
            TownUIManager.instance.OnLoadMiningItemUI(this.miningBuilding.InstantiateRandomItem(1));
            this.UpdateData();
            return;
        }
        TownUIManager.instance.OnWarningUI(WarningName.dontEnoughResource);
    }
    public void MiningTenTime(){
        
        this.UpdateData();
        if(this.miningBuilding.MiningTenTime()){
            TownUIManager.instance.OnLoadMiningItemUI(this.miningBuilding.InstantiateRandomItem(10));
            this.UpdateData();
            return;
        } 
        TownUIManager.instance.OnWarningUI(WarningName.dontEnoughResource);;
    }

    public void Close(){
        
        this.OffUI();
    }

    public void OnUI(){
        
        transform.gameObject.SetActive(true);
        this.UpdateData();
    }

    public void OffUI()
    {
        gameObject.SetActive(false);
    }
}
