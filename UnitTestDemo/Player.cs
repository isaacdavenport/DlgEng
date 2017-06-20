namespace Engine
{
    public class Player
    {
        public int CurrentHitPoints { get; private set; }

        public Player(int hitPoints)
        {
            CurrentHitPoints = hitPoints;
        }

        public void ReceiveDamage(int hitPointsOfDamage)
        {
            if (hitPointsOfDamage < 0)
            {
                return;
            }

            CurrentHitPoints = (CurrentHitPoints - hitPointsOfDamage);

            if (CurrentHitPoints < 0)
            {
                CurrentHitPoints = 0;
            }

        }
    }
}
