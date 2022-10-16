using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/LevelSet")]
public class LevelScriptable : ScriptableObject
{

    [Header("LevelIndex")]

    [SerializeField] private float levelIndex = 5f;
    public float _levelIndex { get { return levelIndex; } }

    [Header("LevelPrefab")]
    [SerializeField] private GameObject levelPrefab;
    public GameObject _levelPrefab { get { return levelPrefab; } }

}
