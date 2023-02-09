using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TakeScreenShot : MonoBehaviour
{
    private static TakeScreenShot instance;
    public string fileName;
    
    private Camera _camera;
    private bool takeScreenshotOnNextFrame;

    private void Awake()
    {
        instance = this;
        _camera = gameObject.gameObject.GetComponent<Camera>();
    }

    private void OnPostRender()
    {
        if (takeScreenshotOnNextFrame)
        {
            takeScreenshotOnNextFrame = false;
            RenderTexture renderTexture = _camera.targetTexture;

            Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
            Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
            renderResult.ReadPixels(rect, 0, 0);

            byte[] byteArray = renderResult.EncodeToPNG();
            System.IO.File.WriteAllBytes(Application.dataPath + "/" + fileName + ".png", byteArray);
            Debug.Log("Saved " + fileName +".png");

            RenderTexture.ReleaseTemporary(renderTexture);
            _camera.targetTexture = null;
        }
    }

    private void TakeScreenshot(int width, int height)
    {
        _camera.targetTexture = RenderTexture.GetTemporary(width, height, 16);
        takeScreenshotOnNextFrame = true;
    }

    public static void TakeScreenshot_Static(int width, int height)
    {
        instance.TakeScreenshot(width, height);
    }
}
