using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeNameUI : LoadBehaviour
{

    public InputField textInput;

    public ProfilePlayerUI profilePlayerUI;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTextInput();
    }

    protected void LoadTextInput(){
        this.textInput = transform.Find("Panel").Find("InputName").GetComponent<InputField>();
    }

    public void Confirm(){
        
        if(textInput.text.Length < 6) return;
        PlayerManager.instance.playerName = this.textInput.text;
        this.profilePlayerUI.UpdatData();
        this.OffUI();
    }

    public void Close(){
        
        this.OffUI();
    }

    public void OnUI(ProfilePlayerUI profilePlayerUI){
        
        this.profilePlayerUI = profilePlayerUI;
        gameObject.SetActive(true);
        this.textInput.text = "";
    }

    public void OffUI(){
        gameObject.SetActive(false);
    }
}
