using UnityEngine;
using System.Collections;

[AddComponentMenu("Sprites/Sprite")]
[RequireComponent (typeof(MeshFilter))]
[RequireComponent (typeof(MeshRenderer))]
public class Sprite : MonoBehaviour 
{
    public Vector2 Size = Vector2.one;
    public Vector2 Zero = Vector2.one / 2;
    public Rect TextureCoords = Rect.MinMaxRect(0, 0, 1, 1);
    
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

    #region Unity messages
    
    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
    }
    
    private void Start()
    {
        InitializeMesh();
    }
    
    #endregion
    
    private void InitializeMesh()
    {
        Camera cam = Camera.main;
        meshFilter.mesh = CreateMesh(Size, Zero, TextureCoords);
    }
    
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
       
    
    private Material _material;
    
    public void SetTexture(Texture2D texture)
    {
        if(_material == null) 
        {
            var bg = FindObjectOfType(typeof(BallsGenerator)) as BallsGenerator;
                        
            _material = bg.Material;
            _material.mainTexture = texture;
        }
        
        renderer.material = _material;
        //renderer.material.mainTexture = ;
    }
}
