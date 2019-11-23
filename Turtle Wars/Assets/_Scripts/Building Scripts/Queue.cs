using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Queue : MonoBehaviour
{
    // things for the tasks
    private float timerMax;
    private float tasktime;
    [SerializeField] private float timerCurrent = 5.0f;
    private bool taskActive = false;
    private float jobTime;
    private float jobTime1;
    private float jobTime2;
    private float jobTime3;
    private float jobTime4;

    public Vector3 pos;


    // things for the Queue
    [SerializeField] private List<string> taskQueue = new List<string>();
    private string testForChange;
    public GameObject commander;


    void Start()
    {
        pos = gameObject.transform.position;
        commander = GameObject.Find("Commander");
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
                    taskActive = true;
                    switch (taskQueue[0])
                    {
                        case "Job":
                            tasktime = jobTime;
                            break;
                        case "Job1":
                            tasktime = jobTime1;
                            break;
                        case "Job2":
                            tasktime = jobTime2;
                            break;
                        case "Job3":
                            tasktime = jobTime3;
                            break;
                        case "Job4":
                            tasktime = jobTime4;
                            break;
                        default:
                            tasktime = 999;
                            Debug.LogError("Couldn't set time for job");
                            break;
                    }
                }
            }
        }
        if (taskQueue.Count == 0)
        {
            testForChange = "clear";
        }

        CancelTask();

        if (taskActive == true)
        {
            DoTask();
        }
        
    }

    private void DoTask()
    {
        timerCurrent -= Time.deltaTime;
        if (timerCurrent == 0)
        {
            taskActive = false;
            timerCurrent = timerMax;
            StartCoroutine(taskQueue[0]);
        }
    }

    public void TaskButton(float taskTime)
    {
        CheckAndAdd("Job");
        jobTime = taskTime;
    }

    public void TaskOneButton(float taskTime)
    {
        CheckAndAdd("Job1");
        jobTime1 = taskTime;
    }

    public void TaskTwoButton(float taskTime)
    {
        CheckAndAdd("Job2");
        jobTime2 = taskTime;
    }

    public void TaskThreeButton(float taskTime)
    {
        CheckAndAdd("Job3");
        jobTime3 = taskTime;
    }

    public void TaskFourButton(float taskTime)
    {
        CheckAndAdd("Job4");
        jobTime4 = taskTime;
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

        if (Input.GetKeyDown(KeyCode.C) && taskActive)
        {
            taskActive = false;
            timerCurrent = timerMax;
            print("I cancelled the task");

            taskQueue.RemoveAt(0); // removes the task
        }
    }


    IEnumerator Job()
    {
        commander.GetComponent<BuildingActions>().FlyingFish(pos);
        print("done job");
        taskQueue.RemoveAt(0);
        yield return null;
        
    }
    IEnumerator Job1()
    {
        commander.GetComponent<BuildingActions>().Crab();
        Debug.Log("job1 happens");
        taskQueue.RemoveAt(0); // removes the task after the task is finished
        yield return null;
    }
    IEnumerator Job2()
    {
        commander.GetComponent<BuildingActions>().Squid();
        Debug.Log("job2 happens");
        taskQueue.RemoveAt(0); // removes the task after the task is finished
        yield return null;
    }
    IEnumerator Job3()
    {
        commander.GetComponent<BuildingActions>().Swordfish();
        Debug.Log("job3 happens");
        taskQueue.RemoveAt(0); // removes the task after the task is finished
        yield return null;
    }
    IEnumerator Job4()
    {
        Debug.Log("job4 happens");
        taskQueue.RemoveAt(0); // removes the task after the task is finished
        yield return null;
    }
}
