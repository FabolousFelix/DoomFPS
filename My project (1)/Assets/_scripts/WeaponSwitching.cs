using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{

    public Transform weaponHolder; // donde se instancia el arma (mano/cámara)

    private GameObject currentWeapon;
    private Weapons currentWeaponData;

    public void EquipWeapon(Weapons newWeapon)
    {
        // destruir arma actual
        if (currentWeapon != null)
        {
            Destroy(currentWeapon);
        }

        // instanciar nueva arma
        currentWeapon = Instantiate(newWeapon.weaponPrefab, weaponHolder);
        currentWeaponData = newWeapon;
    }
}
