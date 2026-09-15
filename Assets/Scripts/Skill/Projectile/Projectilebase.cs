using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileBase : MonoBehaviour
{
    protected float damage;
    protected float range;
    protected float speed;
    protected Vector2 startPosition;
    protected GameObject owner;

    protected Rigidbody2D rb;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        // 2D 투사체가 중력의 영향을 받아 가라앉지 않도록 설정[cite: 1]
        rb.gravityScale = 0f;
    }

    public virtual void Initialize(float damage, float range, float speed, GameObject owner)
    {
        this.damage = damage;
        this.range = range;
        this.speed = speed;
        this.owner = owner;
        this.startPosition = transform.position;

        // 충돌 감지를 위해 2D 콜라이더를 트리거로 설정[cite: 1]
        if (TryGetComponent<Collider2D>(out var col))
        {
            col.isTrigger = true;
        }

        // 투사체의 현재 오른쪽 방향(transform.right)을 기준으로 발사[cite: 1]
        rb.linearVelocity = transform.right * speed;
    }

    protected virtual void Update()
    {
        // 2D 사거리 초과 체크[cite: 1]
        if (Vector2.Distance(startPosition, transform.position) >= range)
        {
            Explode();
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        // 발사한 주인이 자기 자신과 충돌하는 것 방지[cite: 1]
        if (owner != null && (other.gameObject == owner || other.transform.IsChildOf(owner.transform)))
        {
            return;
        }

        // 피격 대상이 IDamageable2D 컴포넌트를 가지고 있는지 확인[cite: 1]
        if (other.TryGetComponent<IDamageable2D>(out var target))
        {
            OnHitTarget(target, other.gameObject);
            Explode();
            return;
        }

        // 벽이나 장애물 태그/레이어 충돌 체크[cite: 1]
        if (other.CompareTag("Wall") || other.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            Explode();
        }
    }

    // 자식 클래스에서 데미지 이외의 추가 효과(예: 화상, 빙결)를 넣기 쉽게 분리한 메서드
    protected virtual void OnHitTarget(IDamageable2D target, GameObject hitObject)
    {
        target.TakeDamage(damage, owner);
    }

    protected virtual void Explode()
    {
        Destroy(gameObject);
    }
}