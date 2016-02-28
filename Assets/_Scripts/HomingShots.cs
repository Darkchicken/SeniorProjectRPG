using UnityEngine;
using System.Collections;

public class HomingShots : MonoBehaviour {

    public GameObject target;
    Transform targetTrans;
    float speed = 2;
	// Use this for initialization
	void Start ()
    {
        targetTrans = target.GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.LookAt(targetTrans);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
