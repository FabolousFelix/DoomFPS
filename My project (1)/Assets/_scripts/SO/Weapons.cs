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
    public AudioClip sound;

}
