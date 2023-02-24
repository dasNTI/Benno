using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeCover : MonoBehaviour
{
    public Sprite[] sprites;
    public bool NpcMode = true;
    public bool Glasses = false;
    public int NpcModeDir = 2;
    public float rotation = 0;
    public Vector2 NpcModeEyeOffset;
    public Sprite[] GlassesSprites;
    public bool active;
    
    private SpriteRenderer sr;
    private Animator ani;
    private Transform EyeTransform;
    private SpriteRenderer GlassesSr;
    void Start()
    {
        if (Glasses) {
            GlassesSr = transform.GetChild(1).GetComponent<SpriteRenderer>();
        }
        if (NpcMode) {
            EyeTransform = transform.GetChild(0).transform;
            sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
            return;
        }
        EyeTransform = transform;
        sr = GetComponent<SpriteRenderer>();
        ani = transform.parent.gameObject.GetComponent<Animator>();
    }
    private void OnEnable() {
        StartCoroutine(cycle());
    }

    void Update()
    {
        if (NpcMode) {
            EyeTransform.localPosition = new Vector3(NpcModeEyeOffset.x, NpcModeEyeOffset.y, -0.1f);
            EyeTransform.eulerAngles = new Vector3(0, 0, rotation);
        }

        switch (NpcMode ? NpcModeDir : ani.GetInteger("Dir")) {
            case 0: // straight
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
                sr.sprite = sprites[1];
                if (!Glasses) break;
                GlassesSr.sprite = GlassesSprites[0];
                GlassesSr.color = Color.white;
            break;
            case 1: // left
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
                sr.sprite = sprites[0];
                if (!Glasses) break;
                GlassesSr.sprite = GlassesSprites[1];
                GlassesSr.color = Color.white;
            break;
            case 2: // back
    	        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0);
                if (!Glasses) break;
                GlassesSr.color = new Color(0,0,0,0);
            break;
            case 3: // right
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
                sr.sprite = sprites[2];
                if (!Glasses) break;
                GlassesSr.sprite = GlassesSprites[2];
                GlassesSr.color = Color.white;
            break;
            case 4:
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
                sr.sprite = sprites[1];
                if (!Glasses) break;
                GlassesSr.sprite = GlassesSprites[0];
                GlassesSr.color = Color.white;
            break;
        }

        if (Glasses) {
            GlassesSr.gameObject.transform.localPosition = new Vector3(NpcModeEyeOffset.x, NpcModeEyeOffset.y, -0.2f);
            GlassesSr.gameObject.transform.eulerAngles = new Vector3(0, 0, rotation);
        }
    }

    IEnumerator cycle() {
        while (enabled) {
            yield return new WaitForSeconds(Random.Range(40, 150) / 10f);
            for (int i = 0; i < 5; i++) {
                EyeTransform.localScale = new Vector3(1, Mathf.Pow(Mathf.Sin(i * Mathf.PI / 4f), 2), 1);
                if (active) yield return new WaitForSeconds(0.1f);
            }
            if (!active) yield return new WaitWhile(() => !active);
        }
    }
}
