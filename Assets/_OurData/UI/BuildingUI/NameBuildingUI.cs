using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameBuildingUI : LoadBehaviour
{
    public Transform buildingNameTrans;
    public Transform buildingLockTrans;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBuildingLockTrans();
        this.LoadBuildingNameTrans();

    }

    private void LoadBuildingNameTrans(){
        this.buildingNameTrans = transform.Find("NameBuilding");
    }
    private void LoadBuildingLockTrans(){
        this.buildingLockTrans = transform.Find("LockBuilding");
    }

    public void Unlock(){
        this.buildingLockTrans.gameObject.SetActive(false);
        this.buildingNameTrans.gameObject.SetActive(true);
    }
    public void Lock(){
        this.buildingLockTrans.gameObject.SetActive(true);
        this.buildingNameTrans.gameObject.SetActive(false);
    }
}
