using UnityEngine;

public class MovingMace : MonoBehaviour
{
    public static float speed = 3f; // 망치의 속도
    public float leftBound = -5f; // 좌측 경계
    public float rightBound = 5f; // 우측 경계
    private bool movingRight = true; // 우측으로 이동하는 상태인지 여부
    public bool isMovingLeft = false; // 좌측으로 이동하는 상태인지 여부

    void Update()
    {
        // 우측으로 이동 중이고 우측 경계에 도달하면 방향을 반대로 변경
        if (movingRight && transform.position.x >= rightBound)
        {
            movingRight = false;
            isMovingLeft = true;
        }
        // 좌측으로 이동 중이고 좌측 경계에 도달하면 방향을 반대로 변경
        else if (!movingRight && transform.position.x <= leftBound)
        {
            movingRight = true;
            isMovingLeft = false;
        }

        // 우측으로 이동 중이면 우측으로 이동
        if (movingRight)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        // 좌측으로 이동 중이면 좌측으로 이동
        else if (isMovingLeft)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }
}
