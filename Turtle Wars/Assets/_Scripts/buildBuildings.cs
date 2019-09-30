using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildBuildings : MonoBehaviour
{
    [SerializeField] private Transform buildingPrefab;
    [SerializeField] private Transform friendlyUnit;
    private GameObject theBuildingThing;

    private bool building;
    private bool barackIsBuild;
    private GameObject spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        GrowBuilding();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // fix this Linus!!!!!!
    public void GrowBuilding() {
        Instantiate(buildingPrefab, new Vector3(20, 13), Quaternion.identity);
        theBuildingThing = GameObject.Find("Building(Clone)");

        print(theBuildingThing.name);
        theBuildingThing.transform.GetChild(0).gameObject.SetActive(true);
        theBuildingThing.transform.GetChild(1).gameObject.SetActive(false);
        theBuildingThing.transform.GetChild(2).gameObject.SetActive(false);
        print("me live");
        new WaitForSeconds(5f);
        

        theBuildingThing.transform.GetChild(0).gameObject.SetActive(false);
        theBuildingThing.transform.GetChild(1).gameObject.SetActive(true);
        theBuildingThing.transform.GetChild(2).gameObject.SetActive(false);
        print("I do my thing");
        new WaitForSeconds(5f);


        theBuildingThing.transform.GetChild(0).gameObject.SetActive(false);
        theBuildingThing.transform.GetChild(1).gameObject.SetActive(false);
        theBuildingThing.transform.GetChild(2).gameObject.SetActive(true);
        barackIsBuild = true;
        print("I have done the thing");
        new WaitForSeconds(5f);

        
    }
}
