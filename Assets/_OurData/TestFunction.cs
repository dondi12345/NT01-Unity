using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFunction : LoadBehaviour
{

    public void RunFunction(){
        this.CopyPosition();
    }


    public Transform transNew;
    public Transform transOld;
    public void CopyPosition(){
        List<Transform> listTransNew = new List<Transform>();
        List<Transform> listTransOld = new List<Transform>();
        foreach (Transform trans in transNew)
        {
            listTransNew.Add(trans);
        }
        foreach (Transform trans in transOld)
        {
            listTransOld.Add(trans);
        }

        int count = listTransNew.Count;
        for (int i = 0; i < count; i++)
        {
            listTransNew[i].position = listTransOld[i].position;
            listTransNew[i].rotation = listTransOld[i].rotation;
        }
    }
}
