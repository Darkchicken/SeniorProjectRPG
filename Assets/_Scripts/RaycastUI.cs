using UnityEngine;
using System.Collections;

public class RaycastUI : MonoBehaviour
{

    private GameObject player;

	void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
    public void OnMouseOver()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        if (player != null)
        {
            player.GetComponent<PlayerMovement>().canMove = false;
        }
    }

    public void OnMouseExit()
    {
        if (player != null)
        {
            player.GetComponent<PlayerMovement>().canMove = true;
        }
    }
}
