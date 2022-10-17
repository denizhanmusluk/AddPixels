using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using ObserverSystem;
public class Builder : Subject
{
    BuildSettings buildSettings;
    [SerializeField] UpgradeSettings upgradeSettings;
    [SerializeField] GameObject coinPoint;
    int brickSize;
    //GameObject brickPrefab;
    Texture2D textureMap;

    int _readPixelStep;

    Vector3 readPixelStep;
    Vector3 brickDistance;
    Color[,] pixels;
    int textureHeight, textureWidth;
    Material material;
    //public bool buildActive = false;
   [SerializeField] int maxBlockCount = 0;
   [SerializeField] int currentBlockCount = 0;
    CameraManager cameraManager;
    private void Awake()
    {
        
    }
    void Initialize()
    {
        buildSettings = LevelManager.Instance.levelBuildSettings[PlayerPrefs.GetInt("level")];
        brickSize = buildSettings._brickSize;
        brickDistance = upgradeSettings._brickPrefab[Globals.brickLevel].transform.localScale * brickSize;
        textureMap = buildSettings._textureMap;
        _readPixelStep = buildSettings._pixelStep;
        material = buildSettings._material;
    }
    public override void SpesificStart()
    {
        cameraManager = CameraManager.Instance;
        Initialize();
        ClickerControl.Instance._builder = this;
        textureHeight = textureMap.height;
        textureWidth = textureMap.width;
        readPixelStep = 1 * _readPixelStep * brickDistance;

        pixels = GetPixelTextures.ReadTextureMap(textureMap, _readPixelStep, brickDistance);

        BlockCounter(pixels, textureWidth / (int)readPixelStep.x, textureHeight / (int)readPixelStep.y);
        FirstBuild(pixels, textureWidth / (int)readPixelStep.x, textureHeight / (int)readPixelStep.y);
    }
    private void FirstBuild(Color[,] pixels, int textureWidth, int textureHeight)
    {
        int i = 0;
        int j = 0;
        int ii = 0;

        while (j < PlayerPrefs.GetInt("BuildHeight"))
        {
            if (i < PlayerPrefs.GetInt("BuildWidth"))
            {
                for (int y = 0; y < i + 1; y++)
                {
                    if (pixels[textureWidth / 2 - i - 1, y].a > 0.5f)
                    {
                        currentBlockCount++;
                        cameraManager.CameraPosSet(currentBlockCount, maxBlockCount);


                        GameObject brick = Instantiate(upgradeSettings._brickPrefab[Globals.brickLevel], transform.position, Quaternion.identity, transform);
                        brick.transform.localScale *= brickSize * buildSettings._brickSizeRatio;
                        brick.transform.localPosition = new Vector3(brickDistance.x * (-i), brickDistance.y * y, 0);
                        brick.GetComponent<MeshRenderer>().material = material;
                        brick.GetComponent<MeshRenderer>().material.color = pixels[textureWidth / 2 - i - 1, y];
                        DoGetValueScale(brick.transform, true, 0.1f, 1, 0.5f, Ease.OutElastic);
                        SetRotBrick(brick.transform);
                    }
                }
            }
            if (i < textureWidth / 2)
            {
                ii = i;
            }
            else
            {
                ii = textureWidth / 2 - 1;
            }
            for (int x = textureWidth / 2 - ii - 1; x < textureWidth / 2 + ii + 1; x++)
            {
                if (pixels[x, j + 1].a > 0.5f)
                {

                    currentBlockCount++;
                    cameraManager.CameraPosSet(currentBlockCount, maxBlockCount);

                    GameObject brick = Instantiate(upgradeSettings._brickPrefab[Globals.brickLevel], transform.position, Quaternion.identity, transform);
                    brick.transform.localScale *= brickSize * buildSettings._brickSizeRatio;
                    brick.transform.localPosition = new Vector3(brickDistance.x * (x - textureWidth / 2 + 1), brickDistance.y * (j + 1), 0);
                    brick.GetComponent<MeshRenderer>().material = material;
                    brick.GetComponent<MeshRenderer>().material.color = pixels[x, (j + 1)];
                    DoGetValueScale(brick.transform, true, 0.1f, 1, 0.5f, Ease.OutElastic);
                    SetRotBrick(brick.transform);
                }
            }

            if (i < PlayerPrefs.GetInt("BuildWidth"))
            {
                for (int y = i; y >= 0; y--)
                {

                    if (pixels[textureWidth / 2 + i, y].a > 0.5f)
                    {
                        currentBlockCount++;
                        cameraManager.CameraPosSet(currentBlockCount, maxBlockCount);

                        GameObject brick = Instantiate(upgradeSettings._brickPrefab[Globals.brickLevel], transform.position, Quaternion.identity, transform);
                        brick.transform.localScale *= brickSize * buildSettings._brickSizeRatio;
                        brick.transform.localPosition = new Vector3(brickDistance.x * (i + 1), brickDistance.y * y, 0);
                        brick.GetComponent<MeshRenderer>().material = material;
                        brick.GetComponent<MeshRenderer>().material.color = pixels[textureWidth / 2 + i, y];
                        DoGetValueScale(brick.transform, true, 0.1f, 1, 0.5f, Ease.OutElastic);
                        SetRotBrick(brick.transform);
                    }
                }
                i++;
            }
            j++;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            BuildManuel(pixels, textureWidth / (int)readPixelStep.x, textureHeight / (int)readPixelStep.y);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            StartCoroutine(Building(pixels, textureWidth / (int)readPixelStep.x, textureHeight / (int)readPixelStep.y));
        }
    }
    private void BuildManuel(Color[,] pixels, int textureWidth, int textureHeight)
    {
        //StartCoroutine(Building(pixels, textureWidth, textureHeight));
        for (int y = 0; y < textureHeight; y++)
        {
            for (int x = 0; x < textureWidth; x++)
            {
                if (pixels[x, y].a > 0.5f)
                {
                    GameObject brick = Instantiate(upgradeSettings._brickPrefab[Globals.brickLevel], transform.position, Quaternion.identity, transform);
                    brick.transform.localScale *= brickSize * buildSettings._brickSizeRatio;
                    brick.transform.localPosition = new Vector3(brickDistance.x * (x - textureWidth / 2), brickDistance.y * y, 0);
                    brick.GetComponent<MeshRenderer>().material = material;
                    brick.GetComponent<MeshRenderer>().material.color = pixels[x, y];
                    SetRotBrick(brick.transform);
                }
            }
        }
    }
    public void BuildStart()
    {
        StartCoroutine(Building(pixels, textureWidth / (int)readPixelStep.x, textureHeight / (int)readPixelStep.y));
    }
    IEnumerator Building(Color[,] pixels, int textureWidth, int textureHeight)
    {
        int i = PlayerPrefs.GetInt("BuildWidth");

        int j = PlayerPrefs.GetInt("BuildHeight");
        int ii = 0;
        while (i < textureWidth / 2 - 2|| j < textureHeight - 1)
        {
            PlayerPrefs.SetInt("BuildHeight", j);
            PlayerPrefs.SetInt("BuildWidth", i);

            if (i < textureWidth / 2)
            {

                for (int y = 0; y < i + 1; y++)
                {
                    if (pixels[textureWidth / 2 - i - 1, y].a > 0.5f)
                    {
                        currentBlockCount++;
                        cameraManager.CameraPosSet(currentBlockCount, maxBlockCount);

                        while (!Globals.buildActive)
                        {
                            yield return null;
                        }
                        Globals.buildActive = false;
                        Debug.Log("textureWidth / 2 - i  = " + (textureWidth / 2) + " : : " + i);
                        GameObject brick = Instantiate(upgradeSettings._brickPrefab[Globals.brickLevel], transform.position, Quaternion.identity, transform);

                        brick.transform.localScale *= brickSize * buildSettings._brickSizeRatio;
                        brick.transform.localPosition = new Vector3(brickDistance.x * (-i), brickDistance.y * y, 0);
                        brick.GetComponent<MeshRenderer>().material = material;
                        brick.GetComponent<MeshRenderer>().material.color = pixels[textureWidth / 2 - i - 1, y];

                        DoGetValueScale(brick.transform, true, 0.1f, 1, 0.5f, Ease.OutElastic);
                        SetRotBrick(brick.transform);

                        Coin(brick.transform.position - new Vector3(0, 0, brick.transform.localScale.z * 2f), Globals.coinPerBrick);
                        yield return null;
                    }
                }
            }
            if (i < textureWidth / 2)
            {
                ii = i;
            }
            else
            {
                ii = textureWidth / 2 - 1;
            }
            for (int x = textureWidth / 2 - ii - 1; x < textureWidth / 2 + ii + 1; x++)
            {
                Debug.Log("textureWidth / 2 - i  x= " + (textureWidth / 2) + " : : " + i + "  " + x);

                if (pixels[x, j + 1].a > 0.5f)
                {

                    currentBlockCount++;
                    cameraManager.CameraPosSet(currentBlockCount, maxBlockCount);

                    while (!Globals.buildActive)
                    {
                        yield return null;
                    }
                    Globals.buildActive = false;

                    GameObject brick = Instantiate(upgradeSettings._brickPrefab[Globals.brickLevel], transform.position, Quaternion.identity, transform);

                    brick.transform.localScale *= brickSize * buildSettings._brickSizeRatio;
                    brick.transform.localPosition = new Vector3(brickDistance.x * (x - textureWidth / 2 + 1), brickDistance.y * (j + 1), 0);
                    brick.GetComponent<MeshRenderer>().material = material;
                    brick.GetComponent<MeshRenderer>().material.color = pixels[x, (j + 1)];

                    DoGetValueScale(brick.transform, true, 0.1f, 1, 0.5f, Ease.OutElastic);
                    SetRotBrick(brick.transform);

                    Coin(brick.transform.position - new Vector3(0, 0, brick.transform.localScale.z * 2f), Globals.coinPerBrick);
                    yield return null;
                }
            }
            if (i < textureWidth / 2)
            {
                for (int y = i; y >= 0; y--)
                {

                    if (pixels[textureWidth / 2 + i, y].a > 0.5f)
                    {
                        currentBlockCount++;
                        cameraManager.CameraPosSet(currentBlockCount, maxBlockCount);

                        while (!Globals.buildActive)
                        {
                            yield return null;
                        }
                        Globals.buildActive = false;

                        GameObject brick = Instantiate(upgradeSettings._brickPrefab[Globals.brickLevel], transform.position, Quaternion.identity, transform);

                        brick.transform.localScale *= brickSize * buildSettings._brickSizeRatio;
                        brick.transform.localPosition = new Vector3(brickDistance.x * (i + 1), brickDistance.y * y, 0);
                        brick.GetComponent<MeshRenderer>().material = material;
                        brick.GetComponent<MeshRenderer>().material.color = pixels[textureWidth / 2 + i, y];

                        DoGetValueScale(brick.transform, true, 0.1f, 1, 0.5f, Ease.OutElastic);
                        SetRotBrick(brick.transform);

                        Coin(brick.transform.position - new Vector3(0, 0, brick.transform.localScale.z * 2f), Globals.coinPerBrick);
                        yield return null;
                    }
                }
                i++;
            }
            j++;
        }
        CompleteBuild();
    }
    void Coin(Vector3 instPos, int Coin)
    {
        var point = Instantiate(coinPoint, instPos, Quaternion.identity);
        point.GetComponent<Point>().PointText.text = "$"+Coin.ToString();
    }
    //IEnumerator Building(Color[,] pixels, int textureWidth, int textureHeight)
    //{
    //    for (int y = PlayerPrefs.GetInt("BuildHeight"); y < textureHeight; y++)
    //    {
    //        PlayerPrefs.SetInt("BuildHeight", y);
    //        for (int x = 0; x < textureWidth; x++)
    //        {
    //            if (pixels[x, y].a > 0.5f)
    //            {
    //                currentBlockCount++;
    //                cameraManager.CameraPosSet(currentBlockCount, maxBlockCount);

