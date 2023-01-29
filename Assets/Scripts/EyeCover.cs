using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeCover : MonoBehaviour
{
    private SpriteRenderer sr;
    private Animator ani;
    public Sprite[] sprites;
    public bool NpcMode = true;
    public bool active;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if (NpcMode) return;
        ani = transform.parent.gameObject.GetComponent<Animator>();
    }
    private void OnEnable() {
        StartCoroutine(cycle());
    }

    // Update is called once per frame
    void Update()
    {
        if (NpcMode) return;
        switch (ani.GetInteger("Dir")) {
            case 0:
                sr.color = new Color(255, 255, 255, 1);
                sr.sprite = sprites[1];
            break;
            case 1:
                sr.color = new Color(255, 255, 255, 1);
                sr.sprite = sprites[0];
            break;
            case 2:
    	        sr.color = new Color(255, 255, 255, 0);
            break;
            case 3:
                sr.color = new Color(255, 255, 255, 1);
                sr.sprite = sprites[2];
            break;
            case 4:
                sr.color = new Color(255, 255, 255, 1);
                sr.sprite = sprites[1];
            break;
        }
    }

    IEnumerator cycle() {
        while (enabled) {
            yield return new WaitForSeconds(Random.Range(1, 9));
            for (int i = 0; i < 5; i++) {
                transform.localScale = new Vector3(1, Mathf.Pow(Mathf.Sin(i * Mathf.PI / 4f), 2), 1);
                if (active) yield return new WaitForSeconds(0.1f);
            }
            if (!active) yield return new WaitWhile(() => !active);
        }
    }
}
