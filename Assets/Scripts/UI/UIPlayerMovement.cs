using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerMovement : MonoBehaviour
{
    public bool LimitedToScreen = true;
    public bool active = true;
    public float Speed = 1f;

    private Image sr;
    private RectTransform rt;
    private InputMaster Master;
    void Start()
    {
        Master = new InputMaster();
        sr = GetComponent<Image>();
        rt = GetComponent<RectTransform>();

        Master.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (!active) return;

        float Factor = Screen.height / 1505f;
        Vector2 Dir = Master.MainInput.Dir.ReadValue<Vector2>() * Speed * Factor * Time.timeScale;
        float DiagonalFactor = (Dir.x != 0 && Dir.y != 0) ? 1f / Mathf.Sqrt(.5f) : 1;

        rt.localPosition = new Vector2(
            rt.localPosition.x + Dir.x,
            rt.localPosition.y + Dir.y
        );

        float margin = 5 * Factor;
        rt.localPosition = new Vector2(
            Mathf.Min(
                Screen.height * 2 / 3f - sr.sprite.texture.width * Factor - margin, 
                Mathf.Max(-Screen.height * 2 / 3f + margin, rt.localPosition.x)
                ),
            Mathf.Min(
                Screen.height / 2f - sr.sprite.texture.height * Factor / 5f - margin, 
                Mathf.Max(-Screen.height / 2f + margin, rt.localPosition.y)
                )
        );
    }
}
