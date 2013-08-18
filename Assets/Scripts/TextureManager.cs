using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TextureManager
{
    public Texture2D Atlas { get; private set; }
    private Rect[] _pieces;
    
    public void GenerateTexturePack () 
    {           
        var texture = new Texture2D(256, 256, TextureFormat.ARGB32, false);
        texture.filterMode = FilterMode.Point;     
        
        var mainColor = new Color(0.5F, 0.4F, 0.3F);
        mainColor = AddNoise(mainColor, 0.3F);
        
        var textures = new List<Texture2D>();
        for(var i = 0; i < 4; i++)
            textures.Add(GenerateTexture(mainColor, 64, new Vector2(0, 0)));
        for(var i = 0; i < 4; i++)
            textures.Add(GenerateTexture(mainColor, 32, new Vector2(0, 0)));
        
        _pieces = texture.PackTextures(textures.ToArray(), 0);                
        
        Atlas = texture;
    }
    
    
    private Texture2D GenerateTexture(Color mainColor, int size, Vector2 position)
    {
        var texture = new Texture2D(size, size, TextureFormat.ARGB32, false);
        
        var center = size / 2;
                
        mainColor = AddNoise(mainColor, 0.1F);        
        mainColor = AddBrightness(mainColor, Random.value * 0.2F - 0.1F);
        
        var transparentColor = new Color(1.0F, 1.0F, 1.0F, 0.0F);

        for(var i = 0; i < size; i++)
        {
            for(var j = 0; j < size; j++)                
            {
                var radius = GetDistance(i, j, center, center);
                if(radius > center)
                {
                    texture.SetPixel((int)position.x + i, (int)position.y + j, transparentColor);
                    continue;
                }
                
                var color = AddBrightness(mainColor, ((float)j - (float)i) / ((float)(center * 4)) + Random.value * 0.1F);                        
                texture.SetPixel((int)position.x + i, (int)position.y + j, color);  
            }
        }
        texture.Apply();
        
        return texture;
    }
    
    
    public Rect GetTextureCoordinats(int size)
    {
        var suitablePieces = new List<Rect>();
        var min = int.MaxValue;
        foreach(Rect a in _pieces)
        {
            var delta = (int)Mathf.Abs(a.width * Atlas.width - size);
            
            if(delta < min)
            {
                min = delta;
                suitablePieces.Clear();
            }
            
            if(delta == min)
            {
                suitablePieces.Add(a);
            }
        }
        if(suitablePieces.Count == 0) return _pieces[0];
        
        return suitablePieces[Random.Range(0, suitablePieces.Count)];
    }
    
    
    private double GetDistance(int x, int y, int x2, int y2)
    {
        return Mathf.Sqrt((x-x2)*(x-x2) + (y-y2)*(y-y2));
    }
    
    
    private Color AddBrightness(Color color, float v)
    {
        return new Color(color.r + v, color.g + v, color.b + v, 1.0F);
    }    
    
    
    private Color AddNoise(Color color, float noise)
    {
        return new Color(color.r + Random.value * noise, color.g + Random.value * noise, color.b + Random.value * noise, 1.0F);
    }
}
