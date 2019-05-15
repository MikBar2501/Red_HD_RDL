using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Stats Globalstats;
    public GameObject MainWindow;
    public GameObject OptionWindow;

    public Slider musicSlider;
    public Slider soundSlider;

    public void Play()
    {
        SceneManager.LoadScene("BaseScene");
    }

    public void Options()
    {
        OptionWindow.SetActive(true);
        MainWindow.SetActive(false);
    }

    public void ToMain()
    {
        OptionWindow.SetActive(false);
        MainWindow.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void SetVolumeSounds(Slider slider)
    {
        Globalstats.Sounds = slider.value;
    }

    public void SetVolumeMusic(Slider slider)
    {
        Globalstats.Music = slider.value;
    }

    void Start()
    {
        musicSlider.value = Globalstats.Music;
        soundSlider.value = Globalstats.Sounds;
        OptionWindow.SetActive(false);
        MainWindow.SetActive(true);
    }


    void Update()
    {
        
    }
}
