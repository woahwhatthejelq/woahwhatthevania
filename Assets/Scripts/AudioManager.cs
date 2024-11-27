using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    // Start is called before the first frame update

    private AudioSource musicSource;
    private AudioSource ambientSource;
    private AudioSource[] sfxSource;

    public static AudioManager instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }

        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.playOnAwake = false;
        musicSource.loop = true;
        ambientSource = gameObject.AddComponent<AudioSource>();
        ambientSource.playOnAwake = false;
        ambientSource.loop = false;
           
        for (int i = 0; i < sfxSource.Length; i++) {
            sfxSource[i] = gameObject.AddComponent<AudioSource>();
            sfxSource[i].playOnAwake = false;
        }
    }

    public void PlayMusic(AudioClip song, float volume) {
        musicSource.clip = song;
        musicSource.volume = volume;
        musicSource.Play();
    }

    public void PlayAmbient(AudioClip song, float volume) {
        ambientSource.clip = song;
        ambientSource.volume = volume;
        ambientSource.Play();
    }

    public void PlaySFX(AudioClip clip, float volume) {
        for (int i = 0; i < sfxSource.Length;i++) {
            if (!sfxSource[i].isPlaying) {
                sfxSource[i].clip = clip;
                sfxSource[i].volume = volume;
                sfxSource[i].Play();
            }
        }
    }

    IEnumerator FadeMusic(AudioSource initSource, AudioSource nextSource, float fadeDuration) {
        float t = 0;
        float volumePercentage = t / fadeDuration;
        yield return null;
    }

    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }
}
