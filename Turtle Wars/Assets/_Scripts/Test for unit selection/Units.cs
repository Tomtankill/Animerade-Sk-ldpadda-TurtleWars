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
    [SerializeField] public int health;
    [SerializeField] private int attack;

    [SerializeField] private Material red;
    [SerializeField] private Material green;
    Vector3 newTarget;
    private MeshRenderer myRend;

    [HideInInspector] public bool currentlySelected = false;
    [SerializeField] private bool isEnemy;

    private float attackTimer;



    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        myRend = GetComponent<MeshRenderer>();
        unitList.Add(this);
        attackTimer = 2.0f;
    }

    Transform GetClosestEnemy(Transform[] enemies)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform t in enemies)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if(dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }

    private void Update()
    {
        attackTimer -= Time.deltaTime;


        if (health <= 0)
        {
            Destroy(this);
        }
        

    }
    // setting postion
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


    private static Units GetClosest(bool targetIsEnemy, Vector3 position)
    {
        Units closest = null;
        foreach (Units units in unitList)
        {
            if (units.isEnemy == targetIsEnemy)
            {
                if (closest == null)
                {
                    closest = units;
                }
                else
                {
                    if (Vector3.Distance(units.GetPosition(), position) < Vector3.Distance(closest.GetPosition(), position))
                    {
                        closest = units;
                    }
                }
            }
        }
        return closest;
    }

    private Vector3 GetPosition()
    {
        return transform.position;
    }

    // if collision of another unit sets a new positon where it is
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            print("Heeey");
            newTarget = transform.position;
        }
    }
}
