using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public static UI _UI;
    public RawImage image;
    public PlayerRadiationSettings PlayerRadiation;
    float basicWidth;

    public RawImage SafeButton;
    public RawImage DangerButton;

    public GameObject XToJason; // "click to interact" window
    public Texture2D safeOff;
    public Texture2D safeOn;
    public Texture2D dangerOff;
    public Texture2D dangerOn;

    public Stats GlobalStats;
    public GameObject ShowWindow;
    public RawImage ShowImageSpot;
    public Text Description;

    public GameObject HUD;
    public GameObject MenuWindow;
    public Slider SoundSlider;
    public Slider MusicSlider;

    bool FramePassed = true;
    
    bool inZone;

    void Awake()
    {
        GlobalStats.isPaused = false;
        Debug.Log(GlobalStats.isPaused);
        FramePassed = true;
        CloseImage();
        _UI = this;
        SetXToJason(false);
    }

    void Start()
    {
        GlobalStats.isPaused = false;
        MenuWindow.SetActive(false);
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
        SoundSlider.value = GlobalStats.Sounds;
        MusicSlider.value = GlobalStats.Music;

        if(Input.GetButtonDown("Esc"))
        {
            if (inMenu())
                CloseMenu();
            else
                DisplayMenu();
        }


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
        GlobalStats.isPaused = true;
    }

    public void CloseImage()
    {
        GlobalStats.isPaused = false;
        ShowWindow.SetActive(false);
    }



    ///Menu:
    public bool inMenu()
    {
        return MenuWindow.activeSelf;
    }

    public void DisplayMenu()
    {
        GlobalStats.isPaused = true;
        MenuWindow.SetActive(true);
        HUD.SetActive(false);
    }

    public void CloseMenu()
    {
        GlobalStats.isPaused = false;
        MenuWindow.SetActive(false);
        HUD.SetActive(true);

    }

    public void SetMusic()
    {
        GlobalStats.Music = MusicSlider.value;
    }

    public void SetSound()
    {
        GlobalStats.Sounds = SoundSlider.value;
    }

    public void LoadMainMenu()
    {
        GlobalStats.isPaused = false;
        SceneManager.LoadScene("Menu");
    }
}
