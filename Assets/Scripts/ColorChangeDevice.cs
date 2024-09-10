using DG.Tweening;
using UnityEngine;

public class ColorChangeDevice : MonoBehaviour
{
    [SerializeField]
    private float _colorChangeDuration = 2.0f;

    private Renderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void Operate()
    {
        Color color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        _renderer.material.DOColor(color, _colorChangeDuration);
    }
}
