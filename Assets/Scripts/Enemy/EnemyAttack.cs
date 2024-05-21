using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private string _towerTag = "MainTower";
    [SerializeField] private Transform _pointForShell;
    [SerializeField] private GameObject _shellObject;
    [SerializeField] private float _reloadTime = 1.4f;
    [SerializeField] private float _impactForce;
    private Vector3 _vector3MainTower;
    private bool _isAttacking = false;
    private bool _isReload = false;
    
    public Vector3 Vector3MainTower { set => _vector3MainTower = value; }
    
    public bool IsAttacking
    {
        set
        {
            _isAttacking = value;
            GetComponent<EnemyAnimator>().ChangeAnimation();
        }
    }

    private void Update()
    {
        if (_isAttacking)
        {   
            if (!_isReload && GetComponent<Animator>().GetBool("isAttack"))
            {
                StartCoroutine(CreateShell());
            }
        }
    }

    private IEnumerator CreateShell()
    {
        GameObject spawnShellObject = Instantiate(_shellObject, _pointForShell.position, Quaternion.identity);
        spawnShellObject.GetComponent<EnemyShell>().ImpactForce = _impactForce;
        spawnShellObject.GetComponent<EnemyShell>().Vector3MainTower = _vector3MainTower;
        spawnShellObject.GetComponent<EnemyShell>().Damage = GetComponent<EnemyMovement>().Damage;
        GetComponent<AudioEnemy>().Audio();
        _isReload = true;
        yield return new WaitForSeconds(_reloadTime);
        _isReload = false;
    }
}
