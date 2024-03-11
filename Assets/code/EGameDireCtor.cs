using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EGameDireCtor : MonoBehaviour
{
    
    void Start()
    {
       
    }

    void Update()
    {
       
    }

    // 타이틀로 돌아가는 함수
    public void ReTilte()
    {
        SceneManager.LoadScene("StartScene"); // StartScene으로 씬을 로드하여 타이틀 화면으로 이동
    }
}
