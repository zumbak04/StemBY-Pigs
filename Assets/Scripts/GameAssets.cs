using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    public static GameAssets instance = null;
    public GameObject stone;
    public Player player;
    public Enemy dog;

    void Awake()
    {
        instance = this;
    }
}
