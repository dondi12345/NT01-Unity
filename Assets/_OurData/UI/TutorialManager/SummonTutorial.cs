using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonTutorial : Tutorial
{
    public override IEnumerator StartTutorial(){
        this.OffAllStep();
        gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        if(TownUIManager.instance.warehouseUI.gameObject.activeSelf){
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
        PlayerManager.instance.passSummonTutorial = true;
    }

    public void OpenWarehouseUI(Step step){
        TownUIManager.instance.warehouseUI.OnUI();
        this.NexStep(step);
    }

    public void OpenSummon(Step step){
        TownUIManager.instance.warehouseUI.ChangeSummonBuildingUI();
        this.NexStep(step);
    }

    public void SummonTenTime(Step step){
        TownUIManager.instance.warehouseUI.summonBuildingUI.SummonTenTime();
        this.NexStep(step);
    }

    public void CloseWarehouseUI(Step step){
        TownUIManager.instance.warehouseUI.OffUI();
        TownUIManager.instance.OffAllUI();
        TownTutorialManager.instance.giftTutorial.OnUI();
        this.NexStep(step);
    }

    public void ReciveGiftTutorial(Step step){
        TownTutorialManager.instance.giftTutorial.OffUI();
        this.NexStep(step);
    }
}
