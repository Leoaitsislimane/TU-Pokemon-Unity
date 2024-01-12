using System;

namespace _2023_GC_A2_Partiel_POO.Level_2
{
    public class Fight
    {
        public Fight(Character character1, Character character2)
        {
            Character1 = character1;
            Character2 = character2;
        }

        public Character Character1 { get; }
        public Character Character2 { get; }

        /// <summary>
        /// Est-ce que la condition de victoire/défaite a été rencontrée ?
        /// </summary>
        public bool IsFightFinished => !Character1.IsAlive || !Character2.IsAlive;

        /// <summary>
        /// Jouer l'enchainement des attaques. Attention à bien gérer l'ordre des attaques par rapport à la speed des personnages
        /// </summary>
        /// <param name="skillFromCharacter1">L'attaque sélectionné par le joueur 1</param>
        /// <param name="skillFromCharacter2">L'attaque sélectionné par le joueur 2</param>
        /// <exception cref="ArgumentNullException">si une des deux attaques est null</exception>
        public void ExecuteTurn(Skill skillFromCharacter1, Skill skillFromCharacter2)
        {
            if (skillFromCharacter1 == null || skillFromCharacter2 == null)
            {
                throw new ArgumentNullException();
            }

            Character attacker = Character1;
            Character defender = Character2;

            // Décider de l'ordre des attaques en fonction de la vitesse
            if (Character1.Speed < Character2.Speed)
            {
                attacker = Character2;
                defender = Character1;
            }

            attacker.ReceiveAttack(skillFromCharacter1);

            if (!defender.IsAlive)
            {
                return;
            }

            defender.ReceiveAttack(skillFromCharacter2);

            if (!attacker.IsAlive)
            {
                return;
            }
        }
    }
}



