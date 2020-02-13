using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private LevelController lvlCntrl;
    private Vector3 onPauseSnapshotVelocity;
    private Vector3 onPauseSnapshotAngularVelocity;
    private void Start()
    {
        DataController.instance.onPauseStateChange.AddListener(OnPauseStateChangeHandler);
    }

    public void OnPauseStateChangeHandler()
    {
        if (DataController.instance.IsPaused)
        {
            onPauseSnapshotVelocity = GetComponent<Rigidbody>().velocity;
            onPauseSnapshotAngularVelocity = GetComponent<Rigidbody>().angularVelocity;
            GetComponent<Rigidbody>().isKinematic = true;
        }
        else
        {
            GetComponent<Rigidbody>().velocity = onPauseSnapshotVelocity;
            GetComponent<Rigidbody>().angularVelocity = onPauseSnapshotAngularVelocity;
            GetComponent<Rigidbody>().isKinematic = false;
        }
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Finish")
    //    {
    //        LevelController.onLevelComplete.Invoke();
    //    }
    //}
}
