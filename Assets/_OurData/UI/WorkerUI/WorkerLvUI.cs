using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WorkerLvUI : LoadBehaviour
{
    public WorkerInfoUI workerInfoUI;

    public Text textLv;
    public Text textAtk;
    public Text textDef;
    public Text textHp;

    public TextMeshProUGUI textCostUpgrade;

    public Transform transSkills;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWorkerInfoUI();
        this.LoadText();
        this.LoadTransSkills();
    }

    protected void LoadWorkerInfoUI(){
        this.workerInfoUI = transform.parent.GetComponent<WorkerInfoUI>();
    }

    public void LoadText(){
        this.textLv = transform.Find("Panel").Find("TextLv").GetComponent<Text>();    

        this.textAtk = transform.Find("Panel").Find("TextAtk").GetComponent<Text>();    
        this.textDef = transform.Find("Panel").Find("TextDef").GetComponent<Text>();    
        this.textHp = transform.Find("Panel").Find("TextHp").GetComponent<Text>();  

        this.textCostUpgrade = transform.Find("Panel").Find("TextCostUpgrade").GetComponent<TextMeshProUGUI>();     
    }

    public void LoadTransSkills(){
        this.transSkills = transform.Find("Panel").Find("Skills");
    }

    public void UpdateData(){
        this.UpdateText();
        this.UpdateSkill();
    }

    public void UpdateText(){
        this.textLv.text = "Lv."+this.workerInfoUI.workerIcon.worker.lv;
        this.textAtk.text = this.workerInfoUI.workerIcon.worker.GetAtk()+"";
        this.textDef.text = this.workerInfoUI.workerIcon.worker.GetDef()+"";
        this.textHp.text = this.workerInfoUI.workerIcon.worker.GetHp()+"";
        this.textCostUpgrade.text = NumberForm.ToString(this.workerInfoUI.workerIcon.worker.experienceNeedLvUp)+"<sprite=0>";
    }

    public void UpdateSkill(){
        this.ClearSkill();
        Sprite imageActiveSkill = this.workerInfoUI.workerIcon.worker.workerCtrl.soldierStats.GetImageActiveSkill();
        if(imageActiveSkill != null){
            SkillWorkerIcon skillWorkerIcon = Instantiate<GameObject>(this.workerInfoUI.skillWorkerGO).GetComponent<SkillWorkerIcon>();
            skillWorkerIcon.imageSkill.sprite = imageActiveSkill;
            skillWorkerIcon.LocalizationKeySkill = this.workerInfoUI.workerIcon.worker.workerCtrl.soldierStats.GetLocalizationKeyActiveSkill();
            skillWorkerIcon.transform.SetParent(this.transSkills);
            skillWorkerIcon.transform.localScale = new Vector3(1,1,1);
        }
    }

    public void ClearSkill(){
        foreach (Transform trans in this.transSkills)
        {
            trans.gameObject.SetActive(false);
            Destroy(trans.gameObject);
        }
    }

    public void UpLv(){
        
        this.workerInfoUI.workerIcon.worker.UpLv();
        this.workerInfoUI.UpdateData();
        this.UpdateData();
    }

    public void Close(){
        
        this.OffUI();
    }

    public void OnUI(){
        
        gameObject.SetActive(true);
        this.UpdateData();
    }

    public void OffUI(){
        gameObject.SetActive(false);
    }

}
