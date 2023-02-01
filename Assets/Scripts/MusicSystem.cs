using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;
using DG.Tweening;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class MusicSystem : MonoBehaviour
{
    private static string currentTrack;
    private static bool playing;
    private static AudioClip[] tracklist;
    public AudioMixerGroup mixertrack;
    public bool InitializeMixer = false;

    private void Awake() {
        
    }
    void Start()
    {
        startTrack("Casual2");
        if (InitializeMixer) InitMixer();
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

    private void InitMixer() {
        Debug.Log("Yeet");

        if (!File.Exists(Application.persistentDataPath + "/Settings/AudioSettings.json")) {
            SaveMixer();
            return;
        }

        FileStream Stream = new FileStream(Application.persistentDataPath + "/Settings/AudioSettings.json", FileMode.Open);
        BinaryFormatter Formatter = new BinaryFormatter();
        AudioMixer Mixer = mixertrack.audioMixer;

        string Json = Formatter.Deserialize(Stream) as string;
        Stream.Close();

        MixerParams Params = JsonUtility.FromJson<MixerParams>(Json);

        Mixer.SetFloat("MasterVol", Params.MasterVol);
        Mixer.SetFloat("MusicVol", Params.MusicVol);
        Mixer.SetFloat("SoundsVol", Params.SoundsVol);
    }

    public void SaveMixer() {
        AudioMixer mixer = mixertrack.audioMixer;

        MixerParams Params = new MixerParams();

        mixer.GetFloat("MasterVol", out Params.MasterVol);
        mixer.GetFloat("MusicVol", out Params.MusicVol);
        mixer.GetFloat("SoundsVol", out Params.SoundsVol);

        if (!Directory.Exists(Application.persistentDataPath + "/Settings"))
            Directory.CreateDirectory(Application.persistentDataPath + "/Settings");
        FileStream Stream = new FileStream(Application.persistentDataPath + "/Settings/AudioSettings.json", FileMode.Create);

        BinaryFormatter Formatter = new BinaryFormatter();

        Formatter.Serialize(Stream, JsonUtility.ToJson(Params));
        Stream.Close();
    }
}

class MixerParams {
    public float MasterVol;
    public float MusicVol;
    public float SoundsVol;
}