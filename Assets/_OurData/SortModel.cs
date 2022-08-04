using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortModel : LoadBehaviour
{
    public List<Transform> transformModel;
    public GameObject modelSample;
    public Transform content;
    public float widthSize = 0;
    public float heighSize = 0;

    public int widthNumber = 0;
    public int heighNumber = 0;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadContent();
        this.LoadTransformModel();
    }

    public void LoadTransformModel(){
        this.transformModel.Clear();
        foreach (Transform trans in this.content)
        {
            this.transformModel.Add(trans);
        }
    }

    public void LoadContent(){
        this.content = transform.Find("Content");
    }

    public void SortPositionModel(){
        if(this.modelSample == null) return;
        this.ClearModel();

        for (int i = 0; i < this.widthNumber; i++)
        {
            for (int j = 0; j < this.heighNumber; j++)
            {   
                GameObject model = Instantiate<GameObject>(this.modelSample);
                model.transform.SetParent(this.content);
                model.transform.position =  new Vector3(transform.position.x+i*this.widthSize, transform.position.y ,transform.position.z+ j*heighSize);
                this.transformModel.Add(model.transform);
            }
        }
    }

    public void ClearModel(){
        foreach (Transform trans in this.transformModel)
        {
            DestroyImmediate(trans.gameObject);
        }
        this.transformModel.Clear();
    }
}