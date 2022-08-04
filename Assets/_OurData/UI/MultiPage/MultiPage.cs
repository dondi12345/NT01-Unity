using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiPage : LoadBehaviour
{
    
    public List<Page> pages;
    public List<BtnPage> btnPages;

    public int currentPage = 1;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPage();
        this.LoadBtnPage();
    }

    public void LoadPage(){
        this.pages.Clear();

        Transform transPage = transform.Find("Pages");
        foreach (Transform trans in transPage)
        {
            Page page = trans.GetComponent<Page>();
            if(page != null){
                this.pages.Add(page);
            }
        }
    }
    public void LoadBtnPage(){
        this.btnPages.Clear();

        Transform transBtnPage = transform.Find("BtnPages");
        foreach (Transform trans in transBtnPage)
        {
            BtnPage btnPage = trans.GetComponent<BtnPage>();
            if(btnPage != null){
                btnPage.multiPage = this;
                this.btnPages.Add(btnPage);
            }
        }
    }

    public void FirstPage(){
        this.OffAllPage();
        this.OffAllBtn();

        this.currentPage = pages[0].number;
        pages[0].gameObject.SetActive(true);

        BtnPage btnPage = this.btnPages.Find((btnPages)=>btnPages.number == currentPage);
        btnPage.OnBtn();
    }

    public void ChangePage(int number){
        this.OffAllPage();
        this.OffAllBtn();

        this.currentPage = number;

        BtnPage btnPage = this.btnPages.Find((btnPages)=>btnPages.number == number);
        btnPage.OnBtn();

        Page page = this.pages.Find((page) => page.number == number);
        page.gameObject.SetActive(true);
    }

    public void OffAllPage(){
        foreach (Page page in this.pages)
        {
            page.gameObject.SetActive(false);
        }
    }

    public void OffAllBtn(){
        foreach (BtnPage btnPage in this.btnPages)
        {
            btnPage.OffBtn();
        }
    }
}
