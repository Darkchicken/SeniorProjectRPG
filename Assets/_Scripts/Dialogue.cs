using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour {

    GameObject dialogueBox;
    Text dialogueText;
    string[] messageList;
    int messageCount = 0;
    int totalMessages = 0;
	// Use this for initialization
	void Start ()
    {
       
        dialogueBox = GameObject.Find("DialogueBox");
        dialogueText = GameObject.Find("DialogueText").GetComponent<Text>();
        dialogueBox.SetActive(false);
    }
	void Update()
    {
        if (totalMessages > 0 && (Input.GetKeyDown(KeyCode.Space)))
        {
            NextMessage();
        }
    }
	
    public void StartDialogue(string[] messages)
    {
        messageList = messages;
        dialogueBox.SetActive(true);
        //set count to 0
        messageCount = 0;
        //get number of messages in dialogue
        totalMessages = messages.Length;
        //display first message
        dialogueText.text = messages[0];
        /*
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
        dialogueBox.SetActive(false);
        */
    }
    public void NextMessage()
    {
        messageCount++;
        if (messageCount < totalMessages)
        {
            dialogueText.text = messageList[messageCount];
        }
        else
        {
            EndDialogue();
        }
    }
    public void EndDialogue()
    {
        totalMessages = 0;
        messageCount = 0;
        dialogueBox.SetActive(false);
    }

    }
