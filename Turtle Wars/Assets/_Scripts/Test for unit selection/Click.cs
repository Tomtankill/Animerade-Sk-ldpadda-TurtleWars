using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    // Defines a new list. That list will contain all gameObjects that have a Units script.
    [SerializeField] private List<Units> selectedUnits;
    // Defines a new list. That list will contain all gameObjects that have a SelfBuildingManager script.
    [HideInInspector] public List<SelfBuildingManager> selectedBuildings;

    // Essentially a master list for all selectable Units.
    [HideInInspector] public List<Units> selectableUnits;
    // Essentially a master list for all selectable Buildings.
    [HideInInspector] public List<SelfBuildingManager> selectableBuildings;

    // Sets the tag that the script is looking for. This needs to be set to the local player on the network.
    // set to Friendly tag for now
    //public string controllingPlayer;

    // defining positions for mouseclick for boxselection
    private Vector3 mousePos1;
    private Vector3 mousePos2;
    public Transform moveThisHere;
    private Vector3 rayhitInWorld;
    GameObject closest = null;

    RaycastHit clohit;
    // Audio Sound Effect
    private AudioSource Sound;

    // UI controller menus
    public GameObject unitControlBuilder;
    public GameObject buildingControlBuilder;
    // selection box

    void Awake()
    {
        //
        Sound = GetComponent<AudioSource>();
        //
        selectedUnits = new List<Units>();
        selectableUnits = new List<Units>();
        //
        selectableBuildings = new List<SelfBuildingManager>();
        selectedBuildings = new List<SelfBuildingManager>();
    }
    // Update is called once per frame

    void Update()
    {
        Vector3 mouse = Input.mousePosition;
        Ray rayHiting = Camera.main.ScreenPointToRay(mouse);
        RaycastHit rayHit;




        //if leftclick is click
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Sound.Play();


            if (Physics.Raycast(ray, out clohit, Mathf.Infinity))
            {
                rayhitInWorld = clohit.point;
                Debug.DrawLine(Camera.main.ScreenToWorldPoint(Input.mousePosition), rayhitInWorld);
            }


            mousePos1 = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit,Mathf.Infinity))
                
            {
                if (rayHit.collider.GetComponent<Units>())
                {
                    // getting the Unit script and rename it to unit to be refrence
                    Units unit = rayHit.collider.GetComponent<Units>();

                    if (rayHit.collider.CompareTag("Friendly"))
                    {
                        unitControlBuilder.SetActive(true);
                        if (Input.GetKey("left ctrl"))
                        {
                            // adds object to list
                            if (unit.currentlySelected == false)
                            {
                                
                                selectedUnits.Add(rayHit.collider.GetComponent<Units>());
                                unit.currentlySelected = true;
                            }
                            // removes object from list
                            else
                            {
                                selectedUnits.Remove(rayHit.collider.GetComponent<Units>());
                                unit.currentlySelected = false;
                            }
                        }

                        else
                        {
                            ClearSelection();
                            selectedUnits.Add(rayHit.collider.GetComponent<Units>());
                            unit.currentlySelected = true;
                        }
                    }
                }
                
                // building statment 
                if (rayHit.collider.GetComponent<SelfBuildingManager>())
                {
                    SelfBuildingManager building = rayHit.collider.GetComponent<SelfBuildingManager>();
                    
                    if (rayHit.collider.CompareTag("Friendly"))
                    {
                        // get the UI thing for building = true
                        ClearSelection();
                        //if(Current selected in building script)
                    }
                    //for (int i = 0; i < selectedObjects.Count; i++)
                    //{
                    //    if (selectedObjects[i].GetComponent<Units>() != null)
                    //    {
                    //        selectedObjects.Remove(selectedObjects[i]);
                    //        print("this work big dumb" + selectedObjects);
                    //    }
                    //}
                }
                else if (rayHit.collider.GetComponent<SelfBuildingManager>())
                {
                    SelfBuildingManager building = rayHit.collider.GetComponent<SelfBuildingManager>();

                    if (rayHit.collider.CompareTag("Friendly"))
                    {
                        buildingControlBuilder.SetActive(true);

                        if (Input.GetKey("left ctrl"))
                        {
                            // this adds more buildings to the list
                            if (building.currentlySelected == false)
                            {
                                //adds the script to the building to the list
                                selectedBuildings.Add(rayHit.collider.GetComponent<SelfBuildingManager>());
                                // Sets the building to selected
                                building.currentlySelected = true;
                                // Runs the IsSelected method on the building that gets selected
                                building.IsSelected();
                            }
                            // removes object from list
                            else
                            {
                                selectedBuildings.Remove(rayHit.collider.GetComponent<SelfBuildingManager>());
                                building.currentlySelected = false;
                                building.IsSelected();
                            }
                        }
                        else
                        {
                            ClearSelection();
                            selectedBuildings.Add(rayHit.collider.GetComponent<SelfBuildingManager>());
                            building.currentlySelected = true;
                            building.IsSelected();
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
            if (selectedUnits.Count == 0)
            {
                return;
            }
            // if we do raycast
            if ((Physics.Raycast(rayHiting, out rayHit, Mathf.Infinity)))
            {
            Debug.DrawRay(rayHiting.origin, rayHiting.direction, Color.red);
                // go cycle through all selected units
                foreach (Units units in selectedUnits)
                {
                    // if ray hits go into attack state
                    if (rayHit.transform.gameObject.CompareTag("Enemy"))
                    {
                        units.state = Units.State.Attack;
                        units.target = rayHit.transform.gameObject;
                    }
                    // else if ray hits is resource and gameobject. sets unit to gathering state
                    else if (rayHit.transform.gameObject.CompareTag("Resource") && units.GetComponent<Units>().unitType == Units.UnitType.Worker)
                    {
                        units.state = Units.State.Gathering;
                        units.target = rayHit.transform.gameObject;
                    }
                    // else move there
                    else if(rayHit.transform.gameObject.CompareTag("Ground"))
                    {
                        units.state = Units.State.Idle;
                        units.MoveToTarget(rayHit.point);
                    }
                }
            }
        }
    }

    // selectedobjects
    void SelectedObjects()
    {
        List<Units> remUnits = new List<Units>();
        List<SelfBuildingManager> remBuild = new List<SelfBuildingManager>();

        // if ctrl is not held down and one clicks, clearselection
        if (Input.GetKey("left ctrl") == false)
        {
            ClearSelection();
        }

        // sets the positions for the box selector
        Rect selectedRect = new Rect(mousePos1.x, mousePos1.y, mousePos2.x - mousePos1.x, mousePos2.y - mousePos1.y);

        // foreach unit in selectedUnits
        // units arent selectableUnits
        // make all units with the playertag be added to selectableUnits
        // should make it work
        foreach (Units selectObject in selectableUnits)
        {
            if (selectObject != null)
            {
                if (!selectObject.CompareTag("Enemy"))
                {
                    // checks if the object is within the box
                    if (selectedRect.Contains(Camera.main.WorldToViewportPoint(selectObject.transform.position), true))
                    {
                        selectedUnits.Add(selectObject);
                        unitControlBuilder.SetActive(true);
                        selectObject.GetComponent<Units>().currentlySelected = true;
                    }
                }
                else
                {
                    remUnits.Add(selectObject);
                }
            }
            // else remove them
            else
            {
                remUnits.Add(selectObject);
            }
        }
        
        foreach (SelfBuildingManager selectObject in selectableBuildings)
        {

            float distanceToClosestBuilding = Vector3.Distance(selectObject.transform.position, rayhitInWorld);
            if (selectObject != null)
            {
                if (!selectObject.CompareTag("Enemy"))
                {
                    if (closest != null)
                    {
                        if (distanceToClosestBuilding < Vector3.Distance(closest.transform.position, rayhitInWorld))
                        {
                            closest = selectObject.gameObject;
                        }
                    }

                    if (closest == null)
                    {
                        closest = selectObject.gameObject;
                    }

                    if (selectedRect.Contains(Camera.main.WorldToViewportPoint(selectObject.transform.position), true))
                    {
                        if (selectObject.gameObject.GetComponent<SelfBuildingManager>().type == closest.GetComponent<SelfBuildingManager>().type)
                        {
                            selectedBuildings.Add(selectObject);
                            buildingControlBuilder.SetActive(true);
                            selectObject.GetComponent<SelfBuildingManager>().currentlySelected = true;
                            selectObject.GetComponent<SelfBuildingManager>().IsSelected();
                        }
                    }
                }
                else
                {
                    remBuild.Add(selectObject);
                }
            }
            else
            {
                remBuild.Add(selectObject);
            }
        }

        // if rem is empty remove rem
        if (remUnits.Count > 0)
        {
            foreach(Units rem in remUnits)
            {
                selectableUnits.Remove(rem);
            }
        }
        if (remBuild.Count > 0)
        {
            foreach (SelfBuildingManager rem in remBuild)
            {
                selectableBuildings.Remove(rem);
            }
        }

        if (selectedBuildings.Count > 0)
        {
            if (selectedBuildings[0])
            {
                
            }
        }


    }

    // clears selction
    void ClearSelection()
    {
        // clears unit list
        if (selectedUnits.Count > 0)
        {
            foreach (Units obj in selectedUnits)
            {
                if(obj != null)
                {
                    obj.GetComponent<Units>().currentlySelected = false;
                    unitControlBuilder.SetActive(false);
                }
            }
        }

        if (selectedBuildings.Count > 0)
        {
            foreach (SelfBuildingManager obj in selectedBuildings)
            {
                if(obj != null)
                {
                    obj.GetComponent<SelfBuildingManager>().currentlySelected = false;
                    buildingControlBuilder.SetActive(false);
                    obj.GetComponent<SelfBuildingManager>().IsSelected();
                }
            }
        }
        
        selectedUnits.Clear();
        selectedBuildings.Clear();
    }
}