using UnityEngine;

[RequireComponent(typeof(Renderer))] // require from object to have Renderer script
public class Home : MonoBehaviour
{
    [SerializeField] private Vector3 baseScale = new Vector3(0.9f, 0.9f, 0.9f);

    public int Id { get; set; }

    public Vector3 BaseScale
    {
        get => baseScale;
        set => baseScale = value;
    }

    public void SetPosition(float x, float z, float y = 0)
    {
        transform.position = new Vector3(x, y, z);
    }

    public void SetRadius(float r)
    {
        transform.localScale = baseScale * r;
    }
}