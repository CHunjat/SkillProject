using UnityEngine;
using System.Collections;
using System;

public class CoroutineLab : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        //세가지를 포함하는 코루틴을 만들어라 
        StartCoroutine(FireSequenceRoutineTest());




    }
    IEnumerator FireSequenceRoutineTest()
    {
        // 0.1초 기다렸다가 디버그로 1출력
        yield return new WaitForSeconds(0.1f);
        Debug.Log("1");

        // 1초 기다렸다가 디버그로 발사 출력
        yield return new WaitForSeconds(1.0f);
        Debug.Log("발사");

        // 2초 뒤에 디버그 완료 출력
        yield return new WaitForSeconds(2.0f);
        Debug.Log("완료");
    }
}
