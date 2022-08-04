using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.SimpleLocalization;

public class MultiLanguage : MonoBehaviour
{
    private void Awake() {
        try
        {
            LocalizationManager.Read();
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e);
        }
        this.SetEnglish();
    }

    public void SetEnglish()
    {
        try
        {
            LocalizationManager.Language = "English";
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e);
        }
    }
    public void SetVietnamese()
    {
        try
        {
            LocalizationManager.Language = "Vietnamese";
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e);
        }
    }
}
