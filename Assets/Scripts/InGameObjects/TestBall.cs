
using Assets.Scripts.Core;


namespace Assets.Scripts.InGameObjects
{
    public class TestBall : Ball
    {
        static int _testBallId;
        bool isClicked;
        public int testBallId;

        void Awake()
        {
            onTable = true;
            testBallId = _testBallId++;
        }

        private void Update()
        {
            onTable = Table.Instance.CheckIfOnTable(transform.position);
            float angle = SoundSampler.Instance.ConvertBallPositionToGrainPosition(GetXandZposition());
            if (isClicked)
            {
                ClickAndDrag();
            }
            // print("XZ: " + Table.Instance.GetCenterXandZposition()+" Angle: "+angle);
        }


        private void OnMouseDown()
        {
            isClicked = true;
        }


        private void OnMouseUp()
        {
            isClicked = false;
        }

        void ClickAndDrag()
        {
            transform.position = MouseInput.currentMousePosTranslated;
        }
    }
}
