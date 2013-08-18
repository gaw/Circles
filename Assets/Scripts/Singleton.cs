using UnityEngine;
using System.Collections;
using System;

public class Singleton
{
    public static readonly Singleton Instance = new Singleton();
    
    public Singleton()
    {
        _textureManager = new TextureManager();
        _textureManager.GenerateTexturePack();
        
        _guiTextScore = GameObject.Find("GuiScore").GetComponent(typeof(GUIText)) as GUIText;
        _guiTextTime = GameObject.Find("GuiTime").GetComponent(typeof(GUIText)) as GUIText;
        _guiTextLevel = GameObject.Find("GuiLevel").GetComponent(typeof(GUIText)) as GUIText;
    }    
    
    public float ScreenWidth;
    public float ScreenHeight;
    
    public float Bottom { get { return -ScreenHeight / 2; } }
    public float Top { get { return ScreenHeight / 2; } }
    
    private TextureManager _textureManager;
    public TextureManager TextureManager
    {
        get { return _textureManager; }
    }
    
    
    #region Score
    
    private GUIText _guiTextScore;
    public int Score { get; private set; }
    
    public void AddScore(int score)
    {
        Score += score;
        UpdateScore();
    }
    
    public void UpdateScore()
    {
        _guiTextScore.text = string.Format("Score: {0}", Score);        
    }
    
    #endregion
    
    
    #region Time
    
    private GUIText _guiTextTime;
    private System.DateTime _startTime;
    public int SecondsElapsed = int.MinValue;
    
    public void StartTime()
    {
        _startTime = System.DateTime.Now;
        UpdateTime();
    }
    
    public void UpdateTime()
    {
        var seconds = (int)((DateTime.Now - _startTime).TotalSeconds);
        if(seconds != SecondsElapsed)
        {
            SecondsElapsed = seconds;
            _guiTextTime.text = string.Format("Time: {0}", SecondsElapsed);
        }
    }
    
    #endregion
    
    
    #region Level
    
    private GUIText _guiTextLevel;
    public int Level = 1;
    
    public void NextLevel()
    {
        Level++;        
        UpdateLevel();
        _textureManager.GenerateTexturePack();
    }    
    
    public void UpdateLevel()
    {
        _guiTextLevel.text = string.Format("Level: {0}", Level);
    }
    
    #endregion
    
    
    public float GetSpeedBySize(float size)
    {
        return (200 - size) * 2 + 30 * Level;
    }
    
    
    public int GetScoreBySize(float size)
    {
        return (150 - (int)size) * 2;
    }
}
