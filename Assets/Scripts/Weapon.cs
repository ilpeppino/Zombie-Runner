using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    [SerializeField] private Camera _fpsCamera;
    [SerializeField] private float _shootingRange = 100f;
    [SerializeField] private int _damage = 30;
    [SerializeField] public GameObject _hitExplosionFX;

    private RaycastHit hit;
    private ParticleSystem _muzzleFlash;

    private void Awake()
    {
        _muzzleFlash = GetComponentInChildren<ParticleSystem>();

    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }


    private void Shoot()
    {
        PlayMuzzleFlash();
        ProcessRaycast();

    }

    private void PlayMuzzleFlash()
    {
        _muzzleFlash.Play();
    }

    private void ProcessRaycast()
    {

        if (Physics.Raycast(_fpsCamera.transform.position, _fpsCamera.transform.forward, out hit, _shootingRange))
        {
            PlayImpactExplosion(hit);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null) { return; }
            target.TakeDamage(_damage);
        }
        else { return; }
    }

    private void PlayImpactExplosion(RaycastHit hit)
    {
        GameObject g = Instantiate(_hitExplosionFX, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(g, 1f);
    }


}
