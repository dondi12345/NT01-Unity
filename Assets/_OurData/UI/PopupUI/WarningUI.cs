using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningUI : LoadBehaviour   
{
    public WarningName warningName = WarningName.noWarning;
    public Text textContent;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadText();
    }

    protected void LoadText(){
        this.textContent = transform.Find("Panel").Find("TextContent").GetComponent<Text>();
    }

    public void Close(){
        
        this.OffUI();
    }

    public void OnUI(){
        
        gameObject.SetActive(true);
    }

    public void OffUI(){
        gameObject.SetActive(false);
    }
}
