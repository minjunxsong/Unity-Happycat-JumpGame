using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_View_B : MonoBehaviour
{
    GameObject gStoryB = null;
    GameObject gStoryTXT = null;
    // Start is called before the first frame update
    void Start()
    {
        gStoryB = GameObject.Find("StoryB");
        gStoryTXT = GameObject.Find("StoryTxt");
    }

    // Update is called once per frame
  
    public void ViewButton()
    {
        gStoryTXT.transform.position = new Vector3(1800, 760, 0);
        gStoryTXT.SetActive(true);
    }

}
