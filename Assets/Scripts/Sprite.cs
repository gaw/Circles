using UnityEngine;
using System.Collections;
// Создание прямоугольного меша
[RequireComponent (typeof(MeshFilter))]
[RequireComponent (typeof(MeshRenderer))]
public class Sprite : MonoBehaviour 
{
    public Vector2 Size = Vector2.one;                        // Размер меша
    public Vector2 Zero = Vector2.one / 2;                    // Центр меша
    public Rect TextureCoords = Rect.MinMaxRect(0, 0, 1, 1);  // Текстурные координаты
    
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
    }
    
    private void Start()
    {
        InitializeMesh();
    }
    
    private void InitializeMesh()
    {
        Camera cam = Camera.main;
        meshFilter.mesh = CreateMesh(Size, Zero, TextureCoords);
    }
    
    
    // Создание меша заданного размера, с заданным центром и заданными текстурными координатами
    private static Mesh CreateMesh(Vector2 size, Vector2 zero, Rect textureCoords)
    {
        var vertices = new[]
                           {
                             new Vector3(0, 0, 0),   
                             new Vector3(0, size.y, 0),
                             new Vector3(size.x, size.y, 0),
                             new Vector3(size.x, 0, 0)
                           };
    
        Vector3 shift = Vector2.Scale(zero, size);
        for (int i = 0; i < vertices.Length; i++)
        {
          vertices[i] -= shift;
        }
    
        var uv = new[]
            {
              new Vector2(textureCoords.xMin, 1 - textureCoords.yMax),
              new Vector2(textureCoords.xMin, 1 - textureCoords.yMin),
              new Vector2(textureCoords.xMax, 1 - textureCoords.yMin),
              new Vector2(textureCoords.xMax, 1 - textureCoords.yMax)
            };
    
        var triangles = new[]
          {
            0, 1, 2,
            0, 2, 3
          };
    
        return new Mesh { vertices = vertices, uv = uv, triangles = triangles };
    }    
       
    
    // Установка материала меша
    public void SetMaterial(Material material)
    {
        renderer.material = material;
    }
}
