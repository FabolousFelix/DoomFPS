using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newGun", menuName = "Weapon/New Weapon")]

public class Weapons : ScriptableObject
{
    [Tooltip("Distancia de disparo")]
    public float range;
    public int verticalRange;
    public int horizontalRange;
    public float fireRate;
    public int damage;
    public GameObject weaponPrefab;
    public AudioClip sound;

    [Header("Ammo")]
    public int maxAmmo;      // cargador
    public int maxReserve;   // munición total extra
    public float reloadTime;

}
