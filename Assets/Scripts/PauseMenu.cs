using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private InputMaster Master;

    private PlayerMovement Player;
    private bool initialPlayerFree;
    private GameObject MainTab;
    private GameObject SettingsTab;
    private GameObject ConfirmTab;

    public bool available = true;
    private bool active = false;
    private string currentTab = "Main";
    private string currentButton = "Continue";

    void Awake()
    {
        Master = new InputMaster();
    }
    private void Start() {
        MainTab = GameObject.Find("PauseMenuMainTab");
        MainTab.SetActive(false);
        SettingsTab = GameObject.Find("PauseMenuSettingsTab");
        SettingsTab.SetActive(false);
        ConfirmTab = GameObject.Find("PauseMenuConfirmLeaveTab");
        ConfirmTab.SetActive(false);
        Player = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }
    private void OnEnable() {
        Master.Enable();
        
        Master.MainInput.Quit.performed += _ => { OnQPress(); };
        Master.MainInput.Dir.performed += _ => { OnNavigate(); }; 
        Master.MainInput.Click.performed += _ => { OnPress(); }; 
    }

    // Update is called once per frame
    private void OnQPress() {
        if (!active && available) {
            try
            {
                active = true;
                Time.timeScale = 0;
                currentTab = "Main";
                currentButton = "Continue";

                initialPlayerFree = Player.free;
                Player.free = false;
                //GameObject.Find("MusicSystem").GetComponent<MusicSystem>().pauseMusic();

                GetComponent<RawImage>().color = new Color(0, 0, 0, .75f);
                MainTab.SetActive(true);
            } catch (System.Exception)
            {
                
                throw;
            }
            return; 
        }
        if (active ) switch (currentTab) {
            case "Main":
                active = false;
                Time.timeScale = 1;

                Player.free = initialPlayerFree;
                //GameObject.Find("MusicSystem").GetComponent<MusicSystem>().resumeMusic();

                GameObject.Find("PauseMenuMainTabContinue").GetComponent<TMPro.TextMeshProUGUI>().fontStyle = TMPro.FontStyles.Underline;
                GameObject.Find("PauseMenuMainTab" + currentButton).GetComponent<TMPro.TextMeshProUGUI>().fontStyle = TMPro.FontStyles.Normal;


                GetComponent<RawImage>().color = new Color(0, 0, 0, 0);
                MainTab.SetActive(false);
            break;

            case "Settings":
                MainTab.SetActive(true);
                SettingsTab.SetActive(false);
                currentButton = "Settings";
                currentTab = "Main";
            break;
        }
    }

    private void OnNavigate() {
        if (!active) return;

        bool sound = false; 
        Vector2 dir = Master.MainInput.Dir.ReadValue<Vector2>();

        void select(string option, bool selected) {
            if (selected) GameObject.Find(option).GetComponent<TMPro.TextMeshProUGUI>().fontStyle = TMPro.FontStyles.Underline;
            if (!selected) GameObject.Find(option).GetComponent<TMPro.TextMeshProUGUI>().fontStyle = TMPro.FontStyles.Normal;
        }

        switch (currentTab) {
            case "Main": 
                switch(currentButton) {
                    case "Continue":
                        if (dir.y < 0) {
                            select("PauseMenuMainTabContinue", false);
                            select("PauseMenuMainTabSettings", true);
                            sound = true;
                            currentButton = "Settings";
                        }
                    break;
                    case "Settings":
                        if (dir.y < 0) {
                            select("PauseMenuMainTabSettings", false);
                            select("PauseMenuMainTabControls", true);
                            sound = true;
                            currentButton = "Controls";
                        }else if (dir.y > 0) {
                            select("PauseMenuMainTabSettings", false);
                            select("PauseMenuMainTabContinue", true);
                            sound = true;
                            currentButton = "Continue";
                        }
                    break;
                    case "Controls":
                        if (dir.y < 0) {
                            select("PauseMenuMainTabControls", false);
                            select("PauseMenuMainTabMainMenu", true);
                            sound = true;
                            currentButton = "MainMenu";
                        }else if (dir.y > 0) {
                            select("PauseMenuMainTabControls", false);
                            select("PauseMenuMainTabSettings", true);
                            sound = true;
                            currentButton = "Settings";
                        }
                    break;
                    case "MainMenu":
                        if (dir.y < 0) {
                            select("PauseMenuMainTabMainMenu", false);
                            select("PauseMenuMainTabLeave", true);
                            sound = true;
                            currentButton = "Leave";
                        }else if (dir.y > 0) {
                            select("PauseMenuMainTabMainMenu", false);
                            select("PauseMenuMainTabControls", true);
                            sound = true;
                            currentButton = "Controls";
                        }
                    break;
                    case "Leave":
                        if (dir.y > 0) {
                            select("PauseMenuMainTabLeave", false);
                            select("PauseMenuMainTabMainMenu", true);
                            sound = true;
                            currentButton = "MainMenu";
                        }
                    break;
                }
            break;

            case "Settings":
                void SelectSetting(string setting, bool selected) {
                    Color c = selected ? new Color(255, 255, 255, 0.1f) : new Color(255, 255, 255, 0);
                    GameObject.Find(setting).GetComponent<Image>().color = c;
                }

                switch (currentButton) {
                    case "MasterVolume":
                        if (dir.y > 0) {
                            SelectSetting("PauseMenuSettingsTabAudioGeneral", false);
                        }else if (dir.y < 0) {
                            SelectSetting("PauseMenuSettingsTabAudioGeneral", false);
                            SelectSetting("PauseMenuSettingsTabAudioMusic", true);
                            currentButton = "MusicVolume";
                        }
                    break;
                    case "MusicVolume":
                        if (dir.y > 0) {
                            SelectSetting("PauseMenuSettingsTabAudioGeneral", true);
                            SelectSetting("PauseMenuSettingsTabAudioMusic", false);
                            currentButton = "MasterVolume";
                        }else if (dir.y < 0) {
                            SelectSetting("PauseMenuSettingsTabAudioMusic", false);
                            SelectSetting("PauseMenuSettingsTabAudioSounds", true);
                            currentButton = "SoundsVolume";
                        }
                    break;
                    case "SoundsVolume":
                        if (dir.y > 0) {
                            SelectSetting("PauseMenuSettingsTabAudioMusic", true);
                            SelectSetting("PauseMenuSettingsTabAudioSounds", false);
                            currentButton = "MusicVolume";
                        }
                    break;
                }
            break;
        }

        if (sound) return;
    }

    void OnPress() {
        if (!active) return;

        switch (currentTab) {
            case "Main":
                switch(currentButton) {
                    case "Continue":
                        active = false;
                        Time.timeScale = 1;

                        Player.free = initialPlayerFree;

                        GetComponent<RawImage>().color = new Color(0, 0, 0, 0);
                        MainTab.SetActive(false);
                    break;
                    case "Settings":
                        SettingsTab.SetActive(true);
                        MainTab.SetActive(false);
                        currentTab = "Settings";
                        currentButton = "MasterVolume";
                        GameObject.Find("PauseMenuSettingsTabAudioGeneral")
                            .GetComponent<Image>().color = new Color(255, 255, 255, 0.1f);
                    break;
                    case "Controls":

                    break;
                    case "MainMenu":

                    break;
                    case "Leave":

                    break;
                }
            break;

            case "Settings":
                switch (currentButton) {
                    case "MasterVolume":
                        currentButton = "MasterVolumeSlider";
                        GameObject.Find("PauseMenuSettingsTabAudioGeneralSlider").GetComponent<UIVolumeSlider>().Select();
                    break;
                    case "MasterVolumeSlider":
                        currentButton = "MasterVolume";
                    break;
                    case "MusicVolume":
                        currentButton = "MusicVolumeSlider";
                        GameObject.Find("PauseMenuSettingsTabAudioMusicSlider").GetComponent<UIVolumeSlider>().Select();
                    break;
                    case "MusicVolumeSlider":
                        currentButton = "MusicVolume";
                    break;
                    case "SoundsVolume":
                        currentButton = "SoundsVolumeSlider";
                        GameObject.Find("PauseMenuSettingsTabAudioSoundsSlider").GetComponent<UIVolumeSlider>().Select();
                    break;
                    case "SoundsVolumeSlider":
                        currentButton = "SoundsVolume";
                    break;
                }
            break;
        }
    }
}
