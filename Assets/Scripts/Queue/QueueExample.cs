using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class QueueNode<T>
{
    public T Data { get; set; }
    public QueueNode<T> NextNode { get; set; }

    public QueueNode(T data)
    {
        Data = data;
        NextNode = null;
    }
}

public class NodeQueue<T>
{
    private QueueNode<T> front;
    private QueueNode<T> rear;
    private int size;

    public NodeQueue()
    {
        front = null;
        rear = null;
        size = 0;
    }
    public void Enqueue(T data)
    {
        QueueNode<T> newNode = new QueueNode<T>(data);

        if (IsEmpty())
        {
            front = newNode;
            rear = newNode;
        }
        else
        {
            rear.NextNode = newNode;
            rear = newNode;
        }

        size++;
    }

    public T Dequeue()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("Queue is empty");
        }
        
        T data = front.Data;
        front = front.NextNode;
        size--;
        
        if (IsEmpty())
        {
            rear = null;
        }

        return data;
    }

    public T Peek()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("Queue is empty");
        }
        return front.Data;
    }

    public bool IsEmpty()
    {
        return size == 0;
    }

    public int Size()
    {
        return size;
    }
}
public class QueueExample : MonoBehaviour
{
    
    NodeQueue<int> queue = new NodeQueue<int>();
    // Start is called before the first frame update
    void Start()
    {
        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);
        
        Debug.Log(queue.Dequeue());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
