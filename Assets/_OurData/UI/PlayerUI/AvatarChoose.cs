using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarChoose : LoadBehaviour
{
    public AvatarPlayerName avatarPlayerName;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        try
        {
            this.avatarPlayerName = AvatarPlayerNameParser.FromString(transform.name);
        }
        catch (System.Exception)
        {
            this.avatarPlayerName = AvatarPlayerName.skull;
        }
    }

    public void OnClick(){
        PlayerManager.instance.avatarPlayerName = this.avatarPlayerName;
        
    }
}
