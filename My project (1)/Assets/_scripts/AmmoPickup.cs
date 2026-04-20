using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int ammoAmount;

    private void OnTriggerEnter(Collider other)
    {
        //detecta al player
        if (other.CompareTag("Player"))
        {
            //añade la municion despus de recogerla
            GunController.instance.AddAmmo(ammoAmount);
            Destroy(gameObject);
        }
    }
}