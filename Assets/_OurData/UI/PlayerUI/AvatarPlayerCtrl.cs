using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarPlayerCtrl : LoadBehaviour
{
    public List<AvatarPlayer> avatarPlayer;
    public AvatarPlayerName avatarChoose = AvatarPlayerName.noAvata;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAvatarPlayer();
    }

    public void LoadAvatarPlayer(){
        this.avatarPlayer.Clear();
        foreach (Transform trans in transform)
        {
            AvatarPlayer avatarPlayer = trans.GetComponent<AvatarPlayer>();
            if(avatarPlayer == null) continue;
            this.avatarPlayer.Add(avatarPlayer);
        }
    }

    public void SetAvatarPlayer(AvatarPlayerName avatarPlayerName){
        if(avatarPlayerName == this.avatarChoose) return;
        this.avatarChoose = avatarPlayerName;
        this.OffAllImage();
        try
        {
            this.GetAvatarPlayerByName(avatarPlayerName).gameObject.SetActive(true);
        }
        catch (System.Exception)
        {
            this.GetAvatarPlayerByName(AvatarPlayerName.skull).gameObject.SetActive(true);
        }
        
    }

    public void OffAllImage(){
        foreach (AvatarPlayer avatarPlayer in this.avatarPlayer)
        {
            avatarPlayer.gameObject.SetActive(false);
        }
    }

    public AvatarPlayer GetAvatarPlayerByName(AvatarPlayerName avatarPlayerName){
        return this.avatarPlayer.Find((avatarPlayer) => avatarPlayer.avatarPlayerName == avatarPlayerName);
    }
}
