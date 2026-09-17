using System;
using UnityEngine;

public class Apple : MonoBehaviour
{
    [SerializeField] private UpdateUI _updateUI;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _updateUI.UpdateCountApple();
            Destroy(gameObject);
        }
    }
}
