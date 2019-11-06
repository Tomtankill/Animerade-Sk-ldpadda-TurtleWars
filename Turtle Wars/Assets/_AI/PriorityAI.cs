﻿using System.Collections;
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
    private bool barrackBuild;

    // this stores the AI's resources
    public float rScore1;
    public float rScore2;

    // refrence the AI's units prefab so it can me instantiate it
    public GameObject myUnits;
    public Transform myBase;

    // list of the transform of resources in the map
    public static List<Transform> r1 = new List<Transform>(), r2 = new List<Transform>();

    // players units and building
    public static List<Transform> playerUnits = new List<Transform>(); 
    public static List<GameObject> AIUnits = new List<GameObject>();

    // this is the acceptableDistance that a unit can find a enemy/resource
    public float acceptableDistance = 25f;

    // sets the state of the AI, that changes what the AI will focus on
    public State state;
    public enum State
    {
        Eco,
        Attack,
        Defend
    }

    void Start()
    {
        // CHECK with Tom how to setup the turntimer so AI dosen't cheat >:c
        //turntime = GetComponent<TurnTimer>();

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
    }

    void Update()
    {
        

        // switches state and runs a function depending on what state it's in
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
        // ADDS that AI checks for it's turn 
    }

    // find the closest resources transform, then return the closest resource
    Transform GetNearestResource(List<Transform> resource)
    {
        Transform closest = null;
        float distance = 99999;

        if(resource.Count == 1)
        {
            return resource[0];
        }

        foreach (Transform r in resource)
        {
            Transform use = closest == null ? resource[0].transform : closest;
            float d = Vector3.Distance(use.transform.position, r.transform.position);

            if (d < distance)
            {
                closest = r;
                distance = d;
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

    void Eco()
    {
        //Tell workers to gather the closes resource

        if (moveWokerUnitsAIEvent != null)
        {
            Debug.Log("Finding Resource");
            // refrence the list of resources, but not in the GetNearestResource, is this the best way of doing it?
            moveWokerUnitsAIEvent(GetNearestResource(r1));
        }

        //check if AI can build barracks
        if(rScore1 > 10 && barrackBuild == false)
        {
            print("IM READY TO BUILD A BARRACKS! UWU");
            rScore1 -= 10f;
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

        if (moveFighterUnitsAIEvent != null)
        {
            // CHANGES this to the player units when Tom is done
            moveFighterUnitsAIEvent(GetNearestEnemy(playerUnits));
        }
    }

    void DefendMode()
    {
        // find all combat units that are left and run back to base
    }

    void MakeUnits()
    {
        Instantiate(myUnits, myBase.position, myBase.rotation);
        //Instantiate(myUnits, new Vector3(0, 0, 0), Quaternion.identity);
        AIUnits.Add(gameObject);
        // CHANGES this into the Units script
        Debug.Log("Hey I'm with the crew" + AIUnits.Count);
    }
}
