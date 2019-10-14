using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

    BuildBuildings commander;

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
        // defultAttackTimmer is the same as attackTimer
        defultAttackTimmer = attackTimer;

        // getcomponent of objects that will be used
        agent = GetComponent<NavMeshAgent>();
        myRend = GetComponent<MeshRenderer>();
        GameObject thePlayer = GameObject.Find("Commander");
        commander = thePlayer.GetComponent<BuildBuildings>();
        
        // adds this gameobject to a list from click
        Camera.main.gameObject.GetComponent<Click>().selectableObjects.Add(this);

        //attackTimer = currentAttackTimer;
        // may not work
        Camera.main.gameObject.GetComponent<Click>().selectableUnits.Add(this);
        unitList.Add(this);
        state = State.Idle;
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
        StopAllCoroutines();
        target = null;
        attacking = false;
        attackTimer = defultAttackTimmer;
        agent.isStopped = false;
    }
    
    // setting movementpostion
    public void MoveToTarget(Vector3 target)
    {
        // sets the gameobject target and gets the postion with the vector 3
        newTarget = target;
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
                StartCoroutine(Gathering());
        }
    }

    IEnumerator Gathering()
    {

        float timeCache = attackTimer;
        attacking = true;

        while (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        if (target == null)
            StopCoroutine(Gathering());

        if (Vector3.Distance(transform.position, target.transform.position) < atkRange)
        {
            if(target.gameObject.name.Contains("Resource 1"))
            {
                commander.r1 += 1;
            }
            else
            {
                commander.r2++;
            }
            
        }

        attackTimer = timeCache;
        attacking = false;
    }
}