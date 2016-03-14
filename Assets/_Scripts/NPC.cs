using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour {

    public string[] dialogue;
    Dialogue dialogueManager;
    [Header("Drop Quest Panel Here")]
    public GameObject questPanel;
    [Header("Type Quest IDs for this NPC")]
    public string[] questId;
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
            StartQuest(0);
        }
    }
    public void StartQuest(int questIndex)
    {
        //if the player has not already accepted this quest or completed this quest
        if (!PlayFabDataStore.playerQuestLog.Contains(questId[questIndex]) 
            && !PlayFabDataStore.playerCompletedQuests.Contains(questId[questIndex]))
        {
            //set the quest panel's quest id to the quest carried by the current npc
            questPanel.GetComponent<LoadQuest>().questId = questId[questIndex];
            questPanel.SetActive(true);
        }
    }
}
