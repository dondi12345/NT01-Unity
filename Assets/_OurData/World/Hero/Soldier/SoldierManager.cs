using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierManager : LoadBehaviour
{
    public static SoldierManager instance;

    public List<Soldier> allies;
    public List<Soldier> enemies;

    //Colections Soldier
    public List<GameObject> colectionSoldier;

    //For nav mesh
    public float soldierRunSpeed = 8f;
    public float soldierWalkSpeed = 3; 

    protected override void Awake()
    {
        base.Awake();
        if (SoldierManager.instance != null) Debug.LogError("Only 1 SoldierManager allow");
        SoldierManager.instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadColectionsSoldier();
        this.LoadAlly();
        this.LoadEnemy();
    }

    public void LoadAlly(){
        if(this.allies.Count > 0) this.allies.Clear();
        Transform transAllies = transform.Find("Allies");

        foreach (Transform trans in transAllies)
        {
            Soldier ally = trans.GetComponent<Soldier>();
            if(ally == null) continue;
            if(!ally.gameObject.activeSelf) continue;
            ally.UpdateData();
            ally.soldierCtrl.healBar.SetColor(Color.green);
            this.allies.Add(ally);
        }

        Debug.Log(transform.name + ": LoadAlly", gameObject);
    }
    public void LoadEnemy(){
        if(this.enemies.Count > 0) this.enemies.Clear();
        Transform transEnemies = transform.Find("Enemies");

        foreach (Transform trans in transEnemies)
        {
            Soldier enemy = trans.GetComponent<Soldier>();
            if(enemy == null) continue;
            if(!enemy.gameObject.activeSelf) continue;
            enemy.UpdateData();
            enemy.soldierCtrl.healBar.SetColor(Color.red);
            this.enemies.Add(enemy);
        }

        Debug.Log(transform.name + ": LoadEnemy", gameObject);
    }

    public void LoadColectionsSoldier(){
        this.colectionSoldier.Clear();
        Transform transCollectionsSoldier = transform.Find("CollectionsSoldier");
        foreach (Transform trans in transCollectionsSoldier)
        {
            foreach (Transform tran in trans)
            {
                Soldier soldier = tran.GetComponent<Soldier>();
                if(soldier != null){
                    this.colectionSoldier.Add(tran.gameObject);
                }   
            }
        }

    }

    //Function

    public GameObject GetColectionSoldierByName(WorkerName workerName){
        foreach (GameObject soldierG in this.colectionSoldier)
        {
            if(soldierG.GetComponent<Soldier>().workerName == workerName){
                return soldierG;
            }
        }
        return null;
    }

    public void ParseAllyFromData(WorkerManagerData workerManagerData){
        if(workerManagerData == null) return;
        this.DeleteAllAlly();

        foreach (WorkerData workerData in workerManagerData.workerDatas)
        {
            //Woker who was assiged born in battle 

            GameObject soldierG = this.GetColectionSoldierByName(workerData.workerName);
            if(soldierG == null) {
                Debug.LogWarning("Can't find soldier");
                continue;
            }
            Soldier ally = Instantiate<GameObject>(soldierG).transform.GetComponent<Soldier>();
            ally.ParseFromData(workerData);
            ally.transform.SetParent(transform.Find("Allies"));
            ally.transform.position = new Vector3(0,0,-15);
        }
        this.LoadAlly();
        this.ArrangeAlly();
    }

    public WorkerManagerData ParseAllyToData(){
        WorkerManagerData workerManagerData = new WorkerManagerData();
        workerManagerData.workerDatas = new List<WorkerData>();
        foreach (Soldier soldier in this.allies)
        {
            workerManagerData.workerDatas.Add(soldier.ParseToData());
        }
        return workerManagerData;
    }

    public void DeleteAllAlly(){
        this.allies.Clear();
        Transform transAllies = transform.Find("Allies");

        foreach (Transform trans in transAllies)
        {
            trans.gameObject.SetActive(false);
            Destroy(trans.gameObject);
        }

    }

    public void ArrangeAlly(){
        foreach (Soldier ally in this.allies)
        {
            if(!ally.isBattle()){
                ally.ComeToBag();
                continue;
            }
            Position position = PositionManager.instance.GetPositionAllyByNumber(ally.numberPosition);
            
            position.PutSoldier(ally);
        }
    }

    public void ParseEnemyFromData(WorkerManagerData workerManagerData){
        if(workerManagerData == null) return;
        this.DeleteAllEnemy();

        foreach (WorkerData workerData in workerManagerData.workerDatas)
        {
            //Woker who was assiged born in battle 

            GameObject enemyG = this.GetColectionSoldierByName(workerData.workerName);
            Soldier enemy = Instantiate<GameObject>(enemyG).transform.GetComponent<Soldier>();
            enemy.ParseFromData(workerData);
            enemy.transform.SetParent(transform.Find("Enemies"));
        }

        this.LoadEnemy();
        this.ArrangeEnemy();
    }

    public void DeleteAllEnemy(){
       this.enemies.Clear();
        Transform transEnemies = transform.Find("Enemies");

        foreach (Transform trans in transEnemies)
        {
            trans.gameObject.SetActive(false);
            Destroy(trans.gameObject);
        }

    }

    public void ArrangeEnemy(){
        foreach (Soldier enemy in this.enemies)
        {
            if(!enemy.isBattle()){
                enemy.ComeToBag();
                continue;
            }
            Position position = PositionManager.instance.GetPositionEnemyByNumber(enemy.numberPosition);
            position.PutSoldier(enemy);
        }
    }


    //Get
    public List<GameObject> GetColectionSoldierReleased(){
        List<GameObject> soldierReleaseds = new List<GameObject>();
        foreach ( GameObject soldierG in this.colectionSoldier)
        {
            Soldier soldier = soldierG.GetComponent<Soldier>();
            if(!soldier.isReleased) continue;

            soldierReleaseds.Add(soldierG);
        }

        return soldierReleaseds;
    }
}
