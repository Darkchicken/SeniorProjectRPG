using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour {

    public string[] dialogue;
    Dialogue dialogueManager;
    GameObject player;
    Camera dialogueCamera;
    //offset from transform.position for each npc
    public Vector3 faceLocation;
    //how close to set camera to npc's face
    public float faceDistance;
    //name of npc
    public string npcName;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        dialogueManager = GameObject.Find("GameManager").GetComponent<Dialogue>();
        dialogueCamera = GameObject.Find("DialogueCamera").GetComponent<Camera>();
    }
    
    public void ClickedNPC()
    {
        //activate dialogue box
        dialogueManager.StartDialogue(npcName,dialogue);
        dialogueCamera.transform.position = transform.position+faceLocation;
        dialogueCamera.transform.rotation = transform.rotation;
        dialogueCamera.transform.Rotate(0,180,0);
        dialogueCamera.transform.Translate(Vector3.back*faceDistance);
        //dialogueCamera.transform.Translate(Vector3.up * 5);


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
