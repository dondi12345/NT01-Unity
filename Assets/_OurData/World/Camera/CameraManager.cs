using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : LoadBehaviour
{
    public Camera camera;

    public List<CameraMode> cams;

    public static CameraManager instance;

    protected override void Awake()
    {
        base.Awake();
        if (CameraManager.instance != null) Debug.LogError("Only 1 CameraManager allow");
        CameraManager.instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCam();
    }

    public void LoadCam(){
        foreach (Transform transCam in transform)
        {
            CameraMode cam = transCam.GetComponent<CameraMode>();
            if(cam == null) continue;
            
            this.cams.Add(cam);
        }
    }

    public void ChangeCam(CamName camName){
        this.OffAllCam();
        this.cams.Find((cam) => (cam.camName == camName)).gameObject.SetActive(true);
    }

    public void OffAllCam(){
        foreach (CameraMode cam in this.cams)
        {
            cam.gameObject.SetActive(false);
        }
    }

    public CameraMode GetActiveCam(){
        foreach (CameraMode cam in this.cams)
        {
            if(cam.gameObject.activeSelf){
                return cam;
            }
        }
        return null;
    }
}
