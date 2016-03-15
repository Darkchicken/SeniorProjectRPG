using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour {

    public string[] dialogue;
    public Shader defaultShader;
    public Shader outlineShader;
    Dialogue dialogueManager;
    [Header("Drop Quest Panel Here")]
    public GameObject questPanel;

    //what quests this npc can grant
    [Header("Quest IDs this NPC can start")]
    public string[] questId;
    //what quests this npc can complete
    [Header("Quest IDs this NPC can end")]
    public string[] endQuestId;
    

    bool finishingQuest = false;

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
        /////////////////////////////test code
        //PlayFabDataStore.playerCompletedQuests.Clear();
        Debug.Log("Current Quests: ");
        foreach(string quests in PlayFabDataStore.playerQuestLog)
        {
            Debug.Log(quests);
        }
        Debug.Log("Completed Quests: " );
        foreach (string quests in PlayFabDataStore.playerCompletedQuests)
        {
            Debug.Log(quests);
        }
        ///////////////////////////
        //if the player is within 3 units of the npc
        if (Vector3.Distance(player.transform.position, transform.position) < 3)
        {
            finishingQuest = false;
            ClickedNPC();
            for(int i = 0; i < endQuestId.Length; i++)
            {
                EndQuest(i);
                if(finishingQuest == true)
                {
                    break;
                }
            }
            //if you arent finishing a quest and this npc has quests to give
            if (finishingQuest == false && questId.Length!=0)
            {
                StartQuest(0);
            }
        }
    }
    public void StartQuest(int questIndex)
    {
        //if this npc is not a quest giver
        if(questId.Length == 0)
        {
            return;
        }
        //if the player has not already accepted this quest or completed this quest
        if (!PlayFabDataStore.playerQuestLog.Contains(questId[questIndex]) 
            && !PlayFabDataStore.playerCompletedQuests.Contains(questId[questIndex]))
        {
            //set the quest panel's quest id to the quest carried by the current npc
            questPanel.GetComponent<LoadQuest>().questId = questId[questIndex];
            questPanel.SetActive(true);
        }
    }
    public void EndQuest(int questIndex)
    {
        //if this npc is not a quest ender
        if (endQuestId.Length == 0)
        {
            return;
        }
        //if the player has accepted this quest and has not completed it
        if (PlayFabDataStore.playerQuestLog.Contains(endQuestId[questIndex])
            && !PlayFabDataStore.playerCompletedQuests.Contains(endQuestId[questIndex]))
        {
            //complete quest
            finishingQuest = true;
            PlayFabDataStore.playerCompletedQuests.Add(endQuestId[questIndex]);
            PlayFabDataStore.playerQuestLog.Remove(endQuestId[questIndex]);
            Debug.Log("You completed "+ endQuestId[questIndex]);
            //questPanel.GetComponent<LoadQuest>().questId = questId[questIndex];
            //questPanel.SetActive(true);
        }
    }


    void OnMouseOver()
    {
        GetComponentInChildren<SkinnedMeshRenderer>().material.shader = outlineShader;
    }

    void OnMouseExit()
    {
        GetComponentInChildren<SkinnedMeshRenderer>().material.shader = defaultShader;
    }
}
