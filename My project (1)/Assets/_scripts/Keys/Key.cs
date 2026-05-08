using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Key : MonoBehaviour
{
    public int keyType;

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si quien tocó la llave es el jugador
        if (other.CompareTag("Player"))
        {
            // Según el tipo de llave, se ejecuta una acción
            switch (keyType)
            {
                case 0:
                    // Activa icono de llave
                    KeyManager.instance.redKeyImage.SetActive(true);
                    // Guarda que el jugador tiene la llave 
                    Stats.hasRedKey = true;
                    break;
                case 1:
                    // Activa icono de llave
                    KeyManager.instance.blueKeyImage.SetActive(true);
                    // Guarda que el jugador tiene la llave 
                    Stats.hasBlueKey = true;
                    break;
                case 2:
                    // Activa icono de llave
                    KeyManager.instance.purpleKeyImage.SetActive(true);
                    // Guarda que el jugador tiene la llave 
                    Stats.hasPurpleKey = true;
                    break;

                // Caso por defecto (si el valor no es válido)
                default:
                    break;
            }
            // Destruye la llave del escenario después de recogerla
            Destroy(gameObject);
        }
    }
}
