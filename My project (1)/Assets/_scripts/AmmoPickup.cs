using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int ammoAmount;

    private void OnTriggerEnter(Collider other)
    {
        // detecta al player
        if (other.CompareTag("Player"))
        {
            // intenta añadir munición
            bool pickedUp =
                GunController.instance.AddAmmo(ammoAmount);

            // solo destruir si sí recogió munición
            if (pickedUp)
            {
                Destroy(gameObject);
            }
        }
    }
}