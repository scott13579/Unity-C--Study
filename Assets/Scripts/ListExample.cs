using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/*public class NodeInt
{
    
    public int Data { get; set; }
    
    public NodeInt Next { get; set; }

    public NodeInt(int data)
    {
        Data = data;
        Next = null;
    }
}

public class NodeFloat
{
    public float Data { get; set; }
    
    public NodeFloat Next { get; set; }

    public NodeFloat(float data)
    {
        Data = data;
        Next = null;
    }
}*/

public class Node<T>
{
    /// <summary>
    /// 이 T가 대체 뭐냐
    /// T가 아니어도 아무거나 저 안에 넣어도 상관 없는데
    /// T를 많이들 쓴다
    /// T 제너릭 타입을 쓰게 되면 뭐가 들어가도 상관없다
    /// 위에 처럼 자료형 마다 만들 필요없이 자동으로 넣은 걸로 된다.
    /// </summary>
    public T Data { get; set; }
    public Node<T> Next { get; set; }

    public Node(T data)
    {
        Data = data;
        Next = null;
    }
    
}
public class LinkedListCustom<T>
{
    
    // 맨 처음에 노드상의 제일 앞 부분을 head 라고 선언한다
    public Node<T> Head { get; set; }

    // 요소를 추가한다 어디에? 뒤에다가
    // 새로운 노드를 추가한다
    // 노드는 데이터와 next를 담은 구조이다.
    public void AddLast(T data)
    {
        Node<T> newNode = new Node<T>(data);
        if (Head == null)
        {
            Head = newNode;
        }
        else
        {
            /// current -> next가 null 일때까지 반복문을 수행
            ///
            /// [head][next] -> [node][next] -> [node][next] -> null
            Node<T> current = Head;
            
            // current를 계속 최신화 시켜준다.
            while (current.Next != null)
            {
                current = current.Next;
            }
            // 맨 마지막의 노드의 next 에다가 새로운 노드를 연결한다.
            current.Next = newNode;
        }
    }

    public void AddFirst(T data)
    {
        Node<T> newNode = new Node<T>(data);

        if (Head == null)
        {
            Head = newNode;
        }
        else
        {
            /// newNode의 Next에 head를 집어넣고
            /// Head를 newNode 로 바꿈
            newNode.Next = Head;
            Head = newNode;
        }
    }

    // 리스트 안의 내용을 출력하는 기능
    public void Traverse()
    {
        Node<T> current = Head;
        while (current != null)
        {
            Debug.Log(current.Data);
            current = current.Next;
        }
    }
}

public class DNode<T>
{
    public T Data { get; set; }
    public DNode<T> Next { get; set; }
    public DNode<T> Prev { get; set; }
    
    public DNode(T data)
    {
        Data = data;
        Next = null;
        Prev = null;
    }
    
}

public class DLinkedListCustom<T>
{
    public DNode<T> Head { get; set; }
    public DNode<T> Tail { get; set; }
    
    public int listSize { get; set; } 

    public void AddLast(T data)
    {
        DNode<T> newNode = new DNode<T>(data);
        if (Head == null)
        {
            /// list가 비어있다면
            /// Head와 Tail 둘 다 null 일 것이므로 
            /// Head 와 Tail 을 새로 생성한 노드로 치환
            Head = newNode;
            Tail = newNode;
        }
        else
        {
            /// 특별한 상황이 아닌 경우
            /// Tail.Next에 새로운 노드를 연결하고
            /// newNode.Prev에 Tail을 연결한다
            /// 그리고 Tail을 newNode로 치환한다.
            Tail.Next = newNode;
            newNode.Prev = Tail;
            Tail = newNode;
        }
        listSize++;
    }

    public void AddFirst(T data)
    {
        DNode<T> newNode = new DNode<T>(data);
        if (Head == null)
        {
            /// AddLast와 같음
            Head = newNode;
            Tail = newNode;
        }
        else
        {
            /// AddFirst는 리스트의 맨 앞에 넣는 것이므로
            /// Head.Prev에 newNode에 연결
            /// newNode.Next에 Head를 연결
            /// 연결이 됐으니까 Head를 newNode로 치환
            Head.Prev = newNode;
            newNode.Next = Head;
            Head = newNode;
        }
        listSize++;
    }

    public void DeleteNode(T data)
    {
        DNode<T> current = Head;
        while (current != null)
        {
            if (Equals(current.Data, data))
            {
                current.Prev.Next = current.Next;
                current.Next.Prev = current.Prev;
                break;
            }
            current = current.Next;
        }
        listSize--;
    }

    /*public int Length()
    {
        int count = 1;
        if (Head == null) return 0;
        
        DNode<T> current = Head;
        while (current.Next != null)
        {
            count++;
            current = current.Next;
        }
        return count;
    }*/
    
