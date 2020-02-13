using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private LevelController lvlCntrl;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            //Уровень пройден
            Debug.Log("Уровень пройден");
            lvlCntrl.onLevelComplete.Invoke();
        }
    }
}
