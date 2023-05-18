using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public float timeToRespawn = 2f;

    public static LevelManager sharedInstance;

    public int gemCollected;
    public GameObject death_Effect;
    public bool MoreHP;

    public bool bossDeath;

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
        if (PlayerPrefs.HasKey("Health"))
        {
            PlayerHealthController.sharedInstance.maxHealth = PlayerPrefs.GetInt("Health");
            PlayerHealthController.sharedInstance.currentHealth = PlayerHealthController.sharedInstance.maxHealth;
            UIController.sharedInstance.UpdateHealthDisplay();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //OutOfBounds();
        DontDestroyOnLoad(this.gameObject);

        if (bossDeath == true)
        {
            StartCoroutine(bossDeathSequence());
        }
    }

    public void RespawnPlayer()
    {
        if (SceneManager.GetActiveScene().name.Equals("Boss_Fight"))
        {
            StartCoroutine(BossRespawnPlayer());
        }
        else if (SceneManager.GetActiveScene().name.Equals("Level_1"))
        {
            StartCoroutine(RespawnPlayerCo());
        }
    }

    public IEnumerator RespawnPlayerCo()
    {
        Playercontroller.sharedInstance.gameObject.SetActive(false);
        //Instantiate(death_Effect, Playercontroller.sharedInstance.transform.position, Playercontroller.sharedInstance.transform.rotation);
        AudioManager.sharedInstance.PlaySFX(8);
        yield return new WaitForSeconds(timeToRespawn);
        Playercontroller.sharedInstance.gameObject.SetActive(true);
        Playercontroller.sharedInstance.canDash = true;
        Playercontroller.sharedInstance.transform.position = CheckpointController.sharedInstance.spawnPoint;
        PlayerHealthController.sharedInstance.currentHealth = PlayerHealthController.sharedInstance.maxHealth;
        UIController.sharedInstance.UpdateHealthDisplay();
    }

    public IEnumerator BossRespawnPlayer()
    {
        AudioManager.sharedInstance.PlaySFX(8);
        if (PlayerHealthController.sharedInstance.maxHealth == 4)
        {
            MoreHP = true;
        }

        Destroy(Playercontroller.sharedInstance.gameObject);
        //Instantiate(death_Effect, Playercontroller.sharedInstance.transform.position, Playercontroller.sharedInstance.transform.rotation);
        UIController.sharedInstance.FadeToBlack();
        yield return new WaitForSeconds(timeToRespawn);
        SceneManager.LoadScene("Boss_Fight");
    }

    private IEnumerator bossDeathSequence()
    {
        UIController.sharedInstance.FadeToBlack();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Credits");
    }
}

//    public void OutOfBounds()
//    {
//        if (Playercontroller.sharedInstance.transform.position.y < -7)
//        {
//            RespawnPlayer();
//        }
//    }
//}
