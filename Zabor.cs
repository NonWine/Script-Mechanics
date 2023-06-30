using Assets.scripts.Units;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.scripts
{
    public class Zabor : MonoBehaviour
    {
        [SerializeField] private Transform[] points;
        [SerializeField] private float health;
        [SerializeField] private Vector3 endScaleAnimate;
        [SerializeField] private Transform objectScale;
        private HealthUI healthUI;
        private Tween tween;
        private Sequence s;
        private Vector3 mainScale;
        private float startHealth;
        private bool dead;
        public bool Dead => dead;
        private void Awake()
        {
            startHealth = health;
            health = PlayerPrefs.GetFloat("health", health);
        
        }

        private void Start()
        {
        
            mainScale = objectScale.localScale;
            healthUI = GetComponent<HealthUI>();
            healthUI.SetHealth(health);
        }

        public void AnimateScale()
        {
            objectScale.localScale = mainScale;
            if (s != null)
            {
                DOTween.Kill(s.id);
                s = null;
            }
            s =  DOTween.Sequence()
                .Append(objectScale.DOScale(endScaleAnimate, 0.25f))
                .Append(objectScale.DOScale(mainScale, 0.25f));
            s.id  = System.Guid.NewGuid();
        }

        public void GetDamage(float damage)
        {
            health -= damage;
            healthUI.GetDamageUI(damage);
            AnimateScale();
            if (healthUI.GetfillAmout() <= 0f)
            {
                dead = true;
                ParticlePool.Instance.PlayZaborDestroy(transform.position,20f);
                if (s != null)
                {
                    DOTween.Kill(s.id);
                    s = null;
                }
                Invoke(nameof(PlayerCanDamage),0.5f);
                gameObject.SetActive(false);
            }
        }

        private void PlayerCanDamage()
        {
            Player.Instance.GetComponent<BoxCollider>().enabled = true;
            Player.Instance.canDamage = true;
        }
    
        public Transform GetPoint()
        {
            return points[Random.Range(0, points.Length)];
        }

        public void Heal()
        {
            dead = false;
            Player.Instance.canDamage = false;
            Player.Instance.GetComponent<BoxCollider>().enabled = false;
            health = startHealth;
            healthUI.ResetUIHealth();
            healthUI.SetHealth(health);
        }
    }
}
