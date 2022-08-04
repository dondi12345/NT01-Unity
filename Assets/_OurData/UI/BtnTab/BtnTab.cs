using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnTab : LoadBehaviour
{
    public Transform imageCover;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadImageCover();
    }

    protected void LoadImageCover(){
        this.imageCover = transform.Find("ImageCover");
    }

    public void OnButton(){
        this.imageCover.gameObject.SetActive(false);
    }

    public void OffButton(){
        this.imageCover.gameObject.SetActive(true);
    }

}
