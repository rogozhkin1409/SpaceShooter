using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _backgroundSprite;
    [SerializeField] private int _tilesX;
    [SerializeField] private int _tilesY;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void GenerateBackground()
    {
        for (int y = 0; y < _tilesY; y++)
        {
            for (int x = 0; x < _tilesX; x++)
            {
                var tile = Instantiate(_backgroundSprite, transform);
                tile.transform.localPosition = new Vector2(x * tile.bounds.size.x, y * tile.bounds.size.y);
            }
        }
    }
}
