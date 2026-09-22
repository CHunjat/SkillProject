using UnityEngine;

public class DataStructureTest : MonoBehaviour
{
    int buffDamage = 10;

    void Start()
    {
        // 1. 카드 데이터 전체를 일단 가져옵니다.
        CardData myCard = CardManager.Instance.GetCardData("바보");

        // 2. 문자열(Description)과 수치(value) 모두 출력해보기
        Debug.Log($"버프 전 텍스트: {myCard.cardDescriptions.Description}");
        Debug.Log($"버프 전 수치: {myCard.cardDescriptions.value}");

        // 3. 구조체 수정 에러 피하기 (임시로 빼서 고친 후 덮어쓰기)
        CardDescriptions tempDesc = myCard.cardDescriptions; // 빼오기
        tempDesc.value += buffDamage;                        // 수정하기
        myCard.cardDescriptions = tempDesc;                  // 덮어씌우기

        // 4. 버프 적용 확인
        Debug.Log($"버프 후 수치: {myCard.cardDescriptions.value}");
    }
}