using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class TurretUIElement : MonoBehaviour
{
    public DragHandler dragHandler;

    // Start is called before the first frame update
    void Start()
    {
        //ui elements. set up
        Image image = GetComponent<Image>();
        Button button = gameObject.AddComponent<Button>();

        dragHandler = gameObject.AddComponent<DragHandler>(); // configure draghandler
        button.transition = Selectable.Transition.None;  // transitions
    }

    // Update is called once per frame
    void Update()
    {

    }
}
