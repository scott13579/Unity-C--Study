using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class ArrayExample : MonoBehaviour
{
    // 플레이어 점수를 저장하는 배열
    [SerializeField] int[] playerScores = new int[10];
    
    // 아이템 이용을 저장하는 배열
    private string[] itemNames = {"검", "방패", "포션", "활", "마법서"};
    
    // 적 프리팹을 저장하는 배열
    public GameObject[] enemyPrefabs;
    
    // 맵의 타일을 저장하는 2D 배열
    private int[,] mapTiles = new int[10, 10];
    
    public int i = 0;

    public GameObject whiteCube;
    public GameObject blackCube;
    public GameObject[,] cubeTiles = new GameObject[10, 10];
    
    void Start()
    {
        /*
        for (int i = 0; i < playerScores.Length-2; i++)
        {
            playerScores[i] = i+1;
            //Debug.Log(playerScores[i]);
        }

        for (int i = 0; i < Array_Size; i++)
        {
            
        }
        */

        /*
        for (int i = 0; i < maplTiles.GetLength(0); i++)
        {
            for (int j = 0; j < maplTiles.GetLength(1); j++)
            {
                maplTiles[i, j] = i*10 + j;
                Debug.Log(maplTiles[i, j]);
            }
        }
        */
        
        PlayerScoresExample();
        ItemInventoryExample();
        MapGeneratorExample();
    }

    void Update()
    {
        i += 1;
        if (i <= 5)
        {
            //EnemySpawnExample();
        }
    }

    void PlayerScoresExample()
    {
        // 플레이어 점수 할당
        for (int i = 0; i < playerScores.Length; i++)
        {
            playerScores[i] = Random.Range(100, 1000);
        }
        
        // 최고점수 찾기
        int highScore = playerScores.Max();
        Debug.Log($"최고 점수 : {highScore}");
        
        // 평균 점수 계산
        
        /*
        Average 미사용
        
        int totalValue = 0;
        float averageValue = 0;

        for (var i = 0; i < playerScores.Length; i++)
        {
            totalValue += playerScores[i];
        }
        
        averageValue = (float)totalValue / (float)playerScores.Length;
        Debug.Log($"평균점수 : {averageValue:F2}");
        */
        
        
        // Average 사용
        float avgScore = (float)playerScores.Average();
        Debug.Log($"평균 점수 : {avgScore:F2}");
        
    }

    void ItemInventoryExample()
    {
        // 랜덤 아이템 선택
        int randomIndex = Random.Range(0, itemNames.Length);
        string selectedItem = itemNames[randomIndex];
        Debug.Log($"선택된 아이템 : {selectedItem}");

        string itemName = "포션";
        
        // contains 직접 구현
        bool hasItem1 = Contains(itemName);
        Debug.Log($"포션 보유 여부 : {hasItem1}");
        
        // 특정 아이템 검색
        string searchItem = "창";
        bool hasItem = itemNames.Contains(searchItem);
        Debug.Log($"아이템 보유 여부 : {hasItem}");
    }

    private bool Contains(string itemName)
    {
        for (var i = 0; i < itemNames.Length; i++)
        {
            if (itemNames[i] == itemName)
            {
                return true;
            }
        }
        return false;
    }

    void EnemySpawnExample()
    {
        if (enemyPrefabs != null && enemyPrefabs.Length > 0)
        {
            // 랜덤 위치에 적 생성
            Vector3 spawnPos = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
            int randomEnemyIndex = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[randomEnemyIndex],spawnPos,Quaternion.identity);
            Debug.Log($"적 생성됨 : {enemyPrefabs[randomEnemyIndex].name}");
        }
        else
        {
            Debug.LogWarning("적 프리팹이 할당되지 않았습니다.");
        }
    }

    void MapGeneratorExample()
    {
        // 간단한 맵 생성 (0: 빈 공간, 1: 벽)
        for (int x = 0; x < mapTiles.GetLength(0); x++)
        {
            for (int y = 0; y < mapTiles.GetLength(1); y++)
            {
                mapTiles[x, y] = Random.value > 0.8f ? 1 : 0;
            }
        }
        
        // 삼항 연산자
        // num1 >= 10  ? 100 : 200;
        // num1 이 10이상이면 100, 미만이면 200 값을 넣겠다는 뜻
        // if 문을 간단하게 쓰는거라고 생각하면 됨
        
        // 맵 출력
        string mapString = "생성된 맵:\n";
        for (int x = 0; x < mapTiles.GetLength(0); x++)
        {
            for (int y = 0; y < mapTiles.GetLength(1); y++)
            {
                // 방법 1. 삼항연산자를 사용하고 그 객체를 관리하기 위해 배열에 넣기
                //cubeTiles[x, y] = mapTiles[x, y] == 1 ? Instantiate(cube) : null;
                
                // 방법2. 조건문을 통해 그 객체를 관리하기 위해 배열에 넣기
                if (mapTiles[x, y] == 1)
                {
                    cubeTiles[x,y] = Instantiate(whiteCube,new Vector3(x-4,y-3.5f,1), Quaternion.identity);
                }
                else
                {
                    cubeTiles[x, y] = Instantiate(blackCube,new Vector3(x-4,y-3.5f,1), Quaternion.identity);
                }
                
                
                mapString += mapTiles[x, y] == 1? "■ " : "□ ";
            }
            mapString += "\n";
        }
        Debug.Log(mapString);
        
    }
}