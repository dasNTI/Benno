using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairMovementTrigger : MonoBehaviour
{
    private BoxCollider2D bc;
    private GameObject player;
    private InputMaster Master;
    private bool entered = false;

    private PlayerMovement pm;
    private BoxCollider2D pbc;

    public float ratio;
    public bool adjustZ = true;
    public bool checkVertical = true;
    public int resetZOffset = 0;
    public float leftZOffset = 0;
    public float rightZOffset = 0;

    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        player = GameObject.Find("Player");
        pm = player.GetComponent<PlayerMovement>();
        pbc = player.GetComponent<BoxCollider2D>();
        Master = new InputMaster();
        Master.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        //float xdif = Mathf.Abs(transform.position.x - player.transform.position.x);
        //float ydif = Mathf.Abs(transform.position.y - player.transform.position.y + bc.bounds.extents.y);

        //bool xtest = xdif < Mathf.Abs(bc.bounds.extents.x - player.GetComponent<BoxCollider2D>().bounds.extents.x);
        //bool ytest = ydif < Mathf.Abs(bc.bounds.extents.y);

        if (!entered) return;
        //Debug.Log("yeet");

        float x = Master.MainInput.Dir.ReadValue<Vector2>().x;
        float dir = x * player.GetComponent<PlayerMovement>().walkSpeed * ratio;

        //if (!CheckDir(Vector2.up * Mathf.Sign(dir), player.GetComponent<BoxCollider2D>().bounds.extents.x * 1.77f)) return;

        bool checkV = pm.CheckDir((Vector2.up * dir).normalized, 1.7f * pbc.bounds.extents.x);
        if (!checkVertical) checkV = true;

        if (pm.CheckDir(Vector2.right * x, 1.55f * pbc.bounds.extents.y) && checkV) {
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + dir, player.transform.position.z);
            if (adjustZ) pm.zOffset -= dir;
        }

        //Debug.Log(pm.CheckDir(Vector2.right * x, 1.55f * pbc.bounds.extents.y));
    }

    private void OnTriggerEnter2D(Collider2D other) {
        entered = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        entered = false;

        if (resetZOffset == 0 || !adjustZ) return;

        float xdif = player.transform.position.x - transform.position.x;

        if (xdif < 0) pm.zOffset = leftZOffset;
        if (xdif > 0) pm.zOffset = rightZOffset;
    }
}
