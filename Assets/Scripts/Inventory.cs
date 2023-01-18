using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public bool Accessable = true;

    private InputMaster Master;
    private Image Sr;
    private AudioSource OpenAudio;
    private GameObject InventoryContent;

    private bool Active = false;

    private void Awake() {
        Master = new InputMaster();

        InventoryContent = GameObject.Find("InventoryContent");
        InventoryContent.SetActive(false);
    }
    private void OnEnable() {
        Master.Enable();
    }
    private void OnDisable() {
        Master.Disable();
    }

    private void Start() {
        Master.MainInput.Dir.performed += _ => {OnNavigate();};
        Master.MainInput.Inventory.performed += _ => {OnIPress();};
        Master.MainInput.Quit.performed += _ => {OnQPress();};
        Master.MainInput.Click.performed += _ => {OnFPress();};

        Sr = GetComponent<Image>();
        OpenAudio = GetComponent<AudioSource>();

        Sr.color = new Color(255, 255, 255, 0);
    }


    void OnIPress() {
        if (!Accessable) return;

        if (!Active) {
            OpenAudio.Play();
            Sr.color = new Color(255, 255, 255, 1);
            InventoryContent.SetActive(true);
            Active = true;
        }else {
            Sr.color = new Color(255, 255, 255, 0);
            InventoryContent.SetActive(false);
            Active = false;
        }
    }

    void OnQPress() {
        if (!Active) return;
    }

    void OnFPress() {
        if (!Active) return;
    }

    void OnNavigate() {

    }

    public void UpdateInventoryContent() {
        
    }
}
