using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildBuildings : MonoBehaviour
{
    static List<GameObject> barracks;
    private bool buildingIsComplete;
    public GameObject prefabBarracks;
    public float healthMax;
    private float currentHealth;
    private float firstCheckPoint;
    // Start is called before the first frame update
    void Start()
    {
        GrowBuilding();
        healthMax = 100f;
        firstCheckPoint = healthMax / 3;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    
    // fix this Linus!!!!!!
    public void GrowBuilding()
    {
        
    }
}
