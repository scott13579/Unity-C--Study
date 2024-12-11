using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class StackNode<T>
{
    public T data;
    public StackNode<T> prev;
}

public class StackCustom<T> where T : new()
{
    public StackNode<T> top;

    public void Push(T data)
    {
        var stackNode = new StackNode<T>();
        
        stackNode.prev = top;
        top = stackNode;
    }

    public T Pop()
    {
        if (top == null)
        {
            return new T();
        }
        else if (top.prev == null)
        {
            var result = top.data;
            top.prev = null;
            return result;
        }
        else
        {
            var result = top.data;
            top.prev = top;
            return result;
        }
    }

    public T Peek()
    {
        if (top == null)
        {
            return new T();
        }
        
        return top.data;
    }
}

public class BracketChecker
{
    public bool AreaBracketsBalanced(string expression)
    {
        Stack<char> stack = new Stack<char>();

        foreach (char c in expression)
        {
            if (c == '(' || c == '[' || c == '{')
            {
                stack.Push(c);
            }
            else if (c == ')' || c == ']' || c == '}')
            {
                if (stack.Count == 0) return false;
                
                char top = stack.Pop();
                if ((c == ')' && top != '(') ||
                    (c == ']' && top != '[') ||
                    (c == '}' && top != '{'))
                {
                    return false;
                }
            }
        }
        return stack.Count == 0;
    }
}

public class StackExample : MonoBehaviour
{
    /// <summary>
    /// Head
    ///  []* -> []* -> []* -> []*
    ///                      top
    /// []* <- []* <- []* <- []*
    /// </summary>
    void Start()
    {
        StackCustom<int> stack = new StackCustom<int>();
        stack.Push(1);
        stack.Push(2);
        stack.Push(3);

        stack.Peek();
        stack.Pop();
        stack.Peek();

        //posStack.Push(transform.position);
        
        StackCustom<char> charStack = new StackCustom<char>();
        
        charStack.Push('{');
        charStack.Push('{');
        charStack.Push('[');
        charStack.Push('[');
        charStack.Push(']');
        charStack.Push('}');
        charStack.Push('(');
        charStack.Push(')');
        charStack.Push('}');
        
    }

    // public 일지라도 인스펙터에 노출은 안되나
    // 다른 C# class 에서는 접근이 가능하다.
    [NonSerialized]
    public float speed = 3.0f;

    // private 이지만 인스펙터에 노출이 된다
    // 하지만 다른 C# class 에서 접근이 불가하다.
    [SerializeField]
    private float speed2 = 3.0f;
    
    void Update()
    {
        /*Vector3 movePos = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            movePos += transform.forward ;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movePos -= transform.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movePos -= transform.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movePos += transform.right;
        }

        // 움직였던 정보를 stack에 저장해서 기억시킴
        if (Input.GetKeyDown(KeyCode.W) ||
            Input.GetKeyDown(KeyCode.S) ||
            Input.GetKeyDown(KeyCode.A) ||
            Input.GetKeyDown(KeyCode.D))
        {
            movePos = Vector3.zero;
            posStack.Push(transform.position);
        }

        // Space를 눌러서 원래 포지션으로 되돌아감
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (posStack.Count > 0)
            {
                reDoStack.Push(transform.position);
                transform.position = posStack.Pop();
            }
            else
            {
                Debug.Log("Undo List Empty ! ! !");
            }
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (reDoStack.Count > 0)
            {
                posStack.Push(transform.position);
                transform.position = reDoStack.Pop();
            }
            else
            {
                Debug.Log("Redo List Empty ! ! !");
            }
        }

        transform.position += movePos.normalized * (speed * Time.deltaTime);
        */
    }
}
