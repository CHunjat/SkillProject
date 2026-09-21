using UnityEngine;
using System;
[System.Serializable]


 public class Monster : MonoBehaviour
{
    // 체력 - 정수? 소수점?
    // 이름
    // 속도
    // 공격력

    [SerializeField] MonsterData monsterData;


    //데이터
    //======================================================


    private void Start()
    {
        Debug.Log($"{monsterData.name}의 체력 : {monsterData.hp}");
        Debug.Log($"{monsterData.name}의스피드 : {monsterData.speed}");
        Debug.Log($"{monsterData.name}의데미지 : {monsterData.damage}");
            
            
    }

    //그래서 누가 공격합니까

    //기능 : 한번 결ㅈ어하면 웬만하면 변경안하는것들 그래서 


    //======================================================

    public void DoCombat(MonsterData defenderMonsterData, MonsterData attackMonsterData)
    {
        int finalDamage = defenderMonsterData.hp - (int)attackMonsterData.damage;

        Debug.Log($"최종 데미지 {finalDamage}");
    }

}
