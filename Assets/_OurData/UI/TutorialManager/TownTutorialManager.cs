using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownTutorialManager : LoadBehaviour
{
    public bool onTutorial = false;

    public GiftTutorial giftTutorial;

    public UpWarehouseTutorial upWarehouseTutorial;
    public UpBuildingTutorial upBuildingTutorial;
    public UseItemTutorial useItemTutorial;
    public MiningTutorial miningTutorial;
    public SummonTutorial summonTutorial;

    public static TownTutorialManager instance;
    protected override void Awake()
    {
        base.Awake();
        if (TownTutorialManager.instance != null) Debug.LogError(transform.name+" Only 1 TutorialManager allow");
        TownTutorialManager.instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadGiftTutorial();

        this.LoadUpWarehouseTutorial();
        this.LoadUpBuildingTutorial();
        this.LoadUseItemTutorial();
        this.LoadMiningTutorial();
        this.LoadSummonTutorial();
    }

    protected void LoadGiftTutorial(){
        this.giftTutorial = transform.Find("Canvas").Find("GiftTutorial").GetComponent<GiftTutorial>();
    }

    protected void LoadUpWarehouseTutorial(){
        this.upWarehouseTutorial = transform.Find("Canvas").Find("UpWarehouseTutorial").GetComponent<UpWarehouseTutorial>();
    }
    protected void LoadUpBuildingTutorial(){
        this.upBuildingTutorial = transform.Find("Canvas").Find("UpBuildingTutorial").GetComponent<UpBuildingTutorial>();
    }
    protected void LoadUseItemTutorial(){
        this.useItemTutorial = transform.Find("Canvas").Find("UseItemTutorial").GetComponent<UseItemTutorial>();
    }
    protected void LoadMiningTutorial(){
        this.miningTutorial = transform.Find("Canvas").Find("MiningTutorial").GetComponent<MiningTutorial>();
    }
    protected void LoadSummonTutorial(){
        this.summonTutorial = transform.Find("Canvas").Find("SummonTutorial").GetComponent<SummonTutorial>();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        this.FollowTutorial();
    }

    protected void FollowTutorial(){
        if(!PlayerManager.instance.passUpWarehouseTutorial && !this.onTutorial){
            this.onTutorial = true;
            StartCoroutine(upWarehouseTutorial.StartTutorial());
        }
        if(onTutorial) return;

        if(!PlayerManager.instance.passSummonTutorial && !this.onTutorial){
            this.onTutorial = true;
            StartCoroutine(summonTutorial.StartTutorial());
        }
        if(onTutorial) return;

        if(!PlayerManager.instance.passUseItemTutorial && !this.onTutorial){
            this.onTutorial = true;
            StartCoroutine(useItemTutorial.StartTutorial());
        }
        if(onTutorial) return;

        if(!PlayerManager.instance.passUpBuildingTutorial && !this.onTutorial){
            this.onTutorial = true;
            StartCoroutine(upBuildingTutorial.StartTutorial());
        }
        if(onTutorial) return;

        if(!PlayerManager.instance.passMiningTutorial && !this.onTutorial){
            this.onTutorial = true;
            StartCoroutine(miningTutorial.StartTutorial());
        }
        if(onTutorial) return;
    }

}
