using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TypewriterEffect_2 : MonoBehaviour
{
    public Text textObject;
    public float typingSpeed = 0.05f;
    float time;
    private string targetText;
    public Text StartButton;
    private bool StartFade = false; // Update 메서드를 실행하여 페이드 인/아웃 효과를 처리할지 여부

    private void Start()
    {
        StartTyping("여기에도 바나나캣은 안보인다. 코인 8개를 모으면 숨겨진 문이 활성화 된다. 코인을 모아서 바나나캣이 있는 다음 방으로 가자!");
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
            StartButton.GetComponent<Text>().color = new Color(1, 1, 1, 1 - time);
        }
        else
        {
            StartButton.GetComponent<Text>().color = new Color(1, 1, 1, time);
            if (time > 1f)
            {
                time = 0;
            }
        }

        time += Time.deltaTime;
    }
}
