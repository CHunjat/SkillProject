using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardManager : Singleton<CardManager>
{
    Dictionary<string, CardData> cardDataOrigin = new(); //얘를 직접사용하면 안됨 왜? 직접 수정금지 싱글톤으로 만들고 /getid로 접근해서 쓰고 / readonly로 만들어라!!

    public CardData[] AllCardDatas;

    //최초로 모든 데이터를 cardDataOrigin에 주입하는 코드가 있어야함

    protected override void Awake()
    {
        base.Awake();
        Init();
    }

    void Init()
    {
        //1.스크립터블 오브젝트 데이터를 모두 가져와서 카드 데이터 오리진이 그 데이터를 사용합니다

        foreach(var Data in AllCardDatas)
        {
            cardDataOrigin.Add(Data.CardTitle, Data.Clone());
        }

        //2.json으로 만들어진 textasset을 읽어서 데이터를 생성합니다.
    }



    //C#은 Class로 생성한 코드는 참조타입, struct는 복사타입 c#class 참조 자동생성되는 문제를 해결해야한다. 
    // cardDataOrigin 직접 수정하면 안됩니다. 싱글톤/GetId 접근해서 쓰고, ReadOnly 해야함!!
    public CardData GetCardData(string cardName)
    {
        return cardDataOrigin[cardName];
    }
  
}
