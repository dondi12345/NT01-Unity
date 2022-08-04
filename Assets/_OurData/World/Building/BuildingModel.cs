using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingModel : LoadBehaviour
{
    public List<ModelLv> modelLvs; 
    protected override void LoadComponents()
    {
        this.LoadModelLvs();
    }

    protected void LoadModelLvs(){
        this.modelLvs.Clear();
        foreach (Transform trans in transform)
        {
            ModelLv modelLv = trans.GetComponent<ModelLv>();
            if(modelLv == null) continue;
            this.modelLvs.Add(modelLv);
        }
    }

    public void SetModelLv(int lv){
        return;
        for (int i = 0; i < this.modelLvs.Count; i++)
        {
            if(i> lv){
                this.modelLvs[i].gameObject.SetActive(false);
            }else{
                this.modelLvs[i].gameObject.SetActive(true);
            }
        }
    }
}
