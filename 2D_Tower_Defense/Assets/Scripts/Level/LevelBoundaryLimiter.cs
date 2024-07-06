using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// ������������ �������. �������� � ������ �� �������� LevelBoundary ���� ������� ������� �� �����.
    /// �������� �� ������, ������� ����� ����������
    /// </summary>
    public class LevelBoundaryLimiter : MonoBehaviour
    {
        public enum Mode
        {
            Limit,
            Teleport
        }

        [SerializeField] private Mode m_LimitMode;
        public Mode LimitMode => m_LimitMode;

        private void Update()
        {
            if (LevelBoundary.Instance == null) return;

            var lb = LevelBoundary.Instance;
            var r = lb.Radius;

            if (transform.position.magnitude > r)
            {
                if(LimitMode == Mode.Limit)
                {
                    transform.position = transform.position.normalized * r;
                }

                if (LimitMode == Mode.Teleport)
                {
                    transform.position = -transform.position.normalized * r;
                }
            }
        }
    }

}