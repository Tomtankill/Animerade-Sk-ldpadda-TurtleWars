using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrowBuilding : MonoBehaviour
{
    // bools
    [SerializeField]private bool buildingFinish;
    private bool controlls;

    // render
    [SerializeField]private Renderer buildingRender;

    // health values
    public float currentHealth;
    [SerializeField]private float maxHealth;
    private float reg = 2f;

    // checkpoints
    [SerializeField] private float checkPointOne;
    [SerializeField] private float checkPointTwo;
    [SerializeField] private float checkPointThree;

    // materials
    [SerializeField]private Material red;
    [SerializeField] private Material yellow;
    [SerializeField] private Material green;

    // Start is called before the first frame update
    void Start()
    {
        // bools are false on start
        buildingFinish = false;
        controlls = false;
        buildingRender = GetComponent<Renderer>();
        currentHealth = 1;

        // setting maxHealth depending on what building it is
        if (gameObject.name.Contains("Townhall"))
        {
            maxHealth = 150;
        }
        else if (gameObject.name.Contains("Barracks"))
        {
            maxHealth = 100;
        }
        else if (gameObject.name.Contains("Armory"))
        {
            maxHealth = 100;
        }
        else if (gameObject.name.Contains("Tower"))
        {
            maxHealth = 75;
        }

        // setting checkpoints
        checkPointOne = maxHealth / 3f;
        checkPointTwo = maxHealth / 2f;
        checkPointThree = maxHealth / 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        // destroy if helath is 0 or lower
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
        
        // if building is currently building run this
        if (buildingFinish == false)
        {
            // heals currenthealth overtime
            currentHealth += reg * Time.deltaTime;

            // changes color depending on amount of health
            if (currentHealth <= checkPointOne)
            {
                buildingRender.material = red;
            }

            else if (currentHealth <= checkPointTwo)
            {
                buildingRender.material = yellow;
            }

            else if (currentHealth <= checkPointThree)
            {
                buildingRender.material = green;
            }

            // if currentHealth is >= maxHealth make play able to control
            // stop healing over time and set currentHealth to maxHealth
            else if (currentHealth >= maxHealth)
            {
                buildingFinish = true;
                controlls = true;
                currentHealth = maxHealth;
            }

        }
    }
}
