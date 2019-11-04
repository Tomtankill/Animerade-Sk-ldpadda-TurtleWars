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


    private TurnTimer turntime;
    private bool barrackBuild;
    public float rScore1;
    public float rScore2;
    public GameObject myUnits;
    public Transform myBase;

    // resources in the game
    public static List<Transform> r1 = new List<Transform>(), r2 = new List<Transform>();
    // players units and building
    public static List<Transform> playerUnits = new List<Transform>(); 
    public static List<GameObject> AIUnits = new List<GameObject>();
    public float acceptableDistance = 25f;

    public State state;
    public enum State
    {
        Eco,
        Attack,
        Defend
    }
    // Start is called before the first frame update
    void Start()
    {

        //turntime = GetComponent<TurnTimer>();
        //r1 = new List<Resource>();
        //r2 = new List<Resource>();

        foreach (Resource r in FindObjectsOfType<Resource>())
        {
            if (r.resourceType == ResourceType.r1)
            {
                r1.Add(r.transform);
            }
            else
                r2.Add(r.transform);
        }

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

    // Update is called once per frame
    void Update()
    {
        

        // switches state
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

        //if (moveFighterUnitsAIEvent != null)
        //{
        //    moveFighterUnitsAIEvent(newTarget);
        //}

        // only if it's AI turn
        //if (turntime.p2Turn == true)
        //{

        //}
    }

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
        //Tell workers to gather resources
        if (moveWokerUnitsAIEvent != null)
        {
            Debug.Log("Finding Resource");
            moveWokerUnitsAIEvent(GetNearestResource(r1));
        }

        //check if we can build barracks
        if(rScore1 > 10 && barrackBuild == false)
        {
            print("IM READY TO BUILD A BARRACKS! UWU");

            barrackBuild = true;
            // add barracks spawnpOINT
        }
        else if (rScore1 > 10 && barrackBuild == true)
        {
            MakeUnits();
            rScore1 -= 10f;
        }

        if (AIUnits.Count > 7)
        {
            state = State.Attack;
        }

    }   

    void AttackMode()
    {

        if (moveFighterUnitsAIEvent != null)
        {
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
        Debug.Log("Hey I'm with the crew" + AIUnits.Count);
    }
}
