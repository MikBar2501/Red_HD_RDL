using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsScript : MonoBehaviour
{
    public float fadeInTime = 2f;
    public float fadeOutTime = 2f;


    private Text[] TextList;
    private bool[] fadeAssist;
    private bool[] fadeInProgress;
    private float totalTime = 0f;
    private int progress = 0;

    void Start()
    {
        TextList = GetComponentsInChildren<Text>(true);
        fadeAssist = new bool[TextList.Length];
        fadeInProgress = new bool[TextList.Length];

        StartCoroutine(TurnOnText(progress, 5f));

    }

    public void StartMusic()
    {

        
    }

    private IEnumerator TurnOnText(int i, float timeBetweenTexts)
    {
        TextList[i].transform.gameObject.SetActive(true);
        yield return new WaitForSeconds(timeBetweenTexts);
        if(progress < TextList.Length - 1)
        {
            progress++;
           // print(progress + " + " + TextList.Length);
            StartCoroutine(TurnOnText(progress, 5f));

        }


    }


    void Update()
    {
        // totalTime += Time.deltaTime;

        for (int i = 0; i < TextList.Length; i++)
        {

            if (TextList[i].IsActive() && !fadeInProgress[i] && !fadeAssist[i])
            {
                StartCoroutine(FadeTextToFullAlpha(fadeInTime, TextList[i], i));
            }
            else
            if (fadeAssist[i] && !fadeInProgress[i])
            {
                if (!fadeInProgress[i])
                    StartCoroutine(FadeTextToZeroAlpha(fadeOutTime, TextList[i], i));
            }
        }


    }

    public IEnumerator FadeTextToFullAlpha(float time, Text i, int number)
    {
        fadeInProgress[number] = true;

        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / time));
            if (i.color.a >= 1.0f)
            {
                fadeInProgress[number] = false;
                fadeAssist[number] = true;
            }
            yield return null;
        }


    }

    public IEnumerator FadeTextToZeroAlpha(float time, Text i, int number)
    {
        fadeInProgress[number] = true;
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / time));
            if (i.color.a <= 0.0f)
            {
                i.color = new Color(i.color.r, i.color.g, i.color.b, 0f);
                i.transform.gameObject.SetActive(false);
            }
            yield return null;
        }

    }
}