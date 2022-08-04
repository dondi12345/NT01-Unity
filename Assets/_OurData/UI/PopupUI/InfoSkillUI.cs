using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoSkillUI : LoadBehaviour
{
    public Image imageSkill;
    public MultiLanguageText multiLanguageTextDetail;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadImageSkill();
        this.LoadMultiLanguageTextDetail();
    }

    protected void LoadImageSkill(){
        this.imageSkill = transform.Find("Panel").Find("SkillBG").Find("Image").GetComponent<Image>();
    }

    protected void LoadMultiLanguageTextDetail(){
        this.multiLanguageTextDetail = transform.Find("Panel").Find("ImageBGText").Find("TextDetail").GetComponent<MultiLanguageText>();
    }

    public void OnUI(Sprite spriteSkill, string LocalizationKeySkill){
        this.imageSkill.sprite = spriteSkill;
        this.multiLanguageTextDetail.SetLocalizationKey(LocalizationKeySkill);
        gameObject.SetActive(true);
    }

    public void OffUI(){
        gameObject.SetActive(false);
    }
}
