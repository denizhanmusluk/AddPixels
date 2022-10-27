using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class tutorial : MonoBehaviour
{
    [SerializeField] float period;
    [SerializeField] Image img1, img2;
    private ParticleSystem[] particles;

    bool tap1 = true;
    private void OnEnable()
    {
        particles = GetComponentsInChildren<ParticleSystem>();

        ParticleSet(0);
        img1.enabled = false;
        img2.enabled = true;
        StartCoroutine(tap(0));
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
