using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShopUI : LoadBehaviour
{

    protected override void LoadComponents()
    {
        base.LoadComponents();
    }

    public void Close(){
        
        this.OffUI();
    }
    
    public void OnUI(){
        
        gameObject.SetActive(true);
    }
    public void OffUI(){
        gameObject.SetActive(false);
    }
}
