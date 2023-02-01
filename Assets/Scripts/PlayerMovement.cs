using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 0.1f;
    public bool free = true;
    public float zOffset = 0;

    private InputMaster Master;
    private BoxCollider2D bc;
    private Animator ani;
    private SpriteRenderer sr;
    public LayerMask lmH;
    public LayerMask lmV;
    private static Vector2 WalkDir;
    private BoxCollider2D diagonalWallCollider;
    private bool idling = true;
    public static string prevScene = "";
    
    void Awake()
    {
        Master = new InputMaster();
    }
    private void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        ani = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        if (WalkDir.magnitude != 1) WalkDir = Vector2.down;

        InitializePosition();
        ani.StartPlayback();
    }
    private void OnEnable()
    {
        Master.Enable();
    }
    private void OnDisable() {
        Master.Disable();
    }
    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y + zOffset);

        if (!free)
        {
            ani.speed = 0;
            return;
        }

        float h = Master.MainInput.Dir.ReadValue<Vector2>().x;
        float v = Master.MainInput.Dir.ReadValue<Vector2>().y;
        float heightCheckLengthFactor = 1.55f;
        float widthCheckLengthFactor = 1.7f;

        if (h != 0 && CheckDir(Vector2.right * h, bc.bounds.extents.y * heightCheckLengthFactor))
            transform.position += Vector3.right * (h + Mathf.Sign(h) * Mathf.Abs(v) * (1 - Mathf.Sqrt(0.5f))) * walkSpeed;

        if (v != 0 && CheckDir(Vector2.up * v, bc.bounds.extents.x * widthCheckLengthFactor)) 
            transform.position += Vector3.up * (v + Mathf.Sign(v) * Mathf.Abs(h) * (1 - Mathf.Sqrt(0.5f))) * walkSpeed; 

        if (new Vector2(h, v) != Vector2.zero)
        {
            if (Vector2.Angle(Vector2.up, new Vector2(h, v)) % 90 == 0) {
                Vector2 current = new Vector2(h, v);
                if (current != WalkDir && current.magnitude == 1)
                    WalkDir = new Vector2(h, v);
            }

            adjustAniDir();

            ani.speed = 1;

            idling = false;
        }else if (Master.MainInput.Dir.ReadValue<Vector2>() == Vector2.zero)
        {
            ani.SetInteger("Dir", 0);
            WalkDir = Vector2.down;
            //idlingcycle();
        }
    }

    void adjustAniDir() {
        int i = (int) (-Vector2.SignedAngle(Vector2.up, WalkDir) + 180) / 90;
        if (i == 0) i = 4;
        ani.SetInteger("Dir", i);
    }

    public bool CheckDir(Vector2 dir, float l)
    {

        bool b = true;
        float o = 0.05f + walkSpeed;
        dir = dir.normalized * (1 + o);
        Vector2 tdp = new Vector2(bc.bounds.center.x, bc.bounds.center.y);

        if (dir.x == 0)
        {
            RaycastHit2D ray = Physics2D.Raycast(tdp + dir * bc.bounds.extents.y + Vector2.left * l / 2, Vector2.right, l, lmH);

            Debug.DrawRay(tdp + dir * bc.bounds.extents.y + Vector2.left * l / 2, Vector2.right * l, Color.green);

            if (ray.collider != null) b = false;
            //if (ray.collider != null && ray.collider.gameObject.tag == "DiagonalWall") handleDiagonalWall((BoxCollider2D) ray.collider);
        }else
        {
            RaycastHit2D ray = Physics2D.Raycast(tdp + dir * bc.bounds.extents.x + Vector2.up * l / 2, Vector2.down, l, lmV);

            Debug.DrawRay(tdp + dir * bc.bounds.extents.x + Vector2.up * l / 2, Vector2.down * l, Color.green);

            if (ray.collider != null) b = false;
            //if (ray.collider != null && ray.collider.gameObject.tag == "DiagonalWall") handleDiagonalWall((BoxCollider2D) ray.collider);
        }

        return b;
    }

    void handleDiagonalWall(BoxCollider2D collider) {
        diagonalWallCollider = collider;
        float rotation = collider.gameObject.transform.rotation.eulerAngles.z;
            rotation = (rotation % 180f) - 180 * Mathf.Floor(rotation / 180);
        Vector2 mov = Master.MainInput.Dir.ReadValue<Vector2>();

        //if (xmov != 0 && ymov != 0) return;

        float f(float x) {
            //return Mathf.Sqrt(1 - Mathf.Pow(2 * x - 1, 2));
            return Mathf.Sin(Mathf.PI * x);
        }

        float xf = f(Mathf.Abs(rotation) / 90f) * mov.y * Mathf.Sign(rotation);
        float yf = f(Mathf.Abs(rotation) / 90f) * mov.x * Mathf.Sign(rotation);

        transform.position = new Vector3(transform.position.x + xf * walkSpeed * xf, transform.position.y +  yf * walkSpeed, transform.position.z);
    }

    void checkOtherSides(float l)
    {
        for (int i = 0; i < 4; i++)
        {
            Vector2 v = new Vector2(Mathf.Cos(i * Mathf.PI / 2), Mathf.Sin(i * Mathf.PI / 2));
            Vector2 tdp = new Vector2(transform.position.x, transform.position.y);

            RaycastHit2D ray = Physics2D.Raycast(tdp + v * bc.bounds.extents.x + Vector2.left * l / 2, Vector2.right, l, lmH);

            if (ray.collider != null)
            {
                bool t = true;
                while (t)
                {
                    transform.position -= new Vector3(v.x, v.y, 0) * 0.005f;

                    RaycastHit2D r = Physics2D.Raycast(tdp + v * bc.bounds.extents.x + Vector2.left * l / 2, Vector2.right, l, lmH);
                    if (r.collider == null) t = false;
                }
            }
        }
    }

    public void walkTo(float x, float y, float dur)
    {
        
    }

    private void idlingcycle() 
    {
        if (idling) return;
        idling = true;

        IEnumerator cycle()
        {
            while (idling) {
                if (!idling) yield break;

                //blink();

                yield return new WaitForSeconds(UnityEngine.Random.Range(1, 1));
            }
        }
        StartCoroutine(cycle());
    }

    private void InitializePosition()
    {
        //IEnumerator d() {
        //    string test = prevScene;
        //    yield return new WaitForSeconds(1);
        //    GameObject.Find("Speech").GetComponent<Speech>().StartMonologue(new Speech.Line[1] {
        //        new Speech.Line(test, 1, 1)
        //    });
        //}
        //StartCoroutine(d());

        if (prevScene.Length < 1)
        {
            prevScene = SceneManager.GetActiveScene().name;
            return;
        }

        string[] possiblePrevScenes = null;
        Vector2[] positionsPlusRotations = null;


        switch (SceneManager.GetActiveScene().name) // First Vector = Position, 2nd Vector = Rotation
        {
            // Erdgeschoss
            case "30er":
                positionsPlusRotations = new Vector2[6] 
                {
                    new Vector2(-85.1f, 33.83f),
                    new Vector2(-61.77f, 26.79f),
                    new Vector2(-22.21f, 32.95f),
                    new Vector2(-20.14f, 27),
                    new Vector2(-74.85f, 29.55f),
                    new Vector2(-74.85f, 29.55f)
                };
                possiblePrevScenes = new string[6]
                {
                    "MusicHall",
                    "MilkHall",
                    "MusicGeographyHallway",
                    "Agora",
                    "Classroom",
                    "ClassroomFilled"
                };
                break;
            case "Agora":
                positionsPlusRotations = new Vector2[4]
                {
                    new Vector2(-22.59f, 25.61f),
                    new Vector2(-26.07f, 1.44f),
                    new Vector2(7.5f, 23.95f),
                    new Vector2(-25.7f, 7.09f)
                };
                possiblePrevScenes = new string[4]
                {
                    "30er",
                    "ScienceHallway",
                    "TeacherFoyer",
                    "Library"
                };
                break;
            case "Classroom":
                positionsPlusRotations = new Vector2[1] { new Vector2(-71.15f, 31.5f) }; //temp
                possiblePrevScenes = new string[1] { "30er" };
                break;
            case "Geography":
                positionsPlusRotations = new Vector2[3]
                {
                    new Vector2(-16.69f, 67.82f),
                    new Vector2(14.4f, 67.72f),
                    new Vector2(14.04f, 63.93f)
                };
                possiblePrevScenes = new string[3]
                {
                    "MusicGeographyHallway",
                    "Mensa",
                    "TeacherFoyer"
                };
                break;
            case "Mensa":

                break;
            case "MilkHall":
                positionsPlusRotations = new Vector2[2]
                {
                    new Vector2(-61.78f, 5.2f),
                    new Vector2(-61.62f, 24.82f)
                };
                possiblePrevScenes = new string[2]
                {
                    "ScienceHallway",
                    "30er"
                };
                break;
            case "Music":
                positionsPlusRotations = new Vector2[1] { new Vector2(-88.05f, 52.21f) };
                possiblePrevScenes = new string[1] { "MusicHall" };
                break;
            case "MusicGeographyHallway":
                positionsPlusRotations = new Vector2[3]
                {
                    new Vector2(-77.21f, 54.57f),
                    new Vector2(-22.25f, 35.25f),
                    new Vector2(-16.77f, 67.95f)
                };
                possiblePrevScenes = new string[3]
                {
                    "MusicHall",
                    "30er",
                    "Geography"
                };
                break;
            case "MusicHall":
                positionsPlusRotations = new Vector2[4]
                {
                    new Vector2(-93.96f, 52.19f),
                    new Vector2(-84.99f, 40.53f),
                    new Vector2(-80.01f, 55.45f),
                    new Vector2(-89.5f, 42.4f)
                };
                possiblePrevScenes = new string[4]
                {
                    "Music",
                    "30er",
                    "MusicGeographyHallway",
                    "OldClassroom"
                };
                break;
            case "ScienceHallway":
                positionsPlusRotations = new Vector2[2]
                {
                    new Vector2(-61.73f, 3.1f),
                    new Vector2(-27.01f, 1.7f)
                };
                possiblePrevScenes = new string[2]
                {
                    "MilkHall",
                    "Agora"
                };
                break;
            case "TeacherFoyer":
                positionsPlusRotations = new Vector2[2]
                {
                    new Vector2(15.85f, 37.79f),
                    new Vector2(6.37f, 25.08f)
                };
                possiblePrevScenes = new string[2]
                {
                    "Geography",
                    "Agora"
                };
                break;
            case "Library":
                positionsPlusRotations = new Vector2[1] { new Vector2(-25.55f, 7) };
                possiblePrevScenes = new string[1] { "Agora" };
                break;

            // 1. OG
            case "OldClassroom":
                positionsPlusRotations = new Vector2[1]
                {
                    new Vector2(-80.31f, 38.01f)
                };
                possiblePrevScenes = new string[1]
                {
                    "MusicHall"
                };
                break;

            default:
                prevScene = SceneManager.GetActiveScene().name;
                return;
        }

        try
        {
            if (possiblePrevScenes == null || positionsPlusRotations == null) return;

            int prevSceneIndex = Array.IndexOf(possiblePrevScenes, prevScene);

            transform.position = positionsPlusRotations[prevSceneIndex];
        }
        catch (System.Exception)
        {
            throw;
        }

        prevScene = SceneManager.GetActiveScene().name;
    }
}
