using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartB : MonoBehaviour
{
    public Image fadeImage; // 페이드 효과에 사용할 이미지
    public Image Stage1; // Stage1 이미지
    public float fadeDuration = 1.5f; // 페이드 인/아웃 지속 시간

    private void Start()
    {
        Button button = GetComponent<Button>(); // 현재 GameObject에서 Button 컴포넌트 가져오기
        button.onClick.AddListener(OnClickButton); // 버튼 클릭 이벤트에 OnClickButton 메서드 추가
    }

    private void OnClickButton()
    {
        StartCoroutine(FadeAndLoadScene("Stage1")); // 페이드 효과와 함께 Scene 로딩하기
    }

    IEnumerator FadeAndLoadScene(string sceneName)
    {
        fadeImage.gameObject.SetActive(true); // 페이드 이미지 활성화

        // Fade in
        float time = 0f;
        Color startingColor = fadeImage.color;
        Color targetColor = new Color(startingColor.r, startingColor.g, startingColor.b, 1f);

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            fadeImage.color = Color.Lerp(startingColor, targetColor, time / fadeDuration);
            yield return null;
        }

        // Stage1 이미지를 페이드 인 애니메이션과 함께 표시
        Stage1.gameObject.SetActive(true);
        float imageTime = 0f;
        Color startingImageColor = Stage1.color;
        Color targetImageColor = new Color(startingImageColor.r, startingImageColor.g, startingImageColor.b, 1f);

        while (imageTime < fadeDuration)
        {
            imageTime += Time.deltaTime;
            Stage1.color = Color.Lerp(startingImageColor, targetImageColor, imageTime / fadeDuration);
            yield return null;
        }

        // 씬 로드 작업 시작
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // 씬 로드가 완료될 때까지 대기
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
