using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour {

    GameObject dialogueBox;
    Text dialogueText;

	// Use this for initialization
	void Start ()
    {
        dialogueBox = GameObject.Find("DialogueBox");
        dialogueText = dialogueBox.transform.FindChild("DialogueText").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update ()
    {
	    
	}
    public void StartDialogue()
    {
        dialogueBox.SetActive(true);
    }
    public void DialogueMessage(string[] messages)
    {
        //get number of messages in dialogue
        int messageCount = 0; 
        int totalMessages = messages.Length;
        //display first message
        dialogueText.text = messages[0];
        
        while(messageCount != totalMessages)
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                //increase message count
                messageCount++;
                //display next message
                dialogueText.text = messages[messageCount];
            }
        }
        EndDialogue();
    }
    public void EndDialogue()
    {
        dialogueBox.SetActive(false);
    }
}
