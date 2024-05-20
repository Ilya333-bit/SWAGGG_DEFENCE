using System;
using UnityEngine;

public class EnemyShell : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private float _impactForce;
    private float _damage;
    private Vector3 _vector3MainTower;
    
    public float ImpactForce { set => _impactForce = value; }
    public Vector3 Vector3MainTower { set => _vector3MainTower = value; }

    public float Damage
    {
        get => _damage;
        set => _damage = value;
    }

    private void Update()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Vector3 direction = (_vector3MainTower - transform.position).normalized;
        _rigidbody.velocity = direction * _impactForce;
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}