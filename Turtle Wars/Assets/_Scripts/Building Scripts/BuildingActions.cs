using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingActions : BuildBuildings
{
    
    public GameObject worker;
    public Transform spawnpoint;
    public LayerMask terrainLayer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Building Actions

    public void Worker()
    {
        GameObject go = Instantiate(worker, new Vector3 (spawnpoint.position.x, 5 ,spawnpoint.position.z), Quaternion.identity);
        Ray ray = new Ray (go.transform.position, Vector3.down); 
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000000.0f, terrainLayer))
        {
            go.transform.position = hit.point + new Vector3(0,1,0);
        }
    }


    // Town Hall Upgrades
    public void HealthUpgrade()
    {
        if (selected.GetComponent<SelfBuildingManager>().townHallControls == true)
        {
            healthupgrade = true;
            for (int i = 0; i < masterBuildingList.Count; i++)
            {
                masterBuildingList[i].GetComponent<SelfBuildingManager>().maxHealth += healthUpgradeAmount;
                masterBuildingList[i].GetComponent<SelfBuildingManager>().currentHealth += healthUpgradeAmount;
            }
        }
    }




















}
