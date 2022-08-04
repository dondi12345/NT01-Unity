using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckboxStatus : LoadBehaviour
{
    public Transform On;
    public Transform Off;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadOn();
        this.LoadOff();
    }

    public void LoadOn(){
        this.On = transform.Find("BtnOn");
    }
    public void LoadOff(){
        this.Off = transform.Find("BtnOff");
    }

    public void Choose(){
        this.On.gameObject.SetActive(true);
        this.Off.gameObject.SetActive(false);
    }

    public void DontChoose(){
        this.On.gameObject.SetActive(false);
        this.Off.gameObject.SetActive(true);
    }
}