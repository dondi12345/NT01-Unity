using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuff : LoadBehaviour
{
    public DebuffName debuffName = DebuffName.noDebuff;

    public DebuffCtrl debuffCtrl;

    public float time = 0;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if(this.debuffName == DebuffName.noDebuff){
            this.debuffName = DebuffNameParser.FromString(transform.name);
        }
        this.LoadDebuffCtrl();
    }

    protected virtual void LoadDebuffCtrl(){
        DebuffCtrl debuffCtrl = transform.parent.GetComponent<DebuffCtrl>();
        if(debuffCtrl == null){
            Debug.LogWarning(transform.name +": "+ "Can't LoadDebuffCtrl");
            return;
        }
        this.debuffCtrl = debuffCtrl;
        Debug.Log(transform.name + ": LoadDebuffCtrl");
    }
}
