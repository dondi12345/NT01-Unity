using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonBuilding : LoadBehaviour
{
    public int costOneSummon = 1;
    public int costTenSummon = 10;

    protected override void LoadComponents()
    {
        base.LoadComponents();
    }

    //Fuction
    public void UpdateData(){
        
    }

    public bool SummonOneTime(){
        Item item = ItemManager.instance.GetItemByName(ItemName.diceItem);

        if(item.number < 1) {
            TownUIManager.instance.OnWarningUI(WarningName.dontEnoughResource);
            return false;
        }
        if(WorkerManager.instance.GetSlot() < WorkerManager.instance.workers.Count +1){
            TownUIManager.instance.OnWarningUI(WarningName.bagFull);
            return false;
        }

        ItemManager.instance.UsingItem(ItemName.diceItem, 1);
        return true;
    }
    public bool SummonTenTime(){
        Item item = ItemManager.instance.GetItemByName(ItemName.diceItem);

        if(item.number < 10){
            TownUIManager.instance.OnWarningUI(WarningName.dontEnoughResource);
            return false;
        }
        if(WorkerManager.instance.GetSlot() <  WorkerManager.instance.workers.Count + 10){
            TownUIManager.instance.OnWarningUI(WarningName.bagFull);
            return false;
        }

        ItemManager.instance.UsingItem(ItemName.diceItem, 10);
        return true;
    }

    public List<Worker> InstantiateRandomWorker(int times){
        List<GameObject> listCollectionWorkerReleased = WorkerManager.instance.GetColectionWorkerReleased();
        List<Worker> workers = new List<Worker>();
        for (int i =0; i< times; i++)
        {
            GameObject workerG = listCollectionWorkerReleased[Random.Range(0, listCollectionWorkerReleased.Count)];
            Worker worker = Instantiate<GameObject>(workerG).transform.GetComponent<Worker>();
            
            worker.OnGameObject();
            worker.transform.position = BuildingManager.instance.summonBuilding.transform.position;
            // worker.lv = BuildingManager.instance.warehouse.expandLv;

            workers.Add(worker);
            worker.transform.SetParent(WorkerManager.instance.transform.Find("Workers"));
            worker.AwakeWorker();
        }
        WorkerManager.instance.UpdateData();

        return workers;
        
    }
}
