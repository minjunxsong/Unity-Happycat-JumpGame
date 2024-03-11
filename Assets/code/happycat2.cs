using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class happycat2 : MonoBehaviour
{
    private AudioSource audioSource; // 오디오 소스 컴포넌트
    public AudioClip startMusic; // 게임 시작 시 재생할 AudioClip
    private int totalCoins; // 총 코인 개수
    public Text Score_txt;
    private Rigidbody2D rigid2D;
    Animator animator;
    float jumpForce = 650.0f;
    float walkForce = 10.0f;
    float maxWalkSpeed = 3.0f;
    float pushForce = 10.0f; // 장애물과 충돌 시 힘을 가하는 값
    GameObject door;
    [SerializeField] GameObject pickUp;
    public Button startButton;
    private bool isGameStarted = false;
    private bool isFading = false; // 페이드 상태 여부
    public Image Story;
    public Image fadeInImage;
    public Image fadeOutImage;
    public Image Stage3;
    public float fadeDuration = 1.5f;
    public Text text;

    void Start()
    {
        door = GameObject.Find("Door");
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        startButton.onClick.AddListener(StartGame);
        door.SetActive(false);
    }

    void Update()
    {
        if (isGameStarted)
        {
            totalCoins = GameObject.FindGameObjectsWithTag("Coin").Length;

            Score_txt.text = "현재 남은 코인: " + totalCoins.ToString();
            // 점프 처리
            if (Input.GetKeyDown(KeyCode.Space) && this.rigid2D.velocity.y == 0)
            {
                this.rigid2D.AddForce(transform.up * this.jumpForce);
            }

            // 이동 처리
            int key = 0;
            if (Input.GetKey(KeyCode.RightArrow)) key = 1;
            if (Input.GetKey(KeyCode.LeftArrow)) key = -1;

            // 이동 속도 제한
            float speedx = Mathf.Abs(this.rigid2D.velocity.x);
            if (speedx < this.maxWalkSpeed)
            {
                this.rigid2D.AddForce(transform.right * key * this.walkForce);
            }

            // 캐릭터 반전
            if (key != 0)
            {
                transform.localScale = new Vector3(key, 1, 1);
            }

            // 코인을 모두 획득하면 문을 활성화
            if (totalCoins == 0)
            {
                door.SetActive(true);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)      // 장애물과 충돌 시 플레이어 밀기 처리
    {
        if (collision.gameObject.CompareTag("MovingObstacle")) // 이동하는 장애물과 충돌했을 때
        {
            Vector3 contactPoint = collision.contacts[0].point;
            Vector3 center = collision.collider.bounds.center;
            Vector3 pushDirection = (contactPoint - center).normalized;
            this.rigid2D.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
        }
    }

    IEnumerator FadeAndLoadScene(string sceneName)    // 페이드 인, 아웃 처리 및 씬 로드
    {
        fadeInImage.gameObject.SetActive(true);

        // Fade in
        float time = 0f;
        Color startingColor = fadeInImage.color;
        Color targetColor = new Color(startingColor.r, startingColor.g, startingColor.b, 1f);

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            fadeInImage.color = Color.Lerp(startingColor, targetColor, time / fadeDuration);
            yield return null;
        }

        // Display Stage3 image with fade-in animation
        Stage3.gameObject.SetActive(true);
        float imageTime = 0f;
        Color startingImageColor = Stage3.color;
        Color targetImageColor = new Color(startingImageColor.r, startingImageColor.g, startingImageColor.b, 1f);

        while (imageTime < fadeDuration)
        {
            imageTime += Time.deltaTime;
            Stage3.color = Color.Lerp(startingImageColor, targetImageColor, imageTime / fadeDuration);
            yield return null;
        }

        // 씬 로드 처리
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // 씬 로딩이 완료될 때까지 대기
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    IEnumerator FadeOutImage()   // 페이드 아웃 처리
    {
        isFading = true;

        // Fade out
        float time = 0f;
        Color startingColor = fadeOutImage.color;
        Color targetColor = new Color(startingColor.r, startingColor.g, startingColor.b, 0f);

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            fadeOutImage.color = Color.Lerp(startingColor, targetColor, time / fadeDuration);
            yield return null;
        }

        fadeOutImage.color = targetColor;

        isFading = false;
    }

    private void OnTriggerEnter2D(Collider2D other)   // 플레이어가 트리거에 진입했을 때 처리
    {
        if (other.CompareTag("Coin")) // Coin과 충돌했을 때
        {
            Destroy(other.gameObject);
            EatItem(); // 아이템 획득 처리
        }
        else if (other.CompareTag("door")) // 문과 충돌했을 때
        {
            StartCoroutine(FadeAndLoadScene("Stage3")); // 씬 전환
        }
    }

    public void EatItem()   // 아이템 획득 처리
    {
        pickUp = Instantiate(pickUp, transform.position, Quaternion.identity);
        pickUp.transform.SetParent(transform);
    }

    private void StartGame()   // 게임 시작 처리
    {
        if (!isFading)
        {
            StartCoroutine(FadeOutImage());
        }

        isGameStarted = true;
        startButton.gameObject.SetActive(false); // 시작 버튼 비활성화
        Story.enabled = false;
        text.gameObject.SetActive(false);
        MusicStart(); // 음악 재생
    }

    private void MusicStart()   // 음악 재생
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = startMusic;
        audioSource.loop = true; // 반복 재생 설정
        audioSource.Play(); // 재생 시작
    }
}
