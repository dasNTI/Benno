using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkingSpeed = 1;
    public bool free = true;

    private InputMaster Master;
    private BoxCollider2D bc;
    private Animator ani;
    private SpriteRenderer sr;
    public LayerMask lm;
    public Sprite[] WalkSprites;

    private Vector2 WalkDir;
    
    void Awake()
    {
        Master = new InputMaster();
        
    }
    private void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        ani = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        WalkDir = Vector2.zero;
    }
    private void OnEnable()
    {
        Master.Enable();
        //Master.MainInput.Dir.performed += _ => checkOtherSides(1);
    }


    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);

        if (!free)
        {
            ani.SetInteger("Dir", 0);
            ani.enabled = false;
            sr.sprite = WalkSprites[(int)(180 + Mathf.Sign(WalkDir.x) * Vector2.Angle(Vector2.up, WalkDir)) / 90 - 1];
            return;
        }

        float h = Master.MainInput.Dir.ReadValue<Vector2>().x;
        float v = Master.MainInput.Dir.ReadValue<Vector2>().y;

        if (h != 0 && CheckDir(Vector2.right * h, bc.size.y)) transform.position += Vector3.right * h * walkingSpeed;
        if (v != 0 && CheckDir(Vector2.up * v, bc.size.x)) transform.position += Vector3.up * v * walkingSpeed;

        if (Vector2.Angle(Vector2.up, new Vector2(h, v)) % 90 == 0 && new Vector2(h, v) != Vector2.zero)
        {
            int i = (int) (180 + Mathf.Sign(h) * Vector2.Angle(Vector2.up, new Vector2(h, v))) / 90;
            WalkDir = new Vector2(h, v);
            //Debug.Log(i);

            ani.enabled = true;
            ani.SetInteger("Dir", i);
        }else if (Master.MainInput.Dir.ReadValue<Vector2>() == Vector2.zero)
        {
            int i = (int) (180 + Mathf.Sign(WalkDir.x) * Vector2.Angle(Vector2.up, WalkDir)) / 90;

            ani.SetInteger("Dir", 0);
            ani.enabled = false;
            sr.sprite = WalkSprites[i - 1];
            //Debug.Log(i - 1);
        }

    }

    bool CheckDir(Vector2 dir, float l)
    {

        bool b = true;
        float o = 0.05f + walkingSpeed;
        dir = dir.normalized * (1 + o);
        Vector2 tdp = new Vector2(bc.bounds.center.x, bc.bounds.center.y);

        if (dir.x == 0)
        {
            RaycastHit2D ray = Physics2D.Raycast(tdp + dir * bc.bounds.extents.y + Vector2.left * l / 2, Vector2.right, l, lm);

            Debug.DrawRay(tdp + dir * bc.bounds.extents.y + Vector2.left * l / 2, Vector2.right * l, Color.green);

            if (ray.collider != null) b = false;
        }else
        {
            RaycastHit2D ray = Physics2D.Raycast(tdp + dir * bc.bounds.extents.x + Vector2.up * l / 2, Vector2.down, l, lm);

            Debug.DrawRay(tdp + dir * bc.bounds.extents.x + Vector2.up * l / 2, Vector2.down * l, Color.green);

            if (ray.collider != null) b = false;
        }

        return b;
    }

    void checkOtherSides(float l)
    {
        for (int i = 0; i < 4; i++)
        {
            Vector2 v = new Vector2(Mathf.Cos(i * Mathf.PI / 2), Mathf.Sin(i * Mathf.PI / 2));
            Vector2 tdp = new Vector2(transform.position.x, transform.position.y);

            RaycastHit2D ray = Physics2D.Raycast(tdp + v * bc.bounds.extents.x + Vector2.left * l / 2, Vector2.right, l, lm);

            if (ray.collider != null)
            {
                bool t = true;
                while (t)
                {
                    transform.position -= new Vector3(v.x, v.y, 0) * 0.005f;

                    RaycastHit2D r = Physics2D.Raycast(tdp + v * bc.bounds.extents.x + Vector2.left * l / 2, Vector2.right, l, lm);
                    if (r.collider == null) t = false;
                }
            }
        }
    }

    public void walkTo(float x, float y, float dur)
    {
        
    }
}
