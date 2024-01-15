using Microsoft.Xna.Framework;
using SharpDX;

namespace Prod_em_on_Team3
{
    internal class EnemyObj
    {
        private Sprite[] instanceSprites = new Sprite[1];

        private int _healthPoints;
        private int _contactDMG;

        private bool _aliveStatus = true;

        public EnemyObj() { }

        public EnemyObj(Sprite headSprite, Sprite bodySprite, int healthPoints, int contactDMG)
        {
            instanceSprites[0] = headSprite;
            instanceSprites[1] = bodySprite;
            this._healthPoints = healthPoints;
            this._contactDMG = contactDMG;
        }

        public int Health
        {
            get { return _healthPoints; }
            set { _healthPoints = value; }
        }
        public int Damage
        {
            get { return _contactDMG; }
            set { _contactDMG = value; }
        }
        public bool LifeStatus
        {
            get { return _aliveStatus; }
            set { _aliveStatus = value; }
        }

    }
}
