using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.SimpleLocalization;

public class BuildingMoneyUI : LoadBehaviour
{

    public BuildingName buildingName;
    public Building building;
    public Text textNameBuilding;

    public TextMeshProUGUI textCostUpgrade;
    public TextMeshProUGUI textCostExpand;

    public TextMeshProUGUI textLv;
    public TextMeshProUGUI textNextLv;
    public TextMeshProUGUI textExpandLv;
    public TextMeshProUGUI textProductPerS;
    public TextMeshProUGUI textMulti;
    public TextMeshProUGUI textSlot;

    public CheckboxStatus upOne;
    public CheckboxStatus upTen;
    public CheckboxStatus upFifty;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBuildingName();
        this.LoadText();
        this.LoadCheckboxStatus();
    }

    public void LoadBuildingName(){
        string name = transform.name.Substring(0,transform.name.Length-2);
        if(this.buildingName == BuildingName.noBuilding){
            this.buildingName = BuildingNameParser.FromString(name);
        }
    }

    public void LoadText(){
        this.textNameBuilding = transform.Find("Panel").Find("TitleBuilding").Find("TextNameBuilding").GetComponent<Text>();
        this.textCostUpgrade = transform.Find("Panel").Find("TextCostUpgrade").GetComponent<TextMeshProUGUI>();
        this.textCostExpand = transform.Find("Panel").Find("TextCostExpand").GetComponent<TextMeshProUGUI>();
        this.textLv = transform.Find("Panel").Find("TextLv").GetComponent<TextMeshProUGUI>();
        this.textNextLv = transform.Find("Panel").Find("TextNextLv").GetComponent<TextMeshProUGUI>();
        this.textExpandLv = transform.Find("Panel").Find("TextExpandLv").GetComponent<TextMeshProUGUI>();
        this.textProductPerS = transform.Find("Panel").Find("TextProductPerS").GetComponent<TextMeshProUGUI>();
        this.textMulti = transform.Find("Panel").Find("TextMulti").GetComponent<TextMeshProUGUI>();
        this.textSlot = transform.Find("Panel").Find("TextSlot").GetComponent<TextMeshProUGUI>();
    }

    public void LoadCheckboxStatus(){
        this.upOne = transform.Find("Panel").Find("UpLvx1").GetComponent<CheckboxStatus>();
        this.upTen = transform.Find("Panel").Find("UpLvx10").GetComponent<CheckboxStatus>();
        this.upFifty = transform.Find("Panel").Find("UpLvx50").GetComponent<CheckboxStatus>();
    }

    public void LoadBuilding(){
        this.building = BuildingManager.instance.GetBuildingByName(this.buildingName);
    }

    public void UpdateData(){
        this.LoadBuilding();
        this.building.UpdateData();
        this.UpdtaCheckbox();

        // this.textNameBuilding.text =this.building.transform.name;
        this.textNameBuilding.transform.GetComponent<LocalizedText>().LocalizationKey = this.building.LocalizationKey;
        this.textNameBuilding.transform.GetComponent<LocalizedText>().Start();
        
        this.textLv.text ="Lv." + this.building.lv.ToString();
        this.textNextLv.text ="+" + this.building.nextLv.ToString();
        this.textCostUpgrade.text = NumberForm.ToString(this.building.buildingCtrl.buildingUpgrade.productCostUpgrade.number) + "<sprite=0>";
        this.textExpandLv.text ="Lv." + this.building.expandLv.ToString(); 
        this.textCostExpand.text =NumberForm.ToString(ItemManager.instance.GetItemByName(ItemName.blueprintBuilding).number)+"/"+ NumberForm.ToString(this.building.buildingCtrl.buildingUpgrade.costExpand)+"<sprite=0>";
        
        this.textProductPerS.text = NumberForm.MinimunToString(this.building.GetMoneyGainPerS())+"/s"; 
        this.textMulti.text = "+" + this.building.multiExpand*100+"%";  
        this.textSlot.text = "+"+this.building.slot.ToString();  
    }

    public void UpdtaCheckbox(){
        float kindUpgrade = building.buildingCtrl.buildingUpgrade.kindUpgradeLv;
        this.OffAllCheckbox();
        if(kindUpgrade == 0){
            this.OffAllCheckbox();
            this.upOne.Choose();
            return;
        }
        if(kindUpgrade == 1){
            this.upTen.Choose();
            return;
        }
        if(kindUpgrade == 2){
            this.upFifty.Choose();
            return;
        }
    }

    //Function
    public void NextBuilding(){
        int index = BuildingManager.instance.buildings.IndexOf(this.building);
        if(index < 0) return;
        for (int i = index+1; i < BuildingManager.instance.buildings.Count; i++)
        {
            if(BuildingManager.instance.buildings[i].lv > 0){
                this.buildingName = BuildingManager.instance.buildings[i].buildingName;
                this.UpdateData();
                return;
            }
        }

        for (int i = 0; i <= index; i++)
        {
            if(BuildingManager.instance.buildings[i].lv > 0){
                this.buildingName = BuildingManager.instance.buildings[i].buildingName;
                this.UpdateData();
                return;
            }
        }
    }

    public void BackBuilding(){
        int index = BuildingManager.instance.buildings.IndexOf(this.building);
        if(index < 0) return;
        for (int i = index -1; i >= 0; i--)
        {
            if(BuildingManager.instance.buildings[i].lv > 0){
                this.buildingName = BuildingManager.instance.buildings[i].buildingName;
                this.UpdateData();
                return;
            }
        }
        for (int i = BuildingManager.instance.buildings.Count-1; i >= index; i--)
        {
            if(BuildingManager.instance.buildings[i].lv > 0){
                this.buildingName = BuildingManager.instance.buildings[i].buildingName;
                this.UpdateData();
                return;
            }
        }

        
    }

    public void UpLvOneTime(){
        
        this.OffAllCheckbox();
        this.upOne.Choose();
        this.building.UpLvOneTime();
        this.UpdateData();
    }
    public void UpLvTenTime(){
        
        this.OffAllCheckbox();
        this.upTen.Choose();
        this.building.UpLvTenTime();
        this.UpdateData();
    }

    public void UpLvHundredTime(){
        
        this.OffAllCheckbox();
        this.upFifty.Choose();
        this.building.UpLvHundredTime();
        this.UpdateData();
    }

    public void OffAllCheckbox(){
        this.upOne.DontChoose();
        this.upTen.DontChoose();
        this.upFifty.DontChoose();
    }

    public void UpgradeLv(){
        
        if(this.building.UpgradeLv()){
            this.UpdateData();
            return;
        } 
        TownUIManager.instance.OnWarningUI(WarningName.dontEnoughResource);
    }
    public void ExpandLv(){
        
        if(this.building.ExpandLv()){
            this.UpdateData();
            return;
        } 
        TownUIManager.instance.OnWarningUI(WarningName.dontEnoughResource);
    }

    public void Close(){
        
        this.OffUI();
    }

    public void OnUI(BuildingName buildingName){
        
        transform.gameObject.SetActive(true);
        this.buildingName = buildingName;
        this.UpdateData();
    }

    public void OffUI()
    {
        gameObject.SetActive(false);
    }


    //ForTest
    public void UsingItemDayMoney(){
        ItemManager.instance.GetItemByName(ItemName.moneyOneDayItem).UsingItem(1);
    }

}
