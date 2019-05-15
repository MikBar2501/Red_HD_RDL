using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChangeScript : MonoBehaviour
{
    public int myMusicPlay;

    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            MusicManager.instance.ChangeMusic(myMusicPlay);
        }
    }
}
