using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffControl : Debuff
{
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if(this.time > 0){
            this.time -= Time.fixedDeltaTime;
        }
        if(this.time <= 0){
            time = 0;
            this.debuffCtrl.UnFreeze();
            gameObject.SetActive(false);
        }
    }
}
