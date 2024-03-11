using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TypewriterEffect_3 : MonoBehaviour
{
    public Text textObject;
    public float typingSpeed = 0.05f;
    float time;
    private string targetText;
    public Text StartButton;
    private bool StartFade = false; // Update 메서드를 실행하여 페이드 인/아웃 효과를 처리할지 여부

    private void Start()
    {
        StartTyping("드디어 바나나캣을 찾았다! 아래에서 톱니바퀴가 올라오고 있다. 여러 장애물들을 피해서 바나나캣을 구출하자!");
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
