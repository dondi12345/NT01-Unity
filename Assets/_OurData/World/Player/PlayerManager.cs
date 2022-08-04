using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerManager : LoadBehaviour
{
    public int lv = 1;

    public int waveLv = 1;

    public float experience = 0;
    public float experienceNeedLvUp = 1;

    //Data Save
    public bool isDailyGiftPlayerLevel = false;

    //TutorialManager
    public bool passUpWarehouseTutorial = false;
    public bool passUpBuildingTutorial = false;
    public bool passUseItemTutorial = false;
    public bool passMiningTutorial = false;
    public bool passSummonTutorial = false;

    
    //Setting
    public float sound = 0.1f;
    public float music = 0.1f;

    public float battleSpeed = 1f;

    public bool darkMode = false;

    public MultiLanguageName multiLanguageName = MultiLanguageName.English;

    //Stuf

    public string playerName = "Player";
    public AvatarPlayerName avatarPlayerName = AvatarPlayerName.skull;

    public string lastTimeInTown;
    public string lastTimeInBattle;
    public string lastTimeLogin;


    public static PlayerManager instance;
    protected override void Awake()
    {
        base.Awake();
        if (PlayerManager.instance != null) Debug.LogError(transform.name+" Only 1 PlayerManager allow");
        PlayerManager.instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
    }

    public void UpdateData(){
        this.PlayerManagerResetNewDay();
        this.UpdateLv();
        SoundManager.instance.UpdatData();
    }

    public void OverWave(int numberWave){
        this.waveLv += numberWave;
        this.UpdateData();
    }

    public void UpdateLv(){
        this.experienceNeedLvUp = (this.lv-1)*(this.lv)/4+this.lv;
        if(this.experience >= this.experienceNeedLvUp){
            experience -= this.experienceNeedLvUp;
            this.lv++;
            this.isDailyGiftPlayerLevel = false;
            this.UpdateLv();
        }
    }

    public bool CheckNewDay(string lastTime){
        DateTime dateTime;
        try
        {
            dateTime = DateTime.Parse(lastTime);
        }
        catch (System.Exception)
        {
            Debug.LogWarning("Can't parse time: " + lastTime);
            return false;
        }
        
        DateTime dateNow = DateTime.Now;
        if(dateTime.Day == dateNow.Day && dateTime.Month == dateNow.Month && dateTime.Year == dateNow.Year) return false;
        return true;
    }

    public void PlayerManagerResetNewDay(){
        if(!PlayerManager.instance.CheckNewDay(PlayerManager.instance.lastTimeLogin)) return;

        this.isDailyGiftPlayerLevel = false;
    }

    //Get

    public float GetMultiMoneyByWave(){
        return (float)this.waveLv/1000;
    }

    public PlayerData ParseToData(){
        this.PlayerManagerResetNewDay();
        this.UpdateData();
        PlayerData playerData = new PlayerData();

        playerData.lv = this.lv;
        playerData.waveLv = this.waveLv;
        playerData.experience = this.experience;

        playerData.playerName = this.playerName;
        playerData.avatarPlayerName = this.avatarPlayerName;

        playerData.isDailyGiftPlayerLevel = this.isDailyGiftPlayerLevel;

        //Tutorial
        playerData.passUpWarehouseTutorial = this.passUpWarehouseTutorial;
        playerData.passUpBuildingTutorial = this.passUpBuildingTutorial;
        playerData.passUseItemTutorial = this.passUseItemTutorial;
        playerData.passMiningTutorial = this.passMiningTutorial;
        playerData.passSummonTutorial = this.passSummonTutorial;

        //Setting
        playerData.sound = this.sound;
        playerData.music = this.music;
        playerData.battleSpeed = this.battleSpeed;
        playerData.darkMode = this.darkMode;
        playerData.multiLanguageName = this.multiLanguageName;

        playerData.lastTimeInTown = this.lastTimeInTown;
        playerData.lastTimeInBattle = this.lastTimeInBattle;
        if(MasterManager.instance.currentSceneName.Equals(MasterManager.TOWN_SCENE)){
            this.lastTimeInTown = DateTime.Now.ToString();
            playerData.lastTimeInTown = DateTime.Now.ToString();
        }

        if(MasterManager.instance.currentSceneName.Equals(MasterManager.BATTLE_SCENE)){
            this.lastTimeInBattle = DateTime.Now.ToString();
            playerData.lastTimeInBattle = DateTime.Now.ToString();
        }

        this.lastTimeLogin = DateTime.Now.ToString();
        playerData.lastTimeLogin = DateTime.Now.ToString();

        return playerData;
    }
    public void ParseFromData(PlayerData playerData){
        if(playerData == null){
            playerData = new PlayerData();
        }
        this.lv = playerData.lv;
        this.waveLv = playerData.waveLv;
        this.experience = playerData.experience;

        this.playerName = playerData.playerName;
        this.avatarPlayerName = playerData.avatarPlayerName;

        this.isDailyGiftPlayerLevel = playerData.isDailyGiftPlayerLevel;

        //Tutorial
        this.passUpWarehouseTutorial = playerData.passUpWarehouseTutorial;
        this.passUpBuildingTutorial = playerData.passUpBuildingTutorial;
        this.passUseItemTutorial = playerData.passUseItemTutorial;
        this.passMiningTutorial = playerData.passMiningTutorial;
        this.passSummonTutorial = playerData.passSummonTutorial;

        //Setting
        this.sound = playerData.sound;
        this.music = playerData.music;
        this.battleSpeed = playerData.battleSpeed;
        this.darkMode = playerData.darkMode;
        this.multiLanguageName = playerData.multiLanguageName;

        this.lastTimeInTown = playerData.lastTimeInTown;
        this.lastTimeInBattle = playerData.lastTimeInBattle;
        this.lastTimeLogin = playerData.lastTimeLogin;

        this.PlayerManagerResetNewDay();
        this.UpdateData();
        LightManager.instance.UpdateData();
        LanguageManager.instance.UpdateData(this.multiLanguageName);
    }
    
}
