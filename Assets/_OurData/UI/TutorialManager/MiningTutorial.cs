using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningTutorial : Tutorial
{

    public override IEnumerator StartTutorial(){
        this.OffAllStep();
        gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        if(TownUIManager.instance.harborUI.gameObject.activeSelf){
            this.steps[1].gameObject.SetActive(true);
        }else{
            TownUIManager.instance.OffAllUI();
            this.steps[0].gameObject.SetActive(true);
        }
        this.transSkip.gameObject.SetActive(true);
    }

    public override void EndTutorial(){
        base.EndTutorial();
        gameObject.SetActive(false);
        PlayerManager.instance.passMiningTutorial = true;
    }

    public void OpenHarborUI(Step step){
        TownUIManager.instance.harborUI.OnUI();
        this.NexStep(step);
    }

    public void OpenMinning(Step step){
        TownUIManager.instance.harborUI.ChangeMiningBuildingUI();
        this.NexStep(step);
    }

    public void MinningTenTime(Step step){
        TownUIManager.instance.harborUI.miningBuildingUI.MiningTenTime();
        this.NexStep(step);
    }

    public void CloseHarborBuildingUI(Step step){
        TownUIManager.instance.harborUI.OffUI();
        TownUIManager.instance.OffAllUI();
        TownTutorialManager.instance.giftTutorial.OnUI();
        this.NexStep(step);
    }

    public void ReciveGiftTutorial(Step step){
        TownTutorialManager.instance.giftTutorial.OffUI();
        this.NexStep(step);
    }
}