    public void InsertNode(T data, int index)
    {
        DNode<T> newNode = new DNode<T>(data);
        DNode<T> current = Head;
        
        if (listSize == 0)
        {
            Head = newNode;
            Tail = newNode;
        }
        
        if (index < 0 || index >= listSize)
        {
            Debug.Log("Index Error ! ! !");
        }
        else if (index == 0)
        {
            newNode.Next = Head;
            Head.Prev = newNode;
            Head = newNode;
        }
        else if (index == listSize - 1)
        {
            newNode.Prev = Tail;
            Tail.Next = newNode;
            Tail = newNode;
        }
        else
        {

            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }
            /// 1. 새로운 노드와 이전에 이 위치에 있던 노드의 다음 인덱스 노드(next -> *)를 연결
            /// 2. 이어서 위에서 말한 다음 인덱스노드의 Prev와 현재 노드를 연결
            /// 3. 새로운 노드의 Prev에 기존 위치에 있던 노드를 연결
            /// 4. 기존의 current 노드의 Next에 newNode를 연결
            newNode.Next = current.Next;
            current.Next.Prev = newNode;
            newNode.Prev = current;
            current.Next = newNode;
        }
        listSize++;
    }
    
    public void Traverse()
    {
        DNode<T> current = Head;
        while (current != null)
        {
            Debug.Log(current.Data);
            current = current.Next;
        }
    }

    public void ReverseTraverse()
    {
        DNode<T> current = Tail;
        while (current != null)
        {
            Debug.Log(current.Data);
            current = current.Prev;
        }
    }
}

public class ListExample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        /// 배열과 리스트에 대한 메모리의 차이
        ///
        /// 배열의 메모리 상태
        /// [][][][][][][][][][][][][]
        /// 배열의 사이즈를 변경하려면
        /// [][][][][][][][][][][][][] + [][][][]
        /// [][][][][][][][][][][][][] 삭제하고
        /// [][][][][][][][][][][][][][][][][] 이렇게 재할당함
        ///
        /// 링크드 리스트의 메모리 상태
        /// * 이 별의 뜻은 연속적이지 않다. 어딘가에 메모리가 존재할 것이다.
        /// 그 주소를 담고 있는게 별이다.
        /// 싱글 링크드 리스트
        /// []* -> []* -> []* -> []*
        /// 더블 링크드 리스트
        /// *[]* -> *[]* -> *[]* -> *[]*
        ///
        /// 리스트의 특징은
        /// [] 이 표현을 요소라 함
        /// 맨 앞 또는 맨 뒤에 요소를 넣는데 속도가 엄청 빠름 (시간 복잡도 O(1))
        /// 시간복잡도는 쉽게 찍먹 해보자면
        /// 반복문이 도는 횟수 정도로 이해하면 됨
        ///
        /// 배열에서 3번째 인덱스에 접근하고 싶다?
        /// 랜덤인덱스 용어가 있는데 즉시 배열의 3번째가 어딘지 알기 때문에 (시간복잡도 O(1)) 접근이 가능
        /// 3번째 인덱스가 어디있는지 계산할 필요도 없이 그냥 다 붙어 있기 때문에
        /// 첫 메모리의 주소값을 알면 그 위치에서 -> 그 데이터의 사이즈만큼 멀어져있는걸 찾는다.
        /// [][][][][][]
        ///
        /// int[] test = new int [5];
        /// tes[0 + 3] = 접근이 매우 빠르다
        ///
        /// 하지만 더블 링크드 리스트는
        /// *[]* -> *[]* -> *[]* -> *[]*
        /// 반복문을 돌면서 iteration 한다
        /// 내가 3번째 요소에 접근하고 싶다면 반복문을 돌아서 3번째 요소까지 접근해야함
        /// 
        /// 순차적으로 데이터를 읽어들이는 과정이 있다면 링크드 리스트가 유리
        ///  
        /// 제목 : Linked List / 링크드 리스트
        /// - 데이터 요소들을 순차적으로 연결한 자료구조
        ///     - 각 노드(node)는 데이터와 다음 노드를 가리키는 포인터로 구성
        ///     - 메모리 상에서 연속적이지 않은 위치에 저장 가능

        
        /*
        // C# 에서 제공하는 링크드 리스트를 체험해보기 위해 선언과 할당을 함
        LinkedList<int> list = new LinkedList<int>();
        
        // Addlast는  list의 마지막(tail)에 데이터(1)를 추가함
        list.AddLast(1);
        list.AddLast(2);
        list.AddLast(3);
        list.AddLast(4);
        
        // AddFirst는 list의 처음(head)에 데이터(2)를 추가함
        list.AddFirst(0);
        
        var enumerator = list.GetEnumerator();
        int findIndex = 1;
        int currentIndex = 0;

        while (enumerator.MoveNext())
        {
            if (currentIndex == findIndex)
            {
                //Debug.Log(enumerator.Current);
                break;  // 반복문 빠져나옴
            }
            currentIndex++;
        }*/
        
        LinkedListCustom<int> list = new LinkedListCustom<int>();

        list.AddFirst(10);
        list.AddLast(1);
        list.AddLast(2);
        list.AddLast(3);
        list.AddLast(4);
        
        list.AddFirst(0);
        
        // list.Traverse();
        
        DLinkedListCustom<int> list2 = new DLinkedListCustom<int>();
        
        list2.AddLast(50);
        list2.AddLast(40);
        list2.AddLast(30);
        list2.AddLast(20);
        list2.AddLast(10);
        list2.AddFirst(60);
        list2.InsertNode(25,3);
        list2.InsertNode(100,6);
        
        list2.DeleteNode(40);
        
        /// 예상 결과값
        /// Index :     0  1  2  3  4  5
        /// Traverse : 60 50 30 25 20 10
        /// Reverse :  10 20 25 30 50 60

        list2.Traverse();
        //list2.ReverseTraverse();
        print($"리스트 길이 : {list2.listSize}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

