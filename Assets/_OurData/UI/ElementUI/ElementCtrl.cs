using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementCtrl : LoadBehaviour
{
    public List<Element> elements;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadElements();
    }

    private void LoadElements(){
        this.elements.Clear();
        foreach (Transform trans in transform)
        {
            Element element = trans.GetComponent<Element>();
            this.elements.Add(element);
        }
    }

    public void SetElement(ElementName elementName){
        this.OffAllElement();
        Element element = this.elements.Find((element)=>(element.elementName == elementName));
        if(element == null) return;
        element.gameObject.SetActive(true);
    }

    private void OffAllElement(){
        foreach (Element element in this.elements)
        {
            element.gameObject.SetActive(false);
        }
    }
}
