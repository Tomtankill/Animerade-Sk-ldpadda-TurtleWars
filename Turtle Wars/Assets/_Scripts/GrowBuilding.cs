using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrowBuilding : MonoBehaviour
{
    [SerializeField]private bool buildingGrow;
    private bool controlls;
    [SerializeField]private Renderer buildingRender;

    public float currentHealth;
    [SerializeField]private float maxHealth;
    private float reg = 5;

    [SerializeField] private float checkPointOne;
    [SerializeField] private float checkPointTwo;
    [SerializeField] private float checkPointThree;

    [SerializeField]private Material red;
    [SerializeField] private Material yellow;
    [SerializeField] private Material green;

    // Start is called before the first frame update
    void Start()
    {
        buildingGrow = false;
        controlls = false;
        buildingRender = GetComponent<Renderer>();
        currentHealth = 0;

        if (gameObject.name == ("barracks(Clone)"))
        {
            maxHealth = 100;
        }
        else if (gameObject.name.Contains("Townhall"))
        {
            maxHealth = 200;
        }
        currentHealth = 1;
        checkPointOne = maxHealth / 3f;
        checkPointTwo = maxHealth / 2f;
        checkPointThree = maxHealth / 1.5f;
        print("This is check one" + checkPointOne);
        print("This is check one" + checkPointTwo);

    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
        
        if (buildingGrow == false)
        {
            currentHealth += reg * Time.deltaTime;
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

            else if (currentHealth >= maxHealth)
            {
                buildingGrow = true;
                controlls = true;
                currentHealth = maxHealth;
            }

        }
    }
}
