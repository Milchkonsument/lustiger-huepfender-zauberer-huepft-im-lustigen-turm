using Unity.VisualScripting;
using UnityEngine;

public class BackgroundRenderer : MonoBehaviour
{
    public GameObject background;
    private GameObject _lastBackground;
    private GameObject _player;
    public float renderDistance;

    public int backgroundAmmount;
    private int _backgroundIndex = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        while (_backgroundIndex < backgroundAmmount)
        {
            _lastBackground = RenderBackground();
        }
    }
    
    void Update()
    {
        if (_player.transform.position.y + renderDistance > _lastBackground.transform.position.y)
        {
            _lastBackground = RenderBackground();
        }
    }

    GameObject RenderBackground()
    {
        _backgroundIndex++;
       return GameObject.Instantiate(background, transform.position + new Vector3(0,background.GetComponent<SpriteRenderer>().bounds.size.y*_backgroundIndex,0), Quaternion.identity,parent:transform);
       
    }
}
