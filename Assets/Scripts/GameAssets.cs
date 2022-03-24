using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    public static GameAssets instance = null;
    public GameObject stone;

    void Awake()
    {
        instance = this;
    }
}
