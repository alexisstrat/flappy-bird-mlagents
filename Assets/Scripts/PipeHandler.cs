using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class PipeHandler : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Where the pipe deactivates itself")]
    public Transform endPos;
    
    [SerializeField] 
    private Pipe pipePrefab = null;

    [SerializeField] 
    [Tooltip("The opening between two pipes")]
    private float gapSize = 4f;
    
    [SerializeField] 
    private float secondsBetweenSpawns = 2f;

    private float spawnTimer;
    
    private readonly List<Pipe> _pipes = new List<Pipe>();

    private ObjectPooler _objectPooler;
    

    // Update is called once per frame
    void Update()
    {
        _objectPooler = GetComponent<ObjectPooler>();
        
        SpawnNewPipes();
    }


    private void SpawnNewPipes()
    {
        spawnTimer -= Time.deltaTime;
        
        if(spawnTimer > 0) return;

        float centerHeight = Random.Range(-1.5f, 4f);

        SpawnPipe(centerHeight, true);
        SpawnPipe(centerHeight, false);
        
        spawnTimer = secondsBetweenSpawns;
    }

    /// <summary>
    /// Spawns a new pipe in the scene
    /// </summary>
    /// <param name="centerHeight">random value to be used as height for pipes</param>
    /// <param name="top">is this a top pipe? if yes do smth otherwise do smth else</param>
    void SpawnPipe(float centerHeight, bool top)
    {
        GameObject pipe = _objectPooler.GetPooledObject();
        pipe.transform.position = transform.position;
        
        
        pipe.transform.rotation = top? Quaternion.Euler(0,0,180f) : Quaternion.identity;
        
        pipe.transform.position +=
            top ? Vector3.up * (centerHeight + gapSize / 2) : Vector3.up * (centerHeight - gapSize / 2);
        
        //pipe.transform.Translate(Vector3.up * (centerHeight + (gapSize/2)), Space.World);
        
        _pipes.Add(pipe.GetComponent<Pipe>());
        pipe.SetActive(true);
    }
    
    /// <summary>
    /// Deactivates all pipes in scene upon agent's
    /// episode begin
    /// </summary>
    public void ResetPipes()
    {
        foreach (var pipe in _pipes)
        {
            pipe.gameObject.SetActive(false);
        }
        
        _pipes.Clear();

        spawnTimer = 0f;
    }
}

