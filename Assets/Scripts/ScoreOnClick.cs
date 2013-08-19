using UnityEngine;
using System.Collections;

// Реакция на щелчок мышью: увеличение очков и уничтожение объекта
public class ScoreOnClick : MonoBehaviour 
{
    public int Score;
    
    void OnMouseDown()   
    {
        Singleton.Instance.AddScore(Score);
        Destroy(gameObject);
    }
}
