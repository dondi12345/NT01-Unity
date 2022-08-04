using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadManager : LoadBehaviour
{
    public static SceneLoadManager instance;

    public Animator animator;
    
    protected override void Awake()
    {
        base.Awake();
        if (SceneLoadManager.instance != null) Debug.LogError("Only 1 SceneLoadManager allow");
        SceneLoadManager.instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAnimator();
    }

    public virtual void LoadAnimator()
    {
        if (this.animator != null) return;
        Transform transAnimator = transform.Find("Canvas").Find("LoadSene");
        Animator animator = transAnimator.GetComponent<Animator>();
        if(animator == null){
            Debug.LogError(transform.name + ": Can't LoadAnimator", gameObject);
            return;
        }
        this.animator = animator;
            
        Debug.Log(transform.name + ": LoadAnimator", gameObject);
    }
}
