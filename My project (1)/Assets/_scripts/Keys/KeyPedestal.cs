using UnityEngine;

public class KeyPedestal : MonoBehaviour
{
    public int keyType; // 0 = roja, 1 = azul, 2 = morada

    public bool isActivated = false;

    [Header("Visual")]
    public GameObject keyVisual; //llave que aparece encima)

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || isActivated) return;

        if (HasCorrectKey())
        {
            PlaceKey();
            isActivated = true;

            Debug.Log("Llave colocada en pedestal");
        }
        else
        {
            Debug.Log("No tienes la llave correcta");
        }
    }

    bool HasCorrectKey()
    {
        switch (keyType)
        {
            case 0: return Stats.hasRedKey;
            case 1: return Stats.hasBlueKey;
            case 2: return Stats.hasPurpleKey;
        }
        return false;
    }

    void PlaceKey()
    {
        switch (keyType)
        {
            case 0:
                Stats.hasRedKey = false;
                KeyManager.instance.redKeyImage.SetActive(false);
                break;
            case 1:
                Stats.hasBlueKey = false;
                KeyManager.instance.blueKeyImage.SetActive(false);
                break;
            case 2:
                Stats.hasPurpleKey = false;
                KeyManager.instance.purpleKeyImage.SetActive(false);
                break;
        }

        // mostrar llave en pedestal
        if (keyVisual != null)
            keyVisual.SetActive(true);
    }
}