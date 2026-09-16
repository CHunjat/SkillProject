using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class FireballLauncher : MonoBehaviour
{
    [Header("입력 설정")]
    [Tooltip("이 스킬을 발동할 키를 선택하세요 (예: Q, W, E, Space)")]
    [SerializeField] private Key skillKey = Key.Space;

    [Header("파이어볼 능력치")]
    [SerializeField] private float spellTime = 2.0f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float range = 15f;
    [SerializeField] private float speed = 10f;
    [SerializeField] private int projectileCount = 1;
    [SerializeField] private float cooldown = 1f;
    [SerializeField] private float spreadAngle = 15f;
    [SerializeField] private float postCastDelay = 0.5f;

    [Header("설정 및 프리팹")]
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private Transform spawnPoint;

 

    private float currentCooldown = 0f;
    private Coroutine castCoroutine;

    // Action: 반환값이 없는 이벤트 (쿨타임 UI 업데이트용)
    public Action<float> OnCooldownStarted;

    // Func: 반환값이 있는 이벤트 (스킬 사용 가능 여부 체크용 - bool 반환)
    public Func<bool> CheckCanCast;

    public bool IsCasting { get; private set; } = false;

    private void Start()
    {
        // 1. Action 구독 (UI의 StartCooldown 함수를 이벤트에 연결)
        //if (cooldownUI != null)
        //{
        //    OnCooldownStarted += cooldownUI.StartCooldown;
        //}

        // 2. Func 구독 (람다식을 사용해 시전 가능 조건을 정의 및 연결)
        CheckCanCast += () =>
        {
            return currentCooldown <= 0 && !IsCasting;
        };
    }

    private void OnDestroy()
    {
        //// 메모리 누수 방지를 위해 오브젝트가 파괴될 때 이벤트 구독 해제
        //if (cooldownUI != null)
        //{
        //    OnCooldownStarted -= cooldownUI.StartCooldown;
        //}
    }

    private void Update()
    {
        if (currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
        }

        if (Keyboard.current != null && Keyboard.current[skillKey].wasPressedThisFrame)
        {
            TryCastFireball();
        }
    }

    public void TryCastFireball()
    {
        // ✅ Func Invoke: CheckCanCast에 연결된 함수가 false를 반환하면 시전 취소
        if (CheckCanCast != null && CheckCanCast.Invoke() == false)
        {
            return;
        }

        if (fireballPrefab == null || spawnPoint == null)
        {
            Debug.LogWarning("2D 프리팹 또는 SpawnPoint가 누락되었습니다.");
            return;
        }

        castCoroutine = StartCoroutine(FireEnumerator());
    }

    public void CancelCasting()
    {
        if (IsCasting && castCoroutine != null)
        {
            StopCoroutine(castCoroutine);
            IsCasting = false;
            Debug.LogWarning("❌ [캔슬] 외부 요인으로 인해 2D 시전이 취소되었습니다.");
        }
    }

    private IEnumerator FireEnumerator()
    {
        IsCasting = true;

        Debug.Log("🔥 [파이어볼 2D] 시전을 시작합니다.");
        yield return new WaitForSeconds(spellTime);

        Debug.Log("🚀 [발사 2D] 시전 완료! 파이어볼을 발사합니다.");
        SpawnProjectiles();

        // 런처 내부의 쿨타임 데이터 갱신
        currentCooldown = cooldown;

        // ✅ Action Invoke: OnCooldownStarted에 연결된 UI 함수들을 일제히 실행
        OnCooldownStarted?.Invoke(cooldown);

        yield return new WaitForSeconds(postCastDelay);
        IsCasting = false;
        Debug.Log("✅ [완료 2D] 후딜레이 종료.");
    }

    private void SpawnProjectiles()
    {
        int startIdx = -(projectileCount / 2);

        for (int i = 0; i < projectileCount; i++)
        {
            float angleOffset = (startIdx + i) * spreadAngle;
            if (projectileCount % 2 == 0) angleOffset += spreadAngle / 2f;

            Quaternion spawnRotation = spawnPoint.rotation * Quaternion.Euler(0, 0, angleOffset);
            GameObject projGo = Instantiate(fireballPrefab, spawnPoint.position, spawnRotation);

            if (projGo.TryGetComponent<FireballProjectile>(out var projectile))
            {
                projectile.Initialize(damage, range, speed, gameObject);
            }
            else
            {
                Debug.LogError("프리팹에 FireballProjectile 컴포넌트가 없습니다!");
                Destroy(projGo);
            }
        }
    }
}