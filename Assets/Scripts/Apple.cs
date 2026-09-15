using System;
using UnityEngine;

public class Apple : MonoBehaviour
{
    [SerializeField] private UpdateUI updateUI;

    private int _score;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _score++;
            Destroy(gameObject);
        }
    }
}
