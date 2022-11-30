using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTrigger : MonoBehaviour
{
    public bool active = true;
    public string EnterScene;

    private bool entering;

    void Start()
    {
        entering = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("yeet");
        if (!active) return;
        if (entering) return;

        GameObject.Find("Transition").GetComponent<CameraTransition>().CloseTransition(EnterScene);
        entering = true;
    }
}
