using System;
using System.Collections.Generic;
using UnityEngine;

// 1. 구조체 바로 위에 [System.Serializable]을 달아주어야 인스펙터에 노출됩니다.
[System.Serializable]
public struct CardDescriptions
{
    public string Description;
    public int value;

    public CardDescriptions(string description, int value) : this()
    {
        Description = description;
        this.value = value;
    }
}


public enum AttackType
{
    MELLE,
    RANGED,
    AREA
}
public enum PowerType
{
    PHYSICAL,
    FIRE,
    ICE,
    POISON
}
public enum SkillType
{
    ATTACK,
    GUARD,
    BUFF,
    DEBUFF,
    HEAL
}

// Effect : 전투를 실행할 때 어떤 효과가 발생해야 하나요?

[CreateAssetMenu(fileName = "CardData", menuName = "Scriptable Objects/CardData")]
public class CardData : ScriptableObject
{
    public int CostText;
    public string CardTitle;
    public Sprite Cardimage;

    // 2. 자료형 이름과 겹치지 않게 변수명 첫 글자를 소문자(cardDescriptions)로 변경합니다.
    public CardDescriptions cardDescriptions;

    public SkillType skillType;

    [SerializeReference] public CardEffect cardEffect;





    public CardData Clone()
    {
        var clone = Instantiate(this);
        clone.cardDescriptions = new CardDescriptions(this.cardDescriptions.Description, this.cardDescriptions.value);

        return clone;
    }
}