using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationUI : LoadBehaviour
{
    public Transform transImage;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTransImage();
    }

    protected void LoadTransImage(){
        this.transImage = transform.Find("Image");
    }
}
