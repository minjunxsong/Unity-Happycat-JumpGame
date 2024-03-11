using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class happycat1 : MonoBehaviour
{

    private AudioSource audioSource; // 오디오 소스 컴포넌트
    public AudioClip startMusic; // 게임 시작 시 재생할 AudioClip
    private Rigidbody2D rigid2D;
    Animator animator;
    float jumpForce = 650.0f;
    float walkForce = 10.0f;
    float maxWalkSpeed = 3.0f;
 
    GameObject door;
    public Button startButton;
    private bool isGameStarted = false;

    private bool isFading = false; // 페이드 상태 여부
    public Image Story;
    public Image fadeInImage;
    public Image fadeOutImage;
    public Image Stage2;
    public float fadeDuration = 1.5f;
    public Text text;

    void Start()
    {
        door = GameObject.Find("Door");
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        startButton.onClick.AddListener(StartGame);
        door.SetActive(true);
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

    IEnumerator FadeAndLoadScene(string sceneName)  // Fade in, out 처리
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
        Stage2.gameObject.SetActive(true);
        float imageTime = 0f;
        Color startingImageColor = Stage2.color;
        Color targetImageColor = new Color(startingImageColor.r, startingImageColor.g, startingImageColor.b, 1f);

        while (imageTime < fadeDuration)
        {
            imageTime += Time.deltaTime;
            Stage2.color = Color.Lerp(startingImageColor, targetImageColor, imageTime / fadeDuration);
            yield return null;
        }

        // Scene Load 처리
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // 씬 로딩이 완료될 때까지 대기
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    IEnumerator FadeOutImage()   // Fade out 처리
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("door"))
        {
            StartCoroutine(FadeAndLoadScene("Stage2"));
        }
    }

    private void StartGame()
    {
        if (!isFading)
        {
            StartCoroutine(FadeOutImage());
        }
       
        isGameStarted = true;
        startButton.gameObject.SetActive(false); // 시작 버튼 비활성화
        Story.enabled = false;
        text.gameObject.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = startMusic;
        audioSource.loop = true; // 반복 재생 설정
        audioSource.Play(); // 재생 시작
    }
}
