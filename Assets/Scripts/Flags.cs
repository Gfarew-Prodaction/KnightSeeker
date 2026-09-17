using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Flags : MonoBehaviour
{
    [SerializeField] private SceneController sceneController;
    [SerializeField] private int sceneLoadIndex;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            sceneController.SceneLoad(sceneLoadIndex);
        }
    }
}