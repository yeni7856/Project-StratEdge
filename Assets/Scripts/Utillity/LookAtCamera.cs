using UnityEngine;

namespace MyStartEdge
{
    public class LookAtCamera : MonoBehaviour
    {
        private Camera mainCamera;
        //public float xRotationOffset = 70f; // 카메라 X축 회전 오프셋

        private void Start()
        {
            mainCamera = Camera.main;
        }
        private void Update()
        {
            // 현재 오브젝트의 포지션 유지
            Vector3 currentPosition = transform.position;

            // 카메라의 X축 회전 값을 가져와서 오브젝트의 X축 회전에 적용
            float cameraXRotation = mainCamera.transform.position.x;
            transform.rotation = Quaternion.Euler(cameraXRotation, 0, 0);

            // 포지션 값을 다시 설정하여 변경되지 않도록 함
            transform.position = currentPosition;
            this.transform.LookAt(mainCamera.transform);
        }
    }
}
