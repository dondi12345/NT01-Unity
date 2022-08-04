using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortColection : LoadBehaviour
{   
    public List<Transform> models;
    public float space = 10;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadModel();
    }

    public void LoadModel(){
        this.models.Clear();
        int current = 0;
        foreach (Transform trans in transform)
        {
            this.models.Add(trans);
            trans.position = new Vector3(transform.position.x + current*space, transform.position.y,transform.position.z);
            current++;
        }
    }
}
