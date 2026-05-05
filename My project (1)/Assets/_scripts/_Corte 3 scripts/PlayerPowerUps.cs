using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerUps : MonoBehaviour
{
    [Header("Referencias")]
    public PlayerMovement movement;
    public GunController gun;


    private float originalSpeed;
    private int originalDamage;


    
    void Start()
    {
        
        originalSpeed = movement.speed;
        originalDamage = gun.weapon.damage;
    }

    public void ActivateBoost(float speedMultiplier, float damageMultiplier, float duration)
    {
        StartCoroutine(BoostCoroutine(speedMultiplier, damageMultiplier,duration));
    }

    IEnumerator BoostCoroutine(float speedMult, float damageMult, float duration)
    {
        Debug.Log("Boost activado");
            movement.speed = movement.speed * speedMult;
        gun.weapon.damage = Mathf.RoundToInt(gun.weapon.damage * damageMult);

        yield return new WaitForSeconds(duration);

        movement.speed = originalSpeed;
        gun.weapon.damage = originalDamage;

        Debug.Log("Boost Terminado");
    }
        
}
