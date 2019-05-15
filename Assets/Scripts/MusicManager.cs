using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip mainMotive; 
    private AudioSource musicSourceA;
	private AudioSource musicSourceB; 
    public static MusicManager instance;

    public AudioClip[] tracks;

	[Range(0f, 1f)] public float musicVolume = 1.0f;

    public float crossFadeTime = 2.0f;
	public int currentTrack = 0;

	//Track 0 - główna muzyczka
	//Track 1 - muzyczka na mocniejsze momenty
	//Track 2 - muzyczka chill
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
		musicSourceA = gameObject.AddComponent<AudioSource>();
		musicSourceB = gameObject.AddComponent<AudioSource>();
		musicSourceA.volume = musicVolume;
		musicSourceB.volume = 0.0f;
		musicSourceA.playOnAwake = false;
		musicSourceB.playOnAwake = false;
		musicSourceA.loop = true;
		musicSourceB.loop = true;
		musicSourceA.Stop();
		musicSourceB.Stop();
        StartMusic();
        
    }

    public void StartMusic() {
		
		StartCoroutine(SwitchTrack(currentTrack));
	}

    private IEnumerator SwitchTrack(int i) {
		bool play_a = true;
		if(musicSourceB.volume == 0.0f) play_a = false;

		if(play_a) {
			musicSourceA.clip = tracks[i];
			yield return StartCoroutine(CrossFade(musicSourceB, musicSourceA, crossFadeTime));
		} else {
			musicSourceB.clip = tracks[i];
			yield return StartCoroutine(CrossFade(musicSourceA, musicSourceB, crossFadeTime));
		}
	}

    private IEnumerator CrossFade(AudioSource a, AudioSource b, float seconds) {
		float step_interval = seconds / 40.0f;
		float vol_interval = musicVolume / 40.0f;

		b.Play();

		for(int i = 0; i < 20; i++) {
			a.volume -= vol_interval;
			b.volume += vol_interval;
			yield return new WaitForSeconds(step_interval);
		}

		a.Stop();
	}

    public IEnumerator StopMusicFade() {
		float step_interval = 10.0f / 20.0f;
		float vol_interval = musicVolume / 20.0f;
		for(int i = 0; i < 20; i++) {
			musicSourceA.volume -= vol_interval;
			musicSourceB.volume -= vol_interval;
			yield return new WaitForSeconds(step_interval);
		}

		musicSourceA.Stop();
		musicSourceB.Stop();
	}

    public void ChangeMusic(int musicOnList) {
        if(currentTrack != musicOnList) {
            StartCoroutine(SwitchTrack(musicOnList));
            currentTrack = musicOnList;
        }
    }
}
