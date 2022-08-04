using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SoldierBagUI : LoadBehaviour
{
    public TextMeshProUGUI textPower;


    public GameObject defaultSoldierIcon;
    public Transform contentSoldierIcons;

    protected override void FixedUpdate() {
        this.textPower.text = NumberForm.ToString(BattleManager.instance.powerAlly);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadDefaultSoldierIcon();
        this.LoadContentSoldierIcon();
        this.LoadTextPower();
    }

    public void LoadDefaultSoldierIcon(){
        Transform trans = transform.parent.parent.Find("SoldierUI").Find("Collections").Find("SoldierIcon");
        if(trans == null){
            Debug.LogWarning(transform.name + ": Not LoadDefaultSoldierIcon", gameObject);
            return;
        }
        this.defaultSoldierIcon = trans.gameObject;
        Debug.Log(transform.name + ": LoadDefaultSoldierIcon", gameObject);
    }

    public void LoadContentSoldierIcon(){
        Transform trans = transform.Find("Panel").Find("Soldiers").Find("ScrollView").Find("Viewport").Find("Content");
        if(trans == null) return;

        this.contentSoldierIcons = trans;
        Debug.Log(transform.name + ": LoadContentSoldierIcon", gameObject);
    }

    public void LoadTextPower(){
        this.textPower = transform.Find("Power").Find("TextMeshNumber").GetComponent<TextMeshProUGUI>();
    }

    public void UpdatData(){
        this.ReloadSoldierIconBySoldier();
    }

    public void ReloadSoldierIconBySoldier(){
        this.ClearSoldierIcon();
        foreach (Soldier soldier in SoldierManager.instance.allies)
        {
            SoldierIcon soldierIcon = Instantiate<GameObject>(this.defaultSoldierIcon).transform.GetComponent<SoldierIcon>();
            soldierIcon.transform.SetParent(this.contentSoldierIcons);
            soldierIcon.soldier = soldier;
            soldierIcon.transform.localScale = new Vector3(1,1,1);
            soldierIcon.UpdateData();
            soldierIcon.gameObject.SetActive(true);
        }
    }

    //Function

    public void CallBackAll(){
        
        PositionManager.instance.CallBackAllToBag();
    }

    public void ClearSoldierIcon(){
        foreach (Transform trans in contentSoldierIcons)
        {
            trans.gameObject.SetActive(false);
            Destroy(trans.gameObject);
        }
    }

    public void Close(){
        
        this.OffUI();
    }

    public void OnUI(){
        
        this.transform.gameObject.SetActive(true);
        this.UpdatData();
    }
    public void OffUI(){
        this.transform.gameObject.SetActive(false);
    }
}
