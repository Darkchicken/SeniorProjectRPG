using UnityEngine;
using System.Collections;

public class MiniMapCameraFollow : MonoBehaviour {

    //public float smoothing = 5f;
    //public Vector3 offset = new Vector3(0f, 100f, 0f);
    public float cameraHeight = 100f;
    private float maxHeight = 100f;
    private float minHeight = 20f;
    private float zoomAmount = 10f;

    private Transform player;

    private RectTransform playerPin;
    //taken from original calculations, can be modified if necessary

    private bool following = false;

    void Start()
    {
        playerPin = GameObject.Find("Unit Pin (Player)").GetComponent<RectTransform>();
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {

            player = GameObject.FindGameObjectWithTag("Player").transform;
            transform.position = new Vector3(player.position.x,cameraHeight, player.position.z);
            following = true;
        }
    }


    void FixedUpdate()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null && following == false)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            transform.position = new Vector3(player.position.x, cameraHeight, player.position.z);
            following = true;
        }
        if (following == true)
        {
            //if player disconnects
            if (player == null)
            {
                following = false;
            }
            else
            {
                transform.position = new Vector3(player.position.x, cameraHeight, player.position.z);
                //set pin rotation
                //make the pin's z rotation the same as the player's y rotation
                Debug.Log(player.rotation.y);
                //playerPin.Rotate(0, 0, player.rotation.y);
                float playerRot = -player.eulerAngles.y;
                playerPin.rotation = Quaternion.Euler(0, 0,playerRot);
                Debug.Log(playerPin.rotation);
            }
        }
    }

    public void ZoomIn()
    {
        if(cameraHeight > minHeight)
        {
            cameraHeight -= zoomAmount;
        }
        else
        {
            cameraHeight = minHeight;
        }
    }
    public void ZoomOut()
    {
        if (cameraHeight < maxHeight)
        {
            cameraHeight += zoomAmount;
        }
        else
        {
            cameraHeight = maxHeight;
        }
    }
}
