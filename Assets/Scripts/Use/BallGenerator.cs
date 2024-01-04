using System.Collections.Generic;
using UnityEngine;

public class BallGenerator : MonoBehaviour
{
    public static BallGenerator instance;
    public GameObject ballPrefab;  // 圆球预制体
    private List<GameObject> generatedBalls = new List<GameObject>();  // 已生成的圆球列表
    private bool isDragging = false;  // 是否正在拖动圆球
    private GameObject currentBall;  // 当前生成的圆球
    public GameObject[] ants;
    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))  // 鼠标左键按下
        {
            GenerateBall();
        }

        if (currentBall != null)
        {
            if (Input.GetMouseButtonDown(1))  // 鼠标右键按下
            {
                StartDragging();
            }

            if (Input.GetMouseButtonUp(1))  // 鼠标右键松开
            {
                StopDragging();
            }

            if (isDragging)
            {
                DragBall();
            }
        }
    }

    void GenerateBall()
    {
        // 生成圆球
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // 使用预制体生成圆球
            currentBall = Instantiate(ballPrefab, hit.point, Quaternion.identity);
            generatedBalls.Add(currentBall);
            for (int i = 0; i < ants.Length; i++)
            {
                ants[i].GetComponent<AntAI>().isTarget = true;
                Debug.Log("here");
                ants[i].GetComponent<AntAI>().target = currentBall.transform;
            }
            // 如果场上超过1个圆球，删除最早生成的圆球
            if (generatedBalls.Count > 1)
            {
                Destroy(generatedBalls[0]);
                generatedBalls.RemoveAt(0);
            }
        }
    }

    void StartDragging()
    {
        // 开始拖动圆球
        isDragging = true;
    }

    void StopDragging()
    {
        // 停止拖动圆球
        isDragging = false;
    }

    void DragBall()
    {
        // 拖动圆球
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // 移动当前圆球到新的位置
            currentBall.transform.position = hit.point;
        }
    }
}