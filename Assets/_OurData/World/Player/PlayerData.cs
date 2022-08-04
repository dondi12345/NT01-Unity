using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData
{
    public int lv = 1;
    public int waveLv = 1;
    public float experience = 0;

    public string playerName ="Player";
    public AvatarPlayerName avatarPlayerName = AvatarPlayerName.chick;

    public bool isDailyGiftPlayerLevel = false;

    //Tutorial
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
    

    public string lastTimeInTown = DateTime.Now.ToString();
    public string lastTimeInBattle = DateTime.Now.ToString();
    public string lastTimeLogin = DateTime.Now.ToString();
}
