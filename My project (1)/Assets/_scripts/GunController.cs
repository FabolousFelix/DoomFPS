using UnityEngine;
using System.Collections;

public class GunController : MonoBehaviour
{
    public static GunController instance;
    private BoxCollider gunTrigger;

    public Weapons weapon;

    [Tooltip("capas en las que actua el rayo")]
    public LayerMask rayscastLayer;

    private bool canShoot;

    public AudioSource audioSource;
    public Transform weaponHolder;
    private GameObject currentWeaponModel;

    [Header("Enemy Test Materials")]
    public Material initialMaterial;
    public Material detectedMaterial;

    [Header("Ammo Runtime")]
    private int currentAmmo;
    private int currentReserve;
    private bool isReloading;

    void Awake()
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
        // Obtiene el BoxCollider del arma
        gunTrigger = GetComponent<BoxCollider>();
        //permite disparars
        canShoot = true;

        // Asigna arma inicial
        if (weapon != null)
        {
            ChangeWeapon(weapon);
        }
        // Ajusta el trigger según el arma
        SetTrigger();

    }
    void Update()
    {
        //Disparo
        Fire();

        //pa recargar al presionar r lol
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());

        }
    }
    // Cambia el arma
    public void ChangeWeapon(Weapons newWeapon)
    {
        weapon = newWeapon;
        // Inicializa munición
        currentAmmo = weapon.maxAmmo;
        currentReserve = weapon.maxReserve;
        // destruir modelo actual
        if (currentWeaponModel != null)
        {
            Destroy(currentWeaponModel);
        }

        // instanciar nuevo modelo
        if (weapon.weaponPrefab != null)
        {
            currentWeaponModel = Instantiate(weapon.weaponPrefab, weaponHolder);
        }

        SetTrigger();
    }
    // Ajusta el tamaño del trigger según el arma
    public void SetTrigger()
    {
        gunTrigger.size = new Vector3(weapon.horizontalRange, weapon.verticalRange, weapon.range);
        gunTrigger.center = new Vector3(0, (0.5f * weapon.verticalRange - 1f), weapon.range * 0.5f);
    }

    public void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canShoot && !isReloading)
        {
            //primero verificar enemigos pq estaba disparando al aire lol
            if (EnemyManager.instance.enemiesInRange.Count == 0)
            {
                Debug.Log("No hay enemigos, no dispara");
                return;
            }

            //luego verificar munición
            if (currentAmmo <= 0)
            {
                Debug.Log("Sin munición");
                return;
            }
            // Consume bala
            currentAmmo--;
            Debug.Log("Disparo = Ammo: " + currentAmmo);
            // Sonido
            audioSource.PlayOneShot(weapon.sound);

            // Dispara a todos los enemigos en rango
            foreach (Enemy enemy in EnemyManager.instance.enemiesInRange)
            {
                var dir = (enemy.transform.position - transform.position).normalized;
                RaycastHit hit;

                if (Physics.Raycast(transform.position, dir, out hit, weapon.range * 1.5f, rayscastLayer))
                {
                    if (hit.transform == enemy.transform)
                    {
                        Quaternion rot = Quaternion.LookRotation(-hit.normal);
                        enemy.Damage(weapon.damage, rot);
                    }

                    Debug.DrawRay(transform.position, dir * weapon.range, Color.green, 1f);
                }
            }
            // Aplica cooldown de disparo
            StartCoroutine(CanFire(weapon.fireRate));
        }
    }

    //añadir municion 
    public void AddAmmo(int amount)
    {

        Debug.Log("Munición actual: " + currentReserve);
        currentReserve += amount;
    }

    IEnumerator Reload()
    {
        // Si ya está recargando, no hace nada
        if (isReloading) yield break;
        // Si el cargador ya está lleno, no recarga
        if (currentAmmo == weapon.maxAmmo) yield break;
        // Si no hay munición en reserva, no recarga
        if (currentReserve <= 0) yield break;
        // Activa estado de recarga
        isReloading = true;

        Debug.Log("INICIO RECARGA");
        Debug.Log("ANTES = Ammo: " + currentAmmo + " | Reserve: " + currentReserve);

        // Espera el tiempo de recarga del arma
        yield return new WaitForSeconds(weapon.reloadTime);

        int neededAmmo = weapon.maxAmmo - currentAmmo;
        // Si hay suficiente reserva para llenar el cargador
        if (currentReserve >= neededAmmo)
        {
            currentAmmo += neededAmmo;
            currentReserve -= neededAmmo;
        }
        else
        {
            // Si no hay suficiente, usa toda la reserva
            currentAmmo += currentReserve;
            currentReserve = 0;
        }

        Debug.Log("DESPUÉS = Ammo: " + currentAmmo + " | Reserve: " + currentReserve);

        isReloading = false;
    }
    // Cooldown de disparo
    IEnumerator CanFire (float time)
    {
        canShoot = false;
        yield return new WaitForSeconds(time);
        canShoot = true;
    }

    // Detecta enemigos en rango
    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();

        if (enemy)
        {
            enemy.GetComponent<Renderer>().material = detectedMaterial;
            EnemyManager.instance.AddEnemy(enemy);
        }
    }

   private void OnTriggerExit(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();

        if (enemy)
        {
            enemy.GetComponent<Renderer>().material = initialMaterial;
            EnemyManager.instance.RemoveEnemy(enemy);
        }
    }
}
