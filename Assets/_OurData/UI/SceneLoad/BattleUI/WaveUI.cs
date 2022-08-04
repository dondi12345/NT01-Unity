using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveUI : LoadBehaviour
{
    public Text numberWave;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadText();
    }

    public void LoadText(){
        this.numberWave = transform.Find("Text").GetComponent<Text>();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        this.UpdateData();
    }

    public void UpdateData(){
        this.numberWave.text = "Wave "+NumberForm.ToString(PlayerManager.instance.waveLv + BattleManager.instance.nextLvWave);
    }
}
