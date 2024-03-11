using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class happycat3 : MonoBehaviour
{
    public Button startButton;
    private bool isGameStarted = false;
    private Rigidbody2D rigid2D;
    Animator animator;
    float jumpForce = 650.0f;
    float walkForce = 10.0f;
    float maxWalkSpeed = 3.0f;
    float pushForce = 10.0f; // 장애물과 충돌 시 힘을 가하는 값
    GameObject gBananacat1;
    GameObject gBananacat2;
    public Image Story; 
    public float fadeDuration = 1.5f;
    private bool isFading = false; // 페이드 상태 여부
    public Image fadeInImage;
    public Image fadeOutImage;
    public Text text;
    public GameObject Saw;
    private AudioSource audioSource; // 오디오 소스 컴포넌트
    public AudioClip startMusic; // 게임 시작 시 재생할 AudioClip

    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        startButton.onClick.AddListener(StartGame);
        gBananacat1 = GameObject.Find("bananacat1");
        gBananacat2 = GameObject.Find("bananacat2");
        gBananacat2.SetActive(false);
        Saw.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isGameStarted)
        {
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
        }
    }

    void OnCollisionEnter2D(Collision2D collision)  // 충돌 처리
    {
        if (collision.gameObject.CompareTag("MovingObstacle")) // 이동하는 장애물과 충돌했을 때
        {
            Vector3 contactPoint = collision.contacts[0].point;
            Vector3 center = collision.collider.bounds.center;
            Vector3 pushDirection = (contactPoint - center).normalized;
            this.rigid2D.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
        }
    }

    IEnumerator FadeAndLoadScene(string sceneName)  //Fade in, out 처리
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

        // 씬 로드 처리
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // 씬 로딩이 완료될 때까지 대기
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    IEnumerator FadeOutImage()   //fade out 처리
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

    private void OnTriggerEnter2D(Collider2D other)  // 플레이어가 트리거에 진입했을 때 처리
    {
        if (other.CompareTag("bananacat")) // 바나나고양이와 충돌했을 때
        {
            gBananacat1.SetActive(false);
            gBananacat2.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(FadeAndLoadScene("LastScene")); // 마지막 씬으로 전환
        }
        else if (other.CompareTag("happycat")) // 행복한 고양이와 충돌했을 때
        {
            StartCoroutine(FadeAndLoadScene("Stage3")); // 씬 전환
        }
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
        Saw.gameObject.SetActive(true);
        MusicStart(); // 음악 재생
    }

    private void MusicStart()  // 음악 재생 처리
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = startMusic;
        audioSource.loop = true; // 반복 재생 설정
        audioSource.Play(); // 재생 시작
    }   
}
