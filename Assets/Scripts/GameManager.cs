// GameManager.cs
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using Vector3 = System.Numerics.Vector3;


[Serializable]
public class PlayerData
{
    public string playerName;
    
    [NonSerialized] public float Distance;
    
    [NonSerialized] public int Rank;

    [NonSerialized] public RaceButton RaceButton;
}

public class GameManager : MonoBehaviour
{
    public float battleTime = 30.0f;
    
    // 경마에 참여할 플레이어 리스트
    public List<PlayerData> Players = new List<PlayerData>();
    
    // ui에 표현 될 버튼 프리팹
    public RaceButton templateButton;
    
    // 버튼들이 붙을 부모오브젝트
    public Transform RaceButtonParent;  
    
    IEnumerator GoToNextPosition(PlayerData pd, int newRank, Vector2 newPosition)
    {
        pd.Rank = newRank;
        pd.RaceButton.text.text = $"{pd.playerName} / { pd.Distance.ToString("0.00") + " km"}";
        
        RectTransform target = pd.RaceButton.rect;
        float time = 0.0f;
        const float lerpTime = 0.3f;
        // 처음 시작하는 좌표를 기억
        Vector2 initPosition = target.anchoredPosition;
        
        // 위치를 바꾸는 코드
        // 0.3 초 안에 이동해라
        while (lerpTime >= time)
        {
            // lerp 라는 보간을 이용해서 0~1 사이 값이 나와서 포지션이 자연스럽게 옮겨가도록 한다.
            target.anchoredPosition = Vector2.Lerp(initPosition, newPosition, time / lerpTime);
            
            // 시간을 더해준다.
            time += Time.deltaTime;
            yield return null;
        }
        // 혹시 끝났을때 좌표가 안맞을 경우를 대비해서 newPosition으로 완전히 세팅한다.
        target.anchoredPosition = newPosition;
    }
    
    IEnumerator BattlerTimer()
    {
        // UI 들이 자동으로 정렬된 포지션을 가지기 위해 저장해 놓는 UI들의 위치
        List<Vector2> ui_positions = new List<Vector2>();
        
        for (var i = 0; i < Players.Count; i++)
        {
            // 오브젝트 생성하기
            var newObj = Instantiate(templateButton.gameObject, RaceButtonParent);

            RaceButton raceButton = newObj.GetComponent<RaceButton>();
            raceButton.text.text = Players[i].playerName;
            
            // RaceButton 컴포넌트 캐싱하기
            Players[i].RaceButton = raceButton;

            Players[i].Rank = i;
        }

        // 한프레임 쉬겠다.
        yield return null;
        
        for (var i = 0; i < Players.Count; i++)
        {
            ui_positions.Add(Players[i].RaceButton.rect.anchoredPosition);
        }
        
        // 정렬해준건 고맙지만 너는 여기까지만 역활이야
        RaceButtonParent.GetComponent<VerticalLayoutGroup>().enabled = false;

        while (battleTime >= 0.0f)
        {
            Debug.Log(battleTime);
            
            // 이 함수는 1초동안 쉰다.
            yield return new WaitForSeconds(1.0f);
            
            // Distance는 1초에 한번 씩 갱신
            foreach (var playerData in Players)
            {
                playerData.Distance += Random.Range(0.0f, 1.0f);
                Debug.Log(playerData.Distance);
            }
            
            var ranks = (from p in Players orderby p.Distance descending select p).ToList ();
            
            // 현재 정해진 i가 랭크 순위이므로 그 위치로 이동시킨다.
            for (var i = 0; i < ranks.Count; i++)
            {
                StartCoroutine(GoToNextPosition(ranks[i], i, ui_positions[i]));
            }
            
            // 어떠한 값이 참이 될때가지 기다리는 YieldInstruction
            // yield return new WaitUntil();

            // 물리 적용이 끝난 시점까지 기다리는 코루틴
            // yield return new FixedUpdate();
            
            battleTime -= 1.0f;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        // 코루틴 함수를 시작한다.
        StartCoroutine(BattlerTimer());
    }

    private float _stepBattleDuration = 1.0f;
    
    // Update is called once per frame
    void Update()
    {
        //Time.realtimeSinceStartup;

        // 1초당 60프레임이다 1/60 = time.deltaTime이 된다.
        // 1초당 120프레임이면 1/120 = time.deltaTime이 된다.
        // Time.deltaTime;

        // 업데이트를 이용한 방법
        // if (0 >= battleTime)
        //     return;
        //
        // if (_stepBattleDuration >= 1.0f)
        // {
        //     Debug.Log(battleTime);
        //     
        //     battleTime -= 1.0f;
        //     _stepBattleDuration = 0.0f;
        // }
        //
        // _stepBattleDuration += Time.deltaTime;
    }
}
