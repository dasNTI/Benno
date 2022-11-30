using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Speech : MonoBehaviour
{
    private GameObject Monologue;
    private TMPro.TextMeshProUGUI MonologueText;
    private GameObject MonologueOptions;
    public TMPro.TMP_FontAsset Font;
    public AudioClip OptionSelect;
    public AudioClip OptionClick;

    private GameObject Dialogue;

    private int SelectedOption;
    private bool OptionsVisible = false;
    private bool initfreestate = true;
    private Option[] CurrentOptions; 

    private InputMaster Master;
    private void Awake()
    {
        Master = new InputMaster();
    }
    private void OnEnable()
    {
        Master.MainInput.Click.performed += _ =>
        {
            if (!OptionsVisible) return;

            AudioSource.PlayClipAtPoint(OptionClick, transform.position, 0.5f);
            GameObject.Find("Player").GetComponent<PlayerMovement>().free = true;
            Monologue.SetActive(false);

            CurrentOptions[SelectedOption].OnClick();
            OptionsVisible = false;
            Master.Disable();
        };

        Master.MainInput.Dir.performed += _ =>
        {
            if (!OptionsVisible) return;

            Vector2 v = Master.MainInput.Dir.ReadValue<Vector2>();
            if (Master.MainInput.Dir.ReadValue<Vector2>().x == 0) return;
            SelectedOption = (int)Mathf.Clamp(SelectedOption + Master.MainInput.Dir.ReadValue<Vector2>().x, 0, CurrentOptions.Length - 1);
            AudioSource.PlayClipAtPoint(OptionSelect, transform.position, 0.5f);
            for (int i = 0; i < CurrentOptions.Length; i++)
            {
                GameObject.Find($"MonologueOption{i}").GetComponent<TMPro.TextMeshProUGUI>().fontStyle = (i == SelectedOption) ? TMPro.FontStyles.Underline : TMPro.FontStyles.Normal;
            }
        };
    }

    void Start()
    {
        Monologue = GameObject.Find("Monologue");
        MonologueText = GameObject.Find("MonologueText").GetComponent<TMPro.TextMeshProUGUI>();
        MonologueOptions = GameObject.Find("MonologueOptions");
        float mheight = 0.325f;
        float mmargin = 0.0013f;
        float fheight = 0.035f;
        Monologue.GetComponent<RectTransform>().sizeDelta = new Vector2(Monologue.GetComponent<RectTransform>().sizeDelta.x, Screen.height * mheight);
        Monologue.SetActive(false);
        MonologueText.gameObject.GetComponent<RectTransform>().offsetMin = new Vector2(30, 20) * mmargin * Screen.height;
        MonologueText.gameObject.GetComponent<RectTransform>().offsetMax = new Vector2(-30, -20) * mmargin * Screen.height;
        MonologueText.fontSize = Screen.height * fheight;
        MonologueOptions.GetComponent<RectTransform>().offsetMin = new Vector2(30, 20) * mmargin * Screen.height;
        MonologueOptions.GetComponent<RectTransform>().offsetMax = new Vector2(-30, -120) * mmargin * Screen.height;

        /*StartMonologue(new Line[] { 
            new Line("Klappt's?", 1, 1) 
        }, new Option[] {
            new Option("Ja", () => {Debug.Log("Toll"); }),
            new Option("Nein", () => {Debug.Log("Scheise"); })
        });*/
    }

    public void StartMonologue(Line[] Lines, Option[] Options = null, bool free = false, Action onfinish = null)
    {
        initfreestate = GameObject.Find("Player").GetComponent<PlayerMovement>().free;
        if (!free) GameObject.Find("Player").GetComponent<PlayerMovement>().free = false;
        Monologue.SetActive(true);


        IEnumerator d()
        {
            if (Lines != null)
            {
                MonologueText.text = "";
                if (MonologueOptions.transform.childCount != 0)
                    for (int i = MonologueOptions.transform.childCount - 1; i >= 0; i--)
                    {
                        Destroy(MonologueOptions.transform.GetChild(i).gameObject);
                    }

                for (int i = 0; i < Lines.Length; i++)
                {
                    string txt = Lines[i].Content;
                    float dur = Lines[i].Duration;
                    float wf = Lines[i].WaitFor;

                    if (MonologueText.text.Length != 0) MonologueText.text += "\n";
                    for (int j = 0; j < txt.Length; j++)
                    {
                        MonologueText.text += txt.ToCharArray()[j];
                        yield return new WaitForSeconds(dur / (float) txt.Length);
                    }

                    yield return new WaitForSeconds(wf);
                }
            }

            if (Options != null)
            {
                float w = Mathf.Abs(MonologueOptions.GetComponent<RectTransform>().rect.width / (float)Options.Length);
                float h = Mathf.Abs(MonologueOptions.GetComponent<RectTransform>().rect.height);
                float fontsize = 0.035f;

                SelectedOption = 0;
                CurrentOptions = Options;

                for (int i = 0; i < Options.Length; i++)
                {
                    GameObject option = new GameObject($"MonologueOption{i}");

                    RectTransform rt = option.AddComponent<RectTransform>();
                    rt.anchorMin = Vector2.up / 2;
                    rt.anchorMax = Vector2.up / 2;
                    rt.pivot = Vector2.up / 2;
                    rt.SetParent(MonologueOptions.transform);
                    rt.anchoredPosition = Vector2.right * w * i;
                    rt.sizeDelta = new Vector2(w, h);
                    rt.localScale = Vector3.one;
                    rt.position = new Vector3(rt.position.x, rt.position.y, 0.1f);
                    TMPro.TextMeshProUGUI tmp = option.AddComponent<TMPro.TextMeshProUGUI>();
                    tmp.font = Font;
                    tmp.fontSize = fontsize * Screen.height;
                    tmp.alignment = TMPro.TextAlignmentOptions.Center;
                    tmp.text = Options[i].Title;
                    if (i == 0) tmp.fontStyle = TMPro.FontStyles.Underline;
                }

                OptionsVisible = true;
                Master.Enable();
            }
            else if (onfinish != null) onfinish();

            if (Options != null) yield break;
            Monologue.SetActive(false);
            GameObject.Find("Player").GetComponent<PlayerMovement>().free = initfreestate;
        }
        StartCoroutine(d());
    }

    public class Line
    {
        public readonly string Content;
        public readonly float Duration;
        public readonly float WaitFor;
        public readonly string Person;
        public Line(string content, float duration, float waitfor, string person = "DB")
        {
            Content = content;
            Duration = duration;
            WaitFor = waitfor;
            Person = person;
        }
    }

    public class Option
    {
        public readonly Action OnClick;
        public readonly string Title;
        public Option(string title, Action onclick)
        {
            OnClick = onclick;
            Title = title;
        }
    }
}
