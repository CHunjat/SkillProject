using UnityEngine;

public class FireballProjectile : MonoBehaviour
{
    private float damage;
    private float range;
    private float speed;
    private GameObject caster; // 시전자 (플레이어)

    private Vector2 startPosition;

    // FireballLauncher에서 발사 시 능력치를 넘겨주는 함수
    public void Initialize(float damage, float range, float speed, GameObject caster)
    {
        this.damage = damage;
        this.range = range;
        this.speed = speed;
        this.caster = caster;

        // 발사된 시작 위치 저장
        startPosition = transform.position;
    }

    private void Update()
    {
        // 바라보는 방향(오른쪽 기준)으로 이동
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        // 이동한 거리가 사거리(range)를 넘어가면 투사체 파괴
        if (Vector2.Distance(startPosition, transform.position) >= range)
        {
            Destroy(gameObject);
        }
    }

    // 적과 부딪혔을 때의 처리 (2D 물리 충돌)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 시전자(플레이어)와 부딪힌 경우는 무시
        if (collision.gameObject == caster) return;

        Debug.Log($"{collision.name}에게 {damage}의 데미지를 입혔습니다!");

        // TODO: 적의 체력을 깎는 코드 추가

        // 타격 후 투사체 파괴
        Destroy(gameObject);
    }
}