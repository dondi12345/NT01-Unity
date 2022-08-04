using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CI.QuickSave;

public class SaveManager : LoadBehaviour
{
    public static SaveManager instance;
    private const string SAVE_1 = "save_1";

    public const string DataWorker = "DataWorker";
    public const string DataBuilding = "DataBuilding";
    public const string DataPlayer = "DataPlayer";
    public const string DataItem = "DataItem";

    public const string DataResource = "DataResource";

    protected override void Awake() {
        if(SaveManager.instance != null) Debug.LogError("Only 1 SaveManager allow");
        SaveManager.instance = this;
    }

    public virtual string GetSaveName(){
        return SaveManager.SAVE_1;
    }
    public virtual string GetSaveName(string dataName){
        return SaveManager.SAVE_1 + dataName;
    }

    public void SaveData(string key, string data){
        // SaveSystem.SetString(key, data);
        QuickSaveWriter.Create(this.GetSaveName()).Write(key, data).Commit();
    }

    public string LoadData(string key){
        // return SaveSystem.GetString(key);
        string data=null;
        QuickSaveReader.Create(this.GetSaveName()).Read<string>(key, (r) => { data = r; });
        return data;
    }

    public virtual void SaveDataWorker(WorkerManagerData workerManagerData){
        string stringJson = JsonUtility.ToJson(workerManagerData);
        this.SaveData(this.GetSaveName(SaveManager.DataWorker), stringJson);
        Debug.Log("Done SaveDataWorker");
    }
    public virtual void SaveDataBuilding(BuildingManagerData buildingManagerData){
        string stringJson = JsonUtility.ToJson(buildingManagerData);
        this.SaveData(this.GetSaveName(SaveManager.DataBuilding), stringJson);
        Debug.Log("Done SaveDataBuilding");
    }
    public virtual void SaveDataPlayer(PlayerData playerData){
        string stringJson = JsonUtility.ToJson(playerData);
        this.SaveData(this.GetSaveName(SaveManager.DataPlayer), stringJson);
        Debug.Log("Done SaveDataPlayer");
    }
    public virtual void SaveResourcesManagerData(ResourcesManagerData resourcesManager){
        string stringJson = JsonUtility.ToJson(resourcesManager);
        this.SaveData(this.GetSaveName(SaveManager.DataResource), stringJson);
        Debug.Log("Done SaveResourcesManagerData");
    }
    public virtual void SaveDataItem(ItemManagerData itemManagerData){
        string stringJson = JsonUtility.ToJson(itemManagerData);
        this.SaveData(this.GetSaveName(SaveManager.DataItem), stringJson);
        Debug.Log("Done SaveDataItem");
    }

    public virtual WorkerManagerData LoadDataWorker(){
        string stringSave = this.LoadData(this.GetSaveName(SaveManager.DataWorker));
        
        WorkerManagerData workerManagerData  = JsonUtility.FromJson<WorkerManagerData>(stringSave);
        Debug.Log("Done LoadDataWorker");
        return workerManagerData;
    }

    public virtual BuildingManagerData LoadDataBuilding(){
        string stringSave = this.LoadData(this.GetSaveName(SaveManager.DataBuilding));
        
        BuildingManagerData buildingManagerData  = JsonUtility.FromJson<BuildingManagerData>(stringSave);
        Debug.Log("Done LoadDataBuilding");
        return buildingManagerData;
    }
    public virtual PlayerData LoadDataPlayer(){
        string stringSave = this.LoadData(this.GetSaveName(SaveManager.DataPlayer));
        
        PlayerData playerData  = JsonUtility.FromJson<PlayerData>(stringSave);
        Debug.Log("Done LoadDataPlayer");
        return playerData;
    }
    public virtual ResourcesManagerData LoadResourcesManagerData(){
        string stringSave = this.LoadData(this.GetSaveName(SaveManager.DataResource));
        
        ResourcesManagerData resourcesManagerData  = JsonUtility.FromJson<ResourcesManagerData>(stringSave);
        Debug.Log("Done LoadResourcesManagerData");
        return resourcesManagerData;
    }

    public virtual ItemManagerData LoadDataItem(){
        string stringSave = this.LoadData(this.GetSaveName(SaveManager.DataItem));
        
        ItemManagerData itemManagerData  = JsonUtility.FromJson<ItemManagerData>(stringSave);
        Debug.Log("Done LoadDataWorker");
        return itemManagerData;
    }

    
}
