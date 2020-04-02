﻿using System.Collections;
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
    private Vector3 originalPosition;
    [SerializeField] private Vector3 liftOffset = new Vector3(-0.15f, 0.03f, -0.2f);
    private Vector3 liftPosition;

    private bool _isZoomCompleted = false;


    private void Awake()
    {

        _muzzleFlash = GetComponentInChildren<ParticleSystem>();
        originalPosition = transform.localPosition;
        liftPosition = originalPosition + liftOffset;

    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
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
