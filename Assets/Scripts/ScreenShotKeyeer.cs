using UnityEngine;

#if UNITY_EDITOR

public class ScreenShotKeyeer : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ScreenCapture.CaptureScreenshot("Screenshots/screenshot" +
                                            System.DateTime.Now.ToString("MM-dd-yy (HH-mm-ss)") + ".png");
            Debug.Log("A screenshot was taken!");
        }
    }
}

#endif