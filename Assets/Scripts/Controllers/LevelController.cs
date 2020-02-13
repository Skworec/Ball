using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> levels = new List<GameObject>();
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private ParticleSystem confetti;
    [SerializeField]
    private AudioClip gameplayClip;
    private GameObject activeLevel = null;
    private static int secondsLeft;
    public static int SecondsLeft
    {
        get
        {
            return secondsLeft;
        }
        set
        {
            secondsLeft = value;
            onTimeChanged.Invoke();
        }
    }
    
    public static UnityEvent onLevelComplete = new UnityEvent();
    public static UnityEvent onLevelFailed = new UnityEvent();
    public static UnityEvent onTimeChanged = new UnityEvent();

    public void Start()
    {
        confetti.Stop();
        onLevelComplete.AddListener(OnLevelCompleteHandler);
        LoadLevel(DataController.instance.reachedLevel);
    }

    public void LoadNextLevel()
    {
            for (int i = 0; i < levels.Count; i++)
            {
                if (activeLevel.name.Contains(levels[i].name))
                {
                    LoadLevel(i + 2);
                    break;
                }
            }
    }

    public void LoadLevel(int levelNumber)
    {
        if (activeLevel != null)
        {
            Destroy(activeLevel);
        }
        if (levelNumber > 0 && levelNumber <= levels.Count)
        {
            StopAllCoroutines();
            DataController.instance.UpdateLevel(levelNumber);
            activeLevel = Instantiate(levels[levelNumber - 1], Vector3.zero, Quaternion.identity, gameObject.transform);
            foreach (Transform childs in activeLevel.transform)
            {
                if (childs.name.Contains("Start"))
                {
                    player.transform.position = childs.position;
                }
            }
            StartCoroutine("Timer");
            AudioSource src = DataController.instance.GetComponent<AudioSource>();
            src.clip = gameplayClip;
            src.loop = true;
            src.Play();
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
    public void RestartLevel()
    {
        for (int i = 0; i < levels.Count; i++)
        {
            if (activeLevel.name.Contains(levels[i].name))
            {
                LoadLevel(i + 1);
            }
        }
    }

    public void OnLevelCompleteHandler()
    {
        StopCoroutine("Timer");
        confetti.Play();
    }

    public IEnumerator Timer()
    {
        SecondsLeft = 60;
        while (SecondsLeft > 0)
        {
            yield return new WaitForSeconds(1);
            if (!DataController.instance.IsPaused)
            {
                SecondsLeft--;
            }
        }
        onLevelFailed.Invoke();
    }
}
