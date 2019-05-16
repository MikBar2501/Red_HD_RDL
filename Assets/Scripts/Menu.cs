using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Stats Globalstats;
    public GameObject MainWindow;
    //public GameObject OptionWindow;
    public GameObject CreditWindow;
    public GameObject Cover;

    public Slider loading;
    public GameObject loadingScreen;

    public void Play()
    {
        loadingScreen.SetActive(true);
        //SceneManager.LoadScene("BaseScene");
        LoadLevel("BaseScene");
    }

    /* public void Options()
    {
        OptionWindow.SetActive(true);
        MainWindow.SetActive(false);
    }*/

    public void Credits()
    {

    }

    public void ToMain()
    {
        //OptionWindow.SetActive(false);
        MainWindow.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }

    /* public void SetVolumeSounds(Slider slider)
    {
        Globalstats.Sounds = slider.value;
    }

    public void SetVolumeMusic(Slider slider)
    {
        Globalstats.Music = slider.value;
    }*/

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        //OptionWindow.SetActive(false);
        MainWindow.SetActive(false);
        Cover.SetActive(true);
        CreditWindow.SetActive(false);
        loadingScreen.SetActive(false);
    }


    void Update()
    {
        if (Input.anyKeyDown)
            if (Cover.activeSelf)
            {
                Cover.SetActive(false);
                MainWindow.SetActive(true);
            }
    }

    public void LoadLevel(string sceneName) {
        StartCoroutine(LoadAsynchronous(sceneName));
    }
    IEnumerator LoadAsynchronous(string sceneName) {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while (! operation.isDone) {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loading.value = progress;
            Debug.Log("Loading progress: " + (progress * 100) + "%");
            yield return null;
        }
    }

}
