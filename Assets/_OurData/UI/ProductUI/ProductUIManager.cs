using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductUIManager : LoadBehaviour
{
    public static ProductUIManager instance;

    public ProductUI money;
    public ProductUI soul;
    public ProductUI diamond;

    public ProductItemUI diceItem;

    protected override void Awake()
    {
        base.Awake();
        if (ProductUIManager.instance != null) Debug.LogError("Only 1 ProductUIManager allow");
        ProductUIManager.instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMoney();
        this.LoadSoul();
        this.LoadDiamond();
        this.LoadDiceItem();
    }

    protected void LoadMoney(){
        this.money = transform.Find("Other").Find("Money").GetComponent<ProductUI>();
    }
    protected void LoadSoul(){
        this.soul = transform.Find("Other").Find("Soul").GetComponent<ProductUI>();
    }
    protected void LoadDiceItem(){
        this.diceItem = transform.Find("Other").Find("DiceItem").GetComponent<ProductItemUI>();
    }
    protected void LoadDiamond(){
        this.diamond = transform.Find("Diamond").GetComponent<ProductUI>();
    }

    public void UpdateData(){

    }

    public void OnSoul(){
        this.money.gameObject.SetActive(false);
        this.soul.gameObject.SetActive(true);
    }
    public void OffSoul(){
        this.money.gameObject.SetActive(true);
        this.soul.gameObject.SetActive(false);
    }
    public void OnDiceItem(){
        this.money.gameObject.SetActive(false);
        this.diceItem.gameObject.SetActive(true);
    }
    public void OffDiceItem(){
        this.money.gameObject.SetActive(true);
        this.diceItem.gameObject.SetActive(false);
    }
    public void OnMoney(){
        this.OffSoul();
        this.OffDiceItem();
    }
}
