using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpBuildingTutorial : Tutorial
{
    public override IEnumerator StartTutorial(){
        TownUIManager.instance.OffAllUI();
        this.OffAllStep();
        gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        this.steps[0].gameObject.SetActive(true);
        this.transSkip.gameObject.SetActive(true);
    }

    public override void EndTutorial(){
        base.EndTutorial();
        gameObject.SetActive(false);
        PlayerManager.instance.passUpBuildingTutorial = true;
    }

    public void OpenHarborUI(Step step){
        TownUIManager.instance.harborUI.OnUI();
        this.NexStep(step);
    }

    public void UpgradeHarborBuildingUI(Step step){
        TownUIManager.instance.harborUI.harborBuildingUI.UpgradeLv();
        this.NexStep(step);
    }
    public void ExpandHarborBuildingUI(Step step){
        TownUIManager.instance.harborUI.harborBuildingUI.ExpandLv();
        TownTutorialManager.instance.giftTutorial.OnUI();
        this.NexStep(step);
    }

    public void ReciveGiftTutorial(Step step){
        TownTutorialManager.instance.giftTutorial.OffUI();
        this.NexStep(step);
    }
}
