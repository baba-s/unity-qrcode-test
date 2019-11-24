using System.IO;
using UnityEditor;
using UnityEngine;
using ZXing;
using ZXing.QrCode;

public static class QRCodeWriter
{
    [MenuItem( "Tools/Create QRCode" )]
    private static void CreateQRCode()
    {
        var path     = Application.dataPath + "/QRCode.png";
        var contents = "Pikachu";
        var width    = 256;
        var height   = 256;

        var writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
                Width  = width,
                Height = height
            }
        };

        var format = TextureFormat.ARGB32;
        var texture = new Texture2D( width, height, format, false );
        var colors  = writer.Write( contents );

        texture.SetPixels32( colors );
        texture.Apply();

        using ( var stream = new FileStream( path, FileMode.OpenOrCreate ) )
        {
            var bytes = texture.EncodeToPNG();
            stream.Write( bytes, 0, bytes.Length );
        }

        AssetDatabase.Refresh();
    }
}