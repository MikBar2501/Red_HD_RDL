using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Fader : MonoBehaviour
{
    public static Fader _Fader;
    public RawImage image;
    float Duration;
    float Remaining;
    float AfterWait;
    bool Dim;
    Function ToCall;
    

    public delegate void Function();


    public static void Fade(float duration, float additional, bool dim, Function f)
    {
        _Fader.ToCall = f;
        _Fader.Dim = dim;
        _Fader.Duration = duration;
        _Fader.Remaining = duration;
        _Fader.AfterWait = additional;
    }

    public static void Fade(float duration, float additional, bool dim, Color color, Function f)
    {
        Fade(duration, additional, dim, f);
        _Fader.image.color = color;
    }

    void Awake()
    {
        Remaining = 0;
        _Fader = this;
    }

    void Update()
    {
        if(Remaining + AfterWait > 0)
        {
            if(Remaining > 0)
                Remaining -= Time.deltaTime;
            Color c = image.color;

            if (Dim)
                c.a = 1 - (Remaining / Duration);
            else
                c.a = Remaining / Duration;
            image.color = c;
            if(Remaining < 0)
            {
                Remaining = 0;
                return;
            }

            if(Remaining  == 0)
            {
                AfterWait -= Time.deltaTime;
                if(AfterWait <= 0)
                {
                    if (Dim)
                        c.a = 1;
                    else
                        c.a = 0;
                    image.color = c;
                    AfterWait = 0;
                    ToCall();
                }
            }
        }
    }
}
