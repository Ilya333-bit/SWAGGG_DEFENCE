using System.Collections;
using UnityEngine;

public class TowerEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _smokeEffect;
    [SerializeField] private float _timeSmokeExist = 1.2f;

    public IEnumerator StartEffect()
    {
        _smokeEffect.Play();
        yield return new WaitForSeconds(_timeSmokeExist);
        _smokeEffect.Stop();
    }
}