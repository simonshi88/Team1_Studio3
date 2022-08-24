using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ScreenShots : MonoBehaviour
{
    public int superSize = 2;
    private int _shotIndex = 0;
    Scene scene;
    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ScreenCapture.CaptureScreenshot(Application.dataPath + $"/screenshots/{scene.name}{_shotIndex}.png", superSize);
            _shotIndex++;
        }
    }
}
