using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/Build/Build_Settings")]
public class BuildSettings : ScriptableObject
{
    [Header("       BRICK")]
    //[SerializeField] private GameObject brickPrefab;
    //public GameObject _brickPrefab { get { return brickPrefab; } }


    [SerializeField] private Material material;
    public Material _material { get { return material; } }

    [Range(1, 10)]
    [SerializeField] private int brickSize;
    public int _brickSize { get {return brickSize; } }

    [Range(0.1f, 1f)]
    [SerializeField] private float brickSizeRatio;
    public float _brickSizeRatio { get { return brickSizeRatio; } }

    [Header("       TEXTURE")]
    [SerializeField] private Texture2D textureMap;
    public Texture2D _textureMap { get { return textureMap; } }


    [Range(1, 10)]
    [SerializeField] private int pixelStep;
    public int _pixelStep { get { return pixelStep; } }

}
