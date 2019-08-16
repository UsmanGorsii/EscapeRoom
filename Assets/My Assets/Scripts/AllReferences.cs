using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllReferences : MonoBehaviour
{
    public static AllReferences Instance;

    private void Awake()
    {
        Instance = this;
    }

    public GameObject teleporter;

}
