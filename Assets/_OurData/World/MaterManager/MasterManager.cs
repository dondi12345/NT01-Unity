using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MasterManager : LoadBehaviour
{
    public const string TOWN_SCENE = "TownScene";
    public const string BATTLE_SCENE = "BattleScene";

    public static MasterManager instance;

    public string currentSceneName = "";

    protected override void Awake() {
        if(MasterManager.instance != null) Debug.LogError("Only 1 MasterManager allow");
        MasterManager.instance = this;
    }

    protected override void Start()
    {
        base.Start();
        this.currentSceneName = SceneManager.GetActiveScene().name;
        try
        {
            SceneLoadManager.instance.animator.SetBool("cover", false);
        }
        catch (System.Exception){}  
        InvokeRepeating("SaveData",5f,5f);
        this.LoadData();
    }

    private void OnApplicationQuit() {
        this.SaveData();
    }

    public IEnumerator LoadScene(string nameScene){

        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene(nameScene);
    }

    public void GoToBattle(){
        
        try
        {
            SceneLoadManager.instance.animator.SetBool("cover", true);
        }
        catch (System.Exception){}

        this.SaveData();
        StartCoroutine(LoadScene(MasterManager.BATTLE_SCENE));
    }

    public void GoToTown(){
        
        try
        {
            SceneLoadManager.instance.animator.SetBool("cover", true);
        }
        catch (System.Exception){}

        this.SaveData();
        StartCoroutine(this.LoadScene(MasterManager.TOWN_SCENE));
    }

    public void LoadData(){
        Debug.Log("Load DataGame...");
        switch (this.currentSceneName)
        {
            case MasterManager.TOWN_SCENE:
                this.LoadDataTownScene();
                break;
            case MasterManager.BATTLE_SCENE:
                this.LoadDataBattleScene();
                break;
        }
        Debug.Log("Done LoadDataGame");
    }
    public void SaveData(){
        Debug.Log("Save DataGame...");
        // return;
        switch (this.currentSceneName)
        {
            case MasterManager.TOWN_SCENE:
                this.SaveDataTownScene();
                break;
            case MasterManager.BATTLE_SCENE:
                this.SaveDataBattleScene();
                break;
        }
        Debug.Log("Done SaveDataGame");
    }

    public void LoadDataTownScene(){
        this.LoadDataPlayer();     
        this.LoadWorkerManagerData();
        this.LoadBuildingManagerData();
        this.LoadItemManagerData();
        this.LoadResourcesManagerData();

        this.SaveDataTownScene();
    }

    public void SaveDataTownScene(){
        this.SaveWorkerManagerData();
        this.SaveBuildingManagerData();
        this.SaveItemManagerData();
        this.SaveResourcesManagerData();
        this.SaveDataPlayer();

    }
    public void LoadDataBattleScene(){
        this.LoadDataPlayer();
        this.LoadWorkerManagerData();
        this.LoadItemManagerData();
        this.LoadResourcesManagerData();

        BattleManager.instance.UpdateData();
        this.SaveDataBattleScene();
    }

    public void SaveDataBattleScene(){
        this.SaveDataPlayer();
        this.SaveWorkerManagerData();
        this.SaveItemManagerData();
        this.SaveResourcesManagerData();
    }

    public void SaveDataPlayer(){
        PlayerManager.instance.UpdateData();
        PlayerData playerData = PlayerManager.instance.ParseToData();
        
        SaveManager.instance.SaveDataPlayer(playerData);
    }
    public void LoadDataPlayer(){
        PlayerData playerData = new PlayerData();
        try
        {
            playerData = SaveManager.instance.LoadDataPlayer();
        }
        catch (System.Exception){}
        PlayerManager.instance.ParseFromData(playerData);
    }

    public void SaveWorkerManagerData(){
        if(this.currentSceneName.Equals(MasterManager.TOWN_SCENE)){
            WorkerManager.instance.UpdateData();
            WorkerManagerData workerManagerData = WorkerManager.instance.ParseToData();
            SaveManager.instance.SaveDataWorker(workerManagerData);
        }

        if(this.currentSceneName.Equals(MasterManager.BATTLE_SCENE)){
            WorkerManagerData workerManagerData = SoldierManager.instance.ParseAllyToData();
            SaveManager.instance.SaveDataWorker(workerManagerData);
        }
        
    }
    public void LoadWorkerManagerData(){
        WorkerManagerData workerManagerData = new WorkerManagerData();
        try
        {
            workerManagerData  = SaveManager.instance.LoadDataWorker();
        }
        catch (System.Exception){}

        if(this.currentSceneName.Equals(MasterManager.TOWN_SCENE)){
            WorkerManager.instance.ParseFromData(workerManagerData);
        }

        if(this.currentSceneName.Equals(MasterManager.BATTLE_SCENE)){
            SoldierManager.instance.ParseAllyFromData(workerManagerData);
        }
        
    }

    public void SaveBuildingManagerData(){
        BuildingManager.instance.UpdateData();
        BuildingManagerData buildingManagerData = BuildingManager.instance.ParseToData();
        SaveManager.instance.SaveDataBuilding(buildingManagerData);
    }
    public void LoadBuildingManagerData(){
        BuildingManagerData buildingManagerData  = new BuildingManagerData();
        try
        {
            buildingManagerData  = SaveManager.instance.LoadDataBuilding();
        }
        catch (System.Exception){}

        BuildingManager.instance.ParseFromData(buildingManagerData);
    }

    public void SaveItemManagerData(){
        ItemManager.instance.UpdateData();
        ItemManagerData itemManagerData = ItemManager.instance.ParseToData();
        SaveManager.instance.SaveDataItem(itemManagerData);
    }
    public void LoadItemManagerData(){
        ItemManagerData itemManagerData = new ItemManagerData();
        try
        {
            itemManagerData = SaveManager.instance.LoadDataItem();
        }
        catch (System.Exception){}

        ItemManager.instance.ParseFromData(itemManagerData);
    }

    public void SaveResourcesManagerData(){
        ResourcesManager.instance.UpdateData();
        ResourcesManagerData resourcesManagerData = ResourcesManager.instance.ParseToData();
        SaveManager.instance.SaveResourcesManagerData(resourcesManagerData);
    }
    public void LoadResourcesManagerData(){
        ResourcesManagerData resourcesManagerData = new ResourcesManagerData();
        try
        {
            resourcesManagerData = SaveManager.instance.LoadResourcesManagerData();
        }
        catch (System.Exception){}
        
        ResourcesManager.instance.ParseFromData(resourcesManagerData);
    } 

}
