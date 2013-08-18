using UnityEngine;
using System.Collections;
using UnityEditor;

public class TextureMaker : MonoBehaviour {
 
    public int TextureSize;
    
	void Start () 
	{	        
        var center = TextureSize / 2;
        
	    var texture = new Texture2D(TextureSize, TextureSize, TextureFormat.ARGB32, false);
        texture.filterMode = FilterMode.Point;
		
        var mainColor = new Color(0.5F, 0.4F, 0.3F);
        mainColor = AddNoise(mainColor, 0.2F);
        
        var transparentColor = new Color(1.0F, 1.0F, 1.0F, 0.0F);

		for(var i = 0; i < TextureSize; i++)
		{
			for(var j = 0; j < TextureSize; j++)				
			{
                var radius = GetDistance(i, j, center, center);
				if(radius > center)
                {
					texture.SetPixel(i, j, transparentColor);
                    continue;
                }
                
                var radius2 = GetDistance(i, j, center, center + 2);
                if(radius2 > center + 1000)
                    texture.SetPixel(i, j, mainColor);
				else {
                    var color = AddBrightness(mainColor, ((float)j - (float)i) / ((float)(center * 4)) + Random.value * 0.1F);
                    //color = GetNoisedColor(color, 0.1F);
                    
	    		    texture.SetPixel(i, j, color);
				}				
			}
		}
	 
	    // Apply all SetPixel calls
	    texture.Apply();
	 
	    // connect texture to material of GameObject this script is attached to
	    renderer.material.mainTexture = texture;
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
