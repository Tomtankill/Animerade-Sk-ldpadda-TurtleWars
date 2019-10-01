using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{

    [SerializeField] private LayerMask clickableLayer;

    private List<Units> selectedObjects;

    public Transform moveThisHere;
    void Start()
    {
        selectedObjects = new List<Units>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 mouse = Input.mousePosition;
        Ray rayHiting = Camera.main.ScreenPointToRay(mouse);
        // raycast on left click
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit rayHit;
            

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit,Mathf.Infinity))
                
            {
                if (rayHit.collider.GetComponent<Units>())
                {
                    Units unit = rayHit.collider.GetComponent<Units>();

                    if (rayHit.collider.CompareTag("Friendly"))
                    {
                        if (Input.GetKey("left ctrl"))
                        {
                            // adds object to list
                            if (unit.currentlySelected == false)
                            {
                                selectedObjects.Add(rayHit.collider.GetComponent<Units>());
                                unit.currentlySelected = true;
                                unit.ClickMe();
                            }
                            // removes object from list
                            else
                            {
                                selectedObjects.Remove(rayHit.collider.GetComponent<Units>());
                                unit.currentlySelected = false;
                                unit.ClickMe();
                            }
                        }

                        // deselects object 
                        else
                        {
                            if (selectedObjects.Count > 0)
                            {
                                foreach (Units obj in selectedObjects)
                                {
                                    obj.GetComponent<Units>().currentlySelected = false;
                                    obj.GetComponent<Units>().ClickMe();
                                }

                                selectedObjects.Clear();
                            }

                            selectedObjects.Add(rayHit.collider.GetComponent<Units>());
                            unit.currentlySelected = true;
                            unit.ClickMe();
                        }
                    }
                    else
                    {
                        return;
                    }
                    
                }

            }
        }

        // move to
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit rayHit;
            if (selectedObjects.Count == 0)
            {
                return;
            }
            if ((Physics.Raycast(rayHiting, out rayHit, Mathf.Infinity, clickableLayer)))
            {
                foreach (Units units in selectedObjects)
                {
                    units.MoveToTarget(rayHit.point);
                }
            }
            //if (rayHit.collider.CompareTag("Enemy"))
            //{
                
            //}
        }
    }
}