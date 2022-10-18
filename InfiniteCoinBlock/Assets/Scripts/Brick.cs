using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
public class Brick : MonoBehaviour
{
    [SerializeField] Transform brickTR;
    [SerializeField] TextMeshProUGUI brickValueText;
    [SerializeField] GameObject hitParticle;

    float firstScaleY;
    void Start()
    {
        ClickerControl.Instance.brick = this;
        firstScaleY = brickTR.localScale.y;
        StartCoroutine(StartDelay());
    }
    IEnumerator StartDelay()
    {
        yield return null;
        brickValueText.text = "x" + Globals.brickPerHit.ToString();
    }
    public void HitBrick()
    {
        DoGetValuePos(brickTR, true, 4f / Globals.currrentAnimSpeed, 10, 0.5f, Ease.OutElastic);
        DoGetValueScale(brickTR, true, 1.3f, 1, 0.7f, Ease.OutElastic);
       GameObject prt = Instantiate(hitParticle, transform.position, Quaternion.identity);
        ParticleSystem.EmissionModule emis = prt.transform.GetChild(0).GetComponent<ParticleSystem>().emission;
        emis.SetBursts(
            new ParticleSystem.Burst[]{
                new ParticleSystem.Burst(0f, Globals.brickPerHit,1,0.01f)
            });
        //emis.burstCount = 10;
        Destroy(prt, 2f);
    }
    public Tween DoGetValuePos(Transform tr, bool active, float value, float lastValue, float duration, DG.Tweening.Ease type)
    {
        Tween tween = DOTween.To
            (() => value, x => value = x, lastValue, duration).SetEase(type).OnUpdate(delegate ()
            {
                tr.localPosition = new Vector3(tr.localPosition.x, value, tr.localPosition.z);
            }).OnComplete(delegate ()
            {

            });

        return tween;
    }
    public Tween DoGetValueScale(Transform tr, bool active, float value, float lastValue, float duration, DG.Tweening.Ease type)
    {
        Tween tween = DOTween.To
            (() => value, x => value = x, lastValue, duration).SetEase(type).OnUpdate(delegate ()
            {
                tr.localScale = new Vector3(tr.localScale.x, firstScaleY * value, tr.localScale.z);
            }).OnComplete(delegate ()
            {

            });

        return tween;
    }
    //void Coin(Vector3 instPos, int Coin)
    //{
    //    var point = Instantiate(coinPoint, instPos, Quaternion.identity);
    //    point.GetComponent<Point>().PointText.text = "$" + Coin.ToString();
    //}
}
