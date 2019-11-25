using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PriorityAI : MonoBehaviour
{
    // this is a type of function signatures. a signature is a combanation of a functions return type and it's list of parameters
    public delegate void MasterAIPositionType(Transform t);

    // this is a event of type MasterAiPostionType. It holds a reference of functions as it's value. The function in this event will move enemy units to target with in units script
    public static event MasterAIPositionType moveFighterUnitsAIEvent;

    // this function is this event will move all worker units with the enemy tag on it to target postion in units script
    public static event MasterAIPositionType moveWokerUnitsAIEvent;

    // this is refrence the turntimer script (HAVEN'T ADDED SO THE AI CARES ABOUT TURNS)
    private TurnTimer turntime;
    public bool barrackBuild;

    // this stores the AI's resources
    public float rScore1;
    public float rScore2;

    // refrence the AI's units prefab so it can me instantiate it
    public GameObject myUnits;
    public GameObject barracks;
    public Transform barrackBuildingSpot;
    public Transform barracksUnitSpawner;

    // list of the transform of resources in the map
    public static List<Transform> r1 = new List<Transform>(), r2 = new List<Transform>();

    // players units and building
    public static List<Transform> playerUnits = new List<Transform>(); 
    public static List<GameObject> AIUnits = new List<GameObject>();
    public static List<Transform> playerBuildings = new List<Transform>();

    // this is the acceptableDistance that a unit can find a enemy/resource
    public float acceptableDistance = 25f;

    // sets the state of the AI, that changes what the AI will focus on
    public State state;
    public enum State
    {
        Eco,
        Attack,
        Defend,
        Idle
    }

    void Start()
    {
        // CHECK with Tom how to setup the turntimer so AI dosen't cheat >:c

        turntime = GameObject.Find("Commander").GetComponent<TurnTimer>();

        // finds all gameobjects with the script <Resources> on it, then depending on what type of resource it adds it to that list
        foreach (Resource r in FindObjectsOfType<Resource>())
        {
            if (r.resourceType == ResourceType.r1)
            {
                r1.Add(r.transform);
            }
            else
                r2.Add(r.transform);
        }

        // finds all the gameobjects with the <Units> script on them, then add them to two seprets lists depending who is controlling them
        foreach (Units U in FindObjectsOfType<Units>())
        {
            if (U.whoControllsThis == Units.WhoControllsThis.Player)
            {
                playerUnits.Add(U.transform);
            }
            else if (U.whoControllsThis == Units.WhoControllsThis.AI)
            {
                AIUnits.Add(new GameObject());
            }
        }
        // finds all the object with selfbuildingmanger on them, then sort out the one that ain't the AI's and adds they transform to the list playerBuildings
        foreach (SelfBuildingManager P in FindObjectsOfType<SelfBuildingManager>())
        {
            if (P.AI == false)
            {
                playerBuildings.Add(P.transform);
            }
        }
    }

    void Update()
    {
        // if it's AI turn, do the thing
        if(turntime.p2Turn == true)
        {
            switch (state)
            {
                case State.Eco:
                    Eco();
                    break;
                case State.Attack:
                    AttackMode();
                    break;
                case State.Defend:
                    DefendMode();
                    break;

            }
        }
        // force the AI to keep doing the eco thing
        else
        {
            Eco();
        }
        // CHANGES THIS TO BUILDINGS!!!
        foreach (Units U in FindObjectsOfType<Units>())
        {
            if (U.whoControllsThis == Units.WhoControllsThis.Player)
            {
                playerUnits.Add(U.transform);
            }
            else if (U.whoControllsThis == Units.WhoControllsThis.AI)
            {
                //AIUnits.Add(new GameObject());
            }
        }

        // switches state and runs a function depending on what state it's in

        // ADDS that AI checks for it's turn 
    }

    // find the closest resources transform, then return the closest resource
    Transform GetNearestResource(Transform person, List<Transform> resource)
    {
        Transform closest = null;
        float distance = 99999;

        if (resource.Count == 1)
        {
            return resource[0];
        }


        foreach (Transform r in resource)
        {

            Transform use = closest == null ? resource[0].transform : closest;
            float dist = Vector3.Distance(person.position, use.position);

            float next = Vector3.Distance(person.position, r.transform.position);
            if (next < dist)
            {
                closest = r;
                distance = dist;
            }
        }
        return closest;
    }

    // finds the closest player units transform, then return the target
    Transform GetNearestEnemy(List<Transform> playerunits)
    {
        Transform closest = null;
        float distance = 99999;

        if (playerunits.Count == 1)
        {
            return playerunits[0];
        }

        foreach (Transform r in playerunits)
        {
            Transform use = closest == null ? playerunits[0].transform : closest;
            float d = Vector3.Distance(use.transform.position, r.transform.position);

            if (d < distance)
            {
                closest = r;
                distance = d;
            }

        }

        return closest;
    }

    // finds the closest player building transform, then return the target
    Transform GetnearestBuilding(Transform person, List<Transform> playerbuilding)
    {
        Transform closest = null;
        float distance = 99999;

        if (playerBuildings.Count == 1)
        {
            return playerBuildings[0];
        }


        foreach (Transform r in playerBuildings)
        {

            Transform use = closest == null ? playerBuildings[0].transform : closest;
            float dist = Vector3.Distance(person.position, use.position);

            float next = Vector3.Distance(person.position, r.transform.position);
            if (next < dist)
            {
                closest = r;
                distance = dist;
            }
        }
        print(closest.name);
        return closest;
    }

    void Eco()
    {
        //Tell workers to gather the closes resource

        if (moveWokerUnitsAIEvent != null)
        {
            Debug.Log("Finding Resource");
            // refrence the list of resources, but not in the GetNearestResource, is this the best way of doing it?
            foreach(GameObject g in AIUnits)
            {
                if (g.GetComponent<Units>())
                {
                    if (g.GetComponent<Units>().unitType == Units.UnitType.Worker)
                    {
                        g.GetComponent<Units>().MoveWorker(GetNearestResource(g.transform, r1));
                    }
                }
            }
        }

        //check if AI can build barracks
        if(rScore1 > 10 && barrackBuild == false)
        {
            print("IM READY TO BUILD A BARRACKS! UWU");
            rScore1 -= 10f;
            GameObject go = Instantiate(barracks, barrackBuildingSpot.position, barrackBuildingSpot.rotation);
            barracksUnitSpawner = GameObject.Find("AI barrack spawner").GetComponent<Transform>();
            barrackBuild = true;

            // add barracks spawnpOINT
        }
        // if barrack is already build, create units at the barracks posistion
        else if (rScore1 > 10 && barrackBuild == true)
        {
            MakeUnits();
            rScore1 -= 10f;
        }

        // if AIunits list is bigger then 7 then go into attack state
        if (AIUnits.Count > 7)
        {
            state = State.Attack;
        }

    }   

    // if target ain't null get the closest player units
    void AttackMode()
    {
        // if moveFighterUnitsAIEvent is not empty get the closest units that the player controlls
        if (moveFighterUnitsAIEvent != null)
        {
            // CHANGES this to the player units when Tom is done
            moveFighterUnitsAIEvent(GetNearestEnemy(playerUnits));
        }
        else
        {
            foreach (GameObject g in AIUnits)
            {
                if (g.GetComponent<Units>())
                {
                    if (g.GetComponent<Units>().unitType != Units.UnitType.Worker)
                    {
                        g.GetComponent<Units>().MoveToTargetAI(GetnearestBuilding(g.transform, playerBuildings));
                    }
                }
            }
        }
        // if units amount is low go back into eco state
        if(AIUnits.Count < 4)
        {
            state = State.Eco;
        }
    }

    void DefendMode()
    {
        // find all combat units that are left and run back to base
    }

    // creates units that the AI controlls
    void MakeUnits()
    {
        Instantiate(myUnits, barracksUnitSpawner.position, barracksUnitSpawner.rotation);
        // CHANGES this into the Units script
        Debug.Log("Hey I'm with the crew" + AIUnits.Count);
       
    }
}
