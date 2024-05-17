using TMPro;
using UnityEngine;

public class UpdateHud : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textCoins;
    
    private void OnEnable()
    {
        Economy.OnCoinsChanged += UpdateTextCoins;
    }

    private void OnDisable()
    {
        Economy.OnCoinsChanged -= UpdateTextCoins;
    }

    private void UpdateTextCoins(int value)
    {
        _textCoins.text = $"X {value}";
    }
}