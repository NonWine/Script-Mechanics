using UnityEngine;

public class DragController : MonoBehaviour
{
    [SerializeField] private string _tagGround;
    [SerializeField] private int _step;
    private Vector3 _target;
    
    private void Start()
    {
        _target = transform.position;
    }

    void OnMouseDrag()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform.gameObject.tag ==  _tagGround)
            {
                _target = Vector3Int.RoundToInt(new Vector3(hit.point.x / _step, 0, hit.point.z / _step)) * _step;
    
            }
            if (hit.transform.gameObject.tag == gameObject.tag) { transform.position = hit.transform.position; }
        }
    }
    
    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _target, Time.deltaTime * 5f);
    }
}
