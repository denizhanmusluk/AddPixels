using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/Upgrade/Upgrade_Settings")]

public class UpgradeSettings : ScriptableObject
{


    [Header("       BRICK UPGRADE")]
    [SerializeField] private GameObject[] brickBoxPrefab;
    public GameObject[] _brickBoxPrefab { get { return brickBoxPrefab; } }

    [SerializeField] private GameObject[] brickPrefab;
    public GameObject[] _brickPrefab { get { return brickPrefab; } }

    [SerializeField] private int[] brickCount;
    public int[] _brickCount { get { return brickCount; } }

    [SerializeField] private int[] coinPerBrick;
    public int[] _coinPerBrick { get { return coinPerBrick; } }

    [SerializeField] private int[] brickUpgradeCost;
    public int[] _brickUpgradeCost { get { return brickUpgradeCost; } }


    [Header("       STAMINA UPGRADE")]

    [SerializeField] private int[] healthDownPerSeconds;
    public int[] _healthDownPerSeconds { get { return healthDownPerSeconds; } }

    [SerializeField] private int[] coolDownPerSeconds;
    public int[] _coolDownPerSeconds { get { return coolDownPerSeconds; } }

    [SerializeField] private int[] staminaUpgradeCost;
    public int[] _staminaUpgradeCost { get { return staminaUpgradeCost; } }

    [Header("       SPEED UPGRADE")]
    [SerializeField] private float[] clickAnimSpeed;
    public float[] _clickAnimSpeed { get { return clickAnimSpeed; } }

    [SerializeField] private int[] clickAnimUpgradeCost;
    public int[] _clickAnimUpgradeCost { get { return clickAnimUpgradeCost; } }

    private void Awake()
    {
        
    }
}
