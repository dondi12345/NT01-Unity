using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUIManager : LoadBehaviour
{

    public static BattleUIManager instance;

    public SoldierBagUI soldierBagUI;

    public Transform playerUI;
    public Transform battleUI;

    public Transform toolUI;
    public Transform toolBattleUI;

    public WinUI winUI;
    public LoseUI loseUI;

    public List<WarningUI> warningUIs;

    protected override void Awake()
    {
        base.Awake();
        if (BattleUIManager.instance != null) Debug.LogError("Only 1 BattleUIManager allow");
        BattleUIManager.instance = this;
    }

    protected override void Start()
    {
        base.Start();
        this.ChangeToBattle();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWinUI();
        this.LoadLoseUI();
        this.LoadSoldierBagUI();
        this.LoadPlayerUI();
        this.LoadBattleUI();
        this.LoadToolUI();
        this.LoadToolBallteUI();
        this.LoadWarningUI();
    }

    private void LoadPlayerUI(){
        this.playerUI = transform.Find("Canvas").Find("PlayerUI");
    }
    private void LoadBattleUI(){
        this.battleUI = transform.Find("Canvas").Find("BattleUI");
    }
    private void LoadToolUI(){
        this.toolUI = transform.Find("Canvas").Find("ToolUI");
    }
    private void LoadToolBallteUI(){
        this.toolBattleUI = transform.Find("Canvas").Find("ToolBattleUI");
    }

    private void LoadSoldierBagUI(){
        SoldierBagUI soldierBagUI;
        try
        {
            soldierBagUI = transform.Find("Canvas").Find("BagUI").Find("SoldierBagUI").GetComponent<SoldierBagUI>();
            this.soldierBagUI = soldierBagUI;
            Debug.Log(transform.name + ": LoadSoldierBagUI", gameObject);
        }
        catch (System.Exception)
        {
            Debug.LogWarning("Can't LoadSoldierBagUI");
            return;
        }
    }

    public void LoadWarningUI(){
        Transform transWarningUIs = transform.Find("Canvas").Find("WarningUI");    
        foreach (Transform tras in transWarningUIs)
        {
            WarningUI warningUI = tras.GetComponent<WarningUI>();
            if (warningUI == null) continue;
            this.warningUIs.Add(warningUI);
        }

        Debug.Log(transform.name + ": LoadBuilding", gameObject);
    }

    public void LoadWinUI(){
        Transform transWinUI= transform.Find("Canvas").Find("WarningUI").Find("WinUI");    
        WinUI winUI = transWinUI.GetComponent<WinUI>();
        if(winUI == null) Debug.Log(transform.name + ": Can't LoadWinUI", gameObject);
        this.winUI = winUI;
        Debug.Log(transform.name + ": LoadWinUI", gameObject);
    }
    public void LoadLoseUI(){
        Transform transLoseUI= transform.Find("Canvas").Find("WarningUI").Find("LoseUI");    
        LoseUI loseUI = transLoseUI.GetComponent<LoseUI>();
        if(loseUI == null) Debug.Log(transform.name + ": Can't LoadLoseUI", gameObject);
        this.loseUI = loseUI;
        Debug.Log(transform.name + ": LoadLoseUI", gameObject);
    }

    public void ChangeToBarracks(){
        this.soldierBagUI.OnUI();
        CameraManager.instance.ChangeCam(CamName.barracksCam);

        PositionManager.instance.UpdateData();

        BattleManager.instance.barracks = true;

        this.playerUI.gameObject.SetActive(false);
        this.battleUI.gameObject.SetActive(false);
    }

    public void ChangeToBattle(){
        this.soldierBagUI.OffUI();
        CameraManager.instance.ChangeCam(CamName.mainCam);

        PositionManager.instance.UpdateData();

        BattleManager.instance.barracks = false;

        this.playerUI.gameObject.SetActive(true);
        this.battleUI.gameObject.SetActive(true);

        MasterManager.instance.SaveData();
    }

    //Get
    public WarningUI GetWarningUIByName(WarningName warningName){
        return this.warningUIs.Find((warningUI) => (warningUI.warningName == warningName));
    }

}
