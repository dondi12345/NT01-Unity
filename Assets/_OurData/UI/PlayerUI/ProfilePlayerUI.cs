using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfilePlayerUI : LoadBehaviour
{
    public AvatarPlayerCtrl avatarPlayerCtrl;

    public Text textLv;
    public Slider sliderExp;

    public Slider sliderSound;
    public Slider sliderMusic;

    public Text textPlayerName;

    public PlayerUICtrl playerUICtrl;

    public Transform iconGift;
    public Transform iconGiftReceived;

    public List<LanguageOption>languageOptions;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAvatarPlayerCtrl();
        this.LoadText();
        this.LoadSliderLv();
        this.LoadSliderSound();
        this.LoadIconGift();
        this.LoadPlayerUICtrl();
        this.LoadLanguageOption();
    }

    protected void LoadAvatarPlayerCtrl(){
        this.avatarPlayerCtrl = transform.Find("Panel").Find("AvatarPlayerCtrl").GetComponent<AvatarPlayerCtrl>();
    }

    protected void LoadText(){
        this.textLv = transform.Find("Panel").Find("TextLv").GetComponent<Text>();
        this.textPlayerName = transform.Find("Panel").Find("PlayerName").Find("TextPlayerName").GetComponent<Text>();
    }

    protected void LoadSliderLv(){
        this.sliderExp = transform.Find("Panel").Find("SliderExp").GetComponent<Slider>();
    }
    protected void LoadSliderSound(){
        this.sliderSound = transform.Find("Panel").Find("SliderSound").GetComponent<Slider>();
        this.sliderMusic = transform.Find("Panel").Find("SliderMusic").GetComponent<Slider>();
    }
    protected void LoadPlayerUICtrl(){
        this.playerUICtrl = transform.parent.GetComponent<PlayerUICtrl>();
    }
    protected void LoadIconGift(){
        this.iconGift = transform.Find("Panel").Find("IconGift");
        this.iconGiftReceived = transform.Find("Panel").Find("IconGiftReceived");
    }
    protected void LoadLanguageOption(){
        this.languageOptions.Clear();
        Transform transLanguageOptions = transform.Find("Panel").Find("LanguageOption");
        foreach (Transform trans in transLanguageOptions)
        {
            this.languageOptions.Add(trans.GetComponent<LanguageOption>());
        }
    }

    public void UpdatData(){
        this.UpdateText();
        this.UpdateSliderExp();
        this.UpdateAvatarPlayer();
        this.UpdateSliderSoud();
        this.UpdateIconGift();
        this.UpdateLanguageOption();
    }

    public void UpdateText(){
        this.textLv.text = "Lv." + PlayerManager.instance.lv.ToString();
        this.textPlayerName.text = PlayerManager.instance.playerName.ToString();
    }

    public void UpdateSliderExp(){
        this.sliderExp.value = PlayerManager.instance.experience/PlayerManager.instance.experienceNeedLvUp;
    }
    public void UpdateSliderSoud(){
        this.sliderSound.value = PlayerManager.instance.sound;
        this.sliderMusic.value = PlayerManager.instance.music;
    }

    public void UpdateAvatarPlayer(){
        this.avatarPlayerCtrl.SetAvatarPlayer(PlayerManager.instance.avatarPlayerName);
    }

    public void UpdateIconGift(){
        if(PlayerManager.instance.isDailyGiftPlayerLevel){
            this.iconGiftReceived.gameObject.SetActive(true);
            this.iconGift.gameObject.SetActive(false);
        }else{
            this.iconGiftReceived.gameObject.SetActive(false);
            this.iconGift.gameObject.SetActive(true);
        }
    }

    public void UpdateLanguageOption(){
        foreach (LanguageOption languageOption in this.languageOptions)
        {
            if(languageOption.multiLanguageName == PlayerManager.instance.multiLanguageName){
                languageOption.OnTriggle();
            }else{
                languageOption.OffTriggle();
            }
        }
    }

    public void UpdateSound(){
        PlayerManager.instance.sound = this.sliderSound.value;
        SoundManager.instance.UpdateSoud();
    }

    public void UpdateMusic(){
        PlayerManager.instance.music = this.sliderMusic.value;
        SoundManager.instance.UpdatMusic();
    }

    public void OnChangeNameUI(){
        this.playerUICtrl.changeNameUI.OnUI(this);
    }

    public void OnDailyGiftPlayerLevel(){
        this.playerUICtrl.dailyGiftPlayerLevel.OnUI(this);
    }

    public void OnChangeAvatarPlayerUI(){
        this.playerUICtrl.changeAvatarPlayerUI.OnUI();
    }

    public void SetEnglishLanguage(Toggle toggle){
        if(!toggle.isOn) return;
        LanguageManager.instance.multiLanguage.SetEnglish();
        PlayerManager.instance.multiLanguageName = MultiLanguageName.English;
    }
    public void SetVietnameseLanguage(Toggle toggle){
        if(!toggle.isOn) return;
        LanguageManager.instance.multiLanguage.SetVietnamese();
        PlayerManager.instance.multiLanguageName = MultiLanguageName.Vietnamese;
    }

    public void Close(){
        
        this.OffUI();
    }

    public void OnUI(){
        
        this.UpdatData();
        gameObject.SetActive(true);
    }
    public void OffUI(){
        gameObject.SetActive(false);
    }
}
