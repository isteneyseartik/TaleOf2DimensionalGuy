using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] PlatformPrefabs;
    public int platformSpawnCount;

    public Vector3 lastEndPoint;




    public void SpawnPlatforms()
    {
        for (int i = 0; i < platformSpawnCount; i++)
        {
            GameObject platform = GameObject.Instantiate(PlatformPrefabs[Random.Range(0,PlatformPrefabs.Length)]);

            Platform platformScript = platform.GetComponent<Platform>();

            

            platform.transform.position = lastEndPoint;

            lastEndPoint = platformScript.ReturnEndPoint();

        
        
        
        }
    }



    //İlk başlangıçta çalışır
    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnPlatforms(); 
    }

   
    private void FixedUpdate()
    {
        
    }
}
