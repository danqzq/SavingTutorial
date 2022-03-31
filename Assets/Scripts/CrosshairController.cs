using UnityEngine;

namespace Dan
{
    public class CrosshairController : MonoBehaviour
    {
        [SerializeField] private Transform _crosshair;
        [SerializeField] private float _maxDistance = 10f;

        private Camera _camera;
        
        public Vector3 GetCrosshairPosition => _crosshair.position;
        
        private void SetCrosshairPosition()
        {
            var mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
            _crosshair.position = new Vector3((int) mousePos.x, (int) mousePos.y, _maxDistance);
        }
        
        private void Start()
        {
            _camera = Camera.main;
            Cursor.visible = false;
        }

        private void Update()
        {
            SetCrosshairPosition();
        }
        
        private void OnDestroy()
        {
            Cursor.visible = true;
        }
    }
}
