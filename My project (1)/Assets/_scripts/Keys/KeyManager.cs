
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    // Instancia única (patrón Singleton)
    public static KeyManager instance;

    // Referencias a los iconos de UI de cada llave
    public GameObject redKeyImage;
    public GameObject blueKeyImage;
    public GameObject purpleKeyImage;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    void Start()
    {
        // Reinicia el estado de todas las llaves (jugador empieza sin ninguna)
        Stats.hasRedKey = false;
        Stats.hasBlueKey = false;
        Stats.hasPurpleKey = false;

        // Oculta todos los iconos de llaves en la UI
        redKeyImage.SetActive(false);
        blueKeyImage.SetActive(false);
        purpleKeyImage.SetActive(false);
    }
}
