using UnityEngine;

namespace UI
{
    public class AnyKey : MonoBehaviour
    {
        private Interactible interactKey;
        // Start is called before the first frame update
        void Start()
        {
            interactKey = GetComponent<Interactible>();
        }


        private void OnTriggerEnter2D(Collider2D col)
        {
            interactKey.interactAction.Invoke();
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            Destroy(gameObject);
        }


    }
}
