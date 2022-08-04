using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpWarehouseTutorial : Tutorial
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
        PlayerManager.instance.passUpWarehouseTutorial = true;
        gameObject.SetActive(false);
    }

    public void OpenWarehouseUI(Step step){
        TownUIManager.instance.warehouseUI.OnUI();
        this.NexStep(step);
    }

    public void ExpandWarehouseUI(Step step){
        TownUIManager.instance.warehouseUI.warehouseBuildingUI.ExpandLv();
        TownTutorialManager.instance.giftTutorial.OnUI();
        this.NexStep(step);
    }

    public void ReciveGiftTutorial(Step step){
        TownTutorialManager.instance.giftTutorial.OffUI();
        this.NexStep(step);
    }

}
