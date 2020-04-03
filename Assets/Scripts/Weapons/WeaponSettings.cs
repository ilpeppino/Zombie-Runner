using UnityEngine;

[CreateAssetMenu(fileName = "Carbine", menuName = "Weapons/Carbine")]

public class WeaponSettings : ScriptableObject
{
    [SerializeField] public int damage;
    [SerializeField] public int shootingRange;
    [SerializeField] public GameObject fxHitExplosion;
    [SerializeField] public Vector3 position;
}

