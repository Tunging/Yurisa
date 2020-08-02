using UnityEngine;
using System.Collections;
using Assets.Code.Algorithm;
using Assets.Code.Algorithm.AStar;

public class NewBehaviourScript1 : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    private void OnGUI()
    {
        if (GUILayout.Button("fffff"))
        {
            //AStarEngine.Instance.FindPath(new Vector2Int(1, 2), new Vector2Int(78, 89));
            LongestChildArr.Instance.Do();
        }
    }
}
