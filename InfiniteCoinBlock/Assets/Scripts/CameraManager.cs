using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private static CameraManager _instance = null;
    public static CameraManager Instance => _instance;


    [SerializeField] Transform cameraFirstPoint, cameraLastPoint;

    private void Awake()
    {
        _instance = this;
    }

    public void CameraPosSet(int currentBlockCount, int maxBlockCount)
    {
        float lerpValue = (float)currentBlockCount / (float)maxBlockCount;
        transform.position = Vector3.Lerp(cameraFirstPoint.position, cameraLastPoint.position, Mathf.Sqrt(lerpValue));
        transform.rotation = Quaternion.Lerp(cameraFirstPoint.rotation, cameraLastPoint.rotation, Mathf.Sqrt(lerpValue));
    }
}
