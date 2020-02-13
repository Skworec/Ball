using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton_Handler : MonoBehaviour
{
    [SerializeField]
    private AudioClip menuClip;
    private void Start()
    {
        AudioSource src = DataController.instance.GetComponent<AudioSource>();
        src.clip = menuClip;
        src.loop = true;
        src.Play();
    }
    public void StartGame()
    {
        DataController.instance.GetComponent<AudioSource>().Stop();
        SceneManager.LoadScene(1);
    }
}
