using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinUI : LoadBehaviour
{

    public int numerWaveAvoid = 1;
    public List<ReceiveItemIcon> receiveItemIcons;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadReceiveItemIcon();
    }

    public void LoadReceiveItemIcon(){
        this.receiveItemIcons.Clear();
        Transform transformReceiveItemIcon = transform.Find("Panel").Find("ReceiveItem");
        foreach (Transform trans in transformReceiveItemIcon)
        {
            ReceiveItemIcon receiveItemIcon = trans.GetComponent<ReceiveItemIcon>();
            if(receiveItemIcon == null) return;
            this.receiveItemIcons.Add(receiveItemIcon);
        }
    }

    public void Close(){
        
        this.OffUI();
    }

    public void OnUI(int numerWaveAvoid){
        if(gameObject.activeSelf) return;
        SoundManager.instance.OnSoundByName(SoundName.soundWin);
        this.numerWaveAvoid = numerWaveAvoid;
        foreach (ReceiveItemIcon receiveItemIcon in receiveItemIcons)
        {
            if(receiveItemIcon.itemName == ItemName.oneExperience){
                receiveItemIcon.number = numerWaveAvoid * ((int)PlayerManager.instance.waveLv/50 + 1);
            }
            if(receiveItemIcon.itemName == ItemName.oneDiamond){
                receiveItemIcon.number = numerWaveAvoid * ((int)PlayerManager.instance.waveLv/50 + 1);
            }
            if(receiveItemIcon.number <= 0){
                receiveItemIcon.number = 1;
            }
            receiveItemIcon.UpdateData();
        }
        gameObject.SetActive(true);
    }

    public void OffUI(){
        foreach (ReceiveItemIcon receiveItemIcon in receiveItemIcons)
        {
            receiveItemIcon.TakeItem();
        }
        PlayerManager.instance.OverWave(this.numerWaveAvoid);
        BattleManager.instance.ResetBattle();
        gameObject.SetActive(false);
    }
}
