using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : LoadBehaviour
{
    public static ItemManager instance;
    public List<Item> items;

    protected override void Awake()
    {
        base.Awake();
        if (ItemManager.instance != null) Debug.LogError("Only 1 ItemManager allow");
        ItemManager.instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadItem();
    }

    public void LoadItem(){
        this.items.Clear();
        foreach (Transform trans in transform)
        {
            foreach (Transform tran in trans)
            {
                Item item = tran.GetComponent<Item>();
                this.items.Add(item);
            }
        }
    }

    public void UpdateData(){
        
    }

    //Function

    public void UsingItem(ItemName itemName, int timeUse){
        switch (itemName)
        {
            case ItemName.moneyOneHourItem:
                this.UsingMoneyItem(1, timeUse);
                break;
            case ItemName.moneyThreeHourItem:
                this.UsingMoneyItem(3, timeUse);
                break;
            case ItemName.moneySixHourItem:
                this.UsingMoneyItem(6, timeUse);
                break;
            case ItemName.moneyTwelveHourItem:
                this.UsingMoneyItem(12, timeUse);
                break;
            case ItemName.moneyOneDayItem:
                this.UsingMoneyItem(24, timeUse);
                break;
            default:
                ItemManager.instance.GetItemByName(itemName).number -= timeUse;
                break;
        }
    }

    public void UsingMoneyItem(int hour,  int timeUse){
        float number = BuildingManager.instance.GetMoneyGainPerS() * hour * 3600 * timeUse;
        ResourcesManager.instance.GetProductStorageByName(ProductName.money).number += number;
    }

    public List<Item> GetItemHave(){
        List<Item> items = new List<Item>();
        foreach (Item item in this.items)
        {
            if(item.number > 0){
                items.Add(item);
            }
        }
        return items;
    }

    public ItemManagerData ParseToData(){
        ItemManagerData itemManagerData = new ItemManagerData();
        itemManagerData.itemDatas = new List<ItemData>();
        foreach (Item item in this.items)
        {
            itemManagerData.itemDatas.Add(item.ParseToData());
        }
        return itemManagerData;
    }

    public void ParseFromData(ItemManagerData itemManagerData){
        if(itemManagerData == null) return;

        foreach (ItemData itemData in itemManagerData.itemDatas)
        {
            this.GetItemByName(itemData.itemName).ParseFromData(itemData);
        }
    }

    public void TakeItem(ItemName itemName, int number){
        switch (itemName)
        {
            case ItemName.oneExperience:
                this.TakeExperienceItem(number*1);
                break;
            case ItemName.oneSoul:
                this.TakeSoulItem(number*1);
                break;
            case ItemName.oneDiamond:
                this.TakeDiamond(number*1);
                break;
            default:
                this.TakeItemBag(itemName, number);
                break;
        }
    }

    public void TakeExperienceItem(int number){
        PlayerManager.instance.experience += number;
    }
    public void TakeSoulItem(int number){
        ResourcesManager.instance.GetProductStorageByName(ProductName.soul).number += number;
    }
    public void TakeDiamond(int number){
        ResourcesManager.instance.GetProductStorageByName(ProductName.diamond).number += number;
    }

    public void TakeItemBag(ItemName itemName, int number){
        try
        {
            this.GetItemByName(itemName).number += number;
        }
        catch (System.Exception)
        {
            Debug.LogWarning("Can't find item: "+itemName);
        }
        
    }

    //Get

    public Item GetItemByName(ItemName itemName){
        return items.Find((item) => (item.itemName == itemName));
    }
}
