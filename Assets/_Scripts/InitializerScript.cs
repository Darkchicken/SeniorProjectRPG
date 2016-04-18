using UnityEngine;
using System.Collections;

public class InitializerScript : MonoBehaviour {


    public static InitializerScript initializer;
    public Transform respawnPoint;

    private GameObject gameManager;
    /// <summary>
    /// hey testing!
    /// </summary>
    void Awake()
    {
        if (GameObject.Find("GameManager") == null)
        {
            Instantiate(Resources.Load("GameManager") as GameObject);
            //gameManager.name = "GameManager";
            //DontDestroyOnLoad(gameManager);
        }
        initializer = this;
        
    }
	
}
