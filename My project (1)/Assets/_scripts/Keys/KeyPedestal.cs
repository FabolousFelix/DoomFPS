using UnityEngine;

public class KeyPedestal : MonoBehaviour
{
    public int keyType; // 0 = roja, 1 = azul, 2 = morada

    public bool isActivated = false;
    public AudioSource audioSource;
    public AudioClip placeKeySound;

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
        Debug.Log("Colocando llave tipo: " + keyType);

        switch (keyType)
        {
            case 0:
                Stats.hasRedKey = false;
                if (KeyManager.instance != null)
                    KeyManager.instance.redKeyImage.SetActive(false);
                break;

            case 1:
                Stats.hasBlueKey = false;
                if (KeyManager.instance != null)
                    KeyManager.instance.blueKeyImage.SetActive(false);
                break;

            case 2:
                Stats.hasPurpleKey = false;
                if (KeyManager.instance != null)
                    KeyManager.instance.purpleKeyImage.SetActive(false);
                break;
        }

        //instanciar prefab
        if (keyVisual != null)
        {
            GameObject key = Instantiate(keyVisual, transform);
            key.transform.localPosition = new Vector3(0, 1.5f, 0);
        }
        else
        {
            Debug.LogError("keyVisual no asignado");
        }

        if (audioSource != null && placeKeySound != null)
        {
            audioSource.PlayOneShot(placeKeySound);
        }
    }
}