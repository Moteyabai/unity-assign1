using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenTransition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnStart()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1.0f;
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
