using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraModeCtrl : LoadBehaviour
{
    public CameraMovement cameraMovement;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCameraMovement();
    }

    protected virtual void LoadCameraMovement()
    {
        if (this.cameraMovement != null) return;
        this.cameraMovement = transform.Find("CameraMovement").GetComponent<CameraMovement>();
        Debug.Log(transform.name + ": LoadCameraMovement", gameObject);
    }
}
