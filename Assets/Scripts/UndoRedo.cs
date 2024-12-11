/*
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

using System.Collections.Generic;
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
    
    public void Clear()
    {
        top = null;
    }
}
public class UndoRedoManager<T> where T : new()
{
    private StackCustom<T> undoStack = new StackCustom<T>();
    private StackCustom<T> redoStack = new StackCustom<T>();

    // 새로운 상태를 추가
    public void Record(T state)
    {
        undoStack.Push(state);         // 1. UndoStack 에 상태 기록
        redoStack.Clear();             // 2. 새로운 작업 발생했으므로 RedoStack 초기화
    }

    // Undo 실행
    public T Undo()
    {
        if (!CanUndo()) return default;

        T state = undoStack.Pop();     // 1. UndoStack 에서 꺼냄
        redoStack.Push(state);         // 2. RedoStack 에 추가
        return state;
    }

    // Redo 실행
    public T Redo()
    {
        if (!CanRedo()) return default;

        T state = redoStack.Pop();     // 1. RedoStack 에서 꺼냄
        undoStack.Push(state);         // 2. UndoStack 에 다시 추가
        return state;
    }

    // UndoStack 비었는지 확인
    public bool CanUndo()
    {
        return undoStack.Count > 0;
    }

    // RedoStack 비었는지 확인
    public bool CanRedo()
    {
        return redoStack.Count > 0;
    }
    
        // 스택 비우기
    public void Clear()
    {
        top = null;  // top을 null로 설정하여 모든 노드를 제거
    }
}

public class UndoRedo : MonoBehaviour
{
    private UndoRedoManager<Vector3> positionManager = new UndoRedoManager<Vector3>();
    private Transform targetObject; // 대상 오브젝트

    private void Start()
    {
        // 초기화: 스크립트를 부착한 GameObject의 Transform 사용
        targetObject = this.transform;

        // 초기 위치 저장
        positionManager.Record(targetObject.position);
    }

    private void Update()
    {
        Movement();
        UndoRedoManage();
    }

    private void UndoRedoManage()
    {
        // Undo 실행 (스페이스 키)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (positionManager.CanUndo())
            {
                Vector3 undoPosition = positionManager.Undo();
                targetObject.position = undoPosition;
            }
            else
            {
                Debug.Log("Undo Stack 이  비었음 .");
            }
        }

        // Redo 실행 (백스페이스 키)
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (positionManager.CanRedo())
            {
                Vector3 redoPosition = positionManager.Redo();
                targetObject.position = redoPosition;
            }
            else
            {
                Debug.Log("Redo Stack 이 비었음 .");
            }
        }
    }

    private void Movement()
    {
        Vector3 movePos = Vector3.zero;
        float speed = 10f;
        
        if (Input.GetKey(KeyCode.W)) movePos += transform.forward;
        if (Input.GetKey(KeyCode.S)) movePos -= transform.forward;
        if (Input.GetKey(KeyCode.A)) movePos -= transform.right;
        if (Input.GetKey(KeyCode.D)) movePos += transform.right;
        
        transform.position += movePos.normalized * (speed * Time.deltaTime);
        
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) ||
            Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            positionManager.Record(targetObject.position);  // 이동이 끝나면 위치 저장
        }
    }
}
*/

