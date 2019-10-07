using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Units : MonoBehaviour
{
    private static List<Units> unitList = new List<Units>();
    private NavMeshAgent agent;
    // units stats
    float health = 25;
    float maxhealth = 25;

    public Material red;
    public Material green;
    Vector3 newTarget;
    private MeshRenderer myRend;

    [HideInInspector] public bool currentlySelected = false;

    public float atkRange;
    public float attackDamage;
    public float attackTimer;
    public bool attacking;


    public GameObject target;
    BuildBuildings commander;
    public State state;
    public enum State
    {
        Idle,
        Gathering,
        Attack
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        myRend = GetComponent<MeshRenderer>();
        GameObject thePlayer = GameObject.Find("Commander");
        commander = thePlayer.GetComponent<BuildBuildings>();

        //attackTimer = currentAttackTimer;
        // may not work
        Camera.main.gameObject.GetComponent<Click>().selectableObjects.Add(this);
        unitList.Add(this);
        state = State.Idle;
    }


    private void Update()
    {
        
        switch (state)
        {
            case State.Attack:
                HandleAttack();
                break;
            case State.Gathering:
                GatheringResource();
                break;
        }


        if (health <= 0)
        {
            Destroy(gameObject);
        }

    }
    private void HandleAttack()
    {
        // if target is null state becomes idle
        if(target == null)
        {
            state = State.Idle;
            target = null;
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
            if(!attacking)
            StartCoroutine(Attack());
        }
    }

    // get hit
    public void TakeDamage (float dmg)
    {
        if(target.GetComponent<SelfBuildingManager>() == null)
        {
            target.GetComponent<Units>().health -= attackDamage;
        }
        else
        {
            target.GetComponent<SelfBuildingManager>().currentHealth -= attackDamage;
        }
        
        
    }
    
    // setting movementpostion
    public void MoveToTarget(Vector3 target)
    {
        newTarget = target;
        agent.SetDestination(newTarget);
    }

    // change color if unit is selected
    public void ClickMe()
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

    public void GatheringResource()
    {
        // if target is null state becomes idle
        if (target == null)
        {
            state = State.Idle;
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

    public void GetResource(float r1)
    {
        
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
            //currentAttackTimer = attackTimer;
            StopCoroutine(Attack());

        if (Vector3.Distance(transform.position, target.transform.position) < atkRange)
        {
            TakeDamage(attackDamage);
        }

        attackTimer = timeCache;
        attacking = false;
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