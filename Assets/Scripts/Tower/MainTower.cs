using UnityEngine;

public class MainTower : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private string _enemyShellTag;
    [SerializeField] private GameObject _updateHudObject;

    private void Start()
    {
        _updateHudObject.GetComponent<UpdateHud>().MaxHealth = _health;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_enemyShellTag))
        {
            _health -= other.GetComponent<EnemyShell>().Damage;
            other.GetComponent<EnemyShell>().DestroyObject();
            _updateHudObject.GetComponent<UpdateHud>().TakeDamage(other.GetComponent<EnemyShell>().Damage);
            if (_health <= 0)
            {
                DestroyMainTower();
            }
        }
    }

    private void DestroyMainTower()
    {
        
    }
}