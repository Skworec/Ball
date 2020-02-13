using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> levels = new List<GameObject>();
    [SerializeField]
    private GameObject player;
    private GameObject activeLevel = null; 
    
    public UnityEvent onLevelComplete = new UnityEvent();

    public void Start()
    {
        onLevelComplete.AddListener(OnLevelCompleteHandler);
        LoadLevel(DataController.instance.reachedLevel);
    }


    public void LoadLevel(int levelNumber)
    {
        if (activeLevel != null)
        {
            Destroy(activeLevel);
        }
        if (levelNumber > 0 && levelNumber <= levels.Count)
        {
            DataController.instance.UpdateLevel(levelNumber);
            activeLevel = Instantiate(levels[levelNumber - 1], Vector3.zero, Quaternion.identity, gameObject.transform);
            foreach (Transform childs in activeLevel.transform)
            {
                if (childs.name.Contains("Start"))
                {
                    player.transform.position = childs.position;
                }
            }
        }
    }

    public void OnLevelCompleteHandler()
    {
        LoadLevel(DataController.instance.reachedLevel + 1);
    }
}
