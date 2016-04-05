using UnityEngine;
using System.Collections;

public class InitializerScript : MonoBehaviour {

    GameObject gameManager;

    void Awake()
    {
        if (GameObject.Find("GameManager") == null)
        {
            Instantiate(Resources.Load("GameManager") as GameObject);
            //gameManager.name = "GameManager";
            //DontDestroyOnLoad(gameManager);
        }
        //Instantiate(Resources.Load("GameHUD") as GameObject);
        
    }
	
}
