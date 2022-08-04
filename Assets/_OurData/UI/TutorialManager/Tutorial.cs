using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : LoadBehaviour
{
    public List<Step> steps;
    public Transform transSkip;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSteps();
        this.LoadTransSkip();
    }

    protected virtual void LoadSteps(){
        this.steps.Clear();
        foreach (Transform trans in transform)
        {
            Step step = trans.GetComponent<Step>();
            if(step == null) continue;
            this.steps.Add(step);
        }
    }

    protected virtual void LoadTransSkip(){
        this.transSkip = transform.Find("Skip");
    }

    public virtual IEnumerator StartTutorial(){
        yield return new WaitForSeconds(1f);
    }

    public virtual void EndTutorial(){
        TownTutorialManager.instance.onTutorial = false;
    }

    public virtual void SkipStep(){
        foreach (Step step in this.steps)
        {
            if(step.pass) continue;
            step.OnClick();
        }
    }

    public virtual void OffAllStep(){
        foreach (Step step in this.steps)
        {
            step.gameObject.SetActive(false);
        }
    }

    public virtual void NexStep(Step step){
        step.pass = true;
        int index = this.steps.IndexOf(step);
        if(index == this.steps.Count -1){
            this.EndTutorial();
            return;
        }
        this.OffAllStep();
        this.steps[index+1].gameObject.SetActive(true);
    }
}
