using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGridScaling : MonoBehaviour
{
    private RectTransform rt;
    private RawImage SrRaw;
    private Image Sr;
    private bool Raw = false;

    public bool AdjustMargins = true;
    public Vector2 UpperLeftMargin;
    public Vector2 LowerRightMargin;

    void Start()
    {
        SrRaw = GetComponent<RawImage>();
        Sr = GetComponent<Image>();
        rt = GetComponent<RectTransform>();

        float ImageHeight, ImageWidth;

        if (Sr) {
            ImageHeight = Sr.sprite.texture.height;
            ImageWidth = Sr.sprite.texture.width;
        }else {
            ImageHeight = SrRaw.texture.height;
            ImageWidth = SrRaw.texture.width;
        }

        rt.sizeDelta = new Vector2(
            Screen.height / 150f * ImageWidth,
            Screen.height / 150f * ImageHeight
        );

        if (!AdjustMargins) return;

        float Factor = Screen.height / 150f;

        rt.offsetMin = new Vector2(
            UpperLeftMargin.x * Factor,
            LowerRightMargin.y * Factor   
        );
        rt.offsetMax = new Vector2(
            -LowerRightMargin.x * Factor,
            -UpperLeftMargin.y * Factor
        );
    }
}
