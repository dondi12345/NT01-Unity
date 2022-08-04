using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SummonBuildingUI : LoadBehaviour
{
    public SummonBuilding summonBuilding;

    public Text textOneTimeCost;
    public Text textTenTimeCost;

    public TextMeshProUGUI textNumberWorker;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadText();
    }

    public void LoadText(){
        this.textOneTimeCost = transform.Find("Panel").Find("TextOneTimeCost").GetComponent<Text>();
        this.textTenTimeCost = transform.Find("Panel").Find("TextTenTimeCost").GetComponent<Text>();
        this.textNumberWorker = transform.Find("Panel").Find("TextMeshNumberWorker").GetComponent<TextMeshProUGUI>();
    }

    //Function
    public void LoadData(){
        this.summonBuilding = BuildingManager.instance.summonBuilding;
        this.summonBuilding.UpdateData();
        this.UpdateText();
    }

    public void UpdateText(){
        this.textOneTimeCost.text = this.summonBuilding.costOneSummon.ToString();
        this.textTenTimeCost.text = this.summonBuilding.costTenSummon.ToString();
        this.textNumberWorker.text = WorkerManager.instance.workers.Count.ToString() +"/"+ WorkerManager.instance.GetSlot();
    }

    public void SummonOneTime(){
        
        this.LoadData();
        if(this.summonBuilding.SummonOneTime()){
            this.LoadData();
            TownUIManager.instance.OnLoadSummonWorkerUI(this.summonBuilding.InstantiateRandomWorker(1));
        }
    }
    public void SummonTenTime(){
        
        this.LoadData();
        if(this.summonBuilding.SummonTenTime()){
            TownUIManager.instance.OnLoadSummonWorkerUI(this.summonBuilding.InstantiateRandomWorker(10));
            this.LoadData();
        } 
    }

    public void Close(){
        
        this.OffUI();
    }

    public void OnUI(){
        
        transform.gameObject.SetActive(true);
        this.LoadData();
        ProductUIManager.instance.OnDiceItem();
    }

    public void OffUI()
    {
        gameObject.SetActive(false);
        ProductUIManager.instance.OffDiceItem();
    }
}
