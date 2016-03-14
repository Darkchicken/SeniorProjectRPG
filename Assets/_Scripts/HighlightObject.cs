using UnityEngine;
using System.Collections;

public class HighlightObject : MonoBehaviour 
{
    RaycastHit hit;
    public static GameObject CurrentSelectedObject;
    public GameObject Target;
    private static Vector3 mouseClickPoint;
	
	void Awake()
    {
        mouseClickPoint = Vector3.zero;
    }
	
	
	void Update () 
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if(Input.GetMouseButtonDown(0))
            {
                mouseClickPoint = hit.point;
            }

            if(hit.collider.name == "Terrain")
            {
                if(Input.GetMouseButtonDown(1))                         //for map projection later
                {
                    GameObject TargetObj = Instantiate(Target, hit.point, Quaternion.identity) as GameObject;
                    TargetObj.name = "Target Instantiated";
                }

                else if(Input.GetMouseButtonUp(0) && DidUserClickLeftMouse(mouseClickPoint))
                {
                    DeselectGameObjectIfSelected();
                }
            }

            else
            {
                if(Input.GetMouseButtonUp(0) && DidUserClickLeftMouse(mouseClickPoint))
                {

                    if(hit.collider.transform.FindChild("Selected"))
                    {
                        Debug.Log("Found a unit!");

                        if(CurrentSelectedObject != hit.collider.gameObject)
                        {
                            GameObject SelectedObj = hit.collider.transform.FindChild("Selected").gameObject;
                            SelectedObj.SetActive(true);

                            if(CurrentSelectedObject != null)
                            {
                                CurrentSelectedObject.transform.FindChild("Selected").gameObject.SetActive(false);
                            }

                            CurrentSelectedObject = hit.collider.gameObject;

                        }
                        
                    }
                }
            }

        }
        Debug.DrawRay(ray.origin, ray.direction * Mathf.Infinity, Color.yellow);
    }

#region Helper functions
    public bool DidUserClickLeftMouse(Vector3 hitPoint) //mouse click checker
    {
        float clickZone = 0.8f;
       
        if((mouseClickPoint.x < hitPoint.x + clickZone && mouseClickPoint.x > hitPoint.x - clickZone) &&
           (mouseClickPoint.x < hitPoint.x + clickZone && mouseClickPoint.x > hitPoint.x - clickZone) &&
            (mouseClickPoint.x < hitPoint.x + clickZone && mouseClickPoint.x > hitPoint.x - clickZone))
        {
            return true;
        }
       
        else
        {
            return false;
        }
    }

    public static void DeselectGameObjectIfSelected()       //deselects object when clicking on another thing
    {
        if(CurrentSelectedObject != null)
        {
            CurrentSelectedObject.transform.FindChild("Selected").gameObject.SetActive(false);
            CurrentSelectedObject = null;
        }
    }

#endregion
}
