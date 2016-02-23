using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class NetworkPlayerScript : MonoBehaviour {

    bool battleArena = true;
    PhotonView photonView;
    // Use this for initialization
    void Start ()
    {
        //get the photon view of this character
        photonView = gameObject.GetComponent<PhotonView>();
        //set proper name and tag to distinguish local player from others
        if (photonView.isMine)//isLocalPlayer)
        {
            gameObject.tag = "Player";
            gameObject.name = "LOCAL player";
        }
        else
        {
          
            if (battleArena)//SceneManager.GetActiveScene().name == "BattleArena")
            {
                gameObject.tag = "Enemy";
                gameObject.name = "Network Enemy";
            }
            else
            {
                gameObject.tag = "Player";
                gameObject.name = "Network player";
            }
           
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}


}
