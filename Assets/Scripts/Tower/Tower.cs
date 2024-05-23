using System.Collections;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] protected float _damage;
    [SerializeField] protected float _reloadTime;
    [SerializeField] protected float _impactForce;
    [SerializeField] protected string _enemyTag = "Enemy";
    [SerializeField] protected Transform _pointShellTransform;
    [SerializeField] protected Transform _rotaterTransform;
    [SerializeField] protected Transform _cannonTransform;
    [SerializeField] protected GameObject _shellObject;
    private bool _isAttacking = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(_enemyTag))
        {   
            Main(other.gameObject);
        }
    }

    private void Main(GameObject other)
    {
        Vector3 direction = other.transform.position - _pointShellTransform.position;
        RotateCannon(direction);
            
        if (!_isAttacking)
        {
            StartCoroutine(DelaySpawnShell(direction, other.gameObject));
        }
    }
    
    private void RotateCannon(Vector3 direction)
    {   
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

        Vector3 euler = targetRotation.eulerAngles;
        
        RotateOnlyCannon(euler);
        
        euler.x = 0f;
        targetRotation = Quaternion.Euler(euler);
        _rotaterTransform.transform.localRotation = Quaternion.Slerp(_rotaterTransform.transform.localRotation, targetRotation, Time.deltaTime * 3);

        void RotateOnlyCannon(Vector3 euler)
        {
            euler.z = 0f;
            euler.y = 0f;
            targetRotation = Quaternion.Euler(euler);
            Quaternion newValue = Quaternion.Slerp(_cannonTransform.localRotation, targetRotation, Time.deltaTime);

            if (newValue.x <= 0.1 && newValue.x >= -0.15)
            {
                _cannonTransform.localRotation = newValue;
            }
        }
    }

    private IEnumerator DelaySpawnShell(Vector3 direction, GameObject enemy)
    {
        _isAttacking = true;
        SpawnShell(enemy);
        yield return new WaitForSeconds(_reloadTime);
        _isAttacking = false;
    }

    private void SpawnShell(GameObject enemy)
    {
        GameObject shellObject = Instantiate(_shellObject, _pointShellTransform.position, Quaternion.identity);
        // Передача информации от Tower к Shell
        Shell shellComponent = shellObject.GetComponent<Shell>();
        shellComponent._enemyObject = enemy;
        shellComponent.Damage(_damage);
        shellComponent.ImpactForce(_impactForce);
        Audio();
        Effect();
    }

    private void Audio()
    {
        GetComponent<AudioTower>().Audio();
    }

    private void Effect()
    {
        StartCoroutine(GetComponent<TowerEffect>().StartEffect());
    }
}
