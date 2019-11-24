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
    [SerializeField] private float tasktime = 5.0f;
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
        timerMax = 9999999999;
        pos = gameObject.transform.position;
        commander = GameObject.Find("Commander");
    }
    void Update()
    {
        //Queue Bit
        if (taskQueue.Count > 0) {
            print("detected something in taskQueue");
            // if the first thing in the list changes, start work on that first task
            if (testForChange != taskQueue[0])
            {
                print("There has been a change to the first task in Taskqueue");
                testForChange = taskQueue[0];
                //Debug.Log("there has been a change to the first item on the list");
                
                // finds out what its working on
                if (taskQueue[0] == "reset")
                {
                    print("Taskqueue reset first item");
                    taskQueue.RemoveAt(0);
                }
                else
                {
                    taskActive = true;
                    print("taskActive = true");
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
                    print("Taskqueue set how long the task will take");
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
            print("Starting Task Now.");
            DoTask();
        }
        
    }

    private void DoTask()
    {
        if (tasktime > 0)
        {
            tasktime -= Time.deltaTime;
            print("Decreasing time");
        }
        if (tasktime <= 0)
        {

            print("task time finished");
            taskActive = false;
            tasktime = timerMax;
            StartCoroutine(taskQueue[0]);
        }
    }
    public void CancelTask()
    {

        if (Input.GetKeyDown(KeyCode.C) && taskActive)
        {
            taskActive = false;
            tasktime = timerMax;
            print("I cancelled the task");

            taskQueue.RemoveAt(0); // removes the task
        }
    }

    public void TaskButton(float taskTime)
    {
        CheckAndAdd("Job");
        print("Added Job");
        jobTime = taskTime;
        print("Time set to:" + jobTime); ;
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
        if (commander.GetComponent<TurnTimer>().IsMyTurn() == true)
            print("Check and Add it's my turn");
        {
            if (taskQueue.Count != 0)
            {
                print("Taskqueue is not 0");
                if (taskQueue[taskQueue.Count - 1] == job)
                {
                    print("Previous task was: " + job);
                    taskQueue.Add("reset");
                    taskQueue.Add(job);
                }
                else
                {
                    print("Added: " + job);
                    taskQueue.Add(job);
                }
            }
            else
            {
                print("Added: " + job);
                taskQueue.Add(job);
            }
        }
    }




    IEnumerator Job()
    {
        commander.GetComponent<BuildingActions>().FlyingFish(pos);
        print(pos);
        print("done job");
        taskQueue.RemoveAt(0);
        print("Queuescript is finished doing everything it needs to do");
        yield return null;
        
    }
    IEnumerator Job1()
    {
        commander.GetComponent<BuildingActions>().Crab(pos);
        Debug.Log("job1 happens");
        taskQueue.RemoveAt(0); // removes the task after the task is finished
        yield return null;
    }
    IEnumerator Job2()
    {
        commander.GetComponent<BuildingActions>().Squid(pos);
        Debug.Log("job2 happens");
        taskQueue.RemoveAt(0); // removes the task after the task is finished
        yield return null;
    }
    IEnumerator Job3()
    {
        commander.GetComponent<BuildingActions>().Swordfish(pos);
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
