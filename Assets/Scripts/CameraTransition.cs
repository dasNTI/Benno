using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CameraTransition : MonoBehaviour
{
    public int Transition = 0;
    public float Duration = 0.5f;
    public bool LinkTransitions = true;


    private RawImage ri;
    private RectTransform rt;
    private PlayerMovement pm;
    private PauseMenu pauseMenu;
    private static int prevTransition;
    private const int Transition0Slices = 5;
    private const int Transition1Steps = 10;

    void Start()
    {
        ri = GetComponent<RawImage>();
        rt = GetComponent<RectTransform>();
        pm = GameObject.Find("Player").GetComponent<PlayerMovement>();
        pauseMenu = GameObject.Find("PauseMenu").GetComponent<PauseMenu>();
        OpenTransition();

        if (!(prevTransition > 0)) prevTransition = Transition;
    }

    public void OpenTransition()
    {
        IEnumerator d()
        {
            pauseMenu.available = false;
            pm.free = false;

            rt.pivot = new Vector2(0.5f, 0);
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.right;

            switch (LinkTransitions ? prevTransition : Transition)
            {
                case 0:
                    ri.color = new Color(0, 0, 0, 1);
                    for (float i = Transition0Slices; i >= 0; i--)
                    {
                        float height = Screen.height * (i / Transition0Slices);
                        rt.sizeDelta = new Vector2(rt.sizeDelta.x, height);
                        yield return new WaitForSeconds(Duration / Transition0Slices);
                    }
                    break;

                case 1:
                    rt.sizeDelta = new Vector2(rt.sizeDelta.x, Screen.height);
                    for (float i = Transition1Steps; i >= 0; i--)
                    {
                        ri.color = new Color(0, 0, 0, i / Transition1Steps);
                        yield return new WaitForSeconds(Duration / Transition1Steps);
                    }
                    break;
                case 2:
                    break;
            }
            pm.free = true;
            pauseMenu.available = true;
        };

        StartCoroutine(d());
    }

    public void CloseTransition(string s, string[] props = null)
    {
        IEnumerator d()
        {
            pm.free = false;

            rt.pivot = new Vector2(0.5f, 1);
            rt.anchorMin = Vector2.up;
            rt.anchorMax = Vector2.one;

            prevTransition = Transition;

            switch (Transition)
            {
                case 0:
                    ri.color = new Color(0, 0, 0, 1);
                    for (float i = 0; i <= Transition0Slices; i++)
                    {
                        float height = Screen.height * (i / Transition0Slices);
                        rt.sizeDelta = new Vector2(rt.sizeDelta.x, height);
                        yield return new WaitForSeconds(Duration / Transition0Slices);
                    }
                    break;

                case 1:
                    rt.sizeDelta = new Vector2(rt.sizeDelta.x, Screen.height);
                    for (float i = 0; i <= Transition1Steps; i++)
                    {
                        ri.color = new Color(0, 0, 0, i / Transition1Steps);
                        yield return new WaitForSeconds(Duration / Transition1Steps);
                    }
                    break;
                case 2:
                    break;
            };
            SceneManager.LoadScene(s, LoadSceneMode.Single);
        };

        StartCoroutine(d());
    }
}
