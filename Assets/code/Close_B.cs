using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Close_B : MonoBehaviour
{
    GameObject gStoryB = null;
    GameObject gStoryTXT = null;
    GameObject gTitle = null;
    GameObject ManualTxt = null;
    // Start is called before the first frame update
    void Start()
    {
        gStoryB = GameObject.Find("StoryB");
        gStoryTXT = GameObject.Find("StoryTxt");
        gTitle = GameObject.Find("Title");
        ManualTxt = GameObject.Find("ManualTxt");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ClsoeButton()
    {
        gStoryTXT.SetActive(false);
        ManualTxt.SetActive(false);
    }
}
