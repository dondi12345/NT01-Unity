using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WarehouseBuildingUI : LoadBehaviour
{

    public Warehouse warehouse;
    public TextMeshProUGUI textCostExpand;
    public TextMeshProUGUI textExpandLv;
    public TextMeshProUGUI textMulti;
    public TextMeshProUGUI textSlot;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadText();
    }


    public void LoadText(){
        this.textCostExpand = transform.Find("Panel").Find("TextCostExpand").GetComponent<TextMeshProUGUI>();
        this.textExpandLv = transform.Find("Panel").Find("TextExpandLv").GetComponent<TextMeshProUGUI>();
        this.textMulti = transform.Find("Panel").Find("TextMulti").GetComponent<TextMeshProUGUI>();
        this.textSlot = transform.Find("Panel").Find("TextSlot").GetComponent<TextMeshProUGUI>();
    }


    //Function
    private void LoadWarehouse(){
        this.warehouse = BuildingManager.instance.warehouse;
    }

    public void LoadData(){
        this.LoadWarehouse();
        this.warehouse.UpdateData();

        this.textCostExpand.text = NumberForm.ToString(ItemManager.instance.GetItemByName(ItemName.blueprintWarehouse).number)+"/"+NumberForm.ToString(this.warehouse.costExpand)+"<sprite=0>";
        this.textExpandLv.text ="Lv " + this.warehouse.expandLv.ToString(); 
        this.textMulti.text = "+" + NumberForm.ToString(this.warehouse.multi*100)+"%";
        this.textSlot.text = "+"+this.warehouse.slot.ToString();   
    }

    public void ExpandLv(){
        
        if(this.warehouse.ExpandLv()){
            this.LoadData();
            return;
        } 
        TownUIManager.instance.OnWarningUI(WarningName.dontEnoughResource);
    }

    public void Close(){
        
        this.OffUI();
    }

    public void OnUI(){
        
        transform.gameObject.SetActive(true);
        this.LoadData();
    }

    public void OffUI()
    {
        gameObject.SetActive(false);
    }
}
