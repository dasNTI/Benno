using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CameraTransition : MonoBehaviour
{
    public int Transition = 0;
    public float Duration = 0.5f;


    private RawImage ri;
    private RectTransform rt;
    private PlayerMovement pm;
    private const int Transition0Slices = 5;
    private const int Transition1Steps = 10;

    void Start()
    {
        ri = GetComponent<RawImage>();
        rt = GetComponent<RectTransform>();
        pm = GameObject.Find("Player").GetComponent<PlayerMovement>();
        OpenTransition();
    }

    public void OpenTransition()
    {
        IEnumerator d()
        {
            pm.free = false;

            rt.pivot = new Vector2(0.5f, 0);
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.right;

            switch (Transition)
            {
                case 0:
                    ri.color = new Color(0, 0, 0, 1);
                    for (float i = Transition0Slices; i >= 0; i--)
                    {
                        float height = Screen.height * (i / Transition0Slices);
                        rt.sizeDelta = new Vector2(rt.sizeDelta.x, height);
                        yield return new WaitForSecondsRealtime(Duration / Transition0Slices);
                    }
                    break;

                case 1:
                    rt.sizeDelta = new Vector2(rt.sizeDelta.x, Screen.height);
                    for (float i = Transition1Steps; i >= 0; i--)
                    {
                        ri.color = new Color(0, 0, 0, i / Transition1Steps);
                        yield return new WaitForSecondsRealtime(Duration / Transition1Steps);
                    }
                    break;
            }
            pm.free = true;
        };

        StartCoroutine(d());
    }

    public void CloseTransition(string s)
    {
        IEnumerator d()
        {
            pm.free = false;

            rt.pivot = new Vector2(0.5f, 1);
            rt.anchorMin = Vector2.up;
            rt.anchorMax = Vector2.one;

            switch (Transition)
            {
                case 0:
                    ri.color = new Color(0, 0, 0, 1);
                    for (float i = 0; i <= Transition0Slices; i++)
                    {
                        float height = Screen.height * (i / Transition0Slices);
                        rt.sizeDelta = new Vector2(rt.sizeDelta.x, height);
                        yield return new WaitForSecondsRealtime(Duration / Transition0Slices);
                    }
                    break;

                case 1:
                    rt.sizeDelta = new Vector2(rt.sizeDelta.x, Screen.height);
                    for (float i = 0; i <= Transition1Steps; i++)
                    {
                        ri.color = new Color(0, 0, 0, i / Transition1Steps);
                        yield return new WaitForSecondsRealtime(Duration / Transition1Steps);
                    }
                    break;
            };
            SceneManager.LoadScene(s, LoadSceneMode.Single);
        };

        StartCoroutine(d());
    }
}
