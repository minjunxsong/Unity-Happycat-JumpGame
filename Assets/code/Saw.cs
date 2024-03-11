using UnityEngine;

public class Saw : MonoBehaviour
{
    public float rotationSpeed = 90f; // 회전 속도
    public float movementSpeed = 3f; // 이동 속도
    private Rigidbody2D rb2D;
    private AudioSource audioSource; // AudioSource 컴포넌트
    public AudioClip startMusic; // 시작 시 재생할 AudioClip

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>(); // AudioSource 컴포넌트 초기화
    }

    private void Update()
    {
        // 회전 각도 계산
        float rotation = rb2D.rotation + rotationSpeed * Time.deltaTime;

        // 회전 적용
        rb2D.MoveRotation(rotation);
        Vector2 movement = new Vector2(0f, movementSpeed);

        // Rigidbody2D의 이동 속도 설정
        rb2D.velocity = movement;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 플레이어와 충돌 시 소리 재생
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = startMusic;
            audioSource.Play(); // 시작 음악 재생
        }
    }
}
