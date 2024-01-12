using System;
using UnityEditor.Experimental.GraphView;

namespace _2023_GC_A2_Partiel_POO.Level_2
{
    /// <summary>
    /// Définition d'un personnage
    /// </summary>
    public class Character
    {
        /// <summary>
        /// Stat de base, HP
        /// </summary>
        int _baseHealth;
        /// <summary>
        /// Stat de base, ATK
        /// </summary>
        int _baseAttack;
        /// <summary>
        /// Stat de base, DEF
        /// </summary>
        int _baseDefense;
        /// <summary>
        /// Stat de base, SPE
        /// </summary>
        int _baseSpeed;
        /// <summary>
        /// Type de base
        /// </summary>
        TYPE _baseType;



        public double CriticalChance { get; set; }

        public Character(int baseHealth, int baseAttack, int baseDefense, int baseSpeed, TYPE baseType)
        {
            _baseHealth = baseHealth;
            _baseAttack = baseAttack;
            _baseDefense = baseDefense;
            _baseSpeed = baseSpeed;
            _baseType = baseType;

            CurrentHealth = MaxHealth;
        }
        /// <summary>
        /// HP actuel du personnage
        /// </summary>
        public int CurrentHealth { get; private set; }
        public TYPE BaseType { get => _baseType;}
        /// <summary>
        /// HPMax, prendre en compte base et equipement potentiel
        /// </summary>
        public int MaxHealth => _baseHealth + (CurrentEquipment?.BonusHealth ?? 0);
        /// <summary>
        /// ATK, prendre en compte base et equipement potentiel
        /// </summary>
        public int Attack => _baseAttack + (CurrentEquipment?.BonusAttack ?? 0);
        /// <summary>
        /// DEF, prendre en compte base et equipement potentiel
        /// </summary>
        public int Defense => _baseDefense + (CurrentEquipment?.BonusDefense ?? 0);

        /// <summary>
        /// SPE, prendre en compte base et equipement potentiel
        /// </summary>
        public int Speed => _baseSpeed + (CurrentEquipment?.BonusSpeed ?? 0);
        /// <summary>
        /// Equipement unique du personnage
        /// </summary>
        public Equipment CurrentEquipment { get; private set; }
        /// <summary>
        /// null si pas de status
        /// </summary>
        public StatusEffect CurrentStatus { get; private set; }

        public bool IsAlive
        {
            get { return CurrentHealth >= 0; }
        }
        


        /// <summary>
        /// Application d'un skill contre le personnage
        /// On pourrait potentiellement avoir besoin de connaitre le personnage attaquant,
        /// Vous pouvez adapter au besoin
        /// </summary>
        /// <param name="s">skill attaquant</param>
        /// <exception cref="NotImplementedException"></exception>
        public void ReceiveAttack(Skill s)
        {
                       
            int damageReceived = CalculateDamageReceived(s);

            CurrentHealth -= damageReceived;

            if (CurrentHealth < 0)
            {
                CurrentHealth = 0;
            }

            if (!IsAlive)
            {
                return;
            }

        }

        private int CalculateDamageReceived(Skill s)
        {
            int baseDamage = Math.Max(s.Power - Defense, 0);

            if (IsCriticalHit())
            {
                Console.WriteLine("Critical Hit!");
                return (int)(baseDamage * 1.5); 
            }

            return baseDamage;
        }

        private bool IsCriticalHit()
        {
            Random random = new Random();
            double randomValue = random.NextDouble();

            // Vérifier si le coup critique a eu lieu en comparant avec la chance critique
            return randomValue < CriticalChance;
        }



        /// <summary>
        /// Equipe un objet au personnage
        /// </summary>
        /// <param name="newEquipment">equipement a appliquer</param>
        /// <exception cref="ArgumentNullException">Si equipement est null</exception>
        public void Equip(Equipment newEquipment)
        {
            if (newEquipment == null)
            {
                throw new ArgumentNullException("newEquipment");
            }

            CurrentEquipment = newEquipment;
        }
        /// <summary>
        /// Desequipe l'objet en cours au personnage
        /// </summary>
        public void Unequip()
        {
            CurrentEquipment = null;
        }

    }
}
