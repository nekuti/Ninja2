using UnityEngine;

using Kojima;


namespace Kondo
{
    public class HandStateChange : MonoBehaviour
    {

        private static Player player;

        // Use this for initialization
        void Start()
        {
            player =  transform.GetComponent<Player>();
        }

  
    }
}

