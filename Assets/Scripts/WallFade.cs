using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WallFade : MonoBehaviour
{
    private bool touching = false;
    private BoxCollider2D bc;
    private SpriteRenderer sr;

    public float touchingOp = 0.3f;
    public float FadeDur = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(TouchingPlayer());

        if (touching != TouchingPlayer())
        {
            touching = TouchingPlayer();
            Change(touching);
        }
    }

    void Change(bool t)
    {
        float o = t ? touchingOp : 1;
        Color c = new Color(sr.color.r, sr.color.g, sr.color.b, o);

        DOTween.To(() => sr.color, x => sr.color = x, c, FadeDur);
        if (transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                SpriteRenderer csr = transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>();
                Color cc = new Color(csr.color.r, csr.color.g, csr.color.b, o);
                DOTween.To(() => csr.color, x => csr.color = x, cc, FadeDur);
            }
        }
    }

    bool TouchingPlayer()
    {
        BoxCollider2D pbc = GameObject.Find("Player").GetComponent<BoxCollider2D>();
        if (pbc.bounds.center.y < bc.bounds.center.y - bc.bounds.extents.y) return false;
        if (Mathf.Abs(bc.bounds.center.x - pbc.bounds.center.x) < bc.bounds.extents.x + pbc.bounds.extents.x &&
            Mathf.Abs(bc.bounds.center.y - pbc.bounds.center.y) < bc.bounds.extents.y + pbc.bounds.extents.y) return true;
        return false;
    }
}
