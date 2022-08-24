using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Color _baseColor, _offsetColor;
    public MeshRenderer Renderer;
    public GameObject _highlight;

    public Status status;
    public enum Status {Unoccupied, Public, Private, Other }

    public void Init(bool isOffset)
    {
        Renderer.material.color = isOffset ? _offsetColor : _baseColor;
    }

    void OnMouseEnter()
    {
        var colorRender = GetComponent<MeshRenderer>();

        var color = colorRender.material.color;
        color.a = 0.2f;
        colorRender.material.SetColor("_Color", color);

        //_highlight.SetActive(true);
    }

    void OnMouseExit()
    {
        var colorRender = GetComponent<MeshRenderer>();

        var color = colorRender.material.color;
        color.a = 1f;
        colorRender.material.SetColor("_Color", color);
        //_highlight.SetActive(false);
    }
}
