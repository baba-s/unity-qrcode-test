using UnityEngine;
using UnityEngine.UI;
using ZXing;

public sealed class QRCodeReader : MonoBehaviour
{
    public Text     m_text;
    public RawImage m_rawImage;

    private WebCamTexture m_webCamTexture;

    private void Awake()
    {
        var width  = Screen.width;
        var height = Screen.height;

        m_webCamTexture = new WebCamTexture( width, height );
        m_webCamTexture.Play();

        m_rawImage.texture = m_webCamTexture;
    }

    private void Update()
    {
        m_text.text = Read( m_webCamTexture );
    }

    private static string Read( WebCamTexture texture )
    {
        var reader = new BarcodeReader();
        var rawRGB = texture.GetPixels32();
        var width  = texture.width;
        var height = texture.height;
        var result = reader.Decode( rawRGB, width, height );

        return result != null ? result.Text : string.Empty;
    }
}