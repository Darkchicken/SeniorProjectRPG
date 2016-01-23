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
        player.GetComponent<PlayerMovement>().canMove = false;
    }

    public void OnMouseExit()
    {
        player.GetComponent<PlayerMovement>().canMove = true;
    }
}
