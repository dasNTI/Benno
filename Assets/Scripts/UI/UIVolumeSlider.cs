using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UIVolumeSlider : MonoBehaviour
{
    public AudioMixer Mixer;
    public string Channel;
    public AudioMixerGroup MixerChannel;
    public bool Selected;
    public Sprite[] UnselectedSprites;
    public Sprite[] SelectedSprites;

    private int VolumeStep = 0;
    private const int Max = 1048576;
    private Image image;
    private InputMaster Master;
    private MusicSystem musicSystem;

    private void Awake() {
        Master = new InputMaster();
        Master.Enable();   
    }
    void Start()
    {
        image = GetComponent<Image>();

        musicSystem = GameObject.Find("MusicSystem").GetComponent<MusicSystem>();

        Master.MainInput.Dir.performed += _ => {
            if (!Selected) return;


            Vector2 Dir = Master.MainInput.Dir.ReadValue<Vector2>();
            int change = Mathf.RoundToInt(Mathf.Max(-1, Mathf.Min(1, Dir.x + Dir.y)));

            Debug.Log(change);

            VolumeStep = (int) Mathf.Max(0, Mathf.Min(9, VolumeStep + change));

            image.sprite = SelectedSprites[VolumeStep];
            Mixer.SetFloat(Channel, StepToVolume(VolumeStep));

        };

        Master.MainInput.Quit.performed += _ => {
            try
            {
                image.sprite = UnselectedSprites[VolumeStep];
            }
            catch (System.Exception) {}            
            
            Selected = false;
            musicSystem.SaveMixer();
        };
        Master.MainInput.Click.performed += _ => {
            try
            {
                image.sprite = UnselectedSprites[VolumeStep];
            }
            catch (System.Exception) {}
            
            Selected = false;
            musicSystem.SaveMixer();
        };
    }

    private void OnEnable() {
        float volume;
        Mixer.GetFloat(Channel, out volume);
        VolumeStep = VolumeToStep(volume);

        image = GetComponent<Image>();
        image.sprite = UnselectedSprites[VolumeStep];
    }

    public void Select() {
        Selected = true;
        image.sprite = SelectedSprites[VolumeStep];
    }

    float StepToVolume(int step) {
        return Mathf.Log10((step + 0.0001f) / 9f) * 20;
    }

    int VolumeToStep(float volume) {
        return Mathf.FloorToInt(Mathf.Pow(10, volume / 20) * 10);
    }
}
