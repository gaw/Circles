using UnityEngine;
using System.Collections;

// Движение спрайта вниз
public class MovingDown : MonoBehaviour {
    public float Size; // Размер спрайта
    
	void Update () {
        // Движение вниз в зависимости от размера спрайта
        var delta = Time.deltaTime * Singleton.Instance.GetSpeedBySize(Size);        
        transform.position = new Vector3(transform.position.x, transform.position.y - delta, transform.position.z);
        
        // При уходе за нижнюю границу уничтожение спрайта        
        if(transform.position.y < Singleton.Instance.Bottom - Size) Destroy(gameObject);
	}
}
