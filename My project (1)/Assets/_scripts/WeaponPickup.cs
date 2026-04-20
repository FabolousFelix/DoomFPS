using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public Weapons weaponData;

    private void OnTriggerEnter(Collider other)
    {
        // busca al player con tag
        if (other.CompareTag("Player"))
        {
            //instancia el arma al player
            GunController.instance.ChangeWeapon(weaponData);
            Destroy(gameObject);
        }
    }
}