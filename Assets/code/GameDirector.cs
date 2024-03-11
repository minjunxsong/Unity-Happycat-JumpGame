using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    GameObject gHappyCat = null; // HappyCat 게임 오브젝트 변수
    GameObject gbananaC = null; // bananaC 게임 오브젝트 변수
    GameObject gbananaH = null; // bananaHeart 게임 오브젝트 변수
    public float moveSpeed = 4f; // 이동 속도
    private float moveDuration = 7f; // 이동 시간
    private float elapsedTime = 0f; // 경과 시간
    private Vector3 startPosition; // 이동 시작 위치
    GameObject gPanel = null; // 대화 패널

    public Text TalkText; // 대화 텍스트를 출력하는 Text 컴포넌트
    public string[] textList; // 대화 텍스트 배열
    private int currentIndex = 0; // 현재 대화 인덱스
    public Text NameText; // 이름을 출력하는 Text 컴포넌트
    public string[] NameList; // 이름 배열
    private int NameIndex = 0; // 현재 이름 인덱스

    public Image portraitImage; // 초상화를 표시하는 Image 컴포넌트
    public Sprite[] portraitSprites; // 초상화 스프라이트 배열
    private int FaceIndex = 0; // 현재 초상화 인덱스

    // Start is called before the first frame update
    void Start()
    {
        gHappyCat = GameObject.Find("HappyCat"); // HappyCat 게임 오브젝트 찾기
        gbananaC = GameObject.Find("bananaC"); // bananaC 게임 오브젝트 찾기
        gbananaH = GameObject.Find("bananaHeart"); // bananaHeart 게임 오브젝트 찾기
        startPosition = gHappyCat.transform.position; // 이동 시작 위치 설정
        textList = new string[] { "해피캣!!!", "내가 구하러 왔으니 걱정마 바나나캣!", "믿고있었어 ㅠㅠ 어서 여길 빠져 나가자!" }; // 대화 텍스트 배열 초기화
        NameList = new string[] { "바나나캣", "해피캣", "바나나캣" }; // 이름 배열 초기화
        gPanel = GameObject.Find("Image"); // 대화 패널 게임 오브젝트 찾기
        gPanel.SetActive(false); // 대화 패널 비활성화
        gbananaH.SetActive(false); // bananaHeart 비활성화
        portraitSprites = new Sprite[3]; // 초상화 스프라이트 배열 초기화

        portraitSprites[0] = Resources.Load<Sprite>("bananaH"); // 초상화 스프라이트 설정
        portraitSprites[1] = Resources.Load<Sprite>("HappyCat"); // 초상화 스프라이트 설정
        portraitSprites[2] = Resources.Load<Sprite>("bananaH"); // 초상화 스프라이트 설정
    }

    // Update is called once per frame
    void Update()
    {
        // HappyCat과 bananaC 사이의 거리가 3.0보다 작으면, bananaC를 비활성화하고 bananaHeart와 대화 패널을 활성화합니다.
        if (gbananaC != null && Vector3.Distance(gHappyCat.transform.position, gbananaC.transform.position) < 3.0f)
        {
            gbananaH.SetActive(true); // bananaHeart 활성화
            gPanel.SetActive(true); // 대화 패널 활성화
            gbananaC.SetActive(false); // bananaC 비활성화

            // Enter 키가 눌리면
            if (Input.GetKeyDown(KeyCode.Return))
            {
                gPanel.SetActive(true);

                // 대화 텍스트 배열의 모든 대화를 출력하지 않았다면 대화를 출력하고 그렇지 않으면 EndingScene 씬을 로드합니다.
                if (currentIndex < textList.Length)
                {
                    string currentText = textList[currentIndex];
                    TalkText.text = currentText;
                    currentIndex++;
                }
                else // 대화가 끝났으면
                {
                    SceneManager.LoadScene("EndingScene"); // EndingScene으로 이동
                }
            }

            // Enter 키가 눌리면
            if (Input.GetKeyDown(KeyCode.Return))
            {
                // 이름 배열의 모든 이름을 출력하지 않았다면 이름을 출력합니다.
                if (NameIndex < NameList.Length)
                {
                    string currentText1 = NameList[NameIndex];
                    NameText.text = currentText1;
                    NameIndex++;
                }
            }

            // Enter 키가 눌리면
            if (Input.GetKeyDown(KeyCode.Return))
            {
                // 초상화 스프라이트 배열의 모든 초상화를 출력하지 않았다면 초상화를 출력합니다.
                if (FaceIndex < portraitSprites.Length)
                {
                    Sprite currentText3 = portraitSprites[FaceIndex];
                    portraitImage.sprite = currentText3;
                    FaceIndex++;
                }
            }
        }

        // 경과 시간이 이동 시간보다 작으면, HappyCat을 오른쪽으로 이동시킵니다.
        if (elapsedTime < moveDuration)
        {
            float distance = moveSpeed * Time.deltaTime;
            gHappyCat.transform.Translate(Vector3.right * distance); // HappyCat을 오른쪽으로 이동
            elapsedTime += Time.deltaTime;
        }
    }

}
