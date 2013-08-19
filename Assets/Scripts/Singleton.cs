using UnityEngine;
using System.Collections;
using System;

// Общие данные
public class Singleton
{
    public static readonly Singleton Instance = new Singleton();
    
    public Singleton()
    {
        // Инициализация текстурного менеджера и генерация первого пака текстур
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
    
    // Умеличение количества очков
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
    
    // Инициализация счётчика времени
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
    
    // Переход на следующий уровень
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
    
    // Получение скорости шарика по его размеру
    public float GetSpeedBySize(float size)
    {
        return (200 - size) * 2 + 30 * Level;
    }
    
    
    // Получение количества очков за шарик по его размеру
    public int GetScoreBySize(float size)
    {
        return (150 - (int)size) * 2;
    }
}
