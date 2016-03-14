using UnityEngine;
using System.Collections;

public class MousePoint : MonoBehaviour 
{
    public GameObject Target;
    RaycastHit hit;
    private float raycastLength = 500;


    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, raycastLength))
        {
    
            if (hit.collider.name == "Terrain")
            {

                if(Input.GetMouseButtonDown(1)) //right click to instantiate target
                {
                    GameObject TargetObj = Instantiate(Target, hit.point, Quaternion.identity) as GameObject;
                    TargetObj.name = "Target Instantiated";
                }
            }
        }

        Debug.DrawRay(ray.origin, ray.direction * raycastLength, Color.green);
    }
}
