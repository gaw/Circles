using UnityEngine;
using System.Collections;

public class MovingDown : MonoBehaviour {
    public float Size;
    
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        var delta = Time.deltaTime * Singleton.Instance.GetSpeedBySize(Size);
        transform.position = new Vector3(transform.position.x, transform.position.y - delta, transform.position.z);
        
        if(transform.position.y < Singleton.Instance.Bottom - Size) Destroy(gameObject);
	}
}
