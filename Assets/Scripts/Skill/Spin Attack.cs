using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpinAttackController : MonoBehaviour
{
    [Header("Skill Stats")]
    [SerializeField] private float attackPower = 25f;        // 공격력
    [SerializeField] private float attackRange = 3.0f;       // 사거리 (반경)
    [SerializeField] private float swingSpeed = 1.0f;        // 휘두르는 속도
    [SerializeField] private float cooldownTime = 1.5f;      // 쿨타임

    [Header("Input Settings (인스펙터에서 직접 매핑)")]
    [SerializeField] private InputAction fireAction;         // 인스펙터에서 바인딩 추가 (예: Space)

    [Header("Visuals & Animation")]
    [SerializeField] private Animator animator;
    [SerializeField] private string attackAnimTrigger = "SpinAttack";
    [SerializeField] private GameObject spinVfxPrefab;

    [Header("Layers")]
    [SerializeField] private LayerMask enemyLayer;

    private bool isCoolingDown = false; // 쿨타임 체크용
    private bool isAttacking = false;   // 중복 시전 방지용

    private void OnEnable()
    {
        fireAction.Enable();
        fireAction.performed += OnAttackPerformed;
    }

    private void OnDisable()
    {
        fireAction.performed -= OnAttackPerformed;
        fireAction.Disable();
    }

    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        // 쿨타임 중이거나 이미 공격 중이면 무시
        if (isCoolingDown || isAttacking) return;

        StartCoroutine(SpinAttackRoutine());
    }

    private IEnumerator SpinAttackRoutine()
    {
        isAttacking = true;

        // [0단계] 애니메이션 및 VFX 시전
        if (animator != null)
        {
            animator.SetTrigger(attackAnimTrigger);
        }

        if (spinVfxPrefab != null)
        {
            GameObject vfxInstance = Instantiate(spinVfxPrefab, transform.position, Quaternion.identity, transform);
            Destroy(vfxInstance, 2.0f);
        }

        // [1단계] 휘두르는 동작 시작 (선딜레이)
        yield return new WaitForSeconds(0.1f / swingSpeed);

        // [2단계] 범위 탐색 및 데미지 적용
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log($"적 타격! 데미지: {attackPower}");
        }

        // [3단계] 공격 판정 종료 후 후딜레이 대기
        yield return new WaitForSeconds(0.3f / swingSpeed);
        isAttacking = false;

        // [4단계] 코루틴을 이용한 쿨타임 제어 시작
        isCoolingDown = true;
        yield return new WaitForSeconds(cooldownTime);
        isCoolingDown = false;
    }

    // 에디터에서 사거리(반경) 시각화
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}