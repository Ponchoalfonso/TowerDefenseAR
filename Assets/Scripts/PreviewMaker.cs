using System.IO;
using UnityEngine;

public class PreviewMaker : MonoBehaviour
{
    public bool create;
    public RenderTexture texture;
    public Camera bakeCam;

    public string spriteName;

    void Update()
    {
        if (create)
        {
            CreateIcon();
            create = false;
        }
    }

    void CreateIcon()
    {
        if (string.IsNullOrEmpty(spriteName))
            spriteName = "Preview";

        string path = SavePath() + spriteName;
        bakeCam.targetTexture = texture;

        RenderTexture currentRT = RenderTexture.active;
        bakeCam.targetTexture.Release();
        RenderTexture.active = bakeCam.targetTexture;
        bakeCam.Render();

        Texture2D imgPng = new Texture2D(texture.width, texture.height, TextureFormat.ARGB32, false);
        imgPng.ReadPixels(new Rect(0, 0, texture.width, texture.height), 0, 0);
        imgPng.Apply();
        RenderTexture.active = currentRT;
        byte[] bytesPng = imgPng.EncodeToPNG();
        File.WriteAllBytes(path +".png", bytesPng);

        Debug.Log("Icon created");
    }

    string SavePath()
    {
        string saveLocation = Application.streamingAssetsPath + "/UI/";

        if (!Directory.Exists(saveLocation))
            Directory.CreateDirectory(saveLocation);

        return saveLocation;
    }
}
