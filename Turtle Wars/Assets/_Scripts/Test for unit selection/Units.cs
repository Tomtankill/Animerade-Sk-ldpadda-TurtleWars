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
    float damage;
    SelfBuildingManager selfbuilding;

    [SerializeField] private Material red;
    [SerializeField] private Material green;
    Vector3 newTarget;
    private MeshRenderer myRend;

    [HideInInspector] public bool currentlySelected = false;
    [SerializeField] private bool isEnemy;

    public float atkRange;
    public float attackDamage;
    public float attackTimer;
    bool attacking;

    public GameObject target;

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

    public void GatheringResource(int r1, int r2)
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
                StartCoroutine(Attack());
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
            //target.GetComponent<Units>().TakeDamage(attackDamage);
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
            // make new script that will be on the resource
        }

        attackTimer = timeCache;
        attacking = false;
    }
}