using UnityEngine;

public class ConveyorBeltScroller : MonoBehaviour
{
    public Conductor conductor;
    public float beatsPerTile = 1f; // how many beats for one full repeat

    private Renderer rend;
    private Material mat;

    void Start()
    {
        rend = GetComponent<Renderer>();
        mat = rend.material;
    }

    void Update()
    {
        float beat = conductor.songPositionInBeats;

        float offset = beat / beatsPerTile;

        mat.mainTextureOffset = new Vector2(offset, 0);
    }
}
