using UnityEngine;
using System.Collections; // 1. 코루틴 사용을 위한 네임스페이스 추가
using UnityEngine.InputSystem;

public class FireBall : MonoBehaviour
{
    [System.Serializable]
    public struct FireBallData
    {
        public float damage;        // 공격력
        public float range;         // 사거리
        public float speed;         // 속도
        public int projectileCount; // 투사체 갯수 (여기서는 총 연사 횟수로 사용)
        public float coolTime;      // 쿨타임 (전체 스킬의 쿨타임)
        public float fireInterval;  // [추가] 투사체 간의 발사 간격 (예: 0.2초 간격 연사)
    }

    [Header("--- FireBall Settings ---")]
    [SerializeField] private FireBallData fireBallData;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;

    [Header("--- Input Settings (마스터 전용) ---")]
    [SerializeField] private InputAction fireAction; // 인스펙터에서 Space 키 매핑 필요

    [Header("--- Runtime State (내부 작동용) ---")]
    [SerializeField] private bool isClone = false;

    private float _lastCastTime;
    private float _spawnTime;
    private Vector3 _startPosition;
    private bool _isCasting = false; // [추가] 코루틴이 현재 실행 중인지 체크하는 플래그

    private void OnEnable()
    {
        if (!isClone)
        {
            fireAction.Enable();
            fireAction.performed += OnFireInput; // 스페이스바 입력 시 작동
        }
    }

    private void OnDisable()
    {
        if (!isClone)
        {
            fireAction.performed -= OnFireInput;
            fireAction.Disable();
        }
    }

    void Start()
    {
        if (isClone)
        {
            _spawnTime = Time.time;
            _startPosition = transform.position;
        }
    }

    void Update()
    {
        if (isClone)
        {
            MoveForward();
            CheckRange();
        }
    }

    // 2. 인풋시스템 입력(스페이스바)
    private void OnFireInput(InputAction.CallbackContext context)
    {
        // 쿨타임 검사 및 현재 발사 중(코루틴 작동 중)인지 검사
        if (Time.time < _lastCastTime + fireBallData.coolTime || _isCasting)
        {
            Debug.Log("파이어볼이 아직 쿨타임 중이거나 발사 중입니다.");
            return;
        }

        // 조건이 맞으면 코루틴 발사 루틴 시작!
        StartCoroutine(FireSequenceRoutine());
    }

 
    // 3. [마스터 기능] 코루틴을 이용한 순차 발사 및 쿨타임 제어
    IEnumerator FireSequenceRoutine()
    {
        _isCasting = true; // 발사 시작 상태 설정
        Debug.Log($"파이어볼 연사 시작 (총 {fireBallData.projectileCount}발)");

        Transform spawnPoint = firePoint != null ? firePoint : transform;

        // projectileCount에 지정된 갯수만큼 반복문 실행
        for (int i = 0; i < fireBallData.projectileCount; i++)
        {
            // 투사체 생성 (마스터가 바라보는 방향으로 발사)
            GameObject cloneObj = Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation);

            FireBall cloneScript = cloneObj.GetComponent<FireBall>();
            if (cloneScript != null)
            {
                cloneScript.isClone = true;
                cloneScript.fireBallData = this.fireBallData;
            }

            Debug.Log($"{i + 1}번째 파이어볼 발사 완료");

            // 마지막 발사 때는 대기하지 않고 루틴을 빠져나감
            if (i < fireBallData.projectileCount - 1)
            {
                // 설정한 연사 간격(예: 0.1초~0.2초)만큼 기다렸다가 다음 루프 실행
                yield return new WaitForSeconds(fireBallData.fireInterval); // 변수로빼서 변수와 인스펙터 연결, 인스펙터에서 조절완료
            }
        }

        // 모든 발사가 끝나면 쿨타임 기록 및 상태 초기화
        _lastCastTime = Time.time;
        _isCasting = false;
        Debug.Log("모든 파이어볼 발사 완료. 쿨타임이 시작됩니다.");
    }

    // =================================================================
    // 4. [복제본 기능] 이동 및 제거 제어
    // =================================================================
    private void MoveForward()
    {
        transform.Translate(Vector3.forward * (fireBallData.speed * Time.deltaTime));
    }

    private void CheckRange()
    {
        float traveledDistance = Vector3.Distance(_startPosition, transform.position);

        if (traveledDistance >= fireBallData.range)
        {
            DestroyProjectile();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isClone) return;

        if (other.CompareTag("Enemy"))
        {
            Debug.Log($"{other.name}에게 {fireBallData.damage}의 피해를 입혔습니다.");
            OnExplode(other.transform.position);
            DestroyProjectile();
        }
    }

    private void OnExplode(Vector3 contactPoint)
    {
        Debug.Log("파이어볼 후속 이벤트: 폭발 이펙트 및 범위 스플래시 데미지 처리 공간");
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
