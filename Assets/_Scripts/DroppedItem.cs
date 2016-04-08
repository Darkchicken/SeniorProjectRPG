using UnityEngine;
using System.Collections;

public class DroppedItem : MonoBehaviour {

    public string itemId;

    void Awake ()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(new Vector3(0, 180, 0));
    }

    public void OnMouseDown()
    {
        
    }




    }
