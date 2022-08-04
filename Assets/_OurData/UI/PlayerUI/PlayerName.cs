using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerName : LoadBehaviour
{
    public Text playerName;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadText();
    }

    public void LoadText(){
        this.playerName = transform.Find("TextPlayerName").GetComponent<Text>();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        this.LoadData();
    }

    public void LoadData(){
        this.playerName.text = PlayerManager.instance.playerName;
    }
}
