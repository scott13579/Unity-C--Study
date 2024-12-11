using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Graph : MonoBehaviour
{
    public class Vertex
    {
        public string Name;
        public Dictionary<Vertex, float> Neighbors = new Dictionary<Vertex, float>();

        public Vertex(string name)
        {
            this.Name = name;
            this.Neighbors = new Dictionary<Vertex, float>();
        }
    }
    
    private Dictionary<String, Vertex> vertices = new Dictionary<String, Vertex>();


    public void AddVertex(string name)
    {
        if (!vertices.ContainsKey(name))
            vertices.Add(name, new Vertex(name));
    }

    public void AddEdge(string fromName, string toName, float weight)
    {
        if (vertices.ContainsKey(fromName) && vertices.ContainsKey(toName))
        {
            Vertex from = vertices[fromName];
            Vertex to = vertices[toName];

            if (!from.Neighbors.ContainsKey(to))
            {
                from.Neighbors.Add(to, weight);
            }
        }
    }

    public void BFS(string StartName)
    {
        if (!vertices.ContainsKey(StartName))
        {
            return;
        }
        
        HashSet<Vertex> visited = new HashSet<Vertex>();
        Queue<Vertex> queue = new Queue<Vertex>();
        
        Vertex startVertex = vertices[StartName];
        queue.Enqueue(startVertex);
        visited.Add(startVertex);

        while (queue.Count > 0)
        {
            Vertex currentVertex = queue.Dequeue();
            Debug.Log($"방문 : {currentVertex.Name}");

            foreach (var neighbor in currentVertex.Neighbors.Keys)
            {
                if (!visited.Contains(neighbor))
                {
                    visited.Add(neighbor);
                    queue.Enqueue(neighbor);
                }
            }
        }
    }

    public void DFS(string StartName)
    {
        if (!vertices.ContainsKey(StartName))
        {
            return;
        }
        
        HashSet<Vertex> visited = new HashSet<Vertex>();
        DFSUtil(vertices[StartName], visited);
    }

    public void DFSUtil(Vertex vertex, HashSet<Vertex> visited)
    {
        visited.Add(vertex);
        Debug.Log($"방문 : {vertex.Name}");
        
        foreach (var neighbor in vertex.Neighbors.Keys)
        {
            if (!visited.Contains(neighbor))
            {
                DFSUtil(neighbor, visited);
            }
        }
    }

    public void Dijkstra(string startName)
    {
        if (!vertices.ContainsKey(startName))
        {
            return;
        }
        
        Dictionary<Vertex, float> distances = new Dictionary<Vertex, float>();
        Dictionary<Vertex, Vertex> previous = new Dictionary<Vertex, Vertex>();
        HashSet<Vertex> unvisited = new HashSet<Vertex>();

        foreach (var vertex in vertices.Values)
        {
            distances[vertex] = float.MaxValue;
            previous[vertex] = null;
            unvisited.Add(vertex);
        }
        
        Vertex start = vertices[startName];
        distances[start] = 0;

        while (unvisited.Count > 0)
        {
            Vertex current = null;
            float minDistance = float.MaxValue;
            foreach (var vertex in unvisited)
            {
                if (distances[vertex] < minDistance)
                {
                    current = vertex;
                    minDistance = distances[vertex];
                }
            }

            if (current == null)
            {
                break;
            }
            
            unvisited.Remove(current);

            foreach (var neighbor in current.Neighbors)
            {
                float alt = distances[neighbor.Key] + neighbor.Value;
                if (alt < distances[neighbor.Key])
                {
                    distances[neighbor.Key] = alt;
                    previous[neighbor.Key] = current;
                }
            }
        }

        foreach (var vertex in vertices.Values)
        {
            Debug.Log($"{startName}에서 {vertex.Name}까지의 최단 거리: {distances[vertex]}");
        }
    }

    /*public float GetDistance(string from, string to)
    {
        
    }*/
    void Start()
    {
        vertices = new Dictionary<string, Vertex>();
        
        Graph graph = GetComponent<Graph>();
        
        graph.AddVertex("집");
        graph.AddVertex("슈퍼마켓");
        graph.AddVertex("미용실");
        graph.AddVertex("레스토랑");
        graph.AddVertex("은행");
        graph.AddVertex("영어학원");
        graph.AddVertex("학교");
        
        graph.AddEdge("집","미용실",5.0f);
        graph.AddEdge("집","슈퍼마켓",10.0f);
        graph.AddEdge("집","영어학원",9.0f);
        graph.AddEdge("미용실","슈퍼마켓",3.0f);
        graph.AddEdge("미용실","은행",11.0f);
        graph.AddEdge("슈퍼마켓", "레스토랑", 3.0f);
        graph.AddEdge("슈퍼마켓", "은행", 10.0f);
        graph.AddEdge("슈퍼마켓", "영어학원", 7.0f);
        graph.AddEdge("레스토랑", "은행", 4.0f);
        graph.AddEdge("은행", "영어학원", 7.0f);
        graph.AddEdge("은행", "학교", 2.0f);
        graph.AddEdge("영어학원", "학교", 12.0f);
        
        
        graph.DFS("집");
        
    }

    void Update()
    {
        
    }
}
