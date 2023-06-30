using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    [SerializeField] private MobStats _playerStats;
    [SerializeField] private Detector detector;
    [SerializeField] private CameraFollowing cameraFollow;
    [SerializeField] private Animator _playerAnim;
    [SerializeField] private int _resistance;
    [SerializeField] private NavMeshAgent agent;
    [Header("Regen")]
    [SerializeField] private float perSecTime;
    [SerializeField] private int regenValue;

    private Weapon weapon;
    private HealthUI playerUI;
    private bool dead, regen;
    private int hpLeft;
    private int currentHealth;
    private float timer;

    private void Awake()
    {
        Instance = this;
        _playerStats.SetDefaltPlayer();
        _resistance = PlayerPrefs.GetInt("resistance", _resistance);
        agent.speed = PlayerPrefs.GetFloat("PlayerSpeed", agent.speed);
        currentHealth = PlayerPrefs.GetInt("currentHealth", _playerStats.GetHealth());
        regen = PlayerPrefsExtra.GetBool("PlayerRegen", regen);
        perSecTime = PlayerPrefs.GetFloat("playerRegentime", perSecTime);
        regenValue = PlayerPrefs.GetInt("RegenValue", regenValue);
    }



    private void Start()
    {
        Debug.Log(_playerStats.GetHealth());
        Debug.Log(currentHealth);
        weapon = GetComponent<Weapon>();
        playerUI = GetComponent<HealthUI>();
      // currentHealth = 1;
        playerUI.SetHealth(_playerStats.GetHealth());
        playerUI.GetDamageUI(_playerStats.GetHealth() -  currentHealth);
        
        Bank.Instance.AddCoins(5);
    }

    private void SettDamag() => DamageController.Instance.PlayerChangeSlider();

    private void Update()
    {
        weapon.SpamAbility();
        if (detector.isDetected() && !dead)
        {
            Unit unit = detector.GetTargetInfo().GetComponent<Unit>();
            _playerAnim.SetBool("Attack", true);
            weapon.Shoot(unit);
        }    
        else
        {
            
            _playerAnim.SetBool("Attack", false);
        }
        if (regen)
        {
            timer += Time.deltaTime;
            if(timer >= perSecTime)
            {
                timer = 0f;
                if(currentHealth <= _playerStats.GetHealth()) 
                {
                    currentHealth += regenValue;
                    playerUI.GetDamageUI(-regenValue);
                    PlayerPrefs.SetInt("currentHealth", currentHealth);
                }
              
            }
        }
    }

    private void Death()
    {

        cameraFollow.enabled = false;
        dead = true;
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<PlayerController>().enabled = false;
        detector.enabled = false;
        playerUI.TurnOffUiHP();
        _playerAnim.SetLayerWeight(0, 1f);
        _playerAnim.SetLayerWeight(1, 0f);
        _playerAnim.SetBool("Death", true);
        
        Invoke(nameof(GameLose), 2f);
    }

    private void GameLose()
    {
       // ADSManagerSDK.Instance.ShowADSVideoPanelAfterDead();
        GameManager.Instance.GameLose();
        //Destroy(gameObject);
    }

    public MobStats GetPlayerStats() { return _playerStats; }

    public void GetDamage(int dmg)
    {
        dmg -= _resistance;
        ParticlePool.Instance.PlayBlood(transform.position);
        currentHealth -= dmg;
        PlayerPrefs.SetInt("currentHealth", currentHealth);
        playerUI.GetDamageUI((float)dmg);
        if (currentHealth <= 0)
            Death();
    }
    

    public void IncreaseResistance(int value)
    {
        _resistance += value;
        PlayerPrefs.SetInt("resistance", _resistance);
    }

    public void IncreaseSpeed(float value)
    {
        agent.speed += value;
        PlayerPrefs.SetFloat("PlayerSpeed", agent.speed);
    }

    public void IncreaseHealth(int value)
    {
        currentHealth += value;
        _playerStats.AddHealth(value);
        playerUI.ResetUIHealth();
        playerUI.SetHealth(_playerStats.GetHealth());
        playerUI.GetDamageUI(_playerStats.GetHealth() - currentHealth);
        PlayerPrefs.SetInt("PlayerHealth", _playerStats.GetHealth());

    }

    public void Respawn()
    {
        gameObject.SetActive(true);
        _playerAnim.SetBool("Death", false);
        playerUI.GetCanvas().SetActive(true);
        GetComponent<NavMeshAgent>().enabled = true;
        GetComponent<PlayerController>().enabled = true;
        cameraFollow.enabled = true;
        dead = false;
        detector.enabled = true;
        
        Heal(200);
    }

    public void SetCanRegen()
    {
        regen = true;
        PlayerPrefsExtra.SetBool("PlayerRegen", regen);
    }

    public void ReduceRegenCd(float value)
    {
        perSecTime -= value;
        PlayerPrefs.SetFloat("playerRegentime", perSecTime);
    }

    public void UpRegenValue(int value)
    {
        regenValue += value;
        PlayerPrefs.SetInt("RegenValue", regenValue);
    }
    
    public void Heal(int value)
    {
        currentHealth += value;
        PlayerPrefs.SetInt("currentHealth", currentHealth);
        playerUI.ResetUIHealth();
        playerUI.SetHealth(_playerStats.GetHealth());
        playerUI.GetDamageUI(_playerStats.GetHealth() - currentHealth);
    }

    public bool isDead() { return dead; }
}