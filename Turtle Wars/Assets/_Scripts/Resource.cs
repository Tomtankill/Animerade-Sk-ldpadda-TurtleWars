using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType { r1, r2 }

public class Resource : MonoBehaviour
{
    public int amountOfResource = 50;
    public int currentResource = 50;
    public ResourceType resourceType;
    private AudioSource Sound;
    // Start is called before the first frame update
    void Start()
    {
        Sound = GetComponent<AudioSource>();
        switch (resourceType)
        {
            case ResourceType.r1:
                PriorityAI.r1.Add(gameObject.transform);
                break;
            case ResourceType.r2:
                //PriorityAI.r2.Add(this);
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    { 
        if (amountOfResource == currentResource-1)
        {
            Sound.Play();
            currentResource = amountOfResource;
        }
        if (amountOfResource <= 0)
        {
            Destroy(gameObject);
        }
    }
}
