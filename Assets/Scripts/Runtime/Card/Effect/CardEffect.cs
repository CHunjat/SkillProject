using UnityEditor.EditorTools;
using UnityEngine;
[System.Serializable]

public class GameContext
{
    //공격하기 위한 대상자
    public ITargetable target; //gameobject 유니티의 모든 오브젝트의 기본이 됨.. Find같은 함수를 사용하면 언제 어디서나 찾을수 있지만 비용이 비쌈.
    public IAttacker owner;
}

public abstract class CardEffect //이코드는 혼자서는 존재 할수없고 자식으로만 사용 가능
{
    public abstract void Apply(GameContext context);
}




 