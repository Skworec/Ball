using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundChange_Handler : MonoBehaviour
{
    [SerializeField]
    private AudioClip onCompleteClip;
    [SerializeField]
    private AudioClip onFailedClip;
    [SerializeField]
    private Text text;
    void Start()
    {
        text.text = DataController.instance.IsSoundEnabled() ? "Sound: On" : "Sound: Off";
        DataController.instance.onVolumeStateChange.AddListener(OnVolumeStateChanged);
        LevelController.onLevelComplete.AddListener(OnLevelCompletHandler);
        LevelController.onLevelFailed.AddListener(OnLevelFailedHandler);
    }

    public void OnVolumeStateChanged()
    {
        text.text = DataController.instance.IsSoundEnabled() ? "Sound: On" : "Sound: Off";
    }

    public void ChangeSoundState()
    {
        DataController.instance.SwitchSoundState();
    }
    public void OnLevelCompletHandler()
    {
        AudioSource src = DataController.instance.GetComponent<AudioSource>();
        src.clip = onCompleteClip;
        src.loop = false;
        src.Play();
    }
    public void OnLevelFailedHandler()
    {
        AudioSource src = DataController.instance.GetComponent<AudioSource>();
        src.clip = onFailedClip;
        src.loop = false;
        src.Play();
    }
}
