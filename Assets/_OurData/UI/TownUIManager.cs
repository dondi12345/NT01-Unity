using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownUIManager : LoadBehaviour
{

    public static TownUIManager instance;

    public int currentShowLv = 0;

    public BaseShopUI baseShopUI;

    public BuildingMoneyUI buildingUI;
    public WarehouseUI warehouseUI;
    public HarborUI harborUI;

    public WorkerBagUI workerBagUI;
    public ItemBagUI itemBagUI;

    public WorkerInfoUI workerInfoUI;
    public WorkerMaterialUI workerMaterialUI;

    public UseItemUI useItemUI;
    public ItemShopInfoUI itemShopInfoUI;
    public MiningItemUI miningItemUI;
    public SummonWorkerUI summonWorkerUI;
    public ProfilePlayerUI profilePlayerUI;
    public InfoSkillUI infoSkillUI;

    public List<WarningUI> warningUIs;
    public BuyItemWarning buyItemWarning;

    protected override void Start(){
        this.OffAllUI();
    }
    
    protected override void Awake()
    {
        base.Awake();
        if (TownUIManager.instance != null) Debug.LogError("Only 1 UIManager allow");
        TownUIManager.instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBaseShopUI();
        this.LoadBuildingUI();
        this.LoadWarehouseUI();
        this.LoadHarborUI();

        this.LoadWorkerBagUI();
        this.LoadItemBagUI();

        this.LoadWarningUI();
        this.LoadBuyItemWarning();

        this.LoadWorkerInfoUI();
        this.LoadWorkerMaterialUI();

        this.LoadUseItemUI();
        this.LoadItemShopInfoUI();
        this.LoadMiningItemUI();
        this.LoadSummonWorkerUI();
        this.LoadInfoSkillUI();
        this.LoadProfilePlayerUI();
    }

    public void LoadBaseShopUI(){
        Transform transBaseShopUI = transform.Find("Canvas").Find("ShopUI").Find("BaseShopUI");
        BaseShopUI baseShopUI = transBaseShopUI.GetComponent<BaseShopUI>();
        this.baseShopUI = baseShopUI;                                               

        Debug.Log(transform.name + ": LoadBaseShopUI", gameObject);
    }
    public void LoadBuildingUI(){
        Transform transBuildingUIs = transform.Find("Canvas").Find("BuildingUI").Find("DefaultBuildingUI");
        BuildingMoneyUI buildingUI = transBuildingUIs.GetComponent<BuildingMoneyUI>();
        if (buildingUI == null){
            Debug.Log(transform.name + ": Can't LoadBuildingUI", gameObject);
            return;
        }
        this.buildingUI = buildingUI;                                               

        Debug.Log(transform.name + ": LoadBuildingUI", gameObject);
    }

    public void LoadWarehouseUI(){
        Transform transWarehouseUI = transform.Find("Canvas").Find("BuildingUI").Find("WarehouseUI");
        WarehouseUI warehouseUI = transWarehouseUI.GetComponent<WarehouseUI>();
        if (warehouseUI == null){
            Debug.Log(transform.name + ": Can't LoadWarehouseUI", gameObject);
            return;
        }
        this.warehouseUI = warehouseUI;

        Debug.Log(transform.name + ": LoadWarehouseUI", gameObject);
    }
    public void LoadHarborUI(){
        Transform transHarborUI = transform.Find("Canvas").Find("BuildingUI").Find("HarborUI");
        HarborUI harborUI = transHarborUI.GetComponent<HarborUI>();
        if (harborUI == null){
            Debug.Log(transform.name + ": Can't LoadHarborUI", gameObject);
            return;
        }
        this.harborUI = harborUI;

        Debug.Log(transform.name + ": LoadHarborUI", gameObject);
    }

    public void LoadWarningUI(){
        Transform transWarningUIs = transform.Find("Canvas").Find("WarningUI");    
        foreach (Transform tras in transWarningUIs)
        {
            WarningUI warningUI = tras.GetComponent<WarningUI>();
            if (warningUI == null) continue;
            this.warningUIs.Add(warningUI);
        }

        Debug.Log(transform.name + ": LoadBuilding", gameObject);
    }
    public void LoadBuyItemWarning(){
        Transform transBuyItemWarning = transform.Find("Canvas").Find("WarningUI").Find("BuyItemWarning");
        this.buyItemWarning = transBuyItemWarning.GetComponent<BuyItemWarning>();    

        Debug.Log(transform.name + ": LoadBuyItemWarning", gameObject);
    }
    public void LoadUseItemUI(){
        Transform transWarningUIs = transform.Find("Canvas").Find("PopupUI");    
        foreach (Transform tras in transWarningUIs)
        {
            UseItemUI useItemUI = tras.GetComponent<UseItemUI>();
            if (useItemUI == null) continue;
            this.useItemUI = useItemUI;
            break;
        }

        Debug.Log(transform.name + ": LoadBuilding", gameObject);
    }
    public void LoadItemShopInfoUI(){
        ItemShopInfoUI itemShopInfoUI = transform.Find("Canvas").Find("PopupUI").Find("ItemShopInfoUI").GetComponent<ItemShopInfoUI>();
        if (itemShopInfoUI == null) return;
        this.itemShopInfoUI = itemShopInfoUI;
        Debug.Log(transform.name + ": LoadItemShopInfoUI", gameObject);
    }

    public void LoadMiningItemUI(){
        Transform transMiningItemUI = transform.Find("Canvas").Find("PopupUI").Find("MiningItemUI");    
        if(transMiningItemUI == null){
            Debug.LogWarning("Can't LoadMiningItemUI");
            return;
        }
        this.miningItemUI = transMiningItemUI.GetComponent<MiningItemUI>();

        Debug.Log(transform.name + ": LoadMiningItemUI", gameObject);
    }
    public void LoadSummonWorkerUI(){
        Transform transSummonWorkerUI = transform.Find("Canvas").Find("PopupUI").Find("SummonWorkerUI");    
        if(transSummonWorkerUI == null){
            Debug.LogWarning("Can't LoadSummonWorkerUI");
            return;
        }
        this.summonWorkerUI = transSummonWorkerUI.GetComponent<SummonWorkerUI>();

        Debug.Log(transform.name + ": LoadSummonWorkerUI", gameObject);
    }
    public void LoadWorkerInfoUI(){
        Transform transWarningUIs = transform.Find("Canvas").Find("PopupUI");    
        foreach (Transform tras in transWarningUIs)
        {
            WorkerInfoUI workerInfoUI = tras.GetComponent<WorkerInfoUI>();
            if (workerInfoUI == null) continue;
            this.workerInfoUI = workerInfoUI;
            break;
        }

        Debug.Log(transform.name + ": LoadBuilding", gameObject);
    }
    public void LoadWorkerMaterialUI(){
        Transform transWarningUIs = transform.Find("Canvas").Find("PopupUI");    
        foreach (Transform tras in transWarningUIs)
        {
            WorkerMaterialUI workerMaterialUI = tras.GetComponent<WorkerMaterialUI>();
            if (workerMaterialUI == null) continue;
            this.workerMaterialUI = workerMaterialUI;
            break;
        }

        Debug.Log(transform.name + ": LoadBuilding", gameObject);
    }
    
    public void LoadWorkerBagUI(){
        WorkerBagUI workerBagUI = transform.Find("Canvas").Find("BagUI").Find("WorkerBagUI").GetComponent<WorkerBagUI>();
        if (workerBagUI == null){
            Debug.Log(transform.name + ": Can't LoadWorkerBagUI", gameObject);
            return;
        }
        this.workerBagUI = workerBagUI;

        Debug.Log(transform.name + ": LoadWorkerBagUI", gameObject);
    }
    public void LoadItemBagUI(){
        ItemBagUI itemBagUI = transform.Find("Canvas").Find("BagUI").Find("ItemBagUI").GetComponent<ItemBagUI>();
        if (itemBagUI == null){
            Debug.Log(transform.name + ": Can't LoadItemBagUI", gameObject);
            return;
        }
        this.itemBagUI = itemBagUI;

        Debug.Log(transform.name + ": LoadItemBagUI", gameObject);
    }

    public void LoadProfilePlayerUI(){
        this.profilePlayerUI = transform.Find("Canvas").Find("PlayerUI").Find("ProfilePlayerUI").GetComponent<ProfilePlayerUI>();
    }
    public void LoadInfoSkillUI(){
        this.infoSkillUI = transform.Find("Canvas").Find("PopupUI").Find("InfoSkillUI").GetComponent<InfoSkillUI>();
    }

    //Function

    public bool isUIActive(){

        if(TownTutorialManager.instance.onTutorial) return false;
        
        if(this.baseShopUI.gameObject.activeSelf) return false;
        
        if(this.buildingUI.gameObject.activeSelf) return false;
        if(this.warehouseUI.gameObject.activeSelf) return false;
        if(this.harborUI.gameObject.activeSelf) return false;

        if(this.workerBagUI.gameObject.activeSelf) return false;
        if(this.itemBagUI.gameObject.activeSelf) return false;
        if(this.profilePlayerUI.gameObject.activeSelf) return false;

        if(this.useItemUI.gameObject.activeSelf) return false;

        foreach (WarningUI warningUI in this.warningUIs)
        {
            if(warningUI.gameObject.activeSelf) return false;
        }

        return true;
    }

    public void OnBuildingUI(BuildingName buildingName){  
        this.buildingUI.OnUI(buildingName);
    }
    public void OnWarehouseUI(){
        this.warehouseUI.OnUI();
    }
    public void OnHarborUI(){
        this.harborUI.OnUI();
    }
    public void OnWarningUI(WarningName warningName){
        this.warningUIs.Find((warningUI) => (warningUI.warningName == warningName)).OnUI();
    }

    public void OnUseItemUI(ItemIcon itemIcon){
        this.useItemUI.OnUI(itemIcon);
    }
    public void OnWorkerInfoUI(WorkerIcon workerIcon){
        this.workerInfoUI.OnUI(workerIcon);
    }

    public void OnLoadMiningItemUI(List<ItemData> itemDatas){
        this.miningItemUI.OnUI(itemDatas);
    }
    public void OnLoadSummonWorkerUI(List<Worker> workers){
        this.summonWorkerUI.OnUI(workers);
    }
    public void OnBaseShopUI(){
        this.baseShopUI.OnUI();
    }

    public void OffAllUI(){
        this.baseShopUI.OffUI();
        this.buildingUI.OffUI();
        this.warehouseUI.OffUI();
        this.harborUI.OffUI();

        this.workerBagUI.OffUI();
        this.itemBagUI.OffUI();

        this.useItemUI.OffUI();
        this.workerInfoUI.OffUI();
        this.miningItemUI.OffUI();
        this.summonWorkerUI.OffUI();
        this.profilePlayerUI.OffUI();

        foreach (WarningUI warningUI in this.warningUIs)
        {
            warningUI.OffUI();
        }
    }

    public WarningUI GetWarningUIByName(WarningName warningName){
        return this.warningUIs.Find((warningUI) => (warningUI.warningName == warningName));
    }

}
