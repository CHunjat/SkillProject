using System.Collections.Generic;
using UnityEngine;

public class DataStructureTest : MonoBehaviour
{

    int buffDamage = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log(CardManager.Instance.GetCardData("바보").cardDescriptions.value);

        CardManager.Instance.GetCardData("바보").cardDescriptions.value += buffDamage;
    }

}