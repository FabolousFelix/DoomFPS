using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Permite crear este asset desde el menú de Unity
[CreateAssetMenu(fileName = "newGun", menuName = "Weapon/New Weapon")]

// Clase ScriptableObject que define los datos de un arma
public class Weapons : ScriptableObject
{
    [Tooltip("Distancia de disparo")]
    //rango
    public float range;
    // Altura del área de detección
    public int verticalRange;
    // Anchura del área de detección
    public int horizontalRange;
    // Tiempo entre disparos (cooldown)
    public float fireRate;
    // Daño que hace cada disparo
    public int damage;
    // Prefab visual del arma (modelo en mano)
    public GameObject weaponPrefab;
    // Sonido del disparo
    public AudioClip sound;

    [Header("Ammo")]
    public int maxAmmo;      // cargador
    public int maxReserve;   // munición total extra
    public float reloadTime; // Tiempo que tarda en recargar

}
