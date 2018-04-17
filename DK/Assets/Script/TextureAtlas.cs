using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TextureAtlas {

    public static readonly TextureAtlas Instance = new TextureAtlas();
    public Texture2D Atlas { get { return atlas; } }
    private Texture2D atlas;

    public void CreateAtlas()
    {
        string[] _images = Directory.GetFiles("Assets/Textures/Blocks/");
        List<KeyValuePair<string, Texture2D>> _textures = new List<KeyValuePair<string, Texture2D>>();
        foreach(string imagePath in _images)
        {
            byte[] bytes = File.ReadAllBytes(imagePath);
            Texture2D texture = new Texture2D(0, 0);
            texture.LoadImage(bytes);
            if (texture.width != 16 || texture.height != 16)
                continue;
            
            _textures.Add(new KeyValuePair<string, Texture2D>(GetFileName(imagePath), texture));

            //Debug.Log(imagePath);
        }

        int pixelWidth = 16;
        int pixelHeight = 16;

        int atlasWidth = Mathf.CeilToInt((Mathf.Sqrt(_textures.Count) + 0) * pixelWidth);
        int atlasHeight = Mathf.CeilToInt((Mathf.Sqrt(_textures.Count) + 0) * pixelHeight);

        atlas = new Texture2D(atlasWidth, atlasHeight);

        int count = 0;
        for(int x = 0; x < atlasWidth / pixelWidth; ++x)
        {
            for(int y = 0; y < atlasHeight / pixelHeight; ++y)
            {
                if (count >= _textures.Count - 1)
                    goto end;

                atlas.SetPixels(x * pixelWidth, y * pixelHeight, pixelWidth, pixelHeight, _textures[count].Value.GetPixels());

                float startx = x * pixelWidth;
                float starty = y * pixelHeight;
                float perpixelratiox = 1.0f / (float)atlas.width;
                float perpixelratioy = 1.0f / (float)atlas.height;
                startx *= perpixelratiox;
                starty *= perpixelratioy;
                float endx = startx + (perpixelratiox * pixelWidth);
                float endy = starty + (perpixelratioy * pixelHeight);

                UVMap map = new UVMap(_textures[count].Key, new Vector2[] {
                    new Vector2(startx, starty),
                    new Vector2(startx, endy),
                    new Vector2(endx, starty),
                    new Vector2(endx, endy),
                });

                map.Register();

                ++count;
            }
        }

        end:;
            File.WriteAllBytes("Assets/Atlas/atlas.png", atlas.EncodeToPNG());

        atlas.filterMode = FilterMode.Point;
    }

    public string GetFileName(string path)
    {
        string fileName = string.Empty;
        if(!string.IsNullOrEmpty(path))
        {
            string[] paths = path.Split('/');
            if(paths.Length > 0 )
                fileName = paths[paths.Length - 1];
        }
        return fileName.Substring(0, fileName.Length - 4); // get rid of .png
    }
}

