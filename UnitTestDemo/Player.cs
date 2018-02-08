namespace Engine
{
    public class Player
    {
        public int CurrentHitPoints { get; private set; }

        public Player(int _hitPoints)
        {
            CurrentHitPoints = _hitPoints;
        }

        public void ReceiveDamage(int _hitPointsOfDamage)
        {
            if (_hitPointsOfDamage < 0)
            {
                return;
            }

            CurrentHitPoints = (CurrentHitPoints - _hitPointsOfDamage);

            if (CurrentHitPoints < 0)
            {
                CurrentHitPoints = 0;
            }

        }
    }
}
