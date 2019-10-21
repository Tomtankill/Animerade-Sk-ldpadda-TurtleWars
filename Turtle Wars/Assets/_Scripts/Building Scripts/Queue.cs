using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queue : MonoBehaviour
{

    [SerializeField] private List<string> taskQueue = new List<string>();
    private string testForChange;
    public GameObject commander;


    void Start()
    {

    }
    void Update()
    {
        if (taskQueue.Count > 0) {
            // if the first thing in the list changes, start work on that first task
            if (testForChange != taskQueue[0])
            {
                testForChange = taskQueue[0];
                //Debug.Log("there has been a change to the first item on the list");
                
                // finds out what its working on
                if (taskQueue[0] == "reset")
                {
                    taskQueue.RemoveAt(0);
                }
                else
                {
                    StartCoroutine(taskQueue[0]);
                }
            }
        }
        if (taskQueue.Count == 0)
        {
            testForChange = "clear";
        }
    }

    public void TaskButton()
    {
        if (commander.GetComponent<TurnTimer>().p1Turn == true)
        {
            if (taskQueue.Count != 0)
            {
                if (taskQueue[taskQueue.Count - 1] == "job")
                {
                    taskQueue.Add("reset");
                    taskQueue.Add("job");
                }
                else
                {
                    taskQueue.Add("job");
                }
            }
            else
            {
                taskQueue.Add("job");
            }
        }
    }

    public void TaskOneButton()
    {
        if (commander.GetComponent<TurnTimer>().p1Turn == true)
        {
            if (taskQueue.Count != 0)
            {
                if (taskQueue[taskQueue.Count - 1] == "job1")
                {
                    taskQueue.Add("reset");
                    taskQueue.Add("job1");
                }
                else
                {
                    taskQueue.Add("job1");
                }
            }
            else
            {
                taskQueue.Add("job1");
            }
        }
    }

    public void TaskTwoButton()
    {
        if (commander.GetComponent<TurnTimer>().p1Turn == true)
        {
            if (taskQueue.Count != 0)
            {
                if (taskQueue[taskQueue.Count - 1] == "job2")
                {
                    taskQueue.Add("reset");
                    taskQueue.Add("job2");
                }
                else
                {
                    taskQueue.Add("job2");
                }
            }
            else
            {
                taskQueue.Add("job2");
            }
        }
    }

    public void TaskThreeButton()
    {
        if (commander.GetComponent<TurnTimer>().p1Turn == true)
        {
            if (taskQueue.Count != 0)
            {
                if (taskQueue[taskQueue.Count - 1] == "job3")
                {
                    taskQueue.Add("reset");
                    taskQueue.Add("job3");
                }
                else
                {
                    taskQueue.Add("job3");
                }
            }
            else
            {
                taskQueue.Add("job3");
            }
        }
    }

    public void TaskFourButton()
    {
        if (commander.GetComponent<TurnTimer>().p1Turn == true)
        {
            if (taskQueue.Count != 0)
            {
                if (taskQueue[taskQueue.Count - 1] == "job4")
                {
                    taskQueue.Add("reset");
                    taskQueue.Add("job4");
                }
                else
                {
                    taskQueue.Add("job4");
                }
            }
            else
            {
                taskQueue.Add("job4");
            }
        }
        else
        {
            
        }
    }

    IEnumerator RunJob(string action, float time)
    {
        //yield return new WaitForSeconds(time);
        //Actions.Invoke(action, 0);

        Debug.Log("job happens");
        taskQueue.RemoveAt(0); // removes the task after the task is finished
    }
    IEnumerator Job1()
    {
        yield return new WaitForSeconds(5.0f);
        Debug.Log("job1 happens");
        taskQueue.RemoveAt(0); // removes the task after the task is finished
    }
    IEnumerator Job2()
    {
        yield return new WaitForSeconds(5.0f);
        Debug.Log("job2 happens");
        taskQueue.RemoveAt(0); // removes the task after the task is finished
    }
    IEnumerator Job3()
    {
        yield return new WaitForSeconds(5.0f);
        Debug.Log("job3 happens");
        taskQueue.RemoveAt(0); // removes the task after the task is finished
    }
    IEnumerator Job4()
    {
        yield return new WaitForSeconds(5.0f);
        Debug.Log("job4 happens");
        taskQueue.RemoveAt(0); // removes the task after the task is finished
    }
}

public class BUildingStuff
{
    public int Health;

    public BUildingStuff(int health, float buildtime, int attackdamage, string name)
    {
        Health = health;

    }
}
