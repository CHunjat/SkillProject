using UnityEngine;


//공격을 할 수 있는 클래스에 대해 붙여서 사용하라!!
// 인터페이스를 부착하면 어떤 효과를 얻습니까?
// 인터페이스 안에 정의되어 있는 모든 기능을 반드시 구현해야한다.
public interface IAttacker
{
    //인터페이스는 함수로 사용해야하는데 사용하려면 GET,SET 프로퍼티 문법사용해야해
    public int HP { get; }
    public int ATK { get; }

    public void DoAttack(ITargetable targerable)
    {

    }
}



public class SampleAttackMonster : MonoBehaviour, IAttacker
{
    CardData carddata;


    public int HP { get => carddata.cardDescriptions.value;}
    public int ATK { get => carddata.cardDescriptions.value;}

    public void DoAttack(ITargetable targerable)
    {
        targerable.HP -= ATK;
    }
}
