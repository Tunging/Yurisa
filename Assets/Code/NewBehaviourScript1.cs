using UnityEngine;
using System.Collections;
using Assets.Code.Algorithm;
using Assets.Code.Algorithm.AStar;
using System.Collections.Generic;

public class NewBehaviourScript1 : MonoBehaviour
{
    public GameObject prefab;
    public Material red;
    public Material green;
    public Material white;
    public Material yellow;

    public int width = 15;
    public int height = 15;

    public int offset = 2;
    GameObject[,] gos;
    // Use this for initialization
    void Start()
    {
        if (gos == null)
        {
            gos = new GameObject[width, height];
        }
        foreach (var item in gos)
        {
            if (item)
            {
                GameObject.Destroy(item);
            }
        }

        AStarManager.Instance.InitMapInfo(width, height);

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                var go = GameObject.Instantiate(prefab);
                go.name = $"{i}_{j}";
                go.transform.position = new Vector3(i * offset, 0, j * offset);

                Node node = AStarManager.Instance.Nodes[i, j];
                node.go = go;
                if (node.NodeType == NodeType.Wall)
                {
                    go.GetComponent<Renderer>().material = red;
                }

                gos[i, j] = go;
            }
        }
    }

    private void OnGUI()
    {
        if (GUILayout.Button("fffff"))
        {
            foreach (var item in gos)
            {
                item.GetComponent<Renderer>().material = white;
            }

            Start();

            var path = AStarManager.Instance.FindPath(new Vector2Int(1, 2), new Vector2Int(4, 4));

            if (path == null)
            {
                Debug.LogError("死路");
                return;
            }
            foreach (var item in path)
            {
                item.go.GetComponent<Renderer>().material = green;
            }
            //LongestChildArr.Instance.Do();
        }
    }
    List<Node> list;
    Vector2 start;
    Vector2 end;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition, Camera.MonoOrStereoscopicEye.Mono);
            if (Physics.Raycast(ray, out RaycastHit hit, 100))
            {
                var param = hit.collider.gameObject.name.Split('_');
                int x = int.Parse(param[0]);
                int y = int.Parse(param[1]);
                Node node = AStarManager.Instance.Nodes[x, y];
                if (node != null && node.go)
                {
                    if (start == Vector2.zero)
                    {
                        if (list!=null && list.Count>0)
                        {
                            foreach (var item in list)
                            {
                                item.go.GetComponent<Renderer>().material = white;
                            }
                            list.Clear();
                        }

                        start = new Vector2(x, y);
                        node.go.GetComponent<Renderer>().material = yellow;
                    }
                    else
                    {
                        end = new Vector2(x, y);
                        node.go.GetComponent<Renderer>().material = yellow;
                        list = AStarManager.Instance.FindPath(start, end);

                        if (list==null)
                        {
                            Debug.LogError("silu ");
                            return;
                        }
                        foreach (var item in list)
                        {
                            item.go.GetComponent<Renderer>().material = green;
                        }

                        start = Vector2.zero;
                    }
                }
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition, Camera.MonoOrStereoscopicEye.Mono);
            if (Physics.Raycast(ray, out RaycastHit hit, 100))
            {
                var param = hit.collider.gameObject.name.Split('_');
                int x = int.Parse(param[0]);
                int y = int.Parse(param[1]);
                Node node = AStarManager.Instance.Nodes[x, y];
                if (node != null && node.go)
                {
                    if (node.NodeType == NodeType.Wall)
                    {
                        node.NodeType = NodeType.Normal;
                        node.go.GetComponent<Renderer>().material = white;
                    }
                    else
                    {
                        node.NodeType = NodeType.Wall;
                        node.go.GetComponent<Renderer>().material = red;
                    }
                }
            }
        }
    }
}
