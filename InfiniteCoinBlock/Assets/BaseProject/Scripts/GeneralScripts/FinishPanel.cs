using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPanel : MonoBehaviour
{
    [SerializeField] RectTransform image1, image2, BGImage;
    [SerializeField] float firstImageScale = 10;
    [SerializeField] float lastImageScale = 0.7f;
    public enum FinishImageType
    {
        set1,
        set2,
        set3
    }
    public FinishImageType fin;

    void Start()
    {
        switch (fin)
        {

            case FinishImageType.set1:
                {
                    StartCoroutine(panelScaleSet(image1));
                }
                break;

            case FinishImageType.set2:
                {
                    StartCoroutine(failPnaelOpen2());
                }
                break;

            case FinishImageType.set3:
                {

                }
                break;
        }
    }
    IEnumerator panelScaleSet(RectTransform image)
    {
        float counter = firstImageScale;
        float scaleFactor = 40f;
        while (counter > lastImageScale)
        {
            counter -= scaleFactor * Time.deltaTime;
            scaleFactor -= 10 * Time.deltaTime;
            image.localScale = new Vector3(counter, counter, counter);
            yield return null;
        }
        image.localScale = new Vector3(lastImageScale, lastImageScale, lastImageScale);
        counter = 0f;
        float scale = 0;
        while (counter < Mathf.PI)
        {
            counter += 10 * Time.deltaTime;
            scale = Mathf.Sin(counter);
            scale *= 0.3f;
            image.localScale = new Vector3(lastImageScale - scale, lastImageScale - scale, lastImageScale - scale);
            yield return null;
        }
        image.localScale = new Vector3(lastImageScale, lastImageScale, lastImageScale);

    }

    IEnumerator failPnaelOpen2()
    {
        image1.localScale = Vector3.zero;
        image2.localScale = Vector3.zero;
        BGImage.localScale = new Vector3(1, 0, 1);


        image1.localScale = new Vector3(firstImageScale, firstImageScale, firstImageScale);
        StartCoroutine(panelScaleSet(image1));
        yield return new WaitForSeconds(0.25f);

        image2.localScale = new Vector3(firstImageScale, firstImageScale, firstImageScale);
        StartCoroutine(panelScaleSet(image2));

        yield return new WaitForSeconds(0.5f);

        StartCoroutine(panelBGScale(BGImage));
    }

    IEnumerator panelBGScale(RectTransform image)
    {
        float counter = 0;
        while (counter < 1f)
        {
            counter += 5 * Time.deltaTime;
            image.localScale = new Vector3(1, counter, 1);
            yield return null;
        }
        image.localScale = new Vector3(1, 1, 1);
        counter = 0f;
        float scale = 0;
        while (counter < Mathf.PI)
        {
            counter += 5 * Time.deltaTime;
            scale = Mathf.Sin(counter);
            scale *= 0.3f;
            image.localScale = new Vector3(1, lastImageScale + scale, 1);
            yield return null;
        }
        image.localScale = new Vector3(1, 1, 1);

    }
}
