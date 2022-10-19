﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Point : MonoBehaviour
{
    [Range(0, 1)] [SerializeField] int alpha;
    [Range(0, 400)] [SerializeField] float UpwardSpeed;
    [Range(0, 4)] [SerializeField] float SimulationSpeed;
    float speedFactor;
    public TextMeshProUGUI PointText;
    private void Start()
    {
        speedFactor = Random.Range(0.5f, 1f);
        //foreach (var txt in GetComponentsInChildren<TextMeshProUGUI>())
        //{
        //    PointText = txt;
        //}
        StartCoroutine(pointUp());
        StartCoroutine(colorSet(alpha));
    }
    //public void TextInit(int)
    //{

    //}
    IEnumerator pointUp()
    {

        float counter = 0;
        float spd = 0;
        while (counter < Mathf.PI / 2)
        {
            counter += speedFactor * SimulationSpeed * Time.deltaTime;
            spd = Mathf.Cos(counter);
            spd *= UpwardSpeed;
            transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0, 1, 0), Time.deltaTime * spd);

            yield return null;
        }
        Destroy(gameObject);
    }
    IEnumerator colorSet(float _alpha)
    {
        float counter = 0;
        while (counter < Mathf.PI / 2)
        {
            counter += speedFactor * SimulationSpeed * Time.deltaTime;
            float currentAlpha = (counter / (Mathf.PI / 2));
            PointText.color = new Color(PointText.color.r, PointText.color.g, PointText.color.b, Mathf.Abs(_alpha - currentAlpha));
            yield return null;
        }
        Destroy(gameObject);
    }
}
