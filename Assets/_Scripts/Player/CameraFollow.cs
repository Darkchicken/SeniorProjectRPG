using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

    public float smoothing = 5f;

    private Transform player;
    private Vector3 offset;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - player.position;
    }


    void FixedUpdate()
    {
        Vector3 targetCamPos = player.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }

}