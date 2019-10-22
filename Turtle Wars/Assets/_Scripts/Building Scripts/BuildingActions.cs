using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingActions : BuildBuildings
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Building Actions

    // Town Hall
    public void HealthUpgrade()
    {
        if (selected.GetComponent<SelfBuildingManager>().townHallControls == true)
        {

            healthupgrade = true;
            for (int i = 0; i < barracksList.Count; i++)
            {
                barracksList[i].GetComponent<SelfBuildingManager>().maxHealth += healthUpgradeAmount;
                barracksList[i].GetComponent<SelfBuildingManager>().currentHealth += healthUpgradeAmount;
            }
        }
    }

















}
