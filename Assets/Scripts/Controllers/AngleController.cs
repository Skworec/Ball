using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleController : MonoBehaviour
{
    private float AccelerometerUpdateInterval = 1f / 60f;
    private float LowPassKernelWidthInSeconds = 1f;
    private float LowPassFilterFactor;
    private Vector3 lowPassValue = Vector3.zero;
    private Quaternion localRotation;
    private float sensivity = 1f;
    public Quaternion calibrationQuaternion;

    void Start()
    {
        LowPassFilterFactor = AccelerometerUpdateInterval / LowPassKernelWidthInSeconds;
        lowPassValue = Input.acceleration;
        CalibrateAccelerometer();
        localRotation = transform.rotation;
    }
    void FixedUpdate()
    {
        Vector3 currentAcceleration = FixAcceleration(LowPassFilterAccelerometer());
        localRotation.x = currentAcceleration.x;
        localRotation.z = currentAcceleration.y;
        transform.rotation = localRotation;
    }

    public void CalibrateAccelerometer()
    {
        Vector3 accelerationSnapshot = LowPassFilterAccelerometer();
        Quaternion rotateQuaternion = Quaternion.FromToRotation(Vector3.back, accelerationSnapshot);
        calibrationQuaternion = Quaternion.Inverse(rotateQuaternion);
    }

    public Vector3 FixAcceleration(Vector3 acceleration)
    {
        Vector3 fixedAcceleration = calibrationQuaternion * acceleration;
        return fixedAcceleration;
    }

    private Vector3 LowPassFilterAccelerometer()
    {
        lowPassValue = (Vector3.Lerp(lowPassValue, Input.acceleration, LowPassFilterFactor)) * sensivity;
        return lowPassValue;
    }
}
