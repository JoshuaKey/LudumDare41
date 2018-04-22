using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioClip startMusic;

    private AudioSource music;
    private List<AudioSource> sound;

    public AudioClip openSound;

    public static AudioManager Instance;

	// Use this for initialization
	void Start () {
        if(Instance != null) { Destroy(this.gameObject); return; }
        DontDestroyOnLoad(this.gameObject);
        Instance = this;

        music = this.gameObject.AddComponent<AudioSource>();
        sound = new List<AudioSource>();
        for(int i = 0; i < 3; i++) {
            sound.Add(this.gameObject.AddComponent<AudioSource>());
            sound[i].playOnAwake = false;
            sound[i].loop = false;
            sound[i].volume = .5f;
        }
        
        music.loop = true;

        Invoke("Initialize", .5f);
    }

    public void Initialize() {

        PlayMusic(startMusic);
        StartCoroutine(FadeIn());
    }


    public IEnumerator FadeIn() {
        float start = Time.time;
        float length = 3f;

        float maxVol = .75f;

        while (Time.time < start + length) {
            float diff = Time.time - start;
            float t = diff / length;
            music.volume = t * maxVol;
            yield return null;
        }

        music.volume = maxVol;
    }

    public void PlayMusic(AudioClip newMusic) {
        music.clip = newMusic;
        music.Play();
    }

    public void PlaySound(AudioClip newSound) {
        for(int i = 0; i < sound.Count; i++) {
            if (!sound[i].isPlaying) {
                sound[i].clip = newSound;
                sound[i].Play();
                return;
            }
        }
        AudioSource temp = this.gameObject.AddComponent<AudioSource>();
        temp.playOnAwake = false;
        temp.loop = false;
        temp.volume = .5f;

        temp.clip = newSound;
        temp.Play();

        sound.Add(temp);
    }
}
