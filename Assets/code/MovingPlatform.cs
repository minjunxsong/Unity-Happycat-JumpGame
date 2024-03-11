using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 3f; // 플랫폼 속도
    public float leftBound = -5f; // 좌측 경계
    public float rightBound = 5f; // 우측 경계

    private bool movingRight = true; // 우측으로 이동하는 상태인지 여부

    private void Update()
    {
        // 우측으로 이동 중이고 우측 경계에 도달하면 방향을 반대로 변경
        if (movingRight && transform.position.x >= rightBound)
        {
            movingRight = false;
        }
        // 좌측으로 이동 중이고 좌측 경계에 도달하면 방향을 반대로 변경
        else if (!movingRight && transform.position.x <= leftBound)
        {
            movingRight = true;
        }

        // 우측으로 이동 중이면 우측으로 이동
        if (movingRight)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        // 좌측으로 이동 중이면 좌측으로 이동
        else
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            // 플레이어를 플랫폼의 자식으로 설정하여 플레이어가 플랫폼을 따라가게 함
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            // 플레이어가 플랫폼에서 떨어지면 부모를 없애서 따라가는 동작을 중지함
            collision.transform.SetParent(null);
        }
    }
}
