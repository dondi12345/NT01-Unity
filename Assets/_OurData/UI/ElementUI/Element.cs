using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : LoadBehaviour
{
    public ElementName elementName;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        try
        {
            this.elementName = ElementNameParser.FromString(transform.name);
        }
        catch (System.Exception)
        {
            this.elementName = ElementName.noElement;
        }
    }
}
