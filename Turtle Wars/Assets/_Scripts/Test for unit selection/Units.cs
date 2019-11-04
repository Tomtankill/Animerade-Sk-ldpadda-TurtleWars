using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 
/// Fighting!
/// 
/// 1. Find all units
/// 2. Cycle through them all
/// 3. find the closest
/// 4. check if theyre in attack distance
/// 5. move to attack then
/// 
/// n. have the ai call a move function to next to the players base so they carve an attack path to there
/// 
/// find building, but stop to attack units on the way
/// </summary>


[RequireComponent(typeof(NavMeshAgent))]
public class Units : MonoBehaviour
{
    // meshrender and material used
    private MeshRenderer myRend;
    public Material red;
    public Material green;

    // sets the x,y,z postion
    public Vector3 newTarget;
    public GameObject target;
    private NavMeshAgent agent;
    
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
                break;
            case WhoControllsThis.Player:
                break;
            default:
                break;
        }

        // on creation adds all units with Enemy tag movement function to PriorityAI events
        if (gameObject.CompareTag("Enemy") && unitType != UnitType.Worker)
        {
            PriorityAI.moveFighterUnitsAIEvent += MoveToTargetAI;
        }
        // on creation adds all units with name Worker and enemy tag to movement function to PriorityAI event
        if (gameObject.CompareTag("Enemy") && unitType == UnitType.Worker)
        {
            PriorityAI.moveWokerUnitsAIEvent += MoveWorker;
        }

        defultAttackTimmer = attackTimer;

        // getcomponent of objects that will be used
        agent = GetComponent<NavMeshAgent>();
        myRend = GetComponent<MeshRenderer>();
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
    }


    private void Update()
    {
        
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


    // doing all the attack things
    private void HandleAttack()
    {
        // if target is null state becomes idle
        if(target == null)
        {
            state = State.Idle;
            target = null;
        }

        // if target is further away then atkRange. Move into attack range
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
            if(!attacking)
            StartCoroutine(Attack());
        }
    }

    // take dmg
    public void TakeDamage(float dmg)
    {
        // units get hit
        if (target.GetComponent<SelfBuildingManager>() == null)
        {
            target.GetComponent<Units>().health -= attackDamage;
        }
        
        // building get hit
        else
        {
            target.GetComponent<SelfBuildingManager>().currentHealth -= attackDamage;
        }
    }

    // attack coratin
    IEnumerator Attack()
    {
        float timeCache = attackTimer;
        attacking = true;

        while (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        if (target == null)
            StopCoroutine(Attack());

        if (Vector3.Distance(transform.position, target.transform.position) < atkRange)
        {
            TakeDamage(attackDamage);
        }

        attackTimer = timeCache;
        attacking = false;
        StopCoroutine(Attack());
    }

    // sets everything to defult state, makes 
    void Idle()
    {
        //StopAllCoroutines();
        target = null;
        attacking = false;
        attackTimer = defultAttackTimmer;
        agent.isStopped = false;
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
            Debug.Log("Me hit this");
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
        // sets the gameobject target and gets the postion with the vector 3

        newTarget = t;
        //target = gameObject.transform.position;
        agent.SetDestination(newTarget);

    }

    // change color if unit is selected
    public void IsSelected()
    {
        if (currentlySelected == false)
        {
            // not selected
            myRend.material = red;
        }
        else
        {
            // selected
            myRend.material = green;
        }

    }

    // gathering things
    public void GatheringResource()
    {
        // if target is null state becomes idle
        if (target == null)
        {
            state = State.Idle;
            attacking = false;
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
            if (!attacking)
            {
                StartCoroutine(Gathering());

            }
        }
    }

    IEnumerator Gathering()
    {
        //TODO: ADD AI
        float timeCache = 1;
        gathering = true;

        while (timeCache > 0)
        {
            timeCache -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        if (target == null)
        {
            StopCoroutine(Gathering());
        }

        if (Vector3.Distance(transform.position, target.transform.position) < atkRange)
        {
            if(CompareTag("Enemy"))
            {
                FindObjectOfType<PriorityAI>().rScore1++;
                print("tick");
            }
            else
            {
                //player
                if(target.gameObject.name.Contains("Resource 1"))
                {
                    commander.r1 += 1;
                }
                else
                {
                    commander.r2++;
                }
            }


            
        }

        gathering = false;
    }
}