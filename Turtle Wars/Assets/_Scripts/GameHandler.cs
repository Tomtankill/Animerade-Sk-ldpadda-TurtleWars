using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{

    [SerializeField] private Transform pfFriendlyWarrior;
    [SerializeField] private Transform pfEnemyyWarrior;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnUnits()
    {
        for (int i =0; i < 5; i++)
        {
            Instantiate(pfEnemyyWarrior, new Vector3(10, 1), Quaternion.identity);
        }
    }
}
