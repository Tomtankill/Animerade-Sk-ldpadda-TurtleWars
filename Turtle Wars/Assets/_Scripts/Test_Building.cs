using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Building : MonoBehaviour
{
    private int constructionTick;
    private int constructionTickMax;
    private Action onConstructionComplete;

    public void AddConstructionTick()
    {
        constructionTick++;
        if (constructionTick >= constructionTickMax)
        {
            onConstructionComplete();
            Destroy(gameObject);
        }
    }

    public float GetConstructionTickNormalized()
    {
        return constructionTick * 1f / constructionTickMax;
    }
}
