using UnityEngine;
using System.Collections;

public class MiniMapCameraFollow : MonoBehaviour {

    //public float smoothing = 5f;
    //public Vector3 offset = new Vector3(0f, 100f, 0f);
    public float cameraHeight = 100f;
    private float maxHeight = 150f;
    private float minHeight = 50f;
    private float zoomAmount = 10f;
    private Transform player;
    //taken from original calculations, can be modified if necessary

    private bool following = false;

    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {

            player = GameObject.FindGameObjectWithTag("Player").transform;
            transform.position = new Vector3(player.position.x,cameraHeight, player.position.z)  ;
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
                /*
                Vector3 targetCamPos = player.position + offset;
                transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
                */
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
