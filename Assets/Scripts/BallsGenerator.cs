using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BallsGenerator : MonoBehaviour {
    public Transform Prefab;               // Префаб спрайта
    public float Interval;                 // Интервал между появлениями новых шариков
    private float _elapsedSeconds = 0;     // Время, прошедшее с последней генерации
    private Material _material;            // Материал шарика
    
    private List<GameObject> _balls = new List<GameObject>();    // Список существующих шариков
        
	void Start () 
    {
        // Загрузка бандла
        //string path = string.Format("file://{0}/Ball.unity3d", Application.streamingAssetsPath);
        var path = "https://dl.dropboxusercontent.com/u/2252362/Ball.unity3d";
        StartCoroutine(DownloadAndCache(path, 1));
	}
    
    
    // Загрузка ресурсов из бандла
    private IEnumerator DownloadAndCache(string url, int version)
    {
        while (!Caching.ready)
            yield return null;
        
        using(WWW loader = WWW.LoadFromCacheOrDownload (url, version))
        {
            yield return loader;
            if (loader.error != null)
            {
                Debug.Log(loader.error);
                Singleton.Instance.GuiTextMessage.text = loader.error;
                return false;
            }
            
            var bundle = loader.assetBundle;
            
            // Загрузка материала из бандла
            _material = bundle.Load("Unlit", typeof(Material)) as Material;
            
            bundle.Unload(false);
        }
        
        OnLoad();        
    }
    
    
    private bool _isLoaded = false;
    private void OnLoad()
    {
        Singleton.Instance.GuiTextScore.enabled = true;
        Singleton.Instance.GuiTextTime.enabled = true;
        Singleton.Instance.GuiTextLevel.enabled = true;
        Singleton.Instance.GuiTextMessage.enabled = false;
        
        // Инициализация времени. Отображение текущих очков, текущего уровня.
        Singleton.Instance.StartTime();
        Singleton.Instance.UpdateScore();        
        Singleton.Instance.UpdateLevel();
        
        _isLoaded = true;
    }
    
    
	void Update () {
        // Если бандл ещё не загружен, выход
        if(!_isLoaded) return;
        
        // Обновление текущего времени
        Singleton.Instance.UpdateTime();
        
        // Удаление из списка всех удалённх шариков
        _balls.RemoveAll(a => a == null);
        
        // Проверка разрешена ли генерация шариков
        if(!CanGenerate()) return;
        
        // Если количество очков превышает определённое число, то запуск перехода на следующий уровень
        if(Singleton.Instance.Score / 500 + 1 > Singleton.Instance.Level) NextLevel();
                
	    _elapsedSeconds += Time.deltaTime;
        while(_elapsedSeconds > Interval)   // Если время с прошлой генерации, превысило интервал между появлениями шариков
        {
            _elapsedSeconds -= Interval;
            
            var width = Singleton.Instance.ScreenWidth;
            var y = Singleton.Instance.Top;
            
            // Создание шарика случаного размера
            var size = Random.Range(32, 129); 
            var ball = Instantiate (Prefab) as Transform;                        
            _balls.Add(ball.gameObject);            
            
            // Ининциализация компонентов шарика
            var md = ball.GetComponent(typeof(MovingDown)) as MovingDown;
            md.Size = size;            
            var sprite = ball.GetComponent(typeof(Sprite)) as Sprite;
            sprite.Size = new Vector2(size, size);            
            var collider = ball.GetComponent(typeof(SphereCollider)) as SphereCollider;
            collider.radius = size / 2;            
            var scoreOnClick = ball.GetComponent(typeof(ScoreOnClick)) as ScoreOnClick;
            scoreOnClick.Score = Singleton.Instance.GetScoreBySize(size);
            
            // Установка материала шарика
            sprite.TextureCoords = Singleton.Instance.TextureManager.GetTextureCoordinats(size);
            _material.mainTexture = Singleton.Instance.TextureManager.Atlas;
            sprite.SetMaterial(_material);
                        
            ball.position = new Vector3(Random.Range(-width / 2 + size, width / 2 - size), y + size, 0);
        }        
	}
    
    
    private bool _canGenerate = true;
    
    // Переход на следующий уровень
    void NextLevel()
    {
        _canGenerate = false;
        Interval *= 0.8F;
    }
    
    
    // Разрешена ли генерация шариков. Запрет длится во время перехода на следующий уровень
    // до тех пор, пока с экрана не исчезнут все шарики
    private bool CanGenerate()
    {
        if(_canGenerate) return true;
        
        if(_balls.Count > 0) return false;
        
        Singleton.Instance.NextLevel();
        _canGenerate = true;
        return true;
    }    
}
