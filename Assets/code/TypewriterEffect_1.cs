using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TypewriterEffect_1 : MonoBehaviour
{
    public Text textObject; // 텍스트를 출력할 UI Text 객체
    public float typingSpeed = 0.05f; // 타이핑 속도
    float time; // 시간 변수
    private string targetText; // 타이핑할 텍스트
    public Text StartButton; // 시작 버튼
    private bool StartFade = false; // Update 메서드를 실행하여 페이드 인/아웃 효과를 처리할지 여부

    private void Start()
    {
        StartTyping("마왕에게 잡혀간 바나나캣을 구하러 가자. 꼭대기에 문이 보인다. 저기로 가면 바나나캣이 있을까...?");
        StartFade = false;
    }

    public void Update()
    {
        if (StartFade)
        {
            OnTypewriterComplete(); // 타이핑 효과가 완료되었을 때 실행할 메서드 호출
        }
    }

    // 텍스트를 출력할 객체를 설정하는 메서드
    public void SetTextObject(Text text)
    {
        textObject = text;
    }

    public void StartTyping(string text)
    {
        targetText = text;
        StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        if (textObject == null)
        {
            Debug.LogError("Text object is not assigned to TypewriterEffect.");
            yield break;
        }

        textObject.text = "";

        for (int i = 0; i < targetText.Length; i++)
        {
            textObject.text += targetText[i];
            yield return new WaitForSeconds(typingSpeed);
        }

        StartFade = true; // 타이핑 효과 완료 후 Update 메서드를 실행하여 페이드 인/아웃 효과 처리
    }

    private void OnTypewriterComplete()
    {
        if (time < 0.5f)
        {
            // 시작 버튼의 텍스트를 서서히 투명하게 만들기
            StartButton.GetComponent<Text>().color = new Color(1, 1, 1, 1 - time);
        }
        else
        {
            // 시작 버튼의 텍스트를 서서히 불투명하게 만들기
            StartButton.GetComponent<Text>().color = new Color(1, 1, 1, time);

            if (time > 1f)
            {
                time = 0; // 시간 변수 초기화
            }
        }

        time += Time.deltaTime; // 시간 업데이트
    }
}
