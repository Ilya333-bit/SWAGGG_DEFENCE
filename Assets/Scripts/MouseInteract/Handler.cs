using UnityEngine;

public class Handler : MonoBehaviour
{
    [SerializeField] private string _objectTag = "PlaceForBuild";
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray);

            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.CompareTag(_objectTag))
                {
                    hit.collider.gameObject.GetComponent<PlaceForBuild>().DisplayMenu();
                    break;
                }
            }
        }
    }
}
