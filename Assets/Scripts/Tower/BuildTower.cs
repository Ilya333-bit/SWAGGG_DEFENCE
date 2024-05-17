using UnityEngine;

public class BuildTower: MonoBehaviour
{
    public void Build(GameObject gameObject, Transform transformPlace, GameObject placeForBuildObject)
    {
        if (!placeForBuildObject.GetComponent<PlaceForBuild>().IsOccupied)
        {
            Instantiate(gameObject, transformPlace.position, transformPlace.rotation);
            placeForBuildObject.GetComponent<PlaceForBuild>().IsOccupied = true;
            placeForBuildObject.GetComponent<PlaceForBuild>().IsHighlighted = false;
        }
        else
        {
            Debug.Log("You can't build here!");
        }
    }
}
