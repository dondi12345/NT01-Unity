using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMode : LoadBehaviour
{
    public SoundName soundName = SoundName.noSound;

    public bool isDestroy = false;
    public float timeDestroy = 1f; 

    protected override void LoadComponents()
    {
        base.LoadComponents();
        try
        {
            if(soundName == SoundName.noSound){
                this.soundName = SoundNameParser.FromString(transform.name);
            }
        }
        catch (System.Exception){}
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        if(isDestroy) this.DestroySound();
    }

    public void DestroySound(){
        Destroy(gameObject,this.timeDestroy);
    }
}
