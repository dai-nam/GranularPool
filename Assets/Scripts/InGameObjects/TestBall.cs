
using Assets.Scripts.Core;


namespace Assets.Scripts.InGameObjects
{
    public class TestBall : Ball
    {
        static int _testBallId;
        public int testBallId;

        void Awake()
        {
            onTable = true;
            testBallId = _testBallId++;
        }

        private void Update()
        {
            onTable = Table.Instance.CheckIfOnTable(transform.position);

        }


    
    }
}
