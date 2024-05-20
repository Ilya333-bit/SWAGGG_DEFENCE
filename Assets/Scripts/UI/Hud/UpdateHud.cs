using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UpdateHud : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textCoins;
    [SerializeField] private Image healthBarImage;
    [SerializeField] private TextMeshProUGUI _textBuild;
    private float _maxHealth;
    private float _currentHealth;
    
    public float MaxHealth { set => _maxHealth = value; }
    
    private void OnEnable()
    {
        Economy.OnCoinsChanged += UpdateTextCoins;
        _currentHealth = 1;
    }

    private void OnDisable()
    {
        Economy.OnCoinsChanged -= UpdateTextCoins;
    }

    private void UpdateTextCoins(int value)
    {
        _textCoins.text = $"X {value}";
    }
    
    private void UpdateHealthBar()
    {
        healthBarImage.fillAmount = _currentHealth;
    }
    
    public void TakeDamage(float damage)
    {
        _currentHealth -= damage / _maxHealth;
        if (_currentHealth < 0) _currentHealth = 0;
        UpdateHealthBar();
    }

    public IEnumerator ActiveTextBuild(string text)
    {
        _textBuild.text = text;
        _textBuild.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        _textBuild.gameObject.SetActive(false);
    }
}