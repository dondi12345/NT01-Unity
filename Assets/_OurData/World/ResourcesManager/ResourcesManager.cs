using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager : LoadBehaviour
{
    public List<ProductStorage> productStorages;

    public static ResourcesManager instance;
    protected override void Awake()
    {
        base.Awake();
        if (ResourcesManager.instance != null) Debug.LogError(transform.name+" Only 1 ResourcesManager allow");
        ResourcesManager.instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadProductStorage();
    }

    public virtual void LoadProductStorage()
    {
        this.productStorages.Clear();
        Transform transProductStorage = transform.Find("ProductStorage");
        try
        {
            foreach(Transform trans in transProductStorage){
                ProductStorage productStorage = trans.GetComponent<ProductStorage>();
                if(productStorage != null){
                    this.productStorages.Add(productStorage);
                }
            }
        }
        catch (System.Exception)
        {
            Debug.LogWarning(transform.name + ": Not find LoadProductStorage", gameObject);
        }

        Debug.Log(transform.name + ": LoadProductStorage", gameObject);
    }

    public void UpdateData(){

    }

    //Get
    public ProductStorage GetProductStorageByName(ProductName productName){
        return this.productStorages.Find(productStorage => productStorage.productName == productName);
    }

    public ResourcesManagerData ParseToData(){
        this.UpdateData();
        ResourcesManagerData resourcesManagerData = new ResourcesManagerData();

        List<ProductStorageData> storageDatas = new List<ProductStorageData>();
        foreach (ProductStorage productStorage in this.productStorages)
        {
            storageDatas.Add(productStorage.ParseToData());
        }
        resourcesManagerData.productStorageDatas = storageDatas;
        return resourcesManagerData;
    }
    public void ParseFromData(ResourcesManagerData resourcesManagerData){
        foreach (ProductStorageData productStorageData in resourcesManagerData.productStorageDatas)
        {
            this.GetProductStorageByName(productStorageData.productName).ParseFromData(productStorageData);
        }
        this.UpdateData();
    }
}
