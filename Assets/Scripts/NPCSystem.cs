using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class NPCSystem : MonoBehaviour
{
    public int maxNPCs = 20;
    public float maxWalkingSpeed = 1.5f;
    public Texture2D[] sheets;
    public LayerMask peopleLM;


    void Start()
    {
        for (int i = 0; i < maxNPCs; i++) {
            newNPC("NPC" + i.ToString());
        }

        InvokeRepeating("rescan", 1f, 5f);
    }

    void rescan() {
        AstarPath.active.Scan();
    }

    void newNPC(string name) {
        GameObject npc = new GameObject(name);
        npc.layer = 7;
        npc.transform.position = new Vector2(-55, 29) + new Vector2(Random.Range(-5, 5), Random.Range(-2, 2));
        npc.transform.localScale = Vector3.one * 10;

        BoxCollider2D bc = npc.AddComponent<BoxCollider2D>();
        Seeker seeker = npc.AddComponent<Seeker>();
        SpriteRenderer sr = npc.AddComponent<SpriteRenderer>();
        NPCMovement npcm = npc.AddComponent<NPCMovement>();

        bc.offset = new Vector2(0, -0.16f);
        bc.size = new Vector2(0.12f, 0.05f);

        npcm.spriteMap = sheets[Random.Range(0, sheets.Length)];
        npcm.walkingSpeed = maxWalkingSpeed + Random.Range(-0.25f, 0.25f);
    }
}
