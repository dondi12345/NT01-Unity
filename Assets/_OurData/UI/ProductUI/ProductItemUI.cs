using System.Collections;
using System.Collections.Generic;
using TMPro;

public class ProductItemUI : LoadBehaviour
{
    public ItemName itemName = ItemName.noItem;
    public TextMeshProUGUI number;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if(this.itemName == ItemName.noItem){
            this.itemName = ItemNameParser.FromString(transform.name);
        }
        this.LoadText();
    }

    public void LoadText(){
        this.number = transform.Find("Number").GetComponent<TextMeshProUGUI>();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        this.UpdateData();
    }

    public void UpdateData(){
        try
        {
            this.number.text = ItemManager.instance.GetItemByName(this.itemName).number.ToString();
        }
        catch (System.Exception){}
    }
}
