using UnityEngine;

public class BGScroller : MonoBehaviour
{
    [SerializeField]
    private Vector2 _scrollSpeed = new Vector2(0f, .5f);

    private Material _material;

    private void Start()
    {
        _material = GetComponent<SpriteRenderer>().material;
        _material.SetVector("_ScrollSpeed", _scrollSpeed);
    }
}
