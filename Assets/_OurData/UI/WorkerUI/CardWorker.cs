using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardWorker : LoadBehaviour
{
    public TextMeshProUGUI textLv;

    public ElementCtrl elementCtrl;
    public EvolveCtrl evolveCtrl;

    public Transform statusWoking;

    public Image imageWorker;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadText();
        this.LoadElementCtrl();
        this.LoadTransStatusWoking();
        this.LoadImageWorker();
        this.LoadEvolveCtrl();

    }

    public void LoadText(){
        Transform transTextLv = transform.Find("TextLv");
        if(transTextLv == null) {
            Debug.LogWarning("Can't LoadText");
            return;
        };
        this.textLv = transTextLv.GetComponent<TextMeshProUGUI>();
    }

    public void LoadTransStatusWoking(){
        Transform trans = transform.Find("Status");
        if(trans == null) {
            Debug.LogWarning("Can't LoadTransStatusWoking");
            return;
        }
        this.statusWoking = trans;
    }

    public void LoadEvolveCtrl(){
        Transform evolveCtrl = transform.Find("EvolveCtrl");
        if(evolveCtrl == null) return;
        this.evolveCtrl = evolveCtrl.GetComponent<EvolveCtrl>();
    }
    public void LoadElementCtrl(){
        Transform elementCtrl = transform.Find("ElementCtrl");
        if(elementCtrl == null) return;
        this.elementCtrl = elementCtrl.GetComponent<ElementCtrl>();
    }

    public void LoadImageWorker(){
        Transform imageSoldier = transform.Find("ImageWorker");
        if(imageSoldier == null) return;
        this.imageWorker = imageSoldier.GetComponent<Image>();
    }

    public void UpdateDataByWorker(Worker worker){
        this.textLv.text = "Lv."+worker.lv;
        if(worker.isBattle()){
            this.statusWoking.gameObject.SetActive(true);
        }else{
            this.statusWoking.gameObject.SetActive(false);
        }
        this.elementCtrl.SetElement(worker.elementName);
        this.evolveCtrl.SetEvolveLv(worker.evolveLv);
        
        if(worker.workerCtrl.soldierStats.GetImage() != null)
        this.imageWorker.sprite = worker.workerCtrl.soldierStats.GetImage();
    }

    public void UpdateDataSoldier(Soldier soldier){
        this.textLv.text = "Lv."+soldier.lv;
        if(soldier.isBattle()){
            this.statusWoking.gameObject.SetActive(true);
        }else{
            this.statusWoking.gameObject.SetActive(false);
        }
        this.elementCtrl.SetElement(soldier.elementName);
        this.evolveCtrl.SetEvolveLv(soldier.evolveLv);
        
        if(soldier.soldierCtrl.soldierStats.GetImage() != null)
        this.imageWorker.sprite = soldier.soldierCtrl.soldierStats.GetImage();
    }
}
