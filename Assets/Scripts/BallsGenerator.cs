using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BallsGenerator : MonoBehaviour {
    public Transform Prefab;
    public float Interval;
    private float _elapsedSeconds = 0;
    
    private List<GameObject> _balls = new List<GameObject>();
        
	void Start () {
	    Singleton.Instance.StartTime();
        Singleton.Instance.UpdateScore();        
        Singleton.Instance.UpdateLevel();
	}
	
	void Update () {
        Singleton.Instance.UpdateTime();
        
        _balls.RemoveAll(a => a == null);
        
        if(!CanGenerate()) return;
        
        if(Singleton.Instance.Score / 500 + 1 > Singleton.Instance.Level) NextLevel();
        
	    _elapsedSeconds += Time.deltaTime;
        while(_elapsedSeconds > Interval)
        {
            _elapsedSeconds -= Interval;
            
            var width = Singleton.Instance.ScreenWidth;
            var y = Singleton.Instance.Top;
            var size = Random.Range(32, 129);
            
            var ball = Instantiate (Prefab) as Transform;                        
            _balls.Add(ball.gameObject);            
            
            var md = ball.GetComponent(typeof(MovingDown)) as MovingDown;
            md.Size = size;
            
            var sprite = ball.GetComponent(typeof(Sprite)) as Sprite;
            sprite.size = new Vector2(size, size);
            
            var collider = ball.GetComponent(typeof(SphereCollider)) as SphereCollider;
            collider.radius = size / 2;
            
            var scoreOnClick = ball.GetComponent(typeof(ScoreOnClick)) as ScoreOnClick;
            scoreOnClick.Score = Singleton.Instance.GetScoreBySize(size);
            
            sprite.SetTexture(Singleton.Instance.TextureManager.Atlas);
            sprite.textureCoords = Singleton.Instance.TextureManager.GetTextureCoordinats(size);
                        
            ball.position = new Vector3(Random.Range(-width / 2 + size, width / 2 - size), y + size, 0);
        }        
	}
    
    
    private bool _canGenerate = true;
    
    void NextLevel()
    {
        _canGenerate = false;
        Interval *= 0.8F;
    }
    
    
    private bool CanGenerate()
    {
        if(_canGenerate) return true;
        
        if(_balls.Count > 0) return false;
        
        Singleton.Instance.NextLevel();
        _canGenerate = true;
        return true;
    }    
}
