using UnityEngine;

public class PlaceForBuild : MonoBehaviour
{   
    [Header("Gameplay")]
    [SerializeField] private GameObject _buildMenu;
    
    [Header("VisualEffects")]
    [SerializeField] private Material _otherMaterial;
    [SerializeField] private float transitionDuration = 0.5f;
    private MeshRenderer _meshRenderer;
    private Material _defaultMaterial;
    private bool _isHighlighted;
    private bool _isOccupied = false;

    public bool IsHighlighted
    {
        get => _isHighlighted;
        set
        {
            _isHighlighted = value;
            _meshRenderer.material = _defaultMaterial;
            _buildMenu.SetActive(false);
        }

    }

    public bool IsOccupied
    {
        get => _isOccupied;
        set => _isOccupied = value;
    }

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _defaultMaterial = _meshRenderer.material;
    }

    private void Update()
    {
        if (_isHighlighted)
        {
            TransfusionColor();
        }
    }

    public void DisplayMenu()
    {   
        StopAllPlacesForBuild();
        _meshRenderer.material = _otherMaterial;
        _isHighlighted = true;
        _buildMenu.SetActive(true);
        _buildMenu.GetComponent<BuildMenu>().PlaceForBuilding(transform);
        _buildMenu.GetComponent<BuildMenu>().UpdateButtons(gameObject);
    }

    private void TransfusionColor()
    {
        float t = Mathf.PingPong(Time.time / transitionDuration, 1f);
        _meshRenderer.material.color = Color.Lerp(_defaultMaterial.color, _otherMaterial.color, t);
    }

    private void StopAllPlacesForBuild()
    {
        Transform parentTransform = transform.parent;
        Transform[] childrenTransform = parentTransform.GetComponentsInChildren<Transform>();

        foreach (Transform child in childrenTransform)
        {   
            if (child == parentTransform) continue;
            
            child.GetComponent<PlaceForBuild>().IsHighlighted = false;
            _buildMenu.GetComponent<BuildMenu>().RemoveAllListenersOfButtons();
        }
    }
}
