using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    internal string currentGameLocation;

    private void Awake()
    {
        instance = this;
    }

}
