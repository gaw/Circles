using UnityEngine;
using System.Collections;

public class ScoreOnClick : MonoBehaviour 
{
    public int Score;
    
    void OnMouseDown()   
    {
        Singleton.Instance.AddScore(Score);
        Destroy(gameObject);
    }
}
