using UnityEngine;
using System.Collections.Generic;

public class ForceDirectedGraph : MonoBehaviour
{
    [Header("Graph Settings")]
    public int nodeCount = 100;
    public float edgeProbability = 0.03f;
    public float nodeSize = 0.5f;
    public float initialSpread = 20f;

    [Header("Physics")]
    public float repulsionStrength = 50f;
    public float attractionStrength = 0.05f;
    public float restLength = 3f;
    public float centerGravity = 0.02f;
    public float damping = 0.95f;
    public float maxForce = 10f;
    public float minDistance = 0.5f;

    [Header("Visuals")]
    public Color lineColor = new Color(1f, 1f, 1f, 0.4f);
    public float lineWidth = 0.04f;

    private struct NodeData
    {
        public Transform transform;
        public Vector3 velocity;
        public MaterialPropertyBlock propBlock;
    }

    private struct Edge
    {
        public int a;
        public int b;
        public LineRenderer line;
    }

    private NodeData[] nodes;
    private List<Edge> edges;
    private Material lineMaterial;

    void Start()
    {
        lineMaterial = new Material(Shader.Find("Sprites/Default"));

        nodes = new NodeData[nodeCount];
        edges = new List<Edge>();

        // Spawn nodes as cubes
        for (int i = 0; i < nodeCount; i++)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.name = "Node_" + i;
            cube.transform.SetParent(transform);
            cube.transform.localPosition = Random.insideUnitSphere * initialSpread;
            cube.transform.localScale = Vector3.one * nodeSize;

            // Remove collider — not needed for custom simulation
            Destroy(cube.GetComponent<Collider>());

            // Random color via MaterialPropertyBlock
            var propBlock = new MaterialPropertyBlock();
            Color color = Color.HSVToRGB(Random.value, 0.7f, 0.9f);
            propBlock.SetColor("_Color", color);
            cube.GetComponent<Renderer>().SetPropertyBlock(propBlock);

            nodes[i] = new NodeData
            {
                transform = cube.transform,
                velocity = Vector3.zero,
                propBlock = propBlock
            };
        }

        // Build spanning tree first to guarantee connectivity
        for (int i = 1; i < nodeCount; i++)
        {
            int j = Random.Range(0, i);
            AddEdge(i, j);
        }

        // Overlay random edges (Erdős–Rényi)
        for (int i = 0; i < nodeCount; i++)
        {
            for (int j = i + 1; j < nodeCount; j++)
            {
                if (Random.value < edgeProbability)
                {
                    if (!HasEdge(i, j))
                        AddEdge(i, j);
                }
            }
        }
    }

    private bool HasEdge(int a, int b)
    {
        for (int i = 0; i < edges.Count; i++)
        {
            if ((edges[i].a == a && edges[i].b == b) ||
                (edges[i].a == b && edges[i].b == a))
                return true;
        }
        return false;
    }

    private void AddEdge(int a, int b)
    {
        GameObject lineObj = new GameObject("Edge_" + a + "_" + b);
        lineObj.transform.SetParent(transform);

        LineRenderer lr = lineObj.AddComponent<LineRenderer>();
        lr.material = lineMaterial;
        lr.startColor = lineColor;
        lr.endColor = lineColor;
        lr.startWidth = lineWidth;
        lr.endWidth = lineWidth;
        lr.positionCount = 2;
        lr.useWorldSpace = true;

        edges.Add(new Edge { a = a, b = b, line = lr });
    }

    void FixedUpdate()
    {
        float dt = Time.fixedDeltaTime;

        // Repulsion: all pairs
        for (int i = 0; i < nodeCount; i++)
        {
            for (int j = i + 1; j < nodeCount; j++)
            {
                Vector3 diff = nodes[i].transform.position - nodes[j].transform.position;
                float dist = diff.magnitude;
                if (dist < minDistance) dist = minDistance;

                Vector3 force = diff.normalized * (repulsionStrength / (dist * dist));
                force = Vector3.ClampMagnitude(force, maxForce);

                nodes[i].velocity += force * dt;
                nodes[j].velocity -= force * dt;
            }
        }

        // Attraction: connected pairs (spring)
        for (int e = 0; e < edges.Count; e++)
        {
            Vector3 diff = nodes[edges[e].b].transform.position - nodes[edges[e].a].transform.position;
            float dist = diff.magnitude;
            float displacement = dist - restLength;
            Vector3 force = diff.normalized * (attractionStrength * displacement);
            force = Vector3.ClampMagnitude(force, maxForce);

            nodes[edges[e].a].velocity += force * dt;
            nodes[edges[e].b].velocity -= force * dt;
        }

        // Center gravity + damping + integration
        for (int i = 0; i < nodeCount; i++)
        {
            // Pull toward origin
            nodes[i].velocity -= nodes[i].transform.position * centerGravity * dt;

            // Damping
            nodes[i].velocity *= damping;

            // Integrate
            nodes[i].transform.position += nodes[i].velocity * dt;
        }
    }

    void LateUpdate()
    {
        // Sync line renderer positions
        for (int e = 0; e < edges.Count; e++)
        {
            edges[e].line.SetPosition(0, nodes[edges[e].a].transform.position);
            edges[e].line.SetPosition(1, nodes[edges[e].b].transform.position);
        }
    }

    void OnDestroy()
    {
        if (lineMaterial != null)
            Destroy(lineMaterial);
    }
}
