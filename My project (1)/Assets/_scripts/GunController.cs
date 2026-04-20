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
    
    gunTrigger = GetComponent<BoxCollider>();
        canShoot = true;

        //arma default lol
        if (weapon != null)
        {
            ChangeWeapon(weapon);
        }
        SetTrigger();

    }

    // Update is called once per frame
    void Update()
    {
        Fire();

        //pa recargar al presionar r lol
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());

        }
    }

    public void ChangeWeapon(Weapons newWeapon)
    {
        weapon = newWeapon;
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

            currentAmmo--;
            Debug.Log("Disparo = Ammo: " + currentAmmo);

            audioSource.PlayOneShot(weapon.sound);

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
        if (isReloading) yield break;
        if (currentAmmo == weapon.maxAmmo) yield break;
        if (currentReserve <= 0) yield break;

        isReloading = true;

        Debug.Log("INICIO RECARGA");
        Debug.Log("ANTES = Ammo: " + currentAmmo + " | Reserve: " + currentReserve);

        yield return new WaitForSeconds(weapon.reloadTime);

        int neededAmmo = weapon.maxAmmo - currentAmmo;

        if (currentReserve >= neededAmmo)
        {
            currentAmmo += neededAmmo;
            currentReserve -= neededAmmo;
        }
        else
        {
            currentAmmo += currentReserve;
            currentReserve = 0;
        }

        Debug.Log("DESPUÉS = Ammo: " + currentAmmo + " | Reserve: " + currentReserve);

        isReloading = false;
    }

    IEnumerator CanFire (float time)
    {
        canShoot = false;
        yield return new WaitForSeconds(time);
        canShoot = true;
    }

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
