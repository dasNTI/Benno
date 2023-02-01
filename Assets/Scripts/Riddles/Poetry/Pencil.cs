using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pencil : MonoBehaviour
{
    public RawImage Paper;
    public Color Tint;
    public Image Background;

    private InputMaster Master;
    private RectTransform rt;
    private RectTransform BackgroundRt;
    private Texture2D Drawing;
    private Color[,] Pixels;
    public bool active;
    private float GridFactor;
    private Vector2 UpperLeftPaperLimit;
    private Vector2 LowerRightPaperLimit;
    void Start()
    {
        Master = new InputMaster();
        rt = GetComponent<RectTransform>();
        BackgroundRt = Background.GetComponent<RectTransform>();
        GridFactor = Screen.height / 150f;

        UpperLeftPaperLimit = new Vector2(
            BackgroundRt.offsetMin.x,
            -BackgroundRt.offsetMax.y
        );
        LowerRightPaperLimit = new Vector2(
            UpperLeftPaperLimit.x + Background.sprite.texture.width,
            UpperLeftPaperLimit.y + Background.sprite.texture.height
        );

        Master.MainInput.Click.performed += _ => {
            active = !active;
        };
        Master.MainInput.Enter.performed += _ => {
            Setup();
        };

        Master.Enable();

        Setup();
    }

    private void Setup() {
        Drawing = new Texture2D(Background.sprite.texture.width, Background.sprite.texture.height);
        Pixels = new Color[Background.sprite.texture.width, Background.sprite.texture.height];

        for (int i = 0; i < Drawing.width; i++) 
            for (int j = 0; j < Drawing.height; j++) {
                Pixels[i, j] = new Color(0, 0, 0, 0);
            }
        updateTexture();
    }

    // Update is called once per frame
    void Update()
    {
        if (!active) return;

        Vector2 GridPosition = ((Vector2) rt.localPosition + new Vector2(Screen.height * 2 / 3f, -Screen.height / 2f));
        GridPosition = new Vector2(
            Mathf.Floor(GridPosition.x),
            Mathf.Floor(GridPosition.y)
        );

        UpperLeftPaperLimit = new Vector2(
            BackgroundRt.offsetMin.x,
            -BackgroundRt.offsetMax.y
        );
        LowerRightPaperLimit = new Vector2(
            UpperLeftPaperLimit.x + Background.sprite.texture.width,
            UpperLeftPaperLimit.y + Background.sprite.texture.height
        );


        Vector2 PaperPosition = new Vector2(GridPosition.x, -GridPosition.y) - UpperLeftPaperLimit;
        //if (GridPosition.x < UpperLeftPaperLimit.x || GridPosition.x > LowerRightPaperLimit.x) return;
        //if (GridPosition.y < UpperLeftPaperLimit.y || GridPosition.y > LowerRightPaperLimit.y) return;

        //Debug.Log(new Vector2((int) PaperPosition.x, (int) PaperPosition.y));

        SetPixel(PaperPosition.x / GridFactor, Background.sprite.texture.height - PaperPosition.y / GridFactor);
        //SetPixel(PaperPosition.x / GridFactor + 1, Background.sprite.texture.height - PaperPosition.y / GridFactor);
        //SetPixel(PaperPosition.x / GridFactor + 1, Background.sprite.texture.height - PaperPosition.y / GridFactor + 1  );
        //SetPixel(PaperPosition.x / GridFactor, Background.sprite.texture.height - PaperPosition.y / GridFactor + 1);

        updateTexture();
        Debug.Log(PaperPosition.x / (120 * GridFactor));
    }

    private void SetPixel(float x, float y) {
        if (x > Background.sprite.texture.width || x < 0) return;
        if (y > Background.sprite.texture.height || y < 0) return;

        Pixels[(int) x, (int) y] = Tint;
    }

    private void updateTexture() {
        for (int i = 0; i < Drawing.width; i++) 
            for (int j = 0; j < Drawing.height; j++)
                Drawing.SetPixel(i, j, Pixels[i, j]);

        Drawing.filterMode = FilterMode.Point;
        Drawing.Apply();

        Paper.texture = (Texture) Drawing;
    }
}
