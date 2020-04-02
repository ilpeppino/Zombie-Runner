using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Effects;

public class Weapon : MonoBehaviour
{

    [SerializeField] private Camera _fpsCamera;
    [SerializeField] private float _shootingRange = 100f;
    [SerializeField] private int _damage = 30;
    [SerializeField] public GameObject _explosionFX;
    [SerializeField] public GameObject _deathExplosionFX;
    [SerializeField] public GameObject _hitExplosionFX;
    [SerializeField] public GameObject reticle;

    private RaycastHit hit;
    private ParticleSystem _muzzleFlash;
    private Vector3 originalPosition;
    [SerializeField] private Vector3 liftOffset = new Vector3(-0.15f, 0.03f, -0.2f);
    private Vector3 liftPosition;
    private ParticleSystemMultiplier psm;
    private bool _isZoomCompleted = false;


    private void Awake()
    {

        _muzzleFlash = GetComponentInChildren<ParticleSystem>();
        psm = _deathExplosionFX.GetComponent<ParticleSystemMultiplier>();
        originalPosition = transform.localPosition;
        liftPosition = originalPosition + liftOffset;
        reticle.SetActive(false);

    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if(_isZoomCompleted)
                
            {
                Shoot();
            }           
        }

        if (Input.GetButton("Fire2"))
        {
            if (!_isZoomCompleted)
            {
                LiftWeapon();
            }
            
        } else
        {
            _isZoomCompleted = false;
            reticle.SetActive(false);
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalPosition, 0.3f);
        }
    }


    private void Shoot()
    {
        PlayMuzzleFlash();
        ProcessRaycast();

    }

    private void LiftWeapon()
    {
        reticle.SetActive(true);

        //transform.localPosition += liftOffset;
        if (transform.localPosition != liftPosition)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, liftPosition, 0.3f);
        }
        else
        {
            _isZoomCompleted = true;
            
        }
        
    }

    private void PlayMuzzleFlash()
    {
        _muzzleFlash.Play();
    }

    private void ProcessRaycast()
    {

        if (Physics.Raycast(_fpsCamera.transform.position, _fpsCamera.transform.forward, out hit, _shootingRange))
        {
            
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null) { return; }
            target.TakeDamage(_damage);
            if (target.isDead)
            {
                psm.ChangeEffectMultiplier(1f);
                PlayImpactExplosion(hit, _deathExplosionFX);
            } else
            {
                psm.ChangeEffectMultiplier(0.05f);
                PlayImpactExplosion(hit, _deathExplosionFX);
            }
        }
        else { return; }
    }

    private void PlayImpactExplosion(RaycastHit hit, GameObject explosion)
    {
        GameObject g = Instantiate(explosion, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(g, 1f);
    }




}
