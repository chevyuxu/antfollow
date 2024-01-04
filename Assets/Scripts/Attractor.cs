using UnityEngine;

public class Attractor : MonoBehaviour
{
    public float attractionForce = 5f;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ant"))  // 假设蚂蚁有一个"Ant"的标签
        {
            Vector3 directionToTarget = transform.position - other.transform.position;
            other.GetComponent<Rigidbody>().AddForce(directionToTarget.normalized * attractionForce);
        }
    }
}