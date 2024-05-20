using UnityEngine;

public class BuildTower: MonoBehaviour
{
    [SerializeField] private GameObject _updateHudObject;
    public void Build(GameObject gameObject, Transform transformPlace, GameObject placeForBuildObject, int price)
    {
        if (!placeForBuildObject.GetComponent<PlaceForBuild>().IsOccupied && price <= Economy.Coins)
        {
            Instantiate(gameObject, transformPlace.position, transformPlace.rotation);
            placeForBuildObject.GetComponent<PlaceForBuild>().IsOccupied = true;
            placeForBuildObject.GetComponent<PlaceForBuild>().IsHighlighted = false;
            Economy.Coins = Economy.Coins - price;
        } else if (price > Economy.Coins)
        {
            StartCoroutine(_updateHudObject.GetComponent<UpdateHud>().ActiveTextBuild("Нет денег"));
            placeForBuildObject.GetComponent<PlaceForBuild>().IsHighlighted = false;
        }
        else
        {
            Debug.Log("You can't build here!");
        }
    }
}
