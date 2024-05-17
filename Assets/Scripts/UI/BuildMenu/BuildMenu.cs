using UnityEngine;
using UnityEngine.UI;

public class BuildMenu : MonoBehaviour
{
    private Transform _placeForBuilding;
    [SerializeField] private GameObject _buildTowerObject;
    [SerializeField] private GameObject[] _listObjects;
    private BuildTower _buildTower;

    public void PlaceForBuilding(Transform value) => _placeForBuilding = value;

    public void UpdateButtons(GameObject placeForBuildObject)
    {
        _buildTower = _buildTowerObject.GetComponent<BuildTower>();
        
        for (int i = 0; i < transform.childCount; i++)
        {   
            Transform buttonObject = transform.GetChild(i);

            if (buttonObject.name != "Element")
            {
                continue;
            }
            
            ButtonData buttonComponent = buttonObject.gameObject.AddComponent<ButtonData>();
            buttonComponent.TowerObject = _listObjects[i];
            
            buttonObject.gameObject.GetComponent<Button>().onClick.AddListener(
                () => _buildTower.Build(buttonComponent.TowerObject, _placeForBuilding, placeForBuildObject)
                );
        }
    }

    public void RemoveAllListenersOfButtons()
    {
        _buildTower = _buildTowerObject.GetComponent<BuildTower>();
        
        for (int i = 0; i < transform.childCount; i++)
        {   
            Transform buttonObject = transform.GetChild(i);

            if (buttonObject.name != "Element")
            {
                continue;
            }
            
            buttonObject.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        }
    }
}