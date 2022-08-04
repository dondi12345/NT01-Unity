using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : LoadBehaviour
{

    public bool battle = false;

    public bool barracks = false;

    public int nextLvWave = 1;
    public int totalLv = 1; 
    public int numberEnemy = 5;
    public int baseNumberEnemy = 5;

    public float powerAlly = 0;
    public float powerEnemy = 0;

    public List<Soldier> alliesAlive;
    public List<Soldier> enemiesAlive;
    public List<Soldier> alliesDead;
    public List<Soldier> enemiesDead;

    public static BattleManager instance;
    protected override void Awake()
    {
        base.Awake();
        if (BattleManager.instance != null) Debug.LogError("Only 1 BattleManager allow");
        BattleManager.instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        this.CheckEndBattle();
    }

    public void UpdateData(){
        this.UpdateWave();

        this.LoadDataBattle();
        this.UpdatePower();
    }

    public void UpdatePower(){
        this.powerAlly = 0;
        foreach (Position allyPosition in PositionManager.instance.allyPositions)
        {
            if(allyPosition.soldier == null) continue;
            this.powerAlly += allyPosition.soldier.GetPowerSoldier();
        }

        this.powerEnemy = 0;
        foreach (Position enemyPosition in PositionManager.instance.enemyPositions)
        {
            if(enemyPosition.soldier == null) continue;
            this.powerEnemy += enemyPosition.soldier.GetPowerSoldier();
        }
    }

    public void NextOneWave(){
        if(this.nextLvWave == 1) return;
        this.nextLvWave = 1;
        this.UpdateData();
    }
    public void NextFiveWave(){
        if(this.nextLvWave == 5) return;
        this.nextLvWave = 5;
        this.UpdateData();
    }

    public void LoadDataBattle(){
        WorkerManagerData allyManagerData  = SaveManager.instance.LoadDataWorker();
        SoldierManager.instance.ParseAllyFromData(allyManagerData);

        WorkerManagerData enemyManagerData = new WorkerManagerData();
        enemyManagerData.workerDatas = this.GetDataEnemyWave();
        SoldierManager.instance.ParseEnemyFromData(enemyManagerData);
    }

    public void UpdateWave(){
        this.totalLv = PlayerManager.instance.waveLv + nextLvWave;
        int numberIncrease = PlayerManager.instance.waveLv /10;
        this.numberEnemy = this.baseNumberEnemy + numberIncrease;
        if(this.numberEnemy > PositionManager.instance.enemyPositions.Count) this.numberEnemy = PositionManager.instance.enemyPositions.Count;
    }

    public void StartBattle(){
        
        this.alliesAlive.Clear();
        this.enemiesAlive.Clear();
        this.alliesDead.Clear();
        this.enemiesDead.Clear();

        foreach (Soldier soldier in SoldierManager.instance.allies)
        {
            if(!soldier.isBattle()) continue;
            soldier.soldierCtrl.soldierBattle.soldierTeamName = SoldierTeamName.ally;
            this.alliesAlive.Add(soldier);
            soldier.soldierCtrl.soldierBattle.StartBattle();
        }
        foreach (Soldier soldier in SoldierManager.instance.enemies)
        {
            if(!soldier.isBattle()) continue;
            soldier.soldierCtrl.soldierBattle.soldierTeamName = SoldierTeamName.enemy;
            this.enemiesAlive.Add(soldier);
            soldier.soldierCtrl.soldierBattle.StartBattle();
        }

        if( this.alliesAlive.Count == 0){
            BattleUIManager.instance.ChangeToBarracks();
            return;
        }

        this.battle = true;
        PositionManager.instance.OffAllAllyPosition();
        BattleUIManager.instance.toolUI.gameObject.SetActive(false);
        BattleUIManager.instance.toolBattleUI.gameObject.SetActive(true);
        this.BattleTimeSpeed(PlayerManager.instance.battleSpeed);
    }

    public void CheckEndBattle(){
        if(!this.battle) return;

        if(this.enemiesAlive.Count == 0){
            this.battle = false;
            BattleUIManager.instance.winUI.OnUI(this.nextLvWave);
            return;
        }

        if(this.alliesAlive.Count == 0){
            this.battle = false;
            BattleUIManager.instance.loseUI.OnUI();
            return;
        }
    }

    public void ResetBattle(){
        this.battle = false;
        this.BattleTimeSpeed(1f);
        this.UpdateData();
        PositionManager.instance.OnAllAllyPosition();
        PositionManager.instance.UpdateData();
        BattleUIManager.instance.toolUI.gameObject.SetActive(true);
        BattleUIManager.instance.toolBattleUI.gameObject.SetActive(false);
        
    }

    public void BattleTimeSpeed(float scale){
        Time.timeScale = scale;
    }

    public List<WorkerData> GetDataEnemyWave(){
        this.UpdateWave();
        int numberEneny = totalLv < numberEnemy ? totalLv : numberEnemy;
        int baseLv = totalLv/numberEneny;

        List<WorkerData> workerDatas = new List<WorkerData>();

        List<GameObject> colectionSoldiers = SoldierManager.instance.GetColectionSoldierReleased();
        for (int i = 0; i < numberEneny; i++)
        {
            WorkerData workerData = new WorkerData();
            workerData.lv = baseLv;
            Soldier soldier = colectionSoldiers[Random.Range(0, colectionSoldiers.Count)].GetComponent<Soldier>();
            workerData.workerName = soldier.workerName;
            workerData.numberPosition = i+1;
            workerDatas.Add(workerData);
        }

        for(int i = baseLv*numberEnemy ; i <= totalLv; i++ ){
            workerDatas[i-baseLv*numberEnemy].lv++;
        }

        return workerDatas;
    }

    public Soldier GetRandEnemy(){
        if(this.enemiesAlive.Count == 0) return null;
        return this.enemiesAlive[Random.Range(0, this.enemiesAlive.Count)];
    }
    public Soldier GetRandAlly(){
        if(this.alliesAlive.Count == 0) return null;
        return this.alliesAlive[Random.Range(0, this.alliesAlive.Count)];
    }

    public void AllyDead(Soldier soldier){
        this.alliesAlive.Remove(soldier);
        this.alliesDead.Add(soldier);
    }

    public void EnemyDead(Soldier soldier){
        this.enemiesAlive.Remove(soldier);
        this.enemiesDead.Add(soldier);
    }
}
