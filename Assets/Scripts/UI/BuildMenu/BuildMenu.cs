using UnityEngine;
using UnityEngine.UI;

public class BuildMenu : MonoBehaviour
{
    private Transform _placeForBuilding;
    [SerializeField] private GameObject _buildTowerObject;
    [SerializeField] private GameObject[] _listObjects;
    [SerializeField] private int[] _priceTowers;
    private BuildTower _buildTower;
    
    public void PlaceForBuilding(Transform value) => _placeForBuilding = value;

    public void UpdateButtons(GameObject placeForBuildObject)
    {
        _buildTower = _buildTowerObject.GetComponent<BuildTower>();
        int count = 0;
        for (int i = 0; i < transform.childCount; i++)
        {   
            Transform buttonObject = transform.GetChild(i);

            if (buttonObject.name != "Element")
            {
                if (buttonObject.name == "Stop")
                {
                    buttonObject.gameObject.GetComponent<Button>().onClick.AddListener(
                        () =>
                        {
                            gameObject.SetActive(false);
                            _placeForBuilding.GetComponent<PlaceForBuild>().IsHighlighted = false;
                        }
                        );
                }
                continue;
            }

            ButtonData buttonComponent = buttonObject.gameObject.AddComponent<ButtonData>();
            buttonComponent.TowerObject = _listObjects[count];
            int localCount = count;
            buttonObject.gameObject.GetComponent<Button>().onClick.AddListener(
                () => _buildTower.Build(buttonComponent.TowerObject, _placeForBuilding,
                    placeForBuildObject, _priceTowers[localCount])
                );
            count++;
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