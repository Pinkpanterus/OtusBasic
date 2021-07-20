using Assets.Scripts.Character.Attack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Character.Health
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int health;
        public int Health { get => health; }

        [SerializeField] private bool isDead;
        public bool IsDead { get => isDead; }

        public Action<int> OnHealthChanged;
        public Action OnDead;
        public string damageSoundName;
        public string deathSoundName;

        private DamageEffect damageEffect;
        //private PlaySound playSound;

        void Start()
        {
            damageEffect = GetComponent<DamageEffect>();
            //playSound = GetComponentInChildren<PlaySound>();
        }
        
        public void ApplyDamage(AttackComponent attackComponent)
        {
            health -= attackComponent.Damage;
            if (damageEffect) damageEffect.ShowDamageEffect();
            PlaySound.instance.Play(damageSoundName,1);

            if(health <=0)
            {
                isDead = true;
                health = 0;
                OnDead?.Invoke();
                PlaySound.instance.Play(deathSoundName,1);
            }

            OnHealthChanged?.Invoke(health);
        }
    }
}
