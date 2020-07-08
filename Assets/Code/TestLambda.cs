using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Profiling;

/// <summary>
/// 用lambda表达式代替Action，可以0 GC
/// </summary>
public class TestLambda : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    int time = 0;
    private void Func(Action callback)
    {
        callback();
    }

    void Callback()
    {
        //Debug.Log(index);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Do();
        }
    }

    private void Do()
    {
        for (int i = 0; i < 10000; i++)
        {
            Func(Callback);
        }
        int count = GC.CollectionCount(0);
        Debug.Log("gc 1 == " + count);

        for (int i = 0; i < 10000; i++)
        {
            Func(() => Callback());
        }

        count = GC.CollectionCount(0);
        //count = GC.CollectionCount(0) - count;
        Debug.Log("gc 2 == " + count);

    }
}
