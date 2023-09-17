using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;
using Unity.Mathematics;

public class NPCMovement : MonoBehaviour
{
    public Texture2D spriteMap;
    public float walkingSpeed = 1.4f;
    public bool roaming = false;

    private Sprite[] sprites;
    private Seeker seeker;
    private SpriteRenderer sr;

    private Vector2 aniDir = Vector2.left;
    private bool animating = true;
    private Vector2 pos;

    private Path path;
    private Vector2 randomOffset;
    private int currentWayPoint;
    private float nextWayPointDistance = .6f;
    void Start()
    {

        seeker = GetComponent<Seeker>();
        sr = GetComponent<SpriteRenderer>();

        ParseSpriteMap();

        sr.sprite = sprites[0];
        pos = transform.position;

        StartCoroutine(walkAni());

        seeker.StartPath(transform.position, new Vector3(-85.2f, 32.5f), p => {
            if (!p.error) {
                path = p;
                currentWayPoint = 0;
            }
        });
        randomOffset = new Vector2(
            UnityEngine.Random.Range(-1f, 1f),
            UnityEngine.Random.Range(-1f, 1f)
        );
        // randomOffset = Vector2.up;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);

        if (path == null || currentWayPoint >= path.vectorPath.Count || !roaming) return;


        Vector2 dir =  (Vector2) path.vectorPath[currentWayPoint] + randomOffset - (Vector2) transform.position;
        dir = Normalize(dir);
        transform.position += walkingSpeed * (Vector3) dir * Time.timeScale / 10f;

        int r(float z) {
            float threshold = 0.7f;

            return (Mathf.Abs(z) > threshold) ? (int)Mathf.Sign(z) : 0;
        };

        aniDir = new Vector2(
                r(dir.x),
                -r(dir.y)
            );

        if (Vector2.Distance((Vector2) transform.position, (Vector2) path.vectorPath[currentWayPoint] + randomOffset) < nextWayPointDistance) {
            currentWayPoint++;
            randomOffset = new Vector2(
                UnityEngine.Random.Range(-1f, 1f),
                UnityEngine.Random.Range(-1f, 1f)
            );
        }
    }

    Vector2 Normalize(Vector2 v) {
        float factor = 1 / v.magnitude;

        return new Vector2(v.x * factor, v.y * factor);
    }

    void ParseSpriteMap() {
        int width = 12;
        int height = 36;

        sprites = new Sprite[12];

        for (int i = 0; i < 12; i++) {
            int xoffset = (i % 3) * width;
            int yoffeset = Mathf.FloorToInt(i / 3) * height;

            sprites.SetValue(Sprite.Create(spriteMap, new Rect(xoffset, yoffeset, width, height), new Vector2(0.5f, 0.5f)), i);
        }
    }

    IEnumerator walkAni() {
        yield return new WaitForSecondsRealtime(UnityEngine.Random.Range(0, 1.5f));

        while (enabled) {
            Vector2 check = aniDir;
            if (aniDir == Vector2.zero) {
                sr.sprite = sprites[0];
                yield return new WaitWhile(() => aniDir == Vector2.zero);
            }

            int dirIndex = 0;
            switch ($"{aniDir.x},{aniDir.y}") {
                case "0,1":
                    dirIndex = 3;
                    break;
                case "1,0":
                    dirIndex = 1;
                    break;
                case "-1,0":
                    dirIndex = 2;
                    break;
            }

            do {
                for (int i = 0; i < 4; i++) {
                    float f(float x, float k) {
                        return Mathf.Abs(1 - Mathf.Abs(x / (k / 2) - 1));
                    }

                    double s = f(i - 1, 2) * (1 + .5 * f(i, 1));
                    sr.sprite = sprites[3 * dirIndex + (int)s];
                    if (aniDir == check) yield return new WaitForSeconds(.25f);
                }
            } while (aniDir == check);

            if (!animating) yield return new WaitWhile(() => !animating);
        }
    }

    public void WalkTo(Vector2 position, float duration) {
        int StepsPerSecond = Application.targetFrameRate;

        IEnumerator d() {
            Vector2 init = transform.position;

            float step = 1f / (duration * StepsPerSecond);
            for (float i = 0; i < duration * StepsPerSecond; i++) {
                transform.position = Vector2.Lerp(init, position, i / step);
                yield return new WaitForSeconds(1f / StepsPerSecond);
            }
        };
        StartCoroutine(d());
    }
}
