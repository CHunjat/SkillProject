using UnityEngine;

public interface IDamageable2D
{
    void TakeDamage(float amount, GameObject attacker);
}


// 인터페이스는 예를들어 데미지가 있는데 pk용으로 데미지 설정해서 다르게 사용할수있는게 장점
public class FireballTarget : MonoBehaviour, IDamageable2D
{
    [Header("타겟 능력치")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;

    [Header("설정")]
    [SerializeField] private bool isInvincible = false;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount, GameObject attacker)
    {
        if (isInvincible || currentHealth <= 0) return;

        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0);

        string attackerName = attacker != null ? attacker.name : "Unknown";
        Debug.Log($"🎯 2D [{gameObject.name}]이(가) [{attackerName}]에게 {amount}의 피해를 입었습니다. (남은 체력: {currentHealth}/{maxHealth})");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"💀 2D [{gameObject.name}]이(가) 사망했습니다.");
        Destroy(gameObject);
    }
}