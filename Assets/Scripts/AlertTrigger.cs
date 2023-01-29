using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class AlertTrigger : MonoBehaviour
{
    public bool active = true;
    public Vector2 AlertPosition;
    public bool enter = false;
    public string Txt;
    public string EnterScene;

    private int ID;
    public Action AlertCallback;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (enter) AlertCallback = () => {
            GameObject.Find("Transition").GetComponent<CameraTransition>().CloseTransition(EnterScene);
            GameObject.Find("Alert").GetComponent<AlertIcon>().hideAlert(ID);
        };
        ID = GameObject.Find("Alert").GetComponent<AlertIcon>().showAlert(AlertPosition, Txt, enter, AlertCallback);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject.Find("Alert").GetComponent<AlertIcon>().hideAlert(ID);
        ID = 0;
    }
}
