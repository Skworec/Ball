using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButtonHandler : MonoBehaviour
{
    private Button pButton;
    void Start()
    {
        pButton = GetComponent<Button>();
        pButton.onClick.AddListener(OnPauseButtonClickHandler);
    }

    public void OnPauseButtonClickHandler()
    {
        DataController.instance.PauseGame();
    }
}
