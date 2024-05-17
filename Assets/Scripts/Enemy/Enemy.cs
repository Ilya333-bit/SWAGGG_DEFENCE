using UnityEngine;

public class Enemy: MonoBehaviour
{
    [SerializeField] protected float _health;
    [SerializeField] protected int _coins;
    [SerializeField] protected float _damage;
    [SerializeField] protected float _speed;

    public float Coins => _coins;
    public float Damage => _damage;
    
    public float Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
        }
    }
}