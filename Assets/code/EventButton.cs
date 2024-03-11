using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventButton : MonoBehaviour
{
    public GameObject EventPrefab;
   
    GameObject gTitle = null;
    GameObject ManualTxt = null;
   
    void Start()
    {
        gTitle = GameObject.Find("Title");
        ManualTxt = GameObject.Find("ManualTxt");
    }
    void Update()
    {

        
    }
    public void EventBUtton()
    {
        ManualTxt.transform.position = new Vector3(1800, 760, 0);
        ManualTxt.SetActive(true);
    }
}
