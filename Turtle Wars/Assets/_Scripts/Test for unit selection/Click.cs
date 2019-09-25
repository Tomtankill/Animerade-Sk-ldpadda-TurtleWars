using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{

    [SerializeField] private LayerMask clickableLayer;

    private List<GameObject> selectedObjects;

    void Start()
    {
        selectedObjects = new List<GameObject>();
    }
    // Update is called once per frame
    void Update()
    {
        // raycast on left click
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit rayHit;

            // if raycast hits object with layer clickableLayer run function
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit,Mathf.Infinity, clickableLayer))
            {
                ClickOn clickOnScript = rayHit.collider.GetComponent<ClickOn>();

                if (Input.GetKey("left ctrl"))
                {
                    // adds object to list
                    if(clickOnScript.currentlySelected == false)
                    {
                        selectedObjects.Add(rayHit.collider.gameObject);
                        clickOnScript.currentlySelected = true;
                        clickOnScript.ClickMe();
                    }
                    // removes object from list
                    else
                    {
                        selectedObjects.Remove(rayHit.collider.gameObject);
                        clickOnScript.currentlySelected = false;
                        clickOnScript.ClickMe();
                    }
                }

                // deselects object 
                else
                {
                    if(selectedObjects.Count > 0)
                    {
                        foreach (GameObject obj in selectedObjects)
                        {
                            obj.GetComponent<ClickOn>().currentlySelected = false;
                            obj.GetComponent<ClickOn>().ClickMe();
                        }

                        selectedObjects.Clear();
                    }

                    selectedObjects.Add(rayHit.collider.gameObject);
                    clickOnScript.currentlySelected = true;
                    clickOnScript.ClickMe();
                }
            }
        }
    }
}