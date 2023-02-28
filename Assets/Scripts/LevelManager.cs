using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public float timeToRespawn = 2f;

    public static LevelManager sharedInstance;

    public int gemCollected;
    public GameObject death_Effect;

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OutOfBounds();
    }

    public void RespawnPlayer()
    {
        StartCoroutine(RespawnPlayerCo());
    }

    public IEnumerator RespawnPlayerCo()
    {
        Playercontroller.sharedInstance.gameObject.SetActive(false);
        Instantiate(death_Effect, Playercontroller.sharedInstance.transform.position, Playercontroller.sharedInstance.transform.rotation);
        yield return new WaitForSeconds(timeToRespawn);
        Playercontroller.sharedInstance.gameObject.SetActive(true);
        Playercontroller.sharedInstance.transform.position = CheckpointController.sharedInstance.spawnPoint;
        PlayerHealthController.sharedInstance.currentHealth = PlayerHealthController.sharedInstance.maxHealth;
        UIController.sharedInstance.UpdateHealthDisplay();
    }

    public void OutOfBounds()
    {
        if (Playercontroller.sharedInstance.transform.position.y < -7)
        {
            RespawnPlayer();
        }
    }
}
