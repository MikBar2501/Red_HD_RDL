using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class StreamVideo : MonoBehaviour
{

    public VideoPlayer videoPlayer;
    public AudioSource audioSource;
    public RawImage rawImage;

    public Menu menu;

    public GameObject panel;

    public AudioClip menuTheme;

    public void Play() {
        audioSource.Stop();
        panel.SetActive(true);
        StartCoroutine(PlayVideo());
    }

    void Start() {
        panel.SetActive(false);
        audioSource.loop = true;
        audioSource.clip = menuTheme;
    }

    private void CheckOver()
    {
        long playerCurrentFrame = videoPlayer.frame;
        long playerFrameCount = (long)videoPlayer.frameCount;
        
        if(playerCurrentFrame < playerFrameCount - 1)
        {
            print ("VIDEO IS PLAYING");
            print("currentFrame: " + playerCurrentFrame + ", Frames: " + playerFrameCount);
        }
        else
        {
            print ("VIDEO IS OVER");
            //Do w.e you want to do for when the video is done playing.
        
            //Cancel Invoke since video is no longer playing
            CancelInvoke("CheckOver");
            menu.Play();
        }
    }

    IEnumerator PlayVideo()
    {
          videoPlayer.Prepare();
          WaitForSeconds waitForSeconds = new WaitForSeconds(1);
          while (!videoPlayer.isPrepared)
          {
               yield return waitForSeconds;
               break;
          }
          rawImage.texture = videoPlayer.texture;
          //audioSource.clip = videoPlayer.clip;
          videoPlayer.Play();
          audioSource.Play();
          InvokeRepeating("CheckOver", .1f,.1f);
     }

}
