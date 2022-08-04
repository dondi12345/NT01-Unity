using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvolveCtrl : LoadBehaviour
{
    public List<EvolveLv> evolveLvs;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEvolveLv();
    }

    protected void LoadEvolveLv(){
        this.evolveLvs.Clear();
        foreach (Transform trans in transform)
        {
            EvolveLv evolveLv = trans.GetComponent<EvolveLv>();
            if(evolveLv == null) continue;
            this.evolveLvs.Add(evolveLv);
        }
    }

    public void SetEvolveLv(int lv){
        this.OffAllEvolveLv();
        EvolveLv evolveLv = this.evolveLvs.Find((evolveLv) => (evolveLv.lv == lv));
        if(evolveLv == null){
            this.evolveLvs[this.evolveLvs.Count-1].gameObject.SetActive(true);
        }else evolveLv.gameObject.SetActive(true);
    }

    protected void OffAllEvolveLv(){
        foreach (EvolveLv evolveLv in this.evolveLvs)
        {
            evolveLv.gameObject.SetActive(false);
        }
    }
}
