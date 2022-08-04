using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.SimpleLocalization;

public class MultiLanguageText : LocalizedText
{
    private void Localize()
    {
        try
        {
            GetComponent<Text>().text = LocalizationManager.Localize(LocalizationKey);
        }
        catch (System.Exception)
        {
            GetComponent<Text>().text = "";
        }
        
    }

    public void SetLocalizationKey(string LocalizationKey){
        this.LocalizationKey = LocalizationKey;
        this.Localize();
    }
}
