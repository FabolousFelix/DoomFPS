
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public static KeyManager instance;

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
        Stats.hasRedKey = false;
        Stats.hasBlueKey = false;
        Stats.hasPurpleKey = false;

        redKeyImage.SetActive(false);
        blueKeyImage.SetActive(false);
        purpleKeyImage.SetActive(false);
    }
}
