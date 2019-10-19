using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queue : MonoBehaviour
{

    public List<string> taskQueue = new List<string>();
    public string testForChange;

    void Start()
    {

    }
    void Update()
    {
        if (taskQueue.Count > 0) {
            Debug.Log("There is a task to complete");
            // if the first thing in the list changes, start work on that first task
            if (testForChange != taskQueue[0])
            {
                Debug.Log("there has been a change to the first item on the list");
                switch (taskQueue[0]) // finds out what its working on
                {
                    case "job":
                        Debug.Log("job has been identified as the task");
                        Task();

                        break;

                    case "job1":
                        TaskOne();

                        break;

                    case "job2":
                        TaskTwo();

                        break;

                    case "job3":


                        break;

                    case "job4":


                        break;

                    default:
                        Debug.LogError("taskQueue failed to find a valid task");
                        taskQueue.RemoveAt(0);
                        break;
                }
            }
            testForChange = taskQueue[0];
        }
    }

    public void TaskButton()
    {
        taskQueue.Add("job");
    }

    public void TaskOneButton()
    {
        taskQueue.Add("job1");
    }

    public void TaskTwoButton()
    {
        taskQueue.Add("job2");
    }

    public void TaskThreeButton()
    {

    }

    public void TaskFourButton()
    {

    }

    IEnumerator Task()
    {
        yield return new WaitForSeconds(5.0f);
        Debug.Log("job happens");
        taskQueue.RemoveAt(0); // removes the task after the task is finished
    }
    public void TaskOne()
    {
        Debug.Log("job1 happens");
        taskQueue.RemoveAt(0); // removes the task after the task is finished
    }
    public void TaskTwo()
    {
        Debug.Log("job2 happens");
        taskQueue.RemoveAt(0); // removes the task after the task is finished
    }
}
