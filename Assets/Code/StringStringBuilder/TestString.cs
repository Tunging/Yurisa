using UnityEngine;
using System.Collections;
using System;
using System.Text;

public class TestString : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    int count = 10000;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            int gc = GC.CollectionCount(0);
            float time = Time.realtimeSinceStartup;
            string str = "";
            for (int i = 0; i < count; i++)
            {
                str += "0";
            }
            int after = GC.CollectionCount(0);
            print(str);
            Debug.Log($"+++ gc = {GC.CollectionCount(0) -  gc}");
            Debug.Log("Time = " + (Time.realtimeSinceStartup - time));

        }
    }
}
