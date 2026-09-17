using System;
using UnityEngine;
using UnityEngine.Serialization;

public class GameData : MonoBehaviour
{
    public static GameData instance;
    
    public int coinsPlayer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }
}
