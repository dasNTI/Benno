using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeCover : MonoBehaviour
{
    private SpriteRenderer sr;
    private Animator ani;
    private Transform EyeTransform;
    public Sprite[] sprites;
    public bool NpcMode = true;
    public int NpcModeDir = 2;
    public Vector2 NpcModeEyeOffset;
    public bool active;
    void Start()
    {
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

    // Update is called once per frame
    void Update()
    {
        if (NpcMode) EyeTransform.localPosition = new Vector3(NpcModeEyeOffset.x, NpcModeEyeOffset.y, -0.1f);

        switch (NpcMode ? NpcModeDir : ani.GetInteger("Dir")) {
            case 0:
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
                sr.sprite = sprites[1];
            break;
            case 1:
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
                sr.sprite = sprites[0];
            break;
            case 2:
    	        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0);
            break;
            case 3:
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
                sr.sprite = sprites[2];
            break;
            case 4:
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
                sr.sprite = sprites[1];
            break;
        }
    }

    IEnumerator cycle() {
        while (enabled) {
            yield return new WaitForSeconds(Random.Range(10, 90) / 10f);
            for (int i = 0; i < 5; i++) {
                EyeTransform.localScale = new Vector3(1, Mathf.Pow(Mathf.Sin(i * Mathf.PI / 4f), 2), 1);
                if (active) yield return new WaitForSeconds(0.1f);
            }
            if (!active) yield return new WaitWhile(() => !active);
        }
    }
}
