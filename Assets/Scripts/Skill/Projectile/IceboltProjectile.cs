using UnityEngine;

public class IceboltProjectile : ProjectileBase
{
    protected override void OnHitTarget(IDamageable2D target, GameObject hitObject)
    {
        // 1. 부모 클래스의 기본 데미지 처리 로직 실행
        base.OnHitTarget(target, hitObject);

        // 2. 얼음 화살 특유의 상태 이상(빙결/둔화) 효과 추가 로직 구현
        Debug.Log("❄️ 대상이 빙결되거나 이동 속도가 느려졌습니다!");

        // (실제 게임에서는 target에 상태 이상을 부여하는 메서드를 호출하도록 구현하시면 됩니다.)
    }

    protected override void Explode()
    {
        // 아이스볼트 전용 파괴 연출 로그
        Debug.Log("2D 아이스볼트 투사체가 부서졌습니다.");

        base.Explode();
    }
}