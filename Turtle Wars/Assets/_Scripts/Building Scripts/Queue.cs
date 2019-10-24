using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Queue : MonoBehaviour
{
    // things for the tasks
    public Image imagetimer;
    private float timerMax = 31.0f;
    [SerializeField] private float timerCurrent = 31.0f;
    private bool taskActive = false;


    // things for the Queue
    [SerializeField] private List<string> taskQueue = new List<string>();
    private string testForChange;
    public GameObject commander;

    void Start()
    {

    }
    void Update()
    {
        //Queue Bit
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

        CancelTask();
        
    }

    public void TaskButton()
    {
        CheckAndAdd("job");
    }

    public void TaskOneButton()
    {
        CheckAndAdd("job1");
    }

    public void TaskTwoButton()
    {
        CheckAndAdd("job2");
    }

    public void TaskThreeButton()
    {
        CheckAndAdd("job3");
    }

    public void TaskFourButton()
    {
        CheckAndAdd("job4");
    }

    public void CheckAndAdd(string job)
    {
        if (GetComponent<TurnTimer>().IsMyTurn() == true)
        {
            if (taskQueue.Count != 0)
            {
                if (taskQueue[taskQueue.Count - 1] == job)
                {
                    taskQueue.Add("reset");
                    taskQueue.Add(job);
                }
                else
                {
                    taskQueue.Add(job);
                }
            }
            else
            {
                taskQueue.Add(job);
            }
        }
    }


    public void CancelTask()
    {
        // Task Bit
        if (timerCurrent != 0 && taskActive)
        {
            timerCurrent -= Time.deltaTime;
            imagetimer.fillAmount = timerCurrent / timerMax;
        }

        if (Input.GetKeyDown(KeyCode.C) && taskActive)
        {
            taskActive = false;
            timerCurrent = timerMax;
            print("I cancelled the task");

            taskQueue.RemoveAt(0); // removes the task after the task is finished
        }

        if (timerCurrent <= 0)
        {
            taskActive = false;
            timerCurrent = timerMax;

            // job activates here somehow

            taskQueue.RemoveAt(0); // removes the task after the task is finished
            print("Job has been completed");
        }

    }


    IEnumerator Job()
    {
        taskActive = true;

        yield return null;
        
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
