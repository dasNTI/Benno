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
        npc.transform.localScale = Vector3.one * 10;
        npc.transform.position = new Vector2(-55, 29);

        bool resetposition = false;
        int tries = 0;
        Vector2 position = transform.position;
        do {
            float nodeSize = AstarPath.active.data.gridGraph.nodeSize;

            position = AstarPath.active.data.gridGraph.center;

            float xoffset = Random.Range(-AstarPath.active.data.gridGraph.width * nodeSize / 2f, AstarPath.active.data.gridGraph.width * nodeSize / 2f);

            float yoffset = Random.Range(-AstarPath.active.data.gridGraph.depth * nodeSize / 2f, AstarPath.active.data.gridGraph.depth * nodeSize / 2f);

            //position += new Vector2(xoffset, yoffset);

            bool possible = AstarPath.active.data.graphs[0].GetNearest(position).node.Flags % 2 == 1;
            //resetposition = !possible;

            tries++;
        } while (!(!resetposition || tries >= 5));
        transform.position = position;

        BoxCollider2D bc = npc.AddComponent<BoxCollider2D>();
        Seeker seeker = npc.AddComponent<Seeker>();
        SpriteRenderer sr = npc.AddComponent<SpriteRenderer>();
        NPCMovement npcm = npc.AddComponent<NPCMovement>();

        bc.offset = new Vector2(0, -0.16f);
        bc.size = new Vector2(0.12f, 0.05f);

        npcm.spriteMap = sheets[Random.Range(0, sheets.Length)];
        npcm.walkingSpeed = maxWalkingSpeed + Random.Range(0, 0.2f);
    }
}
