// AntFollower.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntFollower : MonoBehaviour
{
    private Transform target; // 追蹤的目標

    // 設置目標
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    // 獲取目標位置
    public Vector3 GetTargetPosition()
    {
        if (target != null)
        {
        Debug.Log("Target Position: " + target.position);
            return target.position;
             
        }
        else
        {
            // 如果沒有設置目標，返回原點
            return Vector3.zero; // 將此處修改為返回 Vector3.zero
        }
    }
}
