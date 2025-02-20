using UnityEngine;

namespace MyStartEdge
{
    public class LookAtCamera : MonoBehaviour
    {
        private Camera mainCamera;
        //public float xRotationOffset = 70f; // ī�޶� X�� ȸ�� ������

        private void Start()
        {
            mainCamera = Camera.main;
        }
        private void Update()
        {
            // ���� ������Ʈ�� ������ ����
            Vector3 currentPosition = transform.position;

            // ī�޶��� X�� ȸ�� ���� �����ͼ� ������Ʈ�� X�� ȸ���� ����
            float cameraXRotation = mainCamera.transform.position.x;
            transform.rotation = Quaternion.Euler(cameraXRotation, 0, 0);

            // ������ ���� �ٽ� �����Ͽ� ������� �ʵ��� ��
            transform.position = currentPosition;
            this.transform.LookAt(mainCamera.transform);
        }
    }
}
