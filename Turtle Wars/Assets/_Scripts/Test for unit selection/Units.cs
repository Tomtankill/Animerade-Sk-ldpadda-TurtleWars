﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Units : MonoBehaviour
{

    // sets the x,y,z postion
    public Vector3 newTarget;
    public GameObject target;
    //public GameObject oldTarget;
    private NavMeshAgent agent;

    private GameObject selectedGreenBox;
    // checks if the unit is selected
    [HideInInspector] public bool currentlySelected = false;

    // unit stats
    public float health;
    public float maxhealth;
    public float atkRange;
    public float attackDamage;
    public float attackTimer;
    private float defultAttackTimmer;
    public bool attacking;
    public bool gathering;

    BuildBuildings commander;

    // who is controlling it
    public enum WhoControllsThis { Player, AI }
    public WhoControllsThis whoControllsThis;

    // what type of unit it is
    public UnitType unitType;
    public enum UnitType
    {
        Worker,
        Scout,
        Melee,
        Ranged
    }

    PriorityAI AI;
    // state of the unit
    public State state;
    public enum State
    {
        Idle,
        Gathering,
        Attack
    }


    // Start is called before the first frame update
    protected virtual void Start()
    {


        switch (whoControllsThis)
        {
            case WhoControllsThis.AI:
                PriorityAI.AIUnits.Add(gameObject);
                break;
            case WhoControllsThis.Player:
                Camera.main.transform.GetComponent<Click>().selectableUnits.Add(this);
                break;
            default:
                break;
        }

        // on creation adds all units with Enemy tag movement function to PriorityAI events
        if (gameObject.CompareTag("Enemy") && unitType != UnitType.Worker)
        {
            PriorityAI.moveFighterUnitsAIEvent += MoveToTargetAI;
        }
        // on creation adds all units with type Worker and enemy tag to movement function to PriorityAI event
        if (gameObject.CompareTag("Enemy") && unitType == UnitType.Worker)
        {
            PriorityAI.moveWokerUnitsAIEvent += MoveWorker;
        }

        defultAttackTimmer = attackTimer;

        // getcomponent of objects that will be used
        agent = GetComponent<NavMeshAgent>();
        GameObject thePlayer = GameObject.Find("Commander");
        commander = thePlayer.GetComponent<BuildBuildings>();
        
        // adds this gameobject to a list from click
        Camera.main.gameObject.GetComponent<Click>().selectableUnits.Add(this);

        //attackTimer = currentAttackTimer;
        // may not work
        Camera.main.gameObject.GetComponent<Click>().selectableUnits.Add(this);
        state = State.Idle;
    }

    private void OnDestroy()
    {
        if (gameObject.CompareTag("Enemy") && unitType != UnitType.Worker)
            PriorityAI.moveFighterUnitsAIEvent -= MoveToTargetAI;

        if (gameObject.CompareTag("Enemy") && unitType == UnitType.Worker)
            PriorityAI.moveWokerUnitsAIEvent -= MoveWorker;

        switch (whoControllsThis)
        {
            case WhoControllsThis.AI:
                PriorityAI.AIUnits.Remove(gameObject);
                break;
            case WhoControllsThis.Player:
                Camera.main.transform.GetComponent<Click>().selectableUnits.Remove(this);
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        if (currentlySelected == true)
        {
            selectedGreenBox = gameObject.transform.GetChild(1).gameObject;
            print(selectedGreenBox);
            selectedGreenBox.SetActive(true);
        }
        else if (currentlySelected == false)
        {
            selectedGreenBox.SetActive(false);
        }

        // switches state
        switch (state)
        {
            case State.Idle:
                Idle();
                break;
            case State.Attack:
                HandleAttack();
                break;
            case State.Gathering:
                GatheringResource();
                break;
        }

        // destroy gameobject if health reach 0
        if (health <= 0)
        {
            Destroy(gameObject);
        }


    }

    bool FindSomethingToAttack(out GameObject closestUnit)
    {
        closestUnit = null;
        float lowestDis = Mathf.Infinity;
        foreach (GameObject g in PriorityAI.AIUnits)
        {
            if (g.tag != gameObject.tag)
            {
                float d = Vector3.Distance(transform.position, g.transform.position);
                if (g != this.gameObject)
                {
                    if (d < 25f)
                    {
                        if (closestUnit == null)
                        {
                            closestUnit = g;
                            lowestDis = d;
                        }
                        else if (d < lowestDis)
                        {
                            lowestDis = d;
                            closestUnit = g;
                        }
                    }
                }

            }
        }

        if (closestUnit == null)
        {
            return false;
        }
        else
        {
            //oldTarget = target;
            return true;
        }
    }

    // doing all the attack things
    private void HandleAttack()
    {
        // if target is null state becomes idle
        if(target == null)
        {
            Debug.Log("Target is dead");
            state = State.Idle;
            target = null;
            AI.state = PriorityAI.State.Eco;
        }

        float dis = Vector3.Distance(transform.position, target.transform.position) - agent.radius - target.GetComponent<NavMeshAgent>().radius;

        // if target is further away then atkRange. Move into attack range
        if (dis > atkRange)
        {
            agent.isStopped = false;
            agent.SetDestination(target.transform.position);
        }
        // if in range, atttack
        else
        {
            // this don't run idk whyyy
            agent.isStopped = true;
            agent.SetDestination(transform.position);
            if(!attacking)
            StartCoroutine(Attack());
            print("idk fam");
        }
    }

    // take dmg
    public void DealDmg(float dmg)
    {
        // units get hit
        if (target.GetComponent<SelfBuildingManager>() == null)
        {
            target.GetComponent<Units>().health -= dmg;
        }
        // building get hit
        else
        {
            target.GetComponent<SelfBuildingManager>().currentHealth -= dmg;
        }
    }

    // attack coratin
    IEnumerator Attack()
    {
        print("I'm a cunt, yes");
        float timeCache = attackTimer;
        attacking = true;

        while (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        if (target == null)
        {
            state = State.Idle;
            StopCoroutine(Attack());
        }

        float dis = Vector3.Distance(transform.position, target.transform.position) - agent.radius - target.GetComponent<NavMeshAgent>().radius;
        if (dis < atkRange)
        {
            DealDmg(attackDamage);
        }

        attackTimer = timeCache;
        attacking = false;
        StopCoroutine(Attack());

    }

    // sets everything to defult state, makes 
    void Idle()
    {
        if (gameObject.CompareTag("Friendly"))
        {
            StopAllCoroutines();
            target = null;
            attacking = false;
            attackTimer = defultAttackTimmer;
            agent.isStopped = false;
        }

        //attack if something comes close
        if (FindSomethingToAttack(out target))
        {
            print("This only run this amount of time");
            state = State.Attack;
            HandleAttack();
        }
    }
    

    public void MoveWorker(Transform t)
    {
        target = t.gameObject;

        // if target is further away then atkRange. Move there
        if (Vector3.Distance(transform.position, target.transform.position) > atkRange)
        {
            agent.isStopped = false;
            agent.SetDestination(target.transform.position);
        }

        // if in range, atttack
        else
        {
            agent.isStopped = true;
            agent.SetDestination(transform.position);
            if (!gathering)
            {
                Debug.Log("This is " + target);
                StartCoroutine(Gathering());
            }
        }
    }

    // setting movementpostion
    public void MoveToTargetAI(Transform t)
    {
        target = t.gameObject;

        // sets the gameobject target and gets the postion with the vector 3
        if (Vector3.Distance(transform.position, target.transform.position) > atkRange)
        {
            agent.isStopped = false;
            agent.SetDestination(target.transform.position);
        }

        else
        {
            agent.isStopped = true;
            agent.SetDestination(transform.position);
            if (!attacking)
            {
                StartCoroutine(Attack());
            }
        }
    }

    public void MoveToTarget(Vector3 t)
    {
        newTarget = t;
        agent.SetDestination(newTarget);
    }

    // change color if unit is selected

    // gathering things
    public void GatheringResource()
    {
        // if target is null state becomes idle
        if (target == null)
        {
            state = State.Idle;
            gathering = false;
        }

        // if target is further away then atkRange. Move there
        if (Vector3.Distance(transform.position, target.transform.position) > atkRange)
        {
            agent.isStopped = false;
            agent.SetDestination(target.transform.position);
            
        }

        // if in range, atttack
        else
        {
            agent.isStopped = true;
            agent.SetDestination(transform.position);
            if (!gathering)
            {
                StartCoroutine(Gathering());
            }
        }
    }

    IEnumerator Gathering()
    {
        float timeCache = 1;
        timeCache = attackTimer;
        gathering = true;

        while (timeCache > 0)
        {
            timeCache -= Time.deltaTime;
            //print("timeCache = " + timeCache);
            yield return new WaitForEndOfFrame();
        }

        if (Vector3.Distance(transform.position, target.transform.position) < atkRange)
        {
            if (target == null)
            {
                StopCoroutine(Gathering());
            }

            if (gameObject.CompareTag("Enemy"))
            {
                print("Adding resource");
                FindObjectOfType<PriorityAI>().rScore1++;
            }
            else
            {
                //player
                if (target.gameObject.name.Contains("Sea Resource"))
                {
                    commander.r1++;
                }
                else
                {
                    commander.r2++;
                }
            }
            // takes one resource from the resource node
            target.GetComponent<Resource>().amountOfResource -= 1;
            print("HEUFHeufheu");
        }
        print("This does print");
        gathering = false;
    }
}