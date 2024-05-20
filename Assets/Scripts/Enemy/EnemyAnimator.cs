using System.Collections;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private Animator _animator;
    private string[] _variables = { "isMove", "isAttack", "isGotUp" };

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void ChangeAnimation()
    {
        _animator.SetBool(_variables[0], false);
        _animator.SetBool(_variables[1], false);
        _animator.SetBool(_variables[2], true);
        StartCoroutine(DelayGotUp());
    }

    private IEnumerator DelayGotUp()
    {
        yield return new WaitForSeconds(1.06f);
        _animator.SetBool(_variables[2], false);
        _animator.SetBool(_variables[1], true);
    }
}