    //                while (!Globals.buildActive)
    //                {
    //                    yield return null;
    //                }
    //                Globals.buildActive = false;

    //                GameObject brick = Instantiate(upgradeSettings._brickPrefab[Globals.brickLevel], transform.position, Quaternion.identity, transform);
    //                brick.transform.localScale *= brickSize * buildSettings._brickSizeRatio;
    //                brick.transform.localPosition = new Vector3(brickDistance.x * (x - textureWidth / 2), brickDistance.y * y, 0);
    //                brick.GetComponent<MeshRenderer>().material = material;
    //                brick.GetComponent<MeshRenderer>().material.color = pixels[x, y];
    //                DoGetValueScale(brick.transform, true, 0.1f, 1, 0.5f, Ease.OutElastic);
    //                SetRotBrick(brick.transform);
    //                yield return null;
    //            }
    //        }
    //    }
    //    CompleteBuild();
    //}

    void CompleteBuild()
    {
        Notify(NotificationType.Win);
        PlayerPrefs.SetInt("BuildHeight", 0);
        PlayerPrefs.SetInt("BuildWidth", 0);

    }
    public Tween DoGetValueScale(Transform tr, bool active, float value, float lastValue, float duration, DG.Tweening.Ease type)
    {
        Vector3 firstScale = tr.localScale;
        Tween tween = DOTween.To
            (() => value, x => value = x, lastValue, duration).SetEase(type).OnUpdate(delegate ()
            {
                tr.localScale = firstScale * value;
            });
        return tween;
    }
    void SetRotBrick(Transform brickTR)
    {
        Quaternion rot = Quaternion.Euler(0, Random.Range(-10f, 10f), Random.Range(-10f, 10f));
        brickTR.rotation = rot;
    }
    private void BlockCounter(Color[,] pixels, int textureWidth, int textureHeight)
    {
        for (int y = 0; y < textureHeight; y++)
        {
            for (int x = 0; x < textureWidth; x++)
            {
                if (pixels[x, y].a > 0.5f)
                {
                    maxBlockCount++;
                }
            }
        }
    }
}
