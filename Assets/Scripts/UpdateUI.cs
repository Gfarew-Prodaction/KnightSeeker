using System;
using TMPro;
using UnityEngine;

public class UpdateUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textCountApple;

    private void Start()
    {
        UpdateTextApple(GameData.instance.coinsPlayer);
    }

    public void UpdateTextApple(int count)
    {
        _textCountApple.text = count.ToString();
    }

    public void UpdateCountApple()
    {
        GameData.instance.coinsPlayer++;
        UpdateTextApple(GameData.instance.coinsPlayer);
    }
}
