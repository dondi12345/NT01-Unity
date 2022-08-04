using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInput : LoadBehaviour
{
    public CameraModeCtrl cameraModeCtrl;
    public bool isMouseRotating = false;
    public Vector2 mouseScroll = new Vector2();
    public Vector3 mouseReference = new Vector3();
    public Vector3 mouseRotation = new Vector3();

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        this.InputHandle();
        this.MouseRotation();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadGetModeCtrl();
    }

    protected virtual void LoadGetModeCtrl()
    {
        if (this.cameraModeCtrl != null) return;
        this.cameraModeCtrl = transform.parent.GetComponent<CameraModeCtrl>();
        Debug.Log(transform.name + ": LoadGetModeCtrl", gameObject);
    }

    protected virtual void InputHandle()
    {
        if(!TownUIManager.instance.isUIActive()) return;
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        float y = Input.mouseScrollDelta.y * -1;
        bool leftShift = Input.GetKey(KeyCode.LeftShift);

        this.cameraModeCtrl.cameraMovement.camMovement.x = x;
        this.cameraModeCtrl.cameraMovement.camMovement.z = z;
        this.cameraModeCtrl.cameraMovement.camMovement.y = y;
        this.cameraModeCtrl.cameraMovement.speedShift = leftShift;
    }

    protected virtual void MouseRotation()
    {
        if(!TownUIManager.instance.isUIActive()) return;

        this.isMouseRotating = Input.GetKey(KeyCode.Mouse1);
        if (Input.GetKeyDown(KeyCode.Mouse1)) this.mouseReference = Input.mousePosition;

        if (this.isMouseRotating)
        {
            this.mouseRotation = (Input.mousePosition - this.mouseReference);
            this.mouseRotation.y = -(this.mouseRotation.x + this.mouseRotation.y);
            this.mouseReference = Input.mousePosition;
        }
        else
        {
            this.mouseRotation = Vector3.zero;
        }

        this.cameraModeCtrl.cameraMovement.camRotation.y = this.mouseRotation.x;
    }
}
