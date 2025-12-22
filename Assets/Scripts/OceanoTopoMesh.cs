using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class OceanoTopoMesh : MonoBehaviour
{
    [Header("Shape")]
    public int segments = 120;          // quantos pontos ao longo do X
    public float length = 6f;          // largura total em X
    public float baseGap = 0.4f;        // distância média entre as linhas
    public float minGap = 0.05f;        // evita colapsar em zero (opcional)

    [Header("Animation")]
    public float speed = 3f;

    [Header("Top wave")]
    public float topAmp = 0.8f;
    public float topFreq = 5f;
    public float topNoiseAmp = 0.6f;
    public float topNoiseScale = 0.35f;

    [Header("Bottom wave")]
    public float bottomAmp = 0.8f;
    public float bottomFreq = 3f;
    public float bottomNoiseAmp = 0.6f;
    public float bottomNoiseScale = 0.35f;

    [Header("Offsets")]
    public float topCenterY = 0.8f;
    public float bottomCenterY = -20f;

    Mesh mesh;
    Vector3[] vertices;
    Vector2[] uvs;
    int[] triangles;

    void Awake()
    {
        mesh = new Mesh();
        mesh.name = "RibbonMesh";
        GetComponent<MeshFilter>().sharedMesh = mesh;

        BuildBuffers();
        RebuildStaticTopology();
    }

    void OnValidate()
    {
        segments = Mathf.Max(2, segments);
        length = Mathf.Max(0.01f, length);
        baseGap = Mathf.Max(0f, baseGap);
        minGap = Mathf.Max(0f, minGap);
    }

    void BuildBuffers()
    {
        int vertCount = (segments + 1) * 2;          // 2 vértices por amostra
        vertices = new Vector3[vertCount];
        uvs = new Vector2[vertCount];

        int triCount = segments * 2;                 // 2 triângulos por “quad”
        triangles = new int[triCount * 3];
    }

    void RebuildStaticTopology()
    {
        // UVs e triângulos (topologia fixa)
        for (int i = 0; i <= segments; i++)
        {
            float u = (float)i / segments;
            int v0 = i * 2;       // bottom
            int v1 = i * 2 + 1;   // top

            uvs[v0] = new Vector2(u, 0f);
            uvs[v1] = new Vector2(u, 1f);
        }

        int ti = 0;
        for (int i = 0; i < segments; i++)
        {
            int a = i * 2;       // bottom i
            int b = i * 2 + 1;   // top i
            int c = (i + 1) * 2;     // bottom i+1
            int d = (i + 1) * 2 + 1; // top i+1

            // tri 1: a, b, d
            triangles[ti++] = a;
            triangles[ti++] = b;
            triangles[ti++] = d;

            // tri 2: a, d, c
            triangles[ti++] = a;
            triangles[ti++] = d;
            triangles[ti++] = c;
        }

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = triangles;
        mesh.RecalculateBounds();
    }

    void Update()
    {
        float t = Time.time * speed;

        float half = length * 0.5f;

        for (int i = 0; i <= segments; i++)
        {
            float u = (float)i / segments;
            float x = Mathf.Lerp(-half, half, u);

            float yTop = TopY(x, t);
            float yBottom = BottomY(x, t);

            // Se cruzou, troca para manter "top" acima de "bottom"
            if (yBottom > yTop)
            {
                float mid = (yBottom + yTop) * 0.5f;
                float gap = Mathf.Max(minGap, Mathf.Abs(yBottom - yTop));
                yTop = mid + gap * 0.5f;
                yBottom = mid - gap * 0.5f;
            }

            // Opcional: força uma distância mínima/média (se quiser controlar “abre/fecha”)
            if (baseGap > 0f)
            {
                float mid = (yTop + yBottom) * 0.5f;
                float gapNow = Mathf.Max(minGap, (yTop - yBottom));
                float targetGap = Mathf.Max(minGap, baseGap);
                float k = 0.0f; // 0 = não força / 1 = força total
                float finalGap = Mathf.Lerp(gapNow, targetGap, k);
                yTop = mid + finalGap * 0.5f;
                yBottom = mid - finalGap * 0.5f;
            }

            int v0 = i * 2;
            int v1 = i * 2 + 1;

            vertices[v0] = new Vector3(x, yBottom, 0f);
            vertices[v1] = new Vector3(x, yTop, 0f);
        }

        mesh.vertices = vertices;
        mesh.RecalculateNormals(); // se usar material que precise
        mesh.RecalculateBounds();
    }

    float TopY(float x, float t)
    {
        float s = Mathf.Sin((x * topFreq) + t);
        float n = (Mathf.PerlinNoise(x * topNoiseScale + 10.123f, t * 0.25f) - 0.5f) * 2f;
        return topCenterY + s * topAmp + n * topNoiseAmp;
    }

    float BottomY(float x, float t)
    {
        float s = Mathf.Sin((x * bottomFreq) + t + 1.7f);
        float n = (Mathf.PerlinNoise(x * bottomNoiseScale + 55.77f, t * 0.25f + 3.3f) - 0.5f) * 2f;
        return bottomCenterY + s * bottomAmp + n * bottomNoiseAmp;
    }
}
