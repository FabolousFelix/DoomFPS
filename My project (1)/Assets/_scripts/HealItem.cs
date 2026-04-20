using UnityEngine;

public class HealItem : MonoBehaviour
{
    public float healAmount;

    private void OnTriggerEnter(Collider other)
    {
        //detecta al player
        if (other.CompareTag("Player"))
        {
            //accede a la salud del player
            PlayerHealth player = other.GetComponent<PlayerHealth>();

            if (player != null)
            {
                //cura al playerxd
                player.Heal((int)healAmount);
            }

            Destroy(gameObject);
        }
    }
}

