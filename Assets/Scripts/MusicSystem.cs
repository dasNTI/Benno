using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;
using DG.Tweening;

public class MusicSystem : MonoBehaviour
{
    private static string currentTrack;
    private static bool playing;
    private static AudioClip[] tracklist;
    public AudioMixerGroup mixertrack;
    void Start()
    {
        startTrack("Suspicious2");
        if (!GameObject.Find("music_" + currentTrack)) startTrack(currentTrack);
    }

    private void OnEnable() {
        tracklist = Resources.LoadAll<AudioClip>("Music");
    }

    // Update is called once per frame
    public void startTrack(string trackname, float fadeDur = 0) {
        
        if (!Array.Find<AudioClip>(tracklist, track => track.name == trackname) || currentTrack == trackname) return;

        AudioClip track = Array.Find<AudioClip>(tracklist, track => track.name == trackname);

        GameObject newCurrentTrack = new GameObject("music_" + track.name);
        AudioSource audio = newCurrentTrack.AddComponent<AudioSource>();
        audio.clip = track;
        audio.maxDistance = 0;
        audio.loop = true;
        audio.outputAudioMixerGroup = mixertrack;
        //audio.outputAudioMixerGroup = Resources.Load<AudioMixer>("Mixer.mixer").FindMatchingGroups("Master")[0];

        if (fadeDur != 0) {
            audio.volume = 0;
            DOTween.To(() => audio.volume, x => audio.volume = x, 1f, fadeDur);
        }

        audio.Play();

        DontDestroyOnLoad(newCurrentTrack);
        currentTrack = trackname;
    }

    public void pauseMusic(float fadeDur = .1f) {
        AudioSource audio = GameObject.Find("music_" + currentTrack).GetComponent<AudioSource>();
        IEnumerator d() {
            for (float i = 1; i > 0; i -= 0.01f) {
                audio.volume = i;
                yield return new WaitForSecondsRealtime(fadeDur / 100);
            }
            audio.Pause();
        };
        StartCoroutine(d());
    }

    public void resumeMusic() {
        AudioSource audio = GameObject.Find("music_" + currentTrack).GetComponent<AudioSource>();
        audio.volume = 1;
        audio.Play();
    }
}
