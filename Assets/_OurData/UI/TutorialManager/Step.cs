using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Step : LoadBehaviour
{
    public Button btnStep;
    public bool pass = false;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBtnStep();
    }

    protected void LoadBtnStep(){
        this.btnStep = transform.Find("Button").GetComponent<Button>();
    }

    public void OnClick(){
        this.btnStep.onClick.Invoke();
        this.pass = true;
    }
}
