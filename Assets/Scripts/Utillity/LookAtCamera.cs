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
            // 현재 오브젝트의 포지션 유지
            Vector3 currentPosition = transform.position;
            currentPosition.y = transform.position.y;

            //카메라 방향을 바라보기
            transform.position = currentPosition;
            this.transform.LookAt(mainCamera.transform);

            //회전값 저장 y값고정
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
                fixedY, transform.rotation.eulerAngles.z);
        }
    }
}
