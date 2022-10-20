using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TapticPlugin;

public class VibratoManager : MonoBehaviour
{
    private static VibratoManager _instance = null;
    public static VibratoManager Instance => _instance;
    //public static VibratoManager Instance;
    bool lightVibratoActive = true;
    bool mediumVibratoActive = true;
    bool heavyVibratoActive = true;
    [SerializeField] float frequency;
    void Awake()
    {
        _instance = this;
    }
    // Update is called once per frame
    public void LightViration()
    {
        if (lightVibratoActive)
        {
            TapticManager.Impact(ImpactFeedback.Light);
            Debug.Log("LightViration");
            StartCoroutine(lightVibratoActivator());
        }
    }
    public void MediumViration()
    {
        if (mediumVibratoActive)
        {
            TapticManager.Impact(ImpactFeedback.Medium);
            Debug.Log("MediumViration");
            StartCoroutine(mediumVibratoActivator());
        }
    }
    public void HeavyViration()
    {
        if (heavyVibratoActive)
        {
            TapticManager.Impact(ImpactFeedback.Heavy);
            Debug.Log("HeavyViration");
            StartCoroutine(heavyVibratoActivator());
        }
    }
    public void LongHeavyViration()
    {
        StartCoroutine(LongHeavyVibratoActivator());
    }
    IEnumerator lightVibratoActivator()
    {
        if (lightVibratoActive)
        {
            lightVibratoActive = false;
            yield return new WaitForSeconds(frequency);
            lightVibratoActive = true;
        }
    }
    IEnumerator mediumVibratoActivator()
    {
        if (mediumVibratoActive)
        {
            mediumVibratoActive = false;
            yield return new WaitForSeconds(frequency);
            mediumVibratoActive = true;
        }
    }
    IEnumerator heavyVibratoActivator()
    {
        if (heavyVibratoActive)
        {
            heavyVibratoActive = false;
            yield return new WaitForSeconds(frequency);
            heavyVibratoActive = true;
        }
    }
    IEnumerator LongHeavyVibratoActivator()
    {
        float counter = 0;
        while(counter < 2f)
        {
            counter += Time.deltaTime;
            if (heavyVibratoActive)
            {
                TapticManager.Impact(ImpactFeedback.Heavy);
                Debug.Log("HeavyViration");
                StartCoroutine(heavyVibratoActivator());
            }

            yield return null;
        }
    }

}