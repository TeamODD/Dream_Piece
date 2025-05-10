using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float floatAmplitude = 1f;
    public float floatFrequency = 2f;

    private Vector3 initialEnemyPos;
    private Vector3 initialPlayerPos;

    private float shootTimer;
    public float shootInterval = 5f;
    public float shootPower = 10f;
    public GameObject NMPrefab;

    void Start()
    {
        initialEnemyPos = transform.position;
        initialPlayerPos = player.position;
        shootTimer = shootInterval;
    }

    void Update()
    {
        if (player == null) return;
        float deltaX = player.position.x - initialPlayerPos.x;
        Vector3 pos = transform.position;
        pos.x = initialEnemyPos.x + deltaX;

        pos.y = initialEnemyPos.y + Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;

        transform.position = pos;

        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0f)
        {
            ShootAtPlayer();
            shootTimer = shootInterval;
        }
    }

    void ShootAtPlayer()
    {
        Vector3 dir = (player.position - transform.position).normalized;
        GameObject bullet = Instantiate(NMPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        Vector2 playerVelocity = playerRb.linearVelocity;
        Vector2 shootDir = (player.position - transform.position).normalized;

        // 총알 속도 = 기본 방향 * 속도 + 플레이어 이동 방향 * 일부 보정
        Vector2 bulletVelocity = shootDir * shootPower + playerVelocity * 0.5f;
        rb.linearVelocity = bulletVelocity;
        bullet.GetComponent<ShootStop>().StopAfterSeconds(3f);
    }
}
