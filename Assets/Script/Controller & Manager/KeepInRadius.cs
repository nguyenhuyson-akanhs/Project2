using UnityEngine;

[ExecuteAlways] // Giúp script chạy ngay cả khi không nhấn Play (bao gồm khi làm Animation)
public class KeepInRadius : MonoBehaviour
{
    public Transform playerA;
    public float radius = 3f; // Bán kính r

    void Update()
    {
        if (playerA == null) return;

        // Tính khoảng cách từ Player A đến Quả cầu B
        Vector3 offset = transform.position - playerA.position;

        // Giới hạn độ dài của Vector offset không vượt quá radius (r)
        if (offset.magnitude > radius)
        {
            offset = Vector3.ClampMagnitude(offset, radius);
            transform.position = playerA.position + offset;
        }
    }

    // Vẽ bán kính r trong Scene tab để dễ quan sát khi làm Animation
    private void OnDrawGizmosSelected()
    {
        if (playerA != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(playerA.position, radius);
        }
    }
}