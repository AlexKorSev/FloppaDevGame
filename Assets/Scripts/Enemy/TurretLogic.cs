using UnityEngine;

public class TurretLogic : MonoBehaviour
{
    private Transform player;
    private SpriteRenderer spriteRenderer;

    [Header("Настройки стрельбы")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float shootingRange = 10f;
    public float fireRate = 1.5f;
    private float nextFireTime;

    [Header("Настройки звука")]
    private AudioSource audioSource;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        audioSource = GetComponent<AudioSource>();

        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    void Update()
    {
        if (player != null)
        {
            Vector3 direction = player.position - transform.position;
            float distance = direction.magnitude;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle + 180f);

            if (player.position.x > transform.position.x)
            {
                spriteRenderer.flipY = true;
            }
            else
            {
                spriteRenderer.flipY = false;
            }

            if (distance <= shootingRange && Time.time >= nextFireTime)
            {
                Shoot(direction.normalized);
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    void Shoot(Vector2 shootDirection)
    {
        Transform spawnPoint = firePoint != null ? firePoint : transform;
        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);

        BulletProjectile bulletScript = bullet.GetComponent<BulletProjectile>();
        if (bulletScript != null)
        {
            bulletScript.SetDirection(shootDirection);
        }

        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.PlayOneShot(audioSource.clip);
        }
    }
}