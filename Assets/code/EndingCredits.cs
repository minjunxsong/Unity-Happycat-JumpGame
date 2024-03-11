using UnityEngine;
using UnityEngine.UI;

public class EndingCredits : MonoBehaviour
{
    public Text creditsText; // 크레딧 텍스트를 표시할 텍스트 컴포넌트
    public float scrollSpeed = 200f; // 크레딧 텍스트의 스크롤 속도
    private RectTransform creditsRectTransform; // 크레딧 텍스트의 RectTransform 컴포넌트
    private float scrollPosition; // 현재 스크롤 위치

    private void Start()
    {
        creditsRectTransform = creditsText.GetComponent<RectTransform>(); // 크레딧 텍스트의 RectTransform 컴포넌트를 가져옴
        scrollPosition = creditsRectTransform.anchoredPosition.y; // 초기 스크롤 위치 설정
    }

    private void Update()
    {
        scrollPosition += scrollSpeed * Time.deltaTime; // 스크롤 속도에 따라 현재 스크롤 위치 업데이트
        creditsRectTransform.anchoredPosition = new Vector2(creditsRectTransform.anchoredPosition.x, scrollPosition); // 크레딧 텍스트의 스크롤 위치 업데이트
    }
}
