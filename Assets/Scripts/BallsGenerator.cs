using UnityEngine;
using System.Collections;

public class BallsGenerator : MonoBehaviour {
    public Transform Prefab;
    public float Interval;
    private float _elapsedSeconds = 0;
        
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    _elapsedSeconds += Time.deltaTime;
        while(_elapsedSeconds > Interval)
        {
            _elapsedSeconds -= Interval;
            
            var width = Singleton.Instance.ScreenWidth;
            var y = Singleton.Instance.Top;
            var ballType = Singleton.Instance.GetRandomBallType();
            
            var ball = Instantiate (Prefab) as Transform;                        
            var tm = ball.GetComponent(typeof(TextureMaker)) as TextureMaker;
            tm.TextureSize = ballType.Size;
            var md = ball.GetComponent(typeof(MovingDown)) as MovingDown;
            md.BallType = ballType;
            var sprite = ball.GetComponent(typeof(Sprite)) as Sprite;
            sprite.size = new Vector2(ballType.Size, ballType.Size);
            
            //ball.localScale = new Vector3(ballType.Size, ballType.Size, 1);
            ball.position = new Vector3(Random.value * width - width / 2, y + ballType.Size, 0);
        }        
	}
}
