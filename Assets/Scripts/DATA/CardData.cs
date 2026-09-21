using UnityEngine;

// 1. 구조체 바로 위에 [System.Serializable]을 달아주어야 인스펙터에 노출됩니다.
[System.Serializable]
public struct CardDescriptions
{
    public string Description;
    public int value;
}

[CreateAssetMenu(fileName = "CardData", menuName = "Scriptable Objects/CardData")]
public class CardData : ScriptableObject
{
    public int CostText;
    public string CardTitle;
    public Sprite Cardimage;

    // 2. 자료형 이름과 겹치지 않게 변수명 첫 글자를 소문자(cardDescriptions)로 변경합니다.
    public CardDescriptions cardDescriptions;
}