using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Units : MonoBehaviour
{
    private NavMeshAgent agent;

    [SerializeField] private Material red;
    [SerializeField] private Material green;
    Vector3 newTarget;

    private MeshRenderer myRend;

    [HideInInspector] public bool currentlySelected = false;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        myRend = GetComponent<MeshRenderer>();
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

    // if collision of another unit sets a new positon where it is
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Units>())
        {
            newTarget = transform.position;
        }
    }
}
