using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : LoadBehaviour
{

    void LateUpdate() {
        this.LookAtCamera();  
    }

    public void LookAtCamera(){
        try
        {
            transform.parent.LookAt(transform.position + CameraManager.instance.GetActiveCam().camera.transform.forward);
        }
        catch (System.Exception)
        {
            Debug.LogWarning("Can't LookAtCamera");
        }
    }
}
