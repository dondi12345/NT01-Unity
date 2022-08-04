using System.Collections;
using System.Collections.Generic;
using TMPro;

public class PowerUI : LoadBehaviour
{
    public TextMeshProUGUI numberPowerAlly;
    public TextMeshProUGUI numberPowerEnemy;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadText();
    }

    public void LoadText(){
        this.numberPowerAlly = transform.Find("AllyPower").Find("TextMeshNumber").GetComponent<TextMeshProUGUI>();
        this.numberPowerEnemy = transform.Find("EnemyPower").Find("TextMeshNumber").GetComponent<TextMeshProUGUI>();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        this.UpdateData();
    }

    public void UpdateData(){
        this.numberPowerAlly.text = NumberForm.ToString(BattleManager.instance.powerAlly);
        this.numberPowerEnemy.text = NumberForm.ToString(BattleManager.instance.powerEnemy);
    }
}
