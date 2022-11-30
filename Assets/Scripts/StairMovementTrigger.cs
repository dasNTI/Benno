using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairMovementTrigger : MonoBehaviour
{
    private BoxCollider2D bc;
    private GameObject player;
    private InputMaster Master;

    public float ratio;

    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        player = GameObject.Find("Player");
        Master = new InputMaster();
        Master.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        float xdif = Mathf.Abs(transform.position.x - player.transform.position.x);
        float ydif = Mathf.Abs(transform.position.y - player.transform.position.y + bc.bounds.extents.y);

        bool xtest = xdif < Mathf.Abs(bc.bounds.extents.x - player.GetComponent<BoxCollider2D>().bounds.extents.x);
        bool ytest = ydif < Mathf.Abs(bc.bounds.extents.y);

        if (!(xtest && ytest)) return;
        //Debug.Log("yeet");

        float dir = Master.MainInput.Dir.ReadValue<Vector2>().x * player.GetComponent<PlayerMovement>().walkSpeed * ratio;

        //if (!CheckDir(Vector2.up * Mathf.Sign(dir), player.GetComponent<BoxCollider2D>().bounds.extents.x * 1.77f)) return;

        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + dir, player.transform.position.z);
        player.GetComponent<PlayerMovement>().zOffset -= dir;
    }

    bool CheckDir(Vector2 dir, float l)
    {

        bool b = true;
        float o = 0.05f + player.GetComponent<PlayerMovement>().walkSpeed;
        dir = dir.normalized * (1 + o);
        Vector2 tdp = new Vector2(player.GetComponent<BoxCollider2D>().bounds.center.x, player.GetComponent<BoxCollider2D>().bounds.center.y);

        if (dir.x == 0)
        {
            RaycastHit2D ray = Physics2D.Raycast(tdp + dir * player.GetComponent<BoxCollider2D>().bounds.extents.y + Vector2.left * l / 2, Vector2.right, l, player.GetComponent<PlayerMovement>().lmH);

            Debug.DrawRay(tdp + dir * player.GetComponent<BoxCollider2D>().bounds.extents.y + Vector2.left * l / 2, Vector2.right * l, Color.green);

            if (ray.collider != null) b = false;
        }
        else
        {
            RaycastHit2D ray = Physics2D.Raycast(tdp + dir * player.GetComponent<BoxCollider2D>().bounds.extents.x + Vector2.up * l / 2, Vector2.down, l, player.GetComponent<PlayerMovement>().lmV);

            Debug.DrawRay(tdp + dir * player.GetComponent<BoxCollider2D>().bounds.extents.x + Vector2.up * l / 2, Vector2.down * l, Color.green);

            if (ray.collider != null) b = false;
        }

        return b;
    }
}
