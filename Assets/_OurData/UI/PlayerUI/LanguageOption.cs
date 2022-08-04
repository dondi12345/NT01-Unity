using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageOption : LoadBehaviour
{
    public MultiLanguageName multiLanguageName;

    public Toggle toggle;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadToggle();
    }

    protected void LoadToggle(){
        this.toggle = transform.GetComponent<Toggle>();
    }

    public void OnTriggle(){
        this.toggle.isOn = true;
    }

    public void OffTriggle(){
        this.toggle.isOn = false;
    }
}
