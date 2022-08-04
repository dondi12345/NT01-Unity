using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteModelOff : LoadBehaviour
{
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.DeleteModel();
    }

    private void DeleteModel(){
        foreach (Transform trans in transform)
        {
            foreach (Transform tran in trans)
            {
                if(!tran.gameObject.activeSelf){
                    DestroyImmediate(tran.gameObject);
                }
            }
        }
    }
}
