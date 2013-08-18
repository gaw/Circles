using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))] 
public class Ortho2DCamera : MonoBehaviour {
  [SerializeField] private bool uniform = true;
  [SerializeField] private bool autoSetUniform = false;
 
    
  private void Awake()
  {
    camera.orthographic = true;

    if (uniform)
      SetUniform();
  } 
    
    
  private void LateUpdate()
  {
    if(Singleton.Instance.ScreenWidth != camera.pixelWidth) Singleton.Instance.ScreenWidth = camera.pixelWidth;
    if(Singleton.Instance.ScreenHeight != camera.pixelHeight) Singleton.Instance.ScreenHeight = camera.pixelHeight;
        
    if (autoSetUniform && uniform)
      SetUniform();
  } 
    
    
  private void SetUniform()
  {
    float orthographicSize = camera.pixelHeight/2;
	if (orthographicSize != camera.orthographicSize)
      camera.orthographicSize = orthographicSize;
  }
}