using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseUI : LoadBehaviour
{
    public void Close(){
        
        this.OffUI();
    }

    public void OnUI(){
        if(gameObject.activeSelf) return;
        SoundManager.instance.OnSoundByName(SoundName.soundLose);
        gameObject.SetActive(true);
    }

    public void OffUI(){
        BattleManager.instance.ResetBattle();
        gameObject.SetActive(false);
    }
}
