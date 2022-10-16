using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Brick : MonoBehaviour
{
    [SerializeField] Transform brickTR;
    float firstScaleY;
    void Start()
    {
        ClickerControl.Instance.brick = this;
        firstScaleY = brickTR.localScale.y;
    }
    public void HitBrick()
    {
        DoGetValuePos(brickTR, true, 0.1f, 10, 0.5f, Ease.OutElastic);
        DoGetValueScale(brickTR, true, 0.8f, 1, 0.5f, Ease.OutElastic);
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
}
