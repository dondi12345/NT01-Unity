using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillWorkerIcon : LoadBehaviour
{
    public Image imageSkill;
    public string LocalizationKeySkill;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadImageSkill();
    }

    protected void LoadImageSkill(){
        this.imageSkill = transform.Find("ImageSkill").GetComponent<Image>();
    }

    public void OnClick(){
        TownUIManager.instance.infoSkillUI.OnUI(this.imageSkill.sprite, this.LocalizationKeySkill);
    }
}
