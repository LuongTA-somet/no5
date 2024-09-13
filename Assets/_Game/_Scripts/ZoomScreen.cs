using DG.Tweening;
using UnityEngine;

public class MouseZoom : MonoBehaviour
{
    [SerializeField] private Camera camera;            
    public float zoomSpeed = 5f;     
    public float minZoom = 5f;       
    public float maxZoom = 20f;

    private Vector2 minBounds=Vector2.zero;
    private Vector2 maxBounds=Vector2.one;
    void Update()
    {      
        Vector3 mousePos = Input.mousePosition;    
        Vector3 mouseWorldPosBeforeZoom = camera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, camera.transform.position.z));
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            camera.orthographicSize = Mathf.Clamp(camera.orthographicSize - scroll * zoomSpeed, minZoom, maxZoom);
            Vector3 mouseWorldPosAfterZoom = camera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, camera.transform.position.z));
            Vector3 direction = mouseWorldPosBeforeZoom - mouseWorldPosAfterZoom;
            camera.transform.position += direction;

            if (scroll > 0)
            {
                ClampCamera();
            }
            else if (scroll < 0)
            {
                camera.transform.position =new Vector3(0,0,-10);
            }
               
           
        }
    }
    private void ClampCamera()
    {
        if (camera.transform.position.y >= 1f )
        {
            camera.transform.position = new Vector3(camera.transform.position.x, 1f, -10);
        }
        if (camera.transform.position.y <= -1.7f )
        {
            camera.transform.position = new Vector3(camera.transform.position.x, -1.7f, -10);
        }
         if (camera.transform.position.x >= 2f )
        {
            camera.transform.position = new Vector3(2f,camera.transform.position.y, -10);
        }
        if (camera.transform.position.x <= -2f )
        {
            camera.transform.position = new Vector3(2f, camera.transform.position.y, -10);
        }
        transform.DOMove(Vector3.zero, 0.5f).SetEase(Ease.OutBack);
       
    }
}
