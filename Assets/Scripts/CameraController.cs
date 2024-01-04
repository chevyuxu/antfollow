using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        MoveCamera();
    }

    void MoveCamera()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // 計算移動方向
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // 使用Translate方法移動相機
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }
}
