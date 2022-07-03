using UnityEngine;

public class Grabber : MonoBehaviour
{
    #region Variables

    private GameObject selectedObject;

    #endregion

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (selectedObject == null)
            {
                RaycastHit hit = castRay();

                if (hit.collider != null)
                {
                    if (!hit.collider.CompareTag("Selectable"))
                    {
                        return;
                    }

                    selectedObject = hit.collider.gameObject;
                    Cursor.visible = false; 
                }
            }

            else
            {
                Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedObject.transform.position).z);
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
                selectedObject.transform.position = new Vector3(worldPosition.x, 1f, worldPosition.z);

                selectedObject = null;
                Cursor.visible = true;

            }
        }   

        if (selectedObject != null)
        {

             Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedObject.transform.position).z);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
            if (worldPosition.x > 3.5f && worldPosition.x < 30.5f && worldPosition.z < 4.5f && worldPosition.z > -22.5f)
            {
                selectedObject.transform.position = new Vector3(worldPosition.x, 3f, worldPosition.z);
            }
            
            
        }
    }

    private RaycastHit castRay()
    {
        Vector3 screenMousePosFar = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.farClipPlane);

        Vector3 screenMousePosNear = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.nearClipPlane);

        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);

        RaycastHit hit;

        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);

        return hit;
    }
}
