using UnityEngine;
using UnityEngine.UI;
public class ShowShopButton : MonoBehaviour
{

    [SerializeField] private GameObject _showButton;
    [SerializeField] private Image _image;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _showButton.SetActive(true);
            _image.color = new Color32(26,255,0,255); 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _showButton.SetActive(false);
            _image.color = new Color32(255, 255, 255, 255);
        }

    }

    public void OffButton() => _showButton.SetActive(false);

    public GameObject GetButton() { return _showButton; }

}
