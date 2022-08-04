using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeModel : LoadBehaviour
{
    public List<Transform> transformModel;
    public int currentModel = 0;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTransformModel();
    }

    private void LoadTransformModel(){
        this.transformModel.Clear();
        foreach (Transform trans in transform)
        {
            this.transformModel.Add(trans);
        }
    }

    public void NextModel(){
        if(this.transformModel.Count == 0) return;
        this.currentModel += 1;
        if(this.currentModel >= this.transformModel.Count) this.currentModel = 0;
        this.OffAll();
        this.transformModel[this.currentModel].gameObject.SetActive(true);
    }

    public void BackModel(){
        if(this.transformModel.Count == 0) return;
        this.currentModel -= 1;
        if(this.currentModel < 0 ) this.currentModel = this.transformModel.Count -1;
        this.OffAll();
        this.transformModel[this.currentModel].gameObject.SetActive(true);
    }

    public void ResetModel(){
        this.transformModel[this.currentModel].gameObject.SetActive(false);
        this.transformModel[this.currentModel].gameObject.SetActive(true);
    }

    public void OffAll(){
        foreach (Transform trans in transformModel)
        {
            trans.gameObject.SetActive(false);
        }
    }


}
