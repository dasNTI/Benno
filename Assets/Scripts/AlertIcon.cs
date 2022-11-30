using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class AlertIcon : MonoBehaviour
{
    public Sprite[] KeysSprites;
    public Sprite[] GamepadSprites;
    public float Correction;

    private RectTransform rt;
    private Image ri;
    private GameObject AlertArrow;
    private TMPro.TextMeshProUGUI LabelText;
    private RawImage LabelBackground;

    private Vector3 position;
    private Vector3 checkposition;
    private InputMaster Master;
    private bool gamepad = false;
    public int ArrowSide = 2; // 0 = oben; 1 = rechts; 2 = unten; 3 = links
    private const float ArrowLengthMargin = 8.75f / 7f;
    private const float ArrowTipMargin = 4 / 7f;
    private Action CurrentCb = null;
    private bool CurrentCbModeClick = true;
    private int CurrentID;

    void Start()
    {
        rt = GetComponent<RectTransform>();
        ri = GetComponent<Image>();
        LabelBackground = GameObject.Find("AlertIconLabel").gameObject.GetComponent<RawImage>();
        LabelText = GameObject.Find("AlertIconLabelText").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        AlertArrow = GameObject.Find("AlertArrow");
        Master = new InputMaster();
        Master.Enable();
        Master.MainInput.Click.performed += _ => {if (CurrentCb != null && CurrentCbModeClick) CurrentCb();};
        Master.MainInput.Enter.performed += _ => {if (CurrentCb != null && !CurrentCbModeClick) CurrentCb();};

        ri.color = new Color(255, 255, 255, 0);
        AlertArrow.GetComponent<Image>().color = new Color(255, 255, 255, 0);
        LabelBackground.color = new Color(255, 255, 255, 0);
        LabelText.color = new Color(255, 255, 255, 0);

        position = Vector3.zero;
    }

    private void OnDisable()
    {
        Master.Disable();
    }

    private void Update()
    {
        float margin = 0.3f;
        float x = Mathf.Clamp(position.x, Camera.main.ScreenToWorldPoint(Vector3.zero).x + margin, Camera.main.ScreenToWorldPoint(Vector3.right * Screen.width).x - margin);
        float y = Mathf.Clamp(position.y, Camera.main.ScreenToWorldPoint(Vector3.zero).y + margin, Camera.main.ScreenToWorldPoint(Vector3.up * Screen.height).y - margin);
        float z = GameObject.Find("Overlay").GetComponent<RectTransform>().position.z;
        DOTween.To(() => rt.position, x => rt.position = x, new Vector3(x, y, z), Time.deltaTime * 5);

        float checkdistance = AlertArrow.GetComponent<RectTransform>().sizeDelta.x;
        float xdif = Camera.main.ScreenToWorldPoint(Vector3.right * (Screen.width - checkdistance)).x - position.x;
        float xdif2 = position.x - Camera.main.ScreenToWorldPoint(Vector3.right * checkdistance).x;
        float ydif = Camera.main.ScreenToWorldPoint(Vector3.up * (Screen.height - checkdistance)).y - position.y;

        ArrowSide = 2;
        if (ydif <= 0) ArrowSide = 0;

        if (xdif < 0) ArrowSide = 1;
        if (xdif2 < 0) ArrowSide = 3;

        switch (ArrowSide)
        {
            case 0:
                AlertArrow.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 90);
                rt.pivot = new Vector2(ArrowTipMargin, ArrowLengthMargin);
                break;

            case 1:
                AlertArrow.GetComponent<RectTransform>().eulerAngles = Vector3.zero;
                rt.pivot = new Vector2(ArrowLengthMargin, ArrowTipMargin);
                break;

            case 2:
                AlertArrow.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, -90);
                rt.pivot = new Vector2(ArrowTipMargin, 1 - ArrowLengthMargin);
                break;

            case 3:
                AlertArrow.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, -180);
                rt.pivot = new Vector2(1 - ArrowLengthMargin, ArrowTipMargin);
                break;
        }
    }

    public int showAlert(Vector2 pos, string txt, bool enter, Action cb)
    {
        position = pos;

        float margin = 0.3f;
        float x = Mathf.Clamp(position.x, Camera.main.ScreenToWorldPoint(Vector3.zero).x + margin, Camera.main.ScreenToWorldPoint(Vector3.right * Screen.width).x - margin);
        float y = Mathf.Clamp(position.y, Camera.main.ScreenToWorldPoint(Vector3.zero).y + margin, Camera.main.ScreenToWorldPoint(Vector3.up * Screen.height).y - margin);
        float z = GameObject.Find("Overlay").GetComponent<RectTransform>().position.z;
        rt.position = new Vector3(x, y, z);
        LabelText.text = txt.ToUpper();

        if (enter)
        {
            if (gamepad) ri.sprite = GamepadSprites[1];
            if (!gamepad) ri.sprite = KeysSprites[1];
        }else
        {
            if (gamepad) ri.sprite = GamepadSprites[0];
            if (!gamepad) ri.sprite = KeysSprites[0];
        }

        IEnumerator d()
        {
            yield return new WaitForSeconds(Time.deltaTime * 5);
            DOTween.To(() => ri.color, x => ri.color = x, new Color(255, 255, 255, 1), 0.5f);
            DOTween.To(() => AlertArrow.GetComponent<Image>().color, x => AlertArrow.GetComponent<Image>().color = x, new Color(255, 255, 255, 1), 0.5f);
            if (txt.Length == 0) yield break;
            DOTween.To(() => LabelBackground.color, x => LabelBackground.color = x, new Color(255, 255, 255, 1), 0.5f);
            DOTween.To(() => LabelText.color, x => LabelText.color = x, new Color(255, 255, 255, 1), 0.5f);
        }
        StartCoroutine(d());

        CurrentCb = cb;
        CurrentCbModeClick = !enter;
        CurrentID = UnityEngine.Random.Range(10000, 99999);
        return CurrentID;
    }

    public void hideAlert(int id)
    {
        if (id != CurrentID) return;
        CurrentID = 0;
        CurrentCb = null;
        DOTween.To(() => ri.color, x => ri.color = x, new Color(255, 255, 255, 0), 0.5f);
        DOTween.To(() => AlertArrow.GetComponent<Image>().color, x => AlertArrow.GetComponent<Image>().color = x, new Color(255, 255, 255, 0), 0.5f);
        DOTween.To(() => LabelBackground.color, x => LabelBackground.color = x, new Color(255, 255, 255, 0), 0.5f);
        DOTween.To(() => LabelText.color, x => LabelText.color = x, new Color(255, 255, 255, 0), 0.5f);
    }
}
