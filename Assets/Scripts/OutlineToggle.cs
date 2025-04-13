using UnityEngine;

public class OutlineToggle : MonoBehaviour
{
    private SpriteRenderer _renderer;
    private MaterialPropertyBlock _propBlock;

    void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _propBlock = new MaterialPropertyBlock();
    }

    void OnMouseEnter()
    {
        _renderer.GetPropertyBlock(_propBlock);
        _propBlock.SetFloat("_OutlineEnabled", 1f); // Enable outline
        _renderer.SetPropertyBlock(_propBlock);
    }

    void OnMouseExit()
    {
        _renderer.GetPropertyBlock(_propBlock);
        _propBlock.SetFloat("_OutlineEnabled", 0f); // Disable outline
        _renderer.SetPropertyBlock(_propBlock);
    }
}