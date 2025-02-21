using UnityEngine;

namespace MyStartEdge
{
    public class LookAtCamera : MonoBehaviour
    {
        private Camera mainCamera;
        private float fixedY;

        private void Start()
        {
            mainCamera = Camera.main;
            fixedY = transform.eulerAngles.y;
        }
        private void Update()
        {
            // ���� ������Ʈ�� ������ ����
            Vector3 currentPosition = transform.position;
            currentPosition.y = transform.position.y;

            //ī�޶� ������ �ٶ󺸱�
            transform.position = currentPosition;
            this.transform.LookAt(mainCamera.transform);

            //ȸ���� ���� y������
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
                fixedY, transform.rotation.eulerAngles.z);
        }
    }
}
