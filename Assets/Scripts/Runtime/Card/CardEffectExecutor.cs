using Unity.VisualScripting;
using UnityEngine;

public class CardEffectExecutor : MonoBehaviour
{

    public CardData sample;

    private void Start()
    {
        Excute(sample);
    }


    public void Excute(CardData data)
    {
        // 1. if  조건문

        //if(data.CardTitle == "바보")
        //{

        //}
        //else if(data.CardTitle == "멍청이")
        //{

        //}
        //else if( data.CardTitle == "말미잘")
        //{

        //}
        //else if( data.CardTitle =="해삼")
        //{
        //    int dmg = data.cardDescriptions.value;


        //    dmg *= 2;


        //    Debug.Log($"{dmg}만큼 공격 가했음");
        //}



        // 1-2. switch 조건문

        //switch (data.skillType)
        //{
        //    case SkillType.ATTACK:
        //        //공격
        //        break;
        //    case SkillType.GUARD:
        //        //수비
        //        break;
        //    case SkillType.BUFF:
        //        //버프
        //        break;
        //    case SkillType.DEBUFF:
        //        //디버프
        //        break;
        //    case SkillType.HEAL:
        //        //힐
        //        break;
        //}

        // 2.상속

        GameContext context = new GameContext();


        SampleAttackMonster sampleAttackMonster = new SampleAttackMonster();
        context.target = sampleAttackMonster as ITargetable;


        data.cardEffect.Apply(context);

        // 3. 인터페이스

        // 4. command 패턴
    }
}
