using UnityEngine;

[CreateAssetMenu(fileName = "New Monster", menuName = "Vampire Survivors/Monster Data")]
public class MonsterData : ScriptableObject
{
    [Header("기본 정보")]
    public string monsterName;      // 이름 (monsterData.name 대신 사용 권장)

    [Header("스탯")]
    public int hp;                  // 체력 (정수)
    public float speed;             // 속도
    public float damage;            // 공격력

    // 필요하면 나중에 추가 가능
    // public float defense;
    // public int exp;
    // public float attackRange;
}