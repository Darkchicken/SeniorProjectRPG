using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour {

    public string[] dialogue;
    Dialogue dialogueManager;
    GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        dialogueManager = GameObject.Find("GameManager").GetComponent<Dialogue>();
    }
    
    public void ClickedNPC()
    {
        //activate dialogue box
        dialogueManager.StartDialogue(dialogue);


    }
    public void OnMouseDown()
    {
        //if the player is within 3 units of the npc
        if (Vector3.Distance(player.transform.position, transform.position) < 3)
        {
            ClickedNPC();
        }
    }

}
