using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnPage : LoadBehaviour
{
    public MultiPage multiPage;
    public int number = 1;

    public Transform btnCover;

    public void OnClick(){
        multiPage.ChangePage(this.number);
    }

    public void OffBtn(){
        this.btnCover.gameObject.SetActive(true);
    }

    public void OnBtn(){
        this.btnCover.gameObject.SetActive(false);
    }
}
