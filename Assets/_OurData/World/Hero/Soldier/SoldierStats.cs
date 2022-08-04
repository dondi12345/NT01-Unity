using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierStats : LoadBehaviour
{

    public SoldierSO soldierSO;

    public string GetName(){
        return this.soldierSO.nameSoldier;
    }

    public Sprite GetImage(){
        return this.soldierSO.image;
    }

    public float GetAtk(){
        return this.soldierSO.atk * (1+this.soldierSO.atkBuffPercent);
    }
    public float GetDef(){
        return this.soldierSO.def * (1+this.soldierSO.defBuffPercent);
    }
    public float GetHp(){
        return this.soldierSO.hp * (1+this.soldierSO.hpBuffPercent);
    }
    public float GetAttackDelay(){
        return this.soldierSO.attackDelay / (1+this.soldierSO.attackDelayBuffPercent);
    }
    public float GetAttackRange(){
        return this.soldierSO.attackRange;
    }

    public Sprite GetImageActiveSkill(){
        return this.soldierSO.imageActiveSkill;
    }
    public string GetLocalizationKeyActiveSkill(){
        return this.soldierSO.LocalizationKeyActiveSkill;
    }
}
