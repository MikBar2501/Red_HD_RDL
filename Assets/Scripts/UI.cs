using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public static UI _UI;
    public RawImage image;
    public PlayerRadiationSettings PlayerRadiation;
    float basicWidth;

    public RawImage SafeButton;
    public RawImage DangerButton;

    public GameObject XToJason;
    public Texture2D safeOff;
    public Texture2D safeOn;
    public Texture2D dangerOff;
    public Texture2D dangerOn;

    public GameObject ShowWindow;
    public RawImage ShowImageSpot;
    public Text Description;

    bool FramePassed = true;
    
    bool inZone;

    void Awake()
    {
        FramePassed = true;
        CloseImage();
        _UI = this;
        SetXToJason(false);
    }

    void Start()
    {
        Rect rect = image.rectTransform.rect;
        basicWidth = rect.width;
        inZone = PlayerRadiation.inZone;
        if (PlayerRadiation.inZone)
            SignalDanger();
        else
            SignalSafety();
    }

    void Update()
    {
        Rect rect = image.rectTransform.rect;
        rect.width = basicWidth * PlayerRadiation.shieldValue / PlayerRadiation.maxShieldValue;
        image.rectTransform.sizeDelta = new Vector2(rect.width, image.rectTransform.sizeDelta.y);

        if(inZone != PlayerRadiation.inZone)
        {
            inZone = PlayerRadiation.inZone;
            if (PlayerRadiation.inZone)
                SignalDanger();
            else
                SignalSafety();
        }

        if(isDisplayingImage())
        {
            if (Input.GetButtonDown("Interaction") && FramePassed)
                CloseImage();
        }



        FramePassed = true;
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

    public void SetXToJason(bool state)
    {
        XToJason.SetActive(state);
    }

    public bool isDisplayingImage()
    {
        return ShowWindow.activeInHierarchy;
    }

    public void ShowImage(Texture2D image, string desc = null)
    {
        FramePassed = false;
        ShowWindow.SetActive(true);
        ShowImageSpot.texture = image;
        if (desc != null)
            Description.text = desc;
        Movement.CanMove = false;
    }

    public void CloseImage()
    {
        Movement.CanMove = true;
        ShowWindow.SetActive(false);
    }
}
