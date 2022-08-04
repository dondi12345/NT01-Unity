using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : LoadBehaviour
{
    public float speed = 27f;
    public bool speedShift = false;
    public float minY = 4f;
    public float maxY = 70f;
    public Vector3 camRotation = new Vector3(0, 0, 0);
    public Vector3 camMovement = new Vector3(0, 0, 0);
    public Vector3 camView = new Vector3(45f, 0, 0);

    public CameraModeCtrl cameraModeCtrl;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        this.Moving();
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

    protected virtual void Moving()
    {
        float speed = this.speed;
        if (this.speedShift) speed += this.speed * 2;

        Vector3 movement = this.camMovement;
        movement.x *= speed;
        movement.z *= speed;
        movement.y *= speed * 7;

        cameraModeCtrl.transform.Translate(movement * Time.deltaTime);
        Vector3 newPos = cameraModeCtrl.transform.position;

        if (newPos.y < this.minY) newPos.y = this.minY;
        if (newPos.y > this.maxY) newPos.y = this.maxY;
        cameraModeCtrl.transform.position = newPos;

        this.cameraModeCtrl.transform.parent.Rotate(this.camRotation);
    }
}
