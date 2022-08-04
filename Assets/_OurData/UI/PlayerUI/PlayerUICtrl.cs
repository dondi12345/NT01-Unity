using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUICtrl : LoadBehaviour
{
    public ProfilePlayerUI profilePlayerUI;
    public ChangeNameUI changeNameUI;
    public DailyGiftPlayerLevel dailyGiftPlayerLevel;
    public ChangeAvatarPlayerUI changeAvatarPlayerUI;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadProfilePlayerUI();
        this.LoadChangeNameUI();
        this.LoadDailyGiftPlayerLevel();
        this.LoadChangeAvatarPlayerUI();
    }

    protected void LoadProfilePlayerUI(){
        this.profilePlayerUI = transform.Find("ProfilePlayerUI").GetComponent<ProfilePlayerUI>();
    }
    protected void LoadChangeNameUI(){
        this.changeNameUI = transform.Find("ChangeNameUI").GetComponent<ChangeNameUI>();
    }
    protected void LoadDailyGiftPlayerLevel(){
        this.dailyGiftPlayerLevel = transform.Find("DailyGiftPlayerLevel").GetComponent<DailyGiftPlayerLevel>();
    }
    protected void LoadChangeAvatarPlayerUI(){
        this.changeAvatarPlayerUI = transform.Find("ChangeAvatarPlayerUI").GetComponent<ChangeAvatarPlayerUI>();
    }
}
