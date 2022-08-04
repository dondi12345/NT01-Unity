using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionManager : LoadBehaviour
{
    public static PositionManager instance;

    public List<Position> allyPositions;
    public List<Position> enemyPositions;

    public int width = 6;
    public int heigh = 4;
    public float space = 3.5f;
    public GameObject positionG;

    protected override void Awake()
    {
        base.Awake();
        if (PositionManager.instance != null) Debug.LogError(transform.name+" Only 1 PositionManager allow");
        PositionManager.instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPositionG();
        this.LoadAllyPosition();
        this.LoadEnemyPosition();
        this.GeneratePosition();
        
    }

    private void LoadAllyPosition(){
        this.allyPositions.Clear();
        foreach (Transform trans in transform.Find("Ally"))
        {
            Position position = trans.GetComponent<Position>();
            if(position == null) continue;
            if(!position.gameObject.activeSelf) continue;
            this.allyPositions.Add(position);
        }
    }

    private void LoadEnemyPosition(){
        this.enemyPositions.Clear();
        foreach (Transform trans in transform.Find("Enemy"))
        {
            Position position = trans.GetComponent<Position>();
            if(position == null) continue;
            if(!position.gameObject.activeSelf) continue;
            this.enemyPositions.Add(position);
        }
    }

    private void LoadPositionG(){
        this.positionG = transform.Find("Position").gameObject;
    }

    public void UpdateData(){
        this.LoadLockPosition();
    }

    public void LoadLockPosition(){
        foreach (Position position in allyPositions)
        {
            position.UpdateData();
        }
        foreach (Position position in enemyPositions)
        {
            position.UpdateData();
        }
    }

    public void GeneratePosition(){
        if(this.allyPositions.Count > 0) return;

        int numberCount = 0;
        Transform transAlly = transform.Find("Ally");
        for (int i = 0; i < heigh; i++)
        {
            for (int j = 0; j < width; j++)
            {
                numberCount ++;
                GameObject position = Instantiate<GameObject>(this.positionG);
                position.transform.SetParent(transAlly);
                position.transform.localPosition = new Vector3(j*this.space,0,-i*this.space);
                position.GetComponent<Position>().number = numberCount;
            }
        }

        numberCount = 0;
        Transform transEnemy = transform.Find("Enemy");
        for (int i = 0; i < heigh; i++)
        {
            for (int j = 0; j < width; j++)
            {
                numberCount ++;
                GameObject position = Instantiate<GameObject>(this.positionG);
                position.transform.SetParent(transEnemy);
                position.transform.localPosition = new Vector3(j*this.space,0,-i*this.space);
                position.GetComponent<Position>().number = numberCount;
            }
        }
    }

    public void OffAllAllyPosition(){
        foreach (Position position in this.allyPositions)
        {
            position.gameObject.SetActive(false);
        }
    }
    public void OnAllAllyPosition(){
        foreach (Position position in this.allyPositions)
        {
            position.gameObject.SetActive(true);
        }
    }

    public void CallBackAllToBag(){
        foreach (Position position in this.allyPositions)
        {
            position.UpdateData();
            if(position.soldier == null) continue;
            position.soldier.ComeToBag();
        }
    }

    //Get
    public Position GetPositionAllyByNumber(int number){
        return this.allyPositions.Find((position)=>(position.number == number));
    }
    public Position GetPositionEnemyByNumber(int number){
        return this.enemyPositions.Find((position)=>(position.number == number));
    }

    public Position GetEmptyAllyPosition(){
        foreach (Position position in this.allyPositions)
        {
            if(position.soldier != null) continue;
            if(position.lockPosition) continue;
            return position;
        }
        return null;
    }
}
