using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : MonoBehaviour
{

    private static List<Warrior> warriorList = new List<Warrior>();

    private const float speed = 40f;

    private Vector3 targetPosition;
    [SerializeField] private bool isEnemy;
    
    private enum State
    {
        Normal,
        Busy
    }
    // Start is called before the first frame update
    private void Start()
    {
        warriorList.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
