using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{

    [SerializeField] private Transform pfFriendlyWarrior;
    [SerializeField] private Transform pfEnemyyWarrior;

    private GameObject childObj;
    private bool building;
    private IEnumerator fade;
    private float timmer;
    // Start is called before the first frame update
    void Start()
    {
        building = true;
        StartCoroutine("Fade");
        pfFriendlyWarrior.transform.Find("Child Name");
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Fade()
    {
        Instantiate(pfFriendlyWarrior, new Vector3(20, 10), Quaternion.identity);

        if (building == true)
        {
            print("me live");
            print(pfFriendlyWarrior.GetChild(0).name);
            print(pfFriendlyWarrior.GetChild(1).name);
            print(pfFriendlyWarrior.GetChild(2).name);
            pfFriendlyWarrior.transform.GetChild(1).gameObject.SetActive(true);
            yield return new WaitForSeconds(5f);
        }

        if (building == true)
        {
            pfFriendlyWarrior.transform.GetChild(0).gameObject.SetActive(false);
            pfFriendlyWarrior.transform.GetChild(1).gameObject.SetActive(true);
            yield return new WaitForSeconds(5f);
            print("I do my thing");
        }

        if (building == true)
        {
            pfFriendlyWarrior.transform.GetChild(0).gameObject.SetActive(false);
            pfFriendlyWarrior.transform.GetChild(1).gameObject.SetActive(false);
            pfFriendlyWarrior.transform.GetChild(2).gameObject.SetActive(true);
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
