using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickParent : MonoBehaviour
{
    private static BrickParent _instance = null;
    public static BrickParent Instance => _instance;

    private void Awake()
    {
        _instance = this;
    }
}
