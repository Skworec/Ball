using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuControllerScript : MonoBehaviour
{
    [SerializeField]
    private Button continueButton;
    [SerializeField]
    private LevelController lvlCntrl;
    private void Start()
    {
        transform.localScale = Vector3.zero;
        LevelController.onLevelComplete.AddListener(OnLevelCompleteHandler);
        LevelController.onLevelFailed.AddListener(OnLevelFailedHandler);
    }
    public void ShowMenu()
    {
        DataController.instance.IsPaused = true;
        StartCoroutine("OutAnimationRoutine");
    }
    public void HideMenu()
    {
        StartCoroutine("InAnimationRoutine");
        DataController.instance.IsPaused = false;
    }
    public void OnLevelCompleteHandler()
    {
        continueButton.gameObject.SetActive(true);
        continueButton.onClick.AddListener(OnNextLevelButton);
        ShowMenu();
    }
    public void OnLevelFailedHandler()
    {
        continueButton.gameObject.SetActive(false);
        ShowMenu();
    }
    public void OnNextLevelButton()
    {
        continueButton.onClick.RemoveAllListeners();
        lvlCntrl.LoadNextLevel();
    }
    public IEnumerator OutAnimationRoutine()
    {
        while (transform.localScale != Vector3.one)
        {
            transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
            yield return new WaitForSeconds(0.025f);
        }
    }
    public IEnumerator InAnimationRoutine()
    {
        while (transform.localScale != Vector3.zero)
        {
            transform.localScale -= new Vector3(0.05f, 0.05f, 0.05f);
            yield return new WaitForSeconds(0.025f);
        }

    }
}
