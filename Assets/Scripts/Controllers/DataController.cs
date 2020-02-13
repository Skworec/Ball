using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DataController : MonoBehaviour
{
    public static DataController instance = null;
    [SerializeField] private bool isSoundEnabled;
    public int reachedLevel;

    public UnityEvent onVolumeStateChange = new UnityEvent();
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        InitialiseManager();
    }
    private void InitialiseManager()
    {
        isSoundEnabled = IsSoundEnabled();
        reachedLevel = ReachedLevel();
    }
    #region PlayerPrefs
    public void SwitchSoundState()
    {
        isSoundEnabled = !isSoundEnabled;
        PlayerPrefs.SetInt("isSoundEnabled", isSoundEnabled ? 1 : 0);
        if (isSoundEnabled && !gameObject.GetComponent<AudioSource>().isPlaying)
        {
            gameObject.GetComponent<AudioSource>().mute = false;
        }
        else
        {
            gameObject.GetComponent<AudioSource>().mute = true;
        }
        onVolumeStateChange.Invoke();
    }
    public bool IsSoundEnabled()
    {
        if (PlayerPrefs.HasKey("isSoundEnabled"))
        {
            if (PlayerPrefs.GetInt("isSoundEnabled") != 1)
            {
                gameObject.GetComponent<AudioSource>().mute = true;
            }
            return PlayerPrefs.GetInt("isSoundEnabled") == 1;
        }
        else
        {
            PlayerPrefs.SetInt("isSoundEnabled", 1);
            return true;
        }
    }
    public int ReachedLevel()
    {
        if (PlayerPrefs.HasKey("reachedLevel"))
        {
            return PlayerPrefs.GetInt("reachedLevel");
        }
        else
        {
            PlayerPrefs.SetInt("reachedLevel", 1);
            return 1;
        }
    }
    public void UpdateLevel(int level)
    {
        if (PlayerPrefs.GetInt("reachedLevel") < level)
        {
            reachedLevel = level;
            PlayerPrefs.SetInt("reachedLevel", level);
        }
    }
    #endregion
}
