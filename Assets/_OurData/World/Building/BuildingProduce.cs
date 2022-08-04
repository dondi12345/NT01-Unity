using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingProduce : LoadBehaviour
{
    [SerializeField] protected float createTimer = 0f;
    public float createDelay = 2f;

    public float baseGainProduct = 0.2f;

    public BuildingCtrl buildingCtrl;
    public ProductStorage productCreated;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBuildingCtrl();
        this.LoadProductCreated();
    }

    protected virtual void LoadBuildingCtrl()
    {
        if (this.buildingCtrl != null) return;
        this.buildingCtrl = transform.parent.GetComponent<BuildingCtrl>();
        Debug.Log(transform.name + ": LoadBuildingCtrl", gameObject);
    }

    public virtual void LoadProductCreated()
    {
        try
        {
            Transform transProductCreated = transform.Find("ProductCreated");
            foreach(Transform trans in transProductCreated){
                ProductStorage productCreated = trans.GetComponent<ProductStorage>();
                if(productCreated != null){
                    this.productCreated = productCreated;
                    break;
                }
            }
        }
        catch (System.Exception)
        {
            Debug.LogWarning(transform.name + ": Not find LoadProductCreated", gameObject);
        }

        Debug.Log(transform.name + ": LoadProductCreated", gameObject);
    }

    protected override void FixedUpdate()
    {
        this.CreatingProduct();
    }

    //Function

    public void UpdateData(){
        this.createDelay = BuildingManager.instance.createDelay;
        this.UpdateGainProduction();
    }

    public void UpdateGainProduction(){
        this.buildingCtrl.building.LoadMulti();
        this.buildingCtrl.buildingProduce.productCreated.number = this.baseGainProduct * this.buildingCtrl.building.lv * this.buildingCtrl.building.multiTotal;
    }

    protected virtual void CreatingProduct()
    {
        this.createTimer += Time.fixedDeltaTime;

        if (this.createTimer < this.createDelay) return;
        this.createTimer = 0;
        this.UpdateData();
        this.UpdateGainProduction();
        this.buildingCtrl.building.productStorage.AddProduct(this.productCreated);
    }
}
