using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public RawImage image;
    public PlayerRadiationSettings PlayerRadiation;
    public float basicWidth;

    public RawImage SafeButton;
    public RawImage DangerButton;

    public Texture2D safeOff;
    public Texture2D safeOn;
    public Texture2D dangerOff;
    public Texture2D dangerOn;

    public PlayerRadiationSettings player;
    bool inZone;

  //  public RectTransform DialogWindow;

    void Start()
    {
        Rect rect = image.rectTransform.rect;
        basicWidth = rect.width;
        inZone = player.inZone;
        if (player.inZone)
            SignalDanger();
        else
            SignalSafety();
    }

    void Update()
    {
        Rect rect = image.rectTransform.rect;
        rect.width = basicWidth * PlayerRadiation.shieldValue / PlayerRadiation.maxShieldValue;
        image.rectTransform.sizeDelta = new Vector2(rect.width, image.rectTransform.sizeDelta.y);

        if(inZone != player.inZone)
        {
            inZone = player.inZone;
            if (player.inZone)
                SignalDanger();
            else
                SignalSafety();
        }
    }

    public void SignalDanger()
    {
        SafeButton.texture = safeOff;
        DangerButton.texture = dangerOn;
    }

    public void SignalSafety()
    {
        SafeButton.texture = safeOn;
        DangerButton.texture = dangerOff;
    }
}
