using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinaryTree : MonoBehaviour
{
    //      Node        //
    //      /  /        //
    //     L   R        //
    
    public class Node
    {
        public int Data;
        public Node Left;
        public Node Right;

        public Node(int item)
        {
            Data = item;
            Left = null;
            Right = null;
        }
    }

    private Node _root;

    // 전위 순회
    // 루트 -> 왼쪽 -> 오른쪽
    public void Preorder(Node node)
    {
        if (node == null)
        {
            return;
        }

        Debug.Log(node.Data + " "); // 루트
        Preorder(node.Left);    // 왼쪽 재귀
        Preorder(node.Right);   // 오른쪽 재귀
    }

    // 중위 순회
    // 왼쪽 -> 루트 -> 오른쪽
    public void Inorder(Node node)
    {
        if (node == null)
        {
            return;
        }
        
        Inorder(node.Left);             // 왼쪽 재귀
        Debug.Log(node.Data + " ");     // 루트
        Inorder(node.Right);            // 오른쪽 재귀
    }

    // 후위 순회
    // 왼쪽 -> 오른쪽 -> 루트
    public void Postorder(Node node)
    {
        if (node == null)
        {
            return;
        }
        
        Postorder(node.Left);           // 왼쪽 재귀
        Postorder(node.Right);          // 오른쪽 재귀
        Debug.Log(node.Data + " ");     // 루트
    }

    public void Start()
    {
        Node root = new Node(100);
        root.Left = new Node(25);
        root.Right = new Node(75);
        root.Left.Left = new Node(20);
        root.Left.Right = new Node(30);
        root.Right.Left = new Node(70);
        root.Right.Right = new Node(80);
        
        // 전위 순회 예상 결과
        // 100 -> 25 -> 20 -> 30 -> 75 -> 70 -> 80
        Preorder(root);
        
        // 중위 순회 예상 결과
        // 20 -> 25 -> 30 -> 100 -> 70 -> 75 -> 80 
        Inorder(root);
        
        // 후위 순회 예상 결과
        // 20 -> 30 -> 25 -> 70 -> 80 -> 75 -> 100
        Postorder(root);
    }
}
