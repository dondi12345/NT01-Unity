using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LvUI : LoadBehaviour
{
    public TextMeshProUGUI lv;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadText();
    }

    public void LoadText(){
        this.lv = transform.Find("TextLv").GetComponent<TextMeshProUGUI>();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        this.LoadData();
    }

    public void LoadData(){
        this.lv.text = "Lv." +PlayerManager.instance.lv.ToString();
    }
}
