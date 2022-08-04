using System;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : LoadBehaviour
{
    public Light directionalLight;
    public bool darkMode = false;

    public List<LightMode> lightModes;

    public static LightManager instance;
    protected override void Awake()
    {
        base.Awake();
        if (LightManager.instance != null) Debug.LogError(transform.name+" Only 1 LightManager allow");
        LightManager.instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadLight();
        this.LoadDirectionalLight();
    }

    protected void LoadDirectionalLight(){
        this.directionalLight = GameObject.FindGameObjectWithTag("DirectionalLight").GetComponent<Light>();
    }

    protected void LoadLight(){
        this.lightModes.Clear();
        LightMode[] lightModes = Resources.FindObjectsOfTypeAll(typeof(LightMode)) as LightMode[];
        foreach (LightMode lightMode in lightModes)
        {
            this.lightModes.Add(lightMode);
        }
    }

    public void UpdateData() {
        // this.darkMode = PlayerManager.instance.darkMode;
        // if(PlayerManager.instance.lv >= 5){
        //     float hour = DateTime.Now.Hour;
        //     if(hour < 6 || hour >= 18) this.darkMode = true;
        // }else{
        //     this.darkMode = false;
        // }
        // this.TurnLight();
    }

    protected void TurnLight(){
        Debug.LogWarning("Dark: "+this.darkMode);
        foreach (LightMode lightMode in this.lightModes)
        {
            lightMode.gameObject.SetActive(this.darkMode);
        }
        if(this.darkMode){
            this.directionalLight.intensity = 0.1f;
            // this.directionalLight.GetComponent<Light>().enabled = false;
        }else{
            this.directionalLight.intensity = 0.9f;
            // this.directionalLight.GetComponent<Light>().enabled = true;
        }
    }
}
