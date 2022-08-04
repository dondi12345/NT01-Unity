using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItemTutorial : Tutorial
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
        PlayerManager.instance.passUseItemTutorial = true;
        gameObject.SetActive(false);
    }

    public void OpenBagUI(Step step){
        TownUIManager.instance.itemBagUI.OnUI();
        this.NexStep(step);
    }

    public void OnMoneyItem(Step step){
        TownUIManager.instance.itemBagUI.GetItemIconByName(ItemName.moneyOneHourItem).OnClick();
        this.NexStep(step);
    }

    public void UseItem(Step step){
        TownUIManager.instance.useItemUI.Use();
        TownTutorialManager.instance.giftTutorial.OnUI();
        this.NexStep(step);
    }

    public void ReciveGiftTutorial(Step step){
        TownTutorialManager.instance.giftTutorial.OffUI();
        TownUIManager.instance.itemBagUI.OffUI();
        TownUIManager.instance.OffAllUI();
        this.NexStep(step);
    }

}
