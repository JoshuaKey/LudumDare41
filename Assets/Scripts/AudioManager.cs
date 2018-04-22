using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioClip startMusic;

    private AudioSource music;
    private AudioSource sound;

    public static AudioManager Instance;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);
        Instance = this;

        music = this.gameObject.AddComponent<AudioSource>();
        sound = this.gameObject.AddComponent<AudioSource>();

        music.loop = true;
        PlayMusic(startMusic);
	}

    public void PlayMusic(AudioClip newMusic) {
        music.clip = newMusic;
        music.Play();
    }

    public void PlaySound(AudioClip newSound) {
        sound.clip = newSound;
        sound.Play();
    }
}
