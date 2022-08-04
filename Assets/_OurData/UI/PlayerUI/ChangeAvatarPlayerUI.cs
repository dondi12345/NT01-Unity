using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAvatarPlayerUI : LoadBehaviour
{
    public List<AvatarChoose> avatarChooses;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAvatarChoose();
    }

    protected void LoadAvatarChoose(){
        this.avatarChooses.Clear();
        Transform contentAvatarChoose = transform.Find("Panel").Find("ScrollView").Find("Viewport").Find("Content");
        foreach (Transform trans in contentAvatarChoose)
        {
            AvatarChoose avatarChoose = trans.GetComponent<AvatarChoose>();
            if(avatarChoose == null) continue;
            this.avatarChooses.Add(avatarChoose);
        }
    }

    public void Close(){
        
        this.OffUI();
    }

    public void OnUI(){
        
        gameObject.SetActive(true);
    }

    public void OffUI(){
        gameObject.SetActive(false);
    }
}
