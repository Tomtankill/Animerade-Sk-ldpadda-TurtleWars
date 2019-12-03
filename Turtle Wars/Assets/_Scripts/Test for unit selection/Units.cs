using System.Collections;
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
    private float atkRangeDubbled;
    public float attackDamage;
    public float attackTimer;
    private float defultAttackTimmer;
    public bool attacking;
    public bool gathering;

    BuildBuildings commander;
    // Audio Sound Effect
    private AudioSource Sound;
    // Animation controllor
    public Animator anim;
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
        Attack,
    }


    // Start is called before the first frame update
    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        atkRangeDubbled = 16f;
        selectedGreenBox = gameObject.transform.GetChild(1).gameObject;
        Sound = GetComponent<AudioSource>();
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

        // actives selectedGreenBox if a unit is currently selected
        if (currentlySelected == true && gameObject.CompareTag("Friendly"))
        {

            selectedGreenBox.SetActive(true);
            
        }
        else if (currentlySelected == false && gameObject.CompareTag("Friendly"))
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
            if (gameObject.name.Contains("Squid") || gameObject.name.Contains("Sword fish"))
            {
                anim.SetBool("Death", true);
            }
            Destroy(gameObject);
        }

        if (gameObject.name.Contains("Squid") || gameObject.name.Contains("Sword fish"))
        {
            if (agent.remainingDistance < 0.5f)
            {
                anim.SetBool("Walking", false);
            }
            else
            {
                anim.SetBool("Walking", true);
            }
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

        // if target is a unit
        if (target.GetComponent<Units>())
        {
            if (gameObject.name.Contains("Squid"))
            {
                if ((Vector3.Distance(transform.position, target.transform.position) > atkRange))
                {
                    agent.isStopped = false;
                    agent.SetDestination(target.transform.position);
                }
                else
                {
                    agent.isStopped = true;
                    agent.SetDestination(transform.position);
                    if (!attacking)
                        StartCoroutine(Attack());
                }
            }
            else
            {
                print("This is a unit");
                float dis = Vector3.Distance(transform.position, target.transform.position) - agent.radius - target.GetComponent<NavMeshAgent>().radius;
                //float f = (target.GetComponent<NavMeshAgent>() ? target.GetComponent<NavMeshAgent>().radius : 0.1f);
                if (dis > atkRange)
                {
                    agent.isStopped = false;
                    agent.SetDestination(target.transform.position);
                }
                else
                {
                    agent.isStopped = true;
                    agent.SetDestination(transform.position);
                    if (!attacking)
                        StartCoroutine(Attack());
                }
            }
        }

        // if target is building
        else if (target.GetComponent<SelfBuildingManager>())
        {
            // wack way to fix squids attack towards buildings (may fix later)
            if (gameObject.name.Contains("Squid"))
            {
                if ((Vector3.Distance(transform.position, target.transform.position) > atkRangeDubbled))
                {
                    agent.isStopped = false;
                    agent.SetDestination(target.transform.position);
                }
                else
                {
                    agent.isStopped = true;
                    agent.SetDestination(transform.position);
                    if (!attacking)
                        StartCoroutine(Attack());
                }
            }

            else
            {
                if ((Vector3.Distance(transform.position, target.transform.position) > atkRange))
                {
                    agent.isStopped = false;
                    agent.SetDestination(target.transform.position);
                }
                else
                {
                    agent.isStopped = true;
                    agent.SetDestination(transform.position);
                    if (!attacking)
                        StartCoroutine(Attack());
                }
            }
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
        float timeCache = attackTimer;
        attacking = true;

        if (gameObject.name.Contains("Squid") || gameObject.name.Contains("Sword fish"))
        {
            anim.SetBool("Attack", true);

        }

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

        if (target.GetComponent<Units>())
        {
            float f = target.GetComponent<NavMeshAgent>() ? target.GetComponent<NavMeshAgent>().radius : 0.5f;
            if (f < atkRange)
            {

                print("target should have taken dmg");
                DealDmg(attackDamage);
            }
        }
        // if it's a building
        else
        {
            print("target should have taken dmg");
            DealDmg(attackDamage);
        }

        attackTimer = timeCache;
        attacking = false;
        if (gameObject.name.Contains("Squid") || gameObject.name.Contains("Sword fish"))
        {
            anim.SetBool("Attack", false);
        }
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
            if (gameObject.name.Contains("Squid"))
            {

            }
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
            if (gameObject.name.Contains("Squid") || gameObject.name.Contains("Sword fish"))
            {
                anim.SetBool("Walking", true);
            }

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
        Sound.Play();

        newTarget = t;
        agent.SetDestination(newTarget);
        if (gameObject.name.Contains("Squid"))
        {
            if (gameObject.name.Contains("Squid") || gameObject.name.Contains("Sword fish"))
            {
                anim.SetBool("Walking", true);
            }

        }
    }

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
        }
        print("This does print");
        gathering = false;
    }
}