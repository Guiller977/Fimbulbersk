using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    private Checkpoint[] checkpoints;

    public Vector3 spawnPoint;

    public static CheckpointController sharedInstance;

    public void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        checkpoints = GetComponentsInChildren<Checkpoint>();
        spawnPoint = Playercontroller.sharedInstance.transform.position + new Vector3(0, 2f,0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeactivateCheckpoints()
    {
        for (int i = 0; i < checkpoints.Length; i++)
        {
            checkpoints[i].ResetCheckpoint();
        }
    }

    public void SetSpawnPoint(Vector3 newspawnPoint)
    {
        spawnPoint = newspawnPoint;
    }
}
