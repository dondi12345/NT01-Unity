using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : LoadBehaviour
{
    public AudioSource soundtrack;

    public Transform soundHolder;

    public List<SoundMode> soundEffectCollection;

    public static SoundManager instance;
    protected override void Awake()
    {
        base.Awake();
        if (SoundManager.instance != null) Debug.LogError(transform.name+" Only 1 SoundManager allow");
        SoundManager.instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAudioSource();
        this.LoadSoundMode();
        this.LoadSoundHolder();
    }

    protected void LoadAudioSource(){
        this.soundtrack = transform.Find("BackgroundMusic").Find("Soundtrack").GetComponent<AudioSource>();
    }

    protected void LoadSoundMode(){
        this.soundEffectCollection.Clear();
        Transform transSoundEffectCollection = transform.Find("SoundEffectCollection");
        foreach (Transform trans in transSoundEffectCollection)
        {
            SoundMode soundMode = trans.GetComponent<SoundMode>();
            if(soundMode == null) continue;
            this.soundEffectCollection.Add(soundMode);
        }
    }

    protected void LoadSoundHolder(){
        this.soundHolder = transform.Find("SoundHolder");
    }

    public void UpdatData(){
        this.UpdatMusic();
        this.UpdateSoud();
    }

    public void UpdatMusic(){
        this.soundtrack.volume = PlayerManager.instance.music;
    }

    public void UpdateSoud(){
        foreach (SoundMode soundMode in this.soundEffectCollection)
        {
            AudioSource audioSource = soundMode.GetComponent<AudioSource>();
            if(audioSource == null) continue;
            audioSource.volume = PlayerManager.instance.sound;
        }
    }

    void OnGUI()
    {
        Event m_Event = Event.current;

        if (m_Event.type == EventType.MouseDown)
        {
            SoundManager.instance.OnSoundByName(SoundName.soundClick);
        }
    }

    public void OnSoundByName(SoundName soundName){
        if(soundName == SoundName.noSound) return;
        SoundMode soundMode = this.GetSoundEffectCollectionByName(soundName);
        GameObject soundModeGO = Instantiate<GameObject>(soundMode.gameObject);
        soundModeGO.transform.parent = this.soundHolder;
    }

    public SoundMode GetSoundEffectCollectionByName(SoundName soundName){
        return this.soundEffectCollection.Find((soundMode) =>(soundMode.soundName == soundName));
    }
}
