using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : LoadBehaviour
{

    public MultiLanguage multiLanguage;

    public static LanguageManager instance;
    protected override void Awake()
    {
        base.Awake();
        if (LanguageManager.instance != null) Debug.LogError(transform.name+" Only 1 LanguageManager allow");
        LanguageManager.instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMultiLanguage();
    }

    protected void LoadMultiLanguage(){
        this.multiLanguage = transform.Find("MultiLanguage").GetComponent<MultiLanguage>();
    }

    public void UpdateData(MultiLanguageName multiLanguageName){
        if(multiLanguageName == MultiLanguageName.English){
            this.multiLanguage.SetEnglish();
            return;
        }
        if(multiLanguageName == MultiLanguageName.Vietnamese){
            this.multiLanguage.SetVietnamese();
            return;
        }
    }
}
