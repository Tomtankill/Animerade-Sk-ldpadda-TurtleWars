using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queue : MonoBehaviour
{

    [SerializeField] private List<string> taskQueue = new List<string>();
    private string testForChange;

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
                switch (taskQueue[0]) // finds out what its working on
                {
                    case "job":
                        StartCoroutine("Task");

                        break;

                    case "job1":
                        StartCoroutine("TaskOne");

                        break;

                    case "job2":
                        StartCoroutine("TaskTwo");

                        break;

                    case "job3":
                        StartCoroutine("TaskThree");

                        break;

                    case "job4":
                        StartCoroutine("TaskFour");

                        break;
                    case "reset":
                        taskQueue.RemoveAt(0);
                        break;

                    default:
                        Debug.LogError("taskQueue failed to find a valid task");
                        taskQueue.RemoveAt(0);
                        break;
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
        if (taskQueue.Count != 0)
        {
            if (taskQueue[taskQueue.Count - 1] == "job")
            {
                taskQueue.Add("reset");
                taskQueue.Add("job");
            } else
            {
                taskQueue.Add("job");
            }
        } else
        {
            taskQueue.Add("job");
        }
    }

    public void TaskOneButton()
    {
        if (taskQueue.Count != 0)
        {
            if (taskQueue[taskQueue.Count - 1] == "job1")
            {
                taskQueue.Add("reset");
                taskQueue.Add("job1");
            } else
            {
                taskQueue.Add("job1");
            }
        } else
        {
            taskQueue.Add("job1");
        }
    }

    public void TaskTwoButton()
    {
        if (taskQueue.Count != 0)
        {
            if (taskQueue[taskQueue.Count - 1] == "job2")
            {
                taskQueue.Add("reset");
                taskQueue.Add("job2");
            } else
            {
                taskQueue.Add("job2");
            }
        } else
        {
            taskQueue.Add("job2");
        }
    }

    public void TaskThreeButton()
    {
        if (taskQueue.Count != 0)
        {
            if (taskQueue[taskQueue.Count - 1] == "job3")
            {
                taskQueue.Add("reset");
                taskQueue.Add("job3");
            } else
            {
                taskQueue.Add("job3");
            }
        } else
        {
            taskQueue.Add("job3");
        }
    }

    public void TaskFourButton()
    {
        if (taskQueue.Count != 0)
        {
            if (taskQueue[taskQueue.Count - 1] == "job4")
            {
                taskQueue.Add("reset");
                taskQueue.Add("job4");
            } else
            {
                taskQueue.Add("job4");
            }
        } else
        {
            taskQueue.Add("job4");
        }
    }

    IEnumerator Task()
    {
        yield return new WaitForSeconds(5.0f);
        Debug.Log("job happens");
        taskQueue.RemoveAt(0); // removes the task after the task is finished
    }
    IEnumerator TaskOne()
    {
        yield return new WaitForSeconds(5.0f);
        Debug.Log("job1 happens");
        taskQueue.RemoveAt(0); // removes the task after the task is finished
    }
    IEnumerator TaskTwo()
    {
        yield return new WaitForSeconds(5.0f);
        Debug.Log("job2 happens");
        taskQueue.RemoveAt(0); // removes the task after the task is finished
    }
    IEnumerator TaskThree()
    {
        yield return new WaitForSeconds(5.0f);
        Debug.Log("job3 happens");
        taskQueue.RemoveAt(0); // removes the task after the task is finished
    }
    IEnumerator TaskFour()
    {
        yield return new WaitForSeconds(5.0f);
        Debug.Log("job4 happens");
        taskQueue.RemoveAt(0); // removes the task after the task is finished
    }
}
