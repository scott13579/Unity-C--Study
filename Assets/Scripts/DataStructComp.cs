using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DataStructComp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // bit ->  컴퓨터 데이터 단위 표현의 최소단위
        // 1 byte = 8 bit
        // 00000000 = 1byte
        // 10진법 128 -> 2진법 10000000
        
        // 기본 자료형
        Int32 intValue; // 자료형 -21억 ~ 21억까지 표현 가능
        UInt32 uintValue; // 0 ~ 42억까지 표현 가능

        Int64 longValue;
        UInt64 ulongValue;
        
        float floatValue;   // 3.4E +/- 38(소수점 최대 7자리 숫자)
        // example      
        // 1.23456789
        
        double doubleValue; // 소수점 최대 15자리 숫자
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
