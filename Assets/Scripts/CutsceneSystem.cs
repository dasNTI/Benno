using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneSystem : MonoBehaviour
{
    public TextAsset Cutscene;
    public bool ActivateOnStart = true;
    public bool InitNPCs = true;
    public Texture2D[] CharacterSheets;

    public NPCMovement[] NPCs;
    void Start()
    {
        NPCs = new NPCMovement[CharacterSheets.Length];

        if (ActivateOnStart) StartCutscene();
    }

    void InitCharacters() {
        for (int i = 0; i < CharacterSheets.Length; i++) {
            string name = CharacterSheets[i].name;
            GameObject npc = new GameObject(name);
            npc.layer = 7;
            npc.transform.localScale = Vector3.one * 10;
            npc.transform.position = Vector2.zero;

            BoxCollider2D bc = npc.AddComponent<BoxCollider2D>();
            SpriteRenderer sr = npc.AddComponent<SpriteRenderer>();
            NPCMovement npcm = npc.AddComponent<NPCMovement>();

            bc.offset = new Vector2(0, -0.16f);
            bc.size = new Vector2(0.12f, 0.05f);

            npcm.spriteMap = CharacterSheets[i];
            npcm.walkingSpeed = 1;
            npcm.roaming = false;
            NPCs[i] = npcm;
        }
    }

    public void StartCutscene() {
        if (InitNPCs) InitCharacters();
    }
}
