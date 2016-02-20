using UnityEngine;
using System.Collections;

public class RaycastUI : MonoBehaviour
{

    private GameObject player;

	void Start ()
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
            player.GetComponent<PlayerCombatManager>().canMove = false;
        }
    }

    public void OnMouseExit()
    {
        if (player != null)
        {
            player.GetComponent<PlayerCombatManager>().canMove = true;
        }
    }
}
