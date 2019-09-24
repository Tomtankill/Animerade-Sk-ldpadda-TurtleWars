using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{

    [SerializeField] private Transform buildingPrefab;
    [SerializeField] private Transform pfEnemyyWarrior;
    private GameObject theBuildingThing;

    private GameObject childObj;
    private bool building;
    private IEnumerator fade;
    private float timmer;
    // Start is called before the first frame update
    void Start()
    {
        building = true;
        StartCoroutine("Fade");
        buildingPrefab.transform.Find("Child Name");
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Fade()
    {
        Instantiate(buildingPrefab, new Vector3(20, 13), Quaternion.identity);
        theBuildingThing = GameObject.Find("Building(Clone)");
        if (building == true)
        {
            print("me live");
            print(theBuildingThing.name);
            theBuildingThing.transform.GetChild(0).gameObject.SetActive(true);
            theBuildingThing.transform.GetChild(1).gameObject.SetActive(false);
            theBuildingThing.transform.GetChild(2).gameObject.SetActive(false);
            yield return new WaitForSeconds(5f);
        }

        if (building == true)
        {
            theBuildingThing.transform.GetChild(0).gameObject.SetActive(false);
            theBuildingThing.transform.GetChild(1).gameObject.SetActive(true);
            theBuildingThing.transform.GetChild(2).gameObject.SetActive(false);
            yield return new WaitForSeconds(5f);
            print("I do my thing");
        }

        if (building == true)
        {
            theBuildingThing.transform.GetChild(0).gameObject.SetActive(false);
            theBuildingThing.transform.GetChild(1).gameObject.SetActive(false);
            theBuildingThing.transform.GetChild(2).gameObject.SetActive(true);
            yield return new WaitForSeconds(5f);
            print("I have done the thing");
        }
    }


    private void SpawnUnits()
    {
        for (int i =0; i < 5; i++)
        {
            
        }
    }
}
