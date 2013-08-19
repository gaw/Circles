using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TextureManager
{
    public Texture2D Atlas { get; private set; }      // Текущий пак текстур
    private Rect[] _pieces;                           // Список координат текстур в паке
    
    // Создание пака текстур
    public void GenerateTexturePack () 
    {   
        var texture = new Texture2D(256, 256, TextureFormat.ARGB32, false);
        texture.filterMode = FilterMode.Point;     
        
        // Создание отдельных текстур
        var textures = new List<Texture2D>();
        for(var i = 0; i < 4; i++)
            textures.Add(GenerateTexture(64));
        for(var i = 0; i < 4; i++)
            textures.Add(GenerateTexture(32));
        
        // Объедение в атлас
        _pieces = texture.PackTextures(textures.ToArray(), 0);                
        
        Atlas = texture;
    }
    
    
    // Создание одной текстуры шарика указанного размера
    private Texture2D GenerateTexture(int size)
    {
        var texture = new Texture2D(size, size, TextureFormat.ARGB32, false);
        
        var center = size / 2;
        
        // Основной цвет шарика
        var mainColor = new Color(Random.value, Random.value,Random.value);
        
        var transparentColor = new Color(1.0F, 1.0F, 1.0F, 0.0F);

        for(var i = 0; i < size; i++)
        {
            for(var j = 0; j < size; j++)                
            {
                // Если координаты пикселя выходят за пределы окружности, устанавливается прозрачный цвет
                var radius = GetDistance(i, j, center, center);
                if(radius > center)  
                {
                    texture.SetPixel((int)i, (int)j, transparentColor);
                    continue;
                }
                                
                var color = AddBrightness(mainColor, ((float)j - (float)i) / ((float)(center * 4)) + Random.value * 0.1F);                        
                texture.SetPixel((int)i, (int)j, color);  
            }
        }
        texture.Apply();
        
        return texture;
    }     
    
    
    
    // Получение текстурных координат сдучайной текстуры, подходящей по размеру
    public Rect GetTextureCoordinats(int size)
    {
        var suitablePieces = new List<Rect>();
        var min = int.MaxValue;
        // Поиск текстур, максимально блзких по размеру
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
        
        // Выбор случаной текстуры среди найденных
        return suitablePieces[Random.Range(0, suitablePieces.Count)];
    }
    
    
    // Расстояние между двумя точками плоскости
    private double GetDistance(int x, int y, int x2, int y2)
    {
        return Mathf.Sqrt((x-x2)*(x-x2) + (y-y2)*(y-y2));
    }
    
    
    // Изменение яркости цвета
    private Color AddBrightness(Color color, float v)
    {
        return new Color(color.r + v, color.g + v, color.b + v, 1.0F);
    }    
    
    
    // Случайное изменение цвета
    private Color AddNoise(Color color, float noise)
    {
        return new Color(color.r + Random.value * noise, color.g + Random.value * noise, color.b + Random.value * noise, 1.0F);
    }
}
