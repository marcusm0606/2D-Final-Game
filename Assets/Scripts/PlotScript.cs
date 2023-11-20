using UnityEngine;
using UnityEngine.EventSystems;

public class PlotScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;
    private GameObject tower;

    private Color startColor;

    private void Start()
    {
        startColor = sr.color;
    }

    private void OnMouseEnter()
    {
        sr.color = hoverColor;
    }

    private void OnMouseExit()
    {
        sr.color = startColor;
    }

    private void OnMouseDown()
    {
        
        if (EventSystem.current.IsPointerOverGameObject())
            return; 

        
        if (tower != null)
            return;

        Tower towerToBuild = BuildManager.main.GetSelectedTower();

        if (towerToBuild != null && towerToBuild.cost > LevelManager.main.currency)
        {
            Debug.Log("BROKIE");
            return;
        }

        LevelManager.main.SpendCurrency(towerToBuild.cost);
        tower = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
    }
}
