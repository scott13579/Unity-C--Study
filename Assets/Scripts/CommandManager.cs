using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommand
{
    void Execute();
    void Undo();
}

public class CommandManager : MonoBehaviour
{
    private Stack<ICommand> undoStack = new Stack<ICommand>();
    private Stack<ICommand> redoStack = new Stack<ICommand>();
    
    [NonSerialized]
    public float moveSpeed = 3.0f;
    public float rotateSpeed = 3.0f;

    // 이동한 만큼 롤백하기 위해 이동한 양를 저장한다.
    public Vector3 MoveDelta = Vector3.zero;
    public Vector3 RotateDelta = Vector3.zero;

    //private bool prevMoved = false;
    
    /// <summary>
    /// 1. Exexcute 함수를 실행
    /// 2. Undo 스택에 행동을 저장
    /// 3. 행동을 최신으로 갱신 했기 때문에 다시 되돌아갈 행동이 없으므로 redo 스택 clear
    /// </summary>
    /// <param name="command"></param>
    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
        undoStack.Push(command);
        redoStack.Clear();
    }

    public void Undo()
    {
        if (undoStack.Count > 0)
        {
            // 1. 가장 최근에 실행 된 커맨드를 가져온다.
            // 2. 그 커맨드를 Undo 시킨다.
            // 3. 그리고 다시 Redo 할 수 있기 때문에 redoStack 에 넣는다
            ICommand command = undoStack.Pop();
            command.Undo();
            redoStack.Push(command);
        }
    }

    public void Redo()
    {
        if (redoStack.Count > 0)
        {
            // 1. 가장 최근에 Undo 된 커맨드를 가져온다.
            // 2. 그 커맨드를 Execute해준다.
            // 3. 그리고 다시 Undo 할 수 있도록 undoStack 에 넣어준다.
            ICommand command = redoStack.Pop();
            command.Execute();
            undoStack.Push(command);
        }
    }
    
    void Update()
    {
        Vector3 movePos = Vector3.zero;
        Vector3 deltaRot = Vector3.zero;
        
        // 이동
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

        // 각도 회전
        if (Input.GetKey(KeyCode.Mouse0))
        {
            deltaRot -= transform.right * (Time.deltaTime * rotateSpeed);
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            deltaRot += transform.right * (Time.deltaTime * rotateSpeed);
        }

        Vector3 addtivePosition = movePos.normalized * (moveSpeed * Time.deltaTime);
        
        // 움직였던 정보를 기억하기 위해 이동 정보를 기억
        if (movePos == Vector3.zero && MoveDelta != Vector3.zero)
        {
            // 롤백 포지션으로 돌아가기 위해 transform.position 에서 Movedelta를 빼주고
            var moveCommand = new MoveCommand(transform, transform.position - MoveDelta);
            ExecuteCommand(moveCommand);
            // 이제 다시 기록하기 위해 MoveDelta 초기화
            MoveDelta = Vector3.zero;
            return;
        }

        if (deltaRot == Vector3.zero &&  RotateDelta != Vector3.zero) 
        {
            var rotateCommand = new RotateCommand(transform, Quaternion.LookRotation(transform.forward - RotateDelta, Vector3.up));
            ExecuteCommand(rotateCommand);
            RotateDelta = Vector3.zero;
            return;
        }

        // 원래 포지션으로 되돌아가는 코드
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Undo();
            return;
        }
        transform.position += addtivePosition;
        transform.rotation = Quaternion.LookRotation(transform.forward + deltaRot, Vector3.up);
        
        MoveDelta += addtivePosition;
        RotateDelta += deltaRot;
    }
}

public class MoveCommand : ICommand
{
    private Transform _transform;
    private Vector3 _oldPosition;
    private Vector3 _newPosition;

    public MoveCommand(Transform transform, Vector3 rollBackPosition)
    {
        // 1. 이동하려는 트랜스폼 객체를 참조한다.
        // 2. Undo 할 때 돌아갈 포지션을 정한다.
        // 3 .execute 시에 셋팅 될 포지션 값을 저장한다.
        _transform = transform;
        _oldPosition = rollBackPosition;
        _newPosition = transform.position;
    }

    public void Execute()
    {
        // newPosition 으로 갱신한다.
        _transform.position = _newPosition;
    }

    public void Undo()
    {
        // oldPosition 으로 갱신한다.
        _transform.position = _oldPosition;
    }
}

public class RotateCommand : ICommand
{
    private Transform _transform;
    private Quaternion _oldRotation;
    private Quaternion _newRotation;

    public RotateCommand(Transform transform, Quaternion newRotation)
    {
        // 1. 이동하려는 트랜스폼 객체를 참조한다.
        // 2. Undo 할 때 돌아갈 각도를 정한다.
        // 3. execute 시에 셋팅 될 Rotation 값을 저장한다.
        _transform = transform;
        _oldRotation = _transform.rotation;
        _newRotation = newRotation;
    }

    public void Execute()
    {
        // newRotation 으로 갱신한다.
        _transform.rotation = _newRotation;
    }

    public void Undo()
    {
        // oldRotation 으로 갱신한다.
        _transform.rotation = _oldRotation;
    }
}
