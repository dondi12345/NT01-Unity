using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageAvatar : LoadBehaviour
{
    public AvatarPlayerCtrl avatarPlayerCtrl;

    public ProfilePlayerUI profilePlayerUI;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAvatarPlayerCtrl();
        this.LoadProfilePlayerUI();
    }

    public void LoadAvatarPlayerCtrl(){
        this.avatarPlayerCtrl = transform.Find("AvatarPlayerCtrl").GetComponent<AvatarPlayerCtrl>();
    }

    public void LoadProfilePlayerUI(){
        this.profilePlayerUI = transform.parent.parent.Find("ProfilePlayerUI").GetComponent<ProfilePlayerUI>();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        this.LoadData();
    }

    public void LoadData(){
        this.avatarPlayerCtrl.SetAvatarPlayer(PlayerManager.instance.avatarPlayerName);
    }

    public void OnProfilePlayerUI(){
        this.profilePlayerUI.OnUI();
    }
}
