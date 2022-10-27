using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TapTutorial : MonoBehaviour
{
    [SerializeField] float period, period2;
    [SerializeField] Image img1, img2;
    private ParticleSystem[] particles;

    bool tap1 = true;
    float height;
    float width;

    void OnEnable()
    {
        particles = GetComponentsInChildren<ParticleSystem>();
        height = transform.parent.GetComponent<RectTransform>().sizeDelta.y;
        width = transform.parent.GetComponent<RectTransform>().sizeDelta.x;
        ParticleSet(0);
        img1.enabled = false;
        img2.enabled = true;
        StartCoroutine(tap(0));
        StartCoroutine(tap2(0));
    }

    IEnumerator tap(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        while (tap1)
        {
            img1.enabled = true;
            img2.enabled = false;
            ParticleSet(0);
            yield return new WaitForSeconds(period);
            ParticleSet(1000);
            img1.enabled = false;
            img2.enabled = true;
            yield return new WaitForSeconds(period);

        }
        img1.enabled = false;
        img2.enabled = false;
        ParticleSet(0);
    }

    IEnumerator tap2(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        while (tap1)
        {
            GetComponent<RectTransform>().localPosition = new Vector3(Random.Range(-width / 2, width / 2), Random.Range(-height / 2, height / 2), 0);

            yield return new WaitForSeconds(period2);
        }

    }
    void ParticleSet(int maxParticle)
    {
        foreach (var _particle in particles)
        {
            _particle.maxParticles = maxParticle;
        }
    }
    public void TutorialClose()
    {
        gameObject.SetActive(false);
    }
}
