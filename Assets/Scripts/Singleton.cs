using UnityEngine;
using System.Collections;

public class Singleton
{
    public static readonly Singleton Instance = new Singleton();
    
    public Singleton()
    {
        _ballTypes = new BallType[]
            {
                new BallType { Size = 128, Speed = 100, Score = 1 },
                new BallType { Size = 64, Speed = 150, Score = 2 },
                new BallType { Size = 32, Speed = 200, Score = 5 },
                new BallType { Size = 16, Speed = 300, Score = 10 }
            };
    }    
    
    public float ScreenWidth;
    public float ScreenHeight;
    
    public float Bottom { get { return -ScreenHeight / 2; } }
    public float Top { get { return ScreenHeight / 2; } }
    
    private BallType[] _ballTypes;
    public BallType GetRandomBallType()
    {
        Debug.Log(Random.Range(0, 1));
        return _ballTypes[Random.Range(0, _ballTypes.Length)];
    }
}


public class BallType
{
    public int Size;
    public int Speed;
    public int Score;
}
