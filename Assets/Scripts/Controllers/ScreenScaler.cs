using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenScaler : MonoBehaviour
{
    private int pixelsPerUnit = 64;
    private void Update()
    {
        float height = 1080f / Screen.width * Screen.height;
        Camera.main.orthographicSize = height / pixelsPerUnit / 4;
    }
}
