using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    // 여기도 변수명을 cardData (소문자 시작)로 변경합니다.
    [SerializeField]
    CardData cardData;

    [SerializeField] TextMeshProUGUI CostText;
    [SerializeField] TextMeshProUGUI CardTitle;
    [SerializeField] Image Cardimage;
    [SerializeField] TextMeshProUGUI CardDescription;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        if (cardData == null) return;

        // UI컴포넌트에 데이터를 집어 넣는 작업
        CostText.text = cardData.CostText.ToString();
        CardTitle.text = cardData.CardTitle;
        Cardimage.sprite = cardData.Cardimage;

        // 3. 만들어두신 함수를 사용해서 %d를 수치로 치환한 문자열을 텍스트에 넣습니다.
        CardDescription.text = FormatAttackMessage(cardData.cardDescriptions);
    }

    string FormatAttackMessage(CardDescriptions desc)
    {
        // 설명이 비어있으면 에러가 날 수 있으므로 예외 처리
        if (string.IsNullOrEmpty(desc.Description))
            return "";

        // %d 텍스트를 찾아 정수형 value를 문자열로 변환하여 교체
        return desc.Description.Replace("%d", desc.value.ToString());
    }
}