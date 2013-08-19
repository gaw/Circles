using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))] 
public class Ortho2DCamera : MonoBehaviour {
    private void Awake()
    {
        camera.orthographic = true;
        SetUniform();
    } 
    
    
    private void LateUpdate()
    {
        // Соохранение текущих размеров камеры в синглтоне
        if(Singleton.Instance.ScreenWidth != camera.pixelWidth) Singleton.Instance.ScreenWidth = camera.pixelWidth;
        if(Singleton.Instance.ScreenHeight != camera.pixelHeight) Singleton.Instance.ScreenHeight = camera.pixelHeight;
        
        SetUniform();
    } 
    
    
    // Установка размера камеры для соответствия 1:1 единицы пространства к экранному пикселю
    private void SetUniform()
    {
        float orthographicSize = camera.pixelHeight/2;
    	if (orthographicSize != camera.orthographicSize)
            camera.orthographicSize = orthographicSize;
    }
}