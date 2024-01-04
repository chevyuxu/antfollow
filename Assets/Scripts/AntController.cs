using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntController : MonoBehaviour
{
    public GameObject antPrefab; // 螞蟻的預置物
    public GameObject food;
    public int numberOfAnts = 10; // 螞蟻的數量
    public float antSpeed = 3f;
    public float changeDirectionInterval = 5f; // 改變方向的間隔時間
    public float detectionRadius = 1f;
    public float movementBoundary = 10f; // 移動時保持在指定範圍

    private List<GameObject> ants = new List<GameObject>();
    private Vector3 globalTargetPosition;
    private float nextChangeDirectionTime;
    private bool isFirstAnt = true; // 是否是第一隻螞蟻

    void Start()
    {
        globalTargetPosition = food.transform.position; // 初始目標位置為食物位置
        nextChangeDirectionTime = Time.time + changeDirectionInterval;

        StartCoroutine(SpawnAnts());
    }

    IEnumerator SpawnAnts()
    {
        GameObject previousAnt = null; // 上一隻螞蟻

        for (int i = 0; i < numberOfAnts; i++)
        {
            Vector3 spawnPosition = new Vector3(5f, 0f, 0f); // 在同一位置產生
            GameObject ant = Instantiate(antPrefab, spawnPosition, Quaternion.identity);
            ants.Add(ant);

            // 附加 AntFollower 腳本
            AntFollower antFollower = ant.AddComponent<AntFollower>();

            // 如果是第一隻螞蟻，設置目標為食物，否則設置前一隻螞蟻為目標
            if (isFirstAnt)
            {
                antFollower.SetTarget(food.transform);
                isFirstAnt = false; // 將 isFirstAnt 設置為 false，以後的螞蟻就不會再尋找食物了
            }
            else
            {
                antFollower.SetTarget(previousAnt.transform);
            }

            previousAnt = ant; // 更新上一隻螞蟻

            yield return new WaitForSeconds(1.5f); // 調整等待時間，例如每隔1秒產生一隻螞蟻
        }
    }

    void Update()
    {
        MoveAnts();
        CheckChangeDirection();
    }

    void MoveAnts()
    {
        foreach (GameObject ant in ants)
        {
            AntFollower antFollower = ant.GetComponent<AntFollower>();

            Vector3 targetDirection = (antFollower.GetTargetPosition() - ant.transform.position).normalized;
            Vector3 newPosition = ant.transform.position + targetDirection * antSpeed * Time.deltaTime;
            MoveAnt(ant, newPosition);

            CheckAntReachedFood(ant);
        }
    }

    void MoveAnt(GameObject ant, Vector3 targetPosition)
    {
        targetPosition.x = Mathf.Clamp(targetPosition.x, -movementBoundary, movementBoundary);
        targetPosition.z = Mathf.Clamp(targetPosition.z, -movementBoundary, movementBoundary);

        ant.transform.position = targetPosition;
    }

    void CheckAntReachedFood(GameObject ant)
    {
        float distanceToFood = Vector3.Distance(ant.transform.position, food.transform.position);

        if (distanceToFood < detectionRadius)
        {
            Debug.Log("Ant reached the food!");
            // 在這裡你可以添加相應的處理，例如刪除該螞蟻物件等。
        }
    }

    void CheckChangeDirection()
    {
        if (Time.time >= nextChangeDirectionTime)
        {
            globalTargetPosition = food.transform.position; // 改變目標位置為食物位置
            nextChangeDirectionTime = Time.time + changeDirectionInterval;
        }
    }
}