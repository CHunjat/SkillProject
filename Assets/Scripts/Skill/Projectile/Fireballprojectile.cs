using UnityEngine;

public class FireballProjectile : ProjectileBase
{
    protected override void Explode()
    {
        Debug.Log("💥 2D 파이어볼 투사체가 소멸했습니다."); // 기존 고유 로그 유지[cite: 1]

        // base.Explode()를 호출하여 Destroy(gameObject)를 실행합니다.
        base.Explode();
    }
}