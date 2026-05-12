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

    [Header("Invisibilidad")]
    public bool hasInvisibility = false;
    public float invisDuration;

    public Renderer playerRenderer;

    public bool isInvisible;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ActivateInvisibility();
        }
    }

    void Start()
    {
        
        originalSpeed = movement.speed;
        originalDamage = gun.weapon.damage;
    }

    public void ActivateBoost(float speedMultiplier, float damageMultiplier, float duration)
    {
        StartCoroutine(BoostCoroutine(speedMultiplier, damageMultiplier,duration));
    }

    public void ActivateInvisibility()
    {
        if (hasInvisibility)
        {
            StartCoroutine(InvisibilityCoroutine());
            hasInvisibility = false;
        }
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

    IEnumerator InvisibilityCoroutine()
    {
        Debug.Log("INVISIBLE");

        isInvisible = true;

        //ocultar visualmente
        if (playerRenderer != null)
            playerRenderer.enabled = false;

        yield return new WaitForSeconds(invisDuration);

        isInvisible = false;

        // volver visible
        if (playerRenderer != null)
            playerRenderer.enabled = true;

        Debug.Log("VISIBLE OTRA VEZ");
    }

}
