using System.Collections; // IEnumerator를 사용하기 위해 필수
using UnityEngine;
using UnityEngine.UI;

public class CoolDownBar : MonoBehaviour
{
    [Header("UI Settings")]
    [Tooltip("회색 360도 Radial 이미지 객체를 연결해주세요.")]
    public Image cooltimeImage;

    [Header("Skill Settings")]
    [Tooltip("스킬의 총 쿨타임 (초)")]
    public float cooldownTime = 5.0f;

    [Header("연동할 런처")]
    public FireballLauncher launcher;

    // 현재 실행 중인 코루틴을 추적하기 위한 변수
    private Coroutine cooldownCoroutine;

    void Start()
    {
        if (cooltimeImage != null)
        {
            cooltimeImage.fillAmount = 0f;
        }

        if (launcher != null)
        {
            launcher.OnCooldownStarted += StartCooldown;
        }
    }

    void OnDestroy()
    {
        if (launcher != null)
        {
            launcher.OnCooldownStarted -= StartCooldown;
        }
    }

    // public void Update() 함수 삭제
    // {


    // }

    public void StartCooldown(float time)
    {
        // 이미 쿨타임 코루틴이 실행 중이라면 무시
        if (cooldownCoroutine != null) return;

        cooldownTime = time;

        if (cooltimeImage != null)
            cooltimeImage.fillAmount = 1f;

        // 쿨타임 코루틴 시작
        cooldownCoroutine = StartCoroutine(CooldownRoutine(cooldownTime));
    }

    // 코루틴: 지정된 시간 동안만 돌고 스스로 종료됩니다.
    private IEnumerator CooldownRoutine(float time)
    {
        float currentCooldown = time;

        // currentCooldown이 0보다 큰 동안 매 프레임 반복
        while (currentCooldown > 0f)
        {
            currentCooldown -= Time.deltaTime;

            if (cooltimeImage != null)
            {
                cooltimeImage.fillAmount = currentCooldown / time;
            }

            yield return null; // 다음 프레임까지 대기
        }

        // 쿨타임 종료 후 처리
        if (cooltimeImage != null)
        {
            cooltimeImage.fillAmount = 0f;
        }

        // 코루틴 변수 비우기 (다시 스킬을 쓸 수 있게 됨)
        cooldownCoroutine = null;
    }
}