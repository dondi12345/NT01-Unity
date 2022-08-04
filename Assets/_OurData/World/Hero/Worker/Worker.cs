using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : LoadBehaviour
{
    //base status
    public int lv = 1;
    public int evolveLv = 1;
    
    public ElementName elementName;
    
    public int numberPosition = 0;
    public WorkerName workerName = WorkerName.noWorker;

    public float experienceNeedLvUp = 1;

    public bool isReleased = false;

    //For merge
    public bool gone = false;

    //For animation
    public bool Walk = false;

    //For nav mesh
    public float runSpeed = 1;

    public WorkerCtrl workerCtrl;

    //For Save
    public WorkerData workerData;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWorkerCtrl();
        if(this.workerName == WorkerName.noWorker){
            this.workerName = WorkerNameParser.FromString(transform.name);
        }
    }

    protected virtual void LoadWorkerCtrl()
    {
        if (this.workerCtrl != null) return;
        this.workerCtrl = GetComponent<WorkerCtrl>();
        Debug.Log(transform.name + ": LoadWorkerCtrl", gameObject);
    }

    protected override void Start()
    {
        base.Start();
        this.UpdateData();
    }

    //Function

    public void UpdateData(){
        this.UpDateExperienceNeedLvUp();
        this.UpdateRunSpeed();
    }

    public void UpDateExperienceNeedLvUp(){
        this.experienceNeedLvUp = (this.lv-1)*(this.lv)/4+this.lv;
    }

    public void UpdateRunSpeed(){
        try
        {
            this.runSpeed = WorkerManager.instance.workerRunSpeed; 
            this.workerCtrl.navMeshAgent.speed = this.runSpeed;
        }
        catch (System.Exception){}
    }

    public void UpLv(){
        if(!this.CanUpLv()){
            TownUIManager.instance.GetWarningUIByName(WarningName.dontEnoughResource).OnUI();
            return;
        }
        ResourcesManager.instance.GetProductStorageByName(ProductName.soul).number -= this.experienceNeedLvUp;
        this.lv++;
    }

    public bool CanUpLv(){
        this.UpdateData();
        if(ResourcesManager.instance.GetProductStorageByName(ProductName.soul).number < this.experienceNeedLvUp){
            return false;
        }
        return true;
    }

    public void OnGameObject(){
        gameObject.SetActive(true);
        this.UpdateData();
    }

    public void OffGameObject(){
        gameObject.SetActive(false);
    }

    public WorkerData ParseToData(){
        this.UpdateData();

        this.workerData.lv = this.lv;
        this.workerData.workerName = this.workerName;;
        this.workerData.evolveLv = this.evolveLv;

        this.workerData.x = transform.position.x;
        this.workerData.y = transform.position.y;
        this.workerData.z = transform.position.z;

        this.workerData.timeWorking = this.workerCtrl.workerAI.currentTimeWorking;

        this.workerData.atk = this.GetAtk();
        this.workerData.def = this.GetDef();
        this.workerData.hp = this.GetHp();

        return this.workerData;
    }

    public void ParseFromData(WorkerData workerData){
        this.workerData = workerData;
        this.lv = workerData.lv;
        this.evolveLv = workerData.evolveLv;

        this.workerName = workerData.workerName;
        this.workerCtrl.workerAI.currentTimeWorking = workerData.timeWorking;

        transform.position = new Vector3(workerData.x, workerData.y, workerData.z);
        this.numberPosition = workerData.numberPosition;

        this.UpdateData();
    }

    public void DestroyWorker(){
        this.gone = true;
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    //Get
    public string GetWorkerName(){
        return this.workerCtrl.soldierStats.GetName();
    }

    public float GetAtk(){
        float atk = this.workerCtrl.soldierStats.GetAtk()*this.lv;
        atk = atk * ((this.evolveLv-1) * 0.2f +1);
        //Count buff
        return atk;
    }
    public float GetDef(){
        float def = this.workerCtrl.soldierStats.GetDef()*this.lv;
        def = def * ((this.evolveLv-1) * 0.2f +1);
        //Count buff
        return def;
    }
    public float GetHp(){
        float hp = this.workerCtrl.soldierStats.GetHp()*this.lv;
        hp = hp * ((this.evolveLv-1) * 0.2f +1);
        //Count buff
        return hp;
    }

    public float GetPower(){
        float number = 0;
        this.UpdateData();
        number = this.GetAtk()/2 + this.GetDef() + this.GetHp()/5;
        number = number/this.workerCtrl.soldierStats.GetAttackDelay();
        return number;
    }

    public bool isBattle(){
        if(this.numberPosition == 0) return false;
        return true;
    }

    public void AwakeWorker(){
        this.UpdateData();
        StartCoroutine(SetActive());
    }

    public IEnumerator SetActive(){
        yield return new WaitForSeconds(0.22f);
        gameObject.SetActive(true);
    }
}
