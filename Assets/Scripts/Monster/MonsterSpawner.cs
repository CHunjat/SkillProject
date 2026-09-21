using UnityEngine;
using System.Collections.Generic;
// 데이터가 (자주변할것들) 빼서 보관하자

//public class Wave
//{

//    //웨이브는 뭐가 필요합니까~?
//    public int waveIndex = 0;
//    public int wavebyMonstercount = 5;
//    public int wavemonstertype = 0;
//}

public class MonsterSpawner : MonoBehaviour
{
   // [SerializeField] Monster[] monster1 = new Monster[10];  //배열 비효율
    [SerializeField] List<Monster>  monstergroup = new List <Monster>();  //리스트

    //데이터가 있으니 데이터로 몬스터 생성
    //그래서 소환하는 코드는?

    private void Start()
    {
        ////그래서 소환하는 코드는? 몬스터 그룹에 있는 몬스터를 소환하는 코드를 작성
        ////웨이브당 몇종류의 몇마리를 소환할까요
        //int count = monstergroup.Count;
        //Instantiate(monstergroup[1]);
        //Instantiate(monstergroup[2]);
        //Instantiate (monstergroup[3]); 
        ////xx

        //for
        for(int i =0; i < monstergroup.Count; i++)
        {
            Instantiate(monstergroup[i]);
        }


    }


}
