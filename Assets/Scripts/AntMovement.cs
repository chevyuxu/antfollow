using UnityEngine;

public class AntMovement : MonoBehaviour
{
    private Vector3 moveDirection;
    private float changeDirectionInterval = 0f;
    private float timeSinceLastDirectionChange = 0f;
    private float moveTime = 0f;
    private bool isTurningRight = false;
    private float turnTimer = 0f;

    void Start()
    {
        // 初始化蚂蚁的移动方向和下一次方向更改的间隔
        moveDirection = GetRandom2DDirection();
        SetNextDirectionChangeInterval();
    }

    void Update()
    {
        MoveAnt();

        // 更新时间计数
        timeSinceLastDirectionChange += Time.deltaTime;
        moveTime += Time.deltaTime;

        // 检查是否应该更改方向
        if (timeSinceLastDirectionChange >= changeDirectionInterval)
        {
            moveDirection = GetRandom2DDirection();
            SetNextDirectionChangeInterval();
            timeSinceLastDirectionChange = 0f;
        }

        
    }

    void MoveAnt()
    {
        // 移动蚂蚁
        transform.Translate(moveDirection * 2f * Time.deltaTime);

        // 如果正在右转，将速度方向改为向右
        if (isTurningRight)
        {
            transform.Rotate(Vector3.up * 10f * Time.deltaTime);
        }
    }

    Vector3 GetRandom2DDirection()
    {
        // 在2D平面上生成随机方向
        float angle = Random.Range(0f, 360f);
        return Quaternion.Euler(0f, 0f, angle) * Vector3.up;
    }

    void SetNextDirectionChangeInterval()
    {
        // 设置下一次方向更改的间隔
        changeDirectionInterval = Random.Range(0.1f, 2f);
    }
}