using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{

    [SerializeField] private LayerMask clickableLayer;

    private List<Units> selectedObjects;

    [HideInInspector] public List<Units> selectableObjects;


    private Vector3 mousePos1;
    private Vector3 mousePos2;
    public Transform moveThisHere;
    void Awake()
    {
        selectedObjects = new List<Units>();
        selectableObjects = new List<Units>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 mouse = Input.mousePosition;
        Ray rayHiting = Camera.main.ScreenPointToRay(mouse);
        RaycastHit rayHit;

        //if (Input.GetMouseButtonDown(1))
        //{

        //    ClearSelection();

        //}

        // if leftclick is click 
        if (Input.GetMouseButtonDown(0))
        {
            mousePos1 = Camera.main.ScreenToViewportPoint(Input.mousePosition);

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

                        if (rayHit.collider.CompareTag("Enemy"))
                        {
                            // set state to combat
                        }

                        // deselects object 
                        else
                        {
                            ClearSelection();

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

        // box thing
        if (Input.GetMouseButtonUp(0))
        {
            mousePos2 = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            if(mousePos1 != mousePos2)
            {
                SelectedObjects();
            }
        }
        // on rightclick
        if (Input.GetMouseButtonDown(1))
        {
            // if we don't have people selected
            if (selectedObjects.Count == 0)
            {
                return;
            }
            // if we do raycast
            if ((Physics.Raycast(rayHiting, out rayHit, Mathf.Infinity)))
            {
                // go cycle through all selected units
                foreach (Units units in selectedObjects)
                {
                    // if ray hits go into attack state
                    if (rayHit.transform.gameObject.CompareTag("Enemy"))
                    {
                        units.state = Units.State.Attack;
                        units.target = rayHit.transform.gameObject;
                    }
                    // else move there
                    else
                    {
                        units.MoveToTarget(rayHit.point);
                    }
                   
                }
            }
        }
    }

    // selectedobjects
    void SelectedObjects()
    {
        List<Units> remObjects = new List<Units>();

        // if ctrl is not held down and one clicks, clearselection
        if(Input.GetKey("left ctrl") == false)
        {
            ClearSelection();
        }

        // sets the positions for the box selector
        Rect selectedRect = new Rect(mousePos1.x, mousePos1.y, mousePos2.x - mousePos1.x, mousePos2.y - mousePos1.y);

        // foreach unit in selectedObjects
        foreach (Units selectObject in selectableObjects)
        {
            if (selectObject != null)
            {
                // makes the box selector
                if (selectedRect.Contains(Camera.main.WorldToViewportPoint(selectObject.transform.position), true))
                {
                    // if gameobject in box selction has tag frindly add them to currentlySelected
                    if (gameObject.CompareTag("Friendly"))
                    {
                        selectedObjects.Add(selectObject);
                        selectObject.GetComponent<Units>().currentlySelected = true;
                        selectObject.GetComponent<Units>().ClickMe();
                    }
                }
            }
            // else remove them
            else
            {
                remObjects.Add(selectObject);
            }
        }

        // if rem is empty remove rem
        if(remObjects.Count > 0)
        {
            foreach(Units rem in remObjects)
            {
                selectableObjects.Remove(rem);
            }
        }
    }

    // clears selction
    void ClearSelection()
    {
        if (selectedObjects.Count > 0)
        {
            foreach (Units obj in selectedObjects)
            {
                if(obj != null)
                {
                    obj.GetComponent<Units>().currentlySelected = false;
                    obj.GetComponent<Units>().ClickMe();
                }
            }
        }

        selectedObjects.Clear();
    }
}