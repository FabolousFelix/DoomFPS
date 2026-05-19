using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDAmmoDisplay : MonoBehaviour
{
    public TextMeshProUGUI ammoText; // Asignar desde la Hierarchy (component Text del HUD)

    void Update()
    {
        if (ammoText == null) return;

        if (GunController.instance == null)
        {
            ammoText.text = "Ammo: -- / --";
            return;
        }

        ammoText.text = string.Format("Ammo: {0} / {1}", GunController.instance.CurrentAmmo, GunController.instance.CurrentReserve);
    }
}