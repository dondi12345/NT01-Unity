using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePrefab : LoadBehaviour
{
    public List<Transform> listModel;
    public GameObject modelSample;

    public void Change(){
        if(this.modelSample == null) return;
        if(this.listModel.Count == 0) return;
        foreach (Transform trans in this.listModel)
        {
            foreach (Transform tran in trans)
            {
                this.ClearModel(tran);
                
                GameObject model = Instantiate<GameObject>(this.modelSample);
                model.transform.SetParent(tran);
                model.transform.localPosition =  Vector3.zero;
                model.transform.localRotation = Quaternion.Euler(0,0,0);
            }
        }
    }

    public void ClearModel(Transform trans){
        foreach (Transform tran in trans)
        {
            DestroyImmediate(tran.gameObject);
        }
    }
}
