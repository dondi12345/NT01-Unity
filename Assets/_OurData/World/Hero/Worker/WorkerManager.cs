using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerManager : LoadBehaviour
{
    public static WorkerManager instance;

    public List<Worker> workers;
    public int numberWorkerWorking = 0;
    
    public int maxWorkerVisible = 50;

    //Colections Worker
    public List<GameObject> colectionWorker;

    //For nav mesh
    public float workerRunSpeed = 1.5f;

    protected override void Awake()
    {
        base.Awake();
        if (WorkerManager.instance != null) Debug.LogError("Only 1 WorkerManager allow");
        WorkerManager.instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWorker();
        this.LoadColectionsWorker();
        this.SortWorkerByPower();
    }

    public void LoadWorker(){
        this.workers.Clear();
        Transform transWorkers = transform.Find("Workers");

        foreach (Transform trans in transWorkers)
        {
            Worker worker = trans.GetComponent<Worker>();
            if(worker == null) continue;
            if(worker.gone){
                worker.gameObject.SetActive(false);
                worker.DestroyWorker();
                continue;
            }
            worker.UpdateData();
            this.workers.Add(worker);
        }

        Debug.Log(transform.name + ": LoadWorker", gameObject);
    }

    public void LoadColectionsWorker(){
        this.colectionWorker.Clear();
        Transform transCollectionsWorker = transform.Find("CollectionsWorker");
        foreach (Transform trans in transCollectionsWorker)
        {
            foreach (Transform tran in trans)
            {
                Worker worker = tran.GetComponent<Worker>();
                if(worker != null){
                    this.colectionWorker.Add(tran.gameObject);
                }   
            }
        }

    }

    public void UpdateData(){
        this.UpdateWorker();
        this.SortWorkerByPower();
    }

    public void UpdateWorker(){
        this.LoadWorker();
        this.numberWorkerWorking = 0;
        foreach (Worker worker in this.workers)
        {
            worker.UpdateData();
            this.numberWorkerWorking++;
            if(numberWorkerWorking > this.maxWorkerVisible){
                worker.gameObject.SetActive(false);
            }else{
                worker.gameObject.SetActive(true);
            }
        }
    }


    //Function

    //Get
    public GameObject GetColectionWorkerByName(WorkerName workerName){
        foreach (GameObject workerG in this.colectionWorker)
        {
            if(workerG.GetComponent<Worker>().workerName == workerName){
                return workerG;
            }
        }
        return null;
    }

    public List<GameObject> GetColectionWorkerReleased(){
        List<GameObject> colectionWorkerReleased = new List<GameObject>();

        foreach (GameObject workerG in this.colectionWorker)
        {
            if(workerG.GetComponent<Worker>().isReleased){
                colectionWorkerReleased.Add(workerG);
            }
        }

        return colectionWorkerReleased;
    }

    public List<Worker> GetMaterialWorkersPowerDownToUpByName(WorkerName workerName, int evolveLv){
        Debug.LogWarning(workerName);
        List<Worker> newWorkers = new List<Worker>();
        foreach (Worker worker in this.workers)
        {
            if(worker.workerName == workerName && worker.evolveLv == evolveLv){
                newWorkers.Add(worker);
                continue;
            }
            if(workerName == WorkerName.noWorker && worker.evolveLv == evolveLv){
                newWorkers.Add(worker);
                continue;
            }
        }

        return this.SortWorkerPowerDownToUp(newWorkers);
    }

    public int GetSlot(){
        int numberSlot = 0;
        foreach (Building building in BuildingManager.instance.buildings)
        {
            if(building.lv ==0){
                continue;
            } 
            numberSlot += building.slot;
        }
        return numberSlot + BuildingManager.instance.warehouse.slot;
    }

    //Math

    public void SortWorkerByPower(){
        List<Worker> oldWokers = new List<Worker>();
        foreach (Worker worker in workers)
        {
            oldWokers.Add(worker);
        }

        List<Worker> newWorkers = new List<Worker>();
        for (int i = 0; i <  this.workers.Count; i++)
        {
            Worker worker = this.GetWorkerPowerHighest(oldWokers);
            newWorkers.Add(worker);
            oldWokers.Remove(worker);
            worker.transform.SetAsLastSibling();
        }
        this.LoadWorker();
    }

    public List<Worker> SortWorkerPowerDownToUp(List<Worker> workers){
        int count = workers.Count;
        List<Worker> newWorkers = new List<Worker>();
        for (int i = 0; i < count; i++)
        {
            Worker worker = this.GetWorkerPowerLowest(workers);
            workers.Remove(worker);
            newWorkers.Add(worker);
        }
        return newWorkers;
    }

    public Worker GetWorkerPowerHighest(List<Worker> workers){
        Worker worker = workers[0];
        float powerHighest = workers[0].GetPower();
        foreach (Worker worker1 in workers)
        {
            if(worker1.GetPower() > powerHighest){
                worker = worker1;
                powerHighest = worker1.GetPower();
            }

        }
        return worker;
    }

    public Worker GetWorkerPowerLowest(List<Worker> workers){
        Worker worker = workers[0];
        float powerLowest = workers[0].GetPower();
        foreach (Worker worker1 in workers)
        {
            if(worker1.GetPower() < powerLowest){
                worker = worker1;
                powerLowest = worker1.GetPower();
            }

        }
        return worker;
    }

    public WorkerManagerData ParseToData(){
        this.UpdateData();
        WorkerManagerData workerManagerData = new WorkerManagerData();
        workerManagerData.workerDatas = new List<WorkerData>();

        foreach (Worker worker in this.workers)
        {
            workerManagerData.workerDatas.Add(worker.ParseToData());
        }
        return workerManagerData;
    }

    public void ParseFromData(WorkerManagerData workerManagerData){
        if(workerManagerData == null) return;
        this.DeleteAllWorker();
        foreach (WorkerData workerData in workerManagerData.workerDatas)
        {
            GameObject workerG = GetColectionWorkerByName(workerData.workerName);
            Worker worker = Instantiate<GameObject>(workerG).transform.GetComponent<Worker>();
            worker.ParseFromData(workerData);
            worker.transform.SetParent(transform.Find("Workers"));
        }

        this.LoadWorker();
        this.UpdateData();
    }

    public void DeleteAllWorker(){
        if(this.workers.Count > 0) this.workers.Clear();
        Transform transWorkers = transform.Find("Workers");

        foreach (Transform trans in transWorkers)
        {
            trans.gameObject.SetActive(false);
            Destroy(trans.gameObject);
        }

    }
    
}
