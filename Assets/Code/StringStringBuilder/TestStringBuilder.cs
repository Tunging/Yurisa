using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code
{
    //用stringbuilder 是0 GC
    class TestStringBuilder:MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                int gc = GC.CollectionCount(0);
                int count = 10000;
                float time = Time.realtimeSinceStartup;
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < count; i++)
                {
                    sb.Append("0");
                }
                int after = GC.CollectionCount(0);
                print(sb.ToString());
                Debug.Log($"sb gc = {after - gc  }");
                Debug.Log("Time = " + (Time.realtimeSinceStartup - time));
            }
        }
    }
}
