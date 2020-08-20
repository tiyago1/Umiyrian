using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Umiyrian.Inputs
{
    public class InputListener : MonoBehaviour
    {
        private PlayerActions playerActions;

        void OnEnable()
        {
            // See PlayerActions.cs for this setup.
            playerActions = PlayerActions.CreateWithDefaultBindings();
            //playerActions.Move.OnLastInputTypeChanged += ( lastInputType ) => Debug.Log( lastInputType );
        }

        private void Update()
        {

        }

        void OnDisable()
        {
            // This properly disposes of the action set and unsubscribes it from
            // update events so that it doesn't do additional processing unnecessarily.
            playerActions.Destroy();
        }

    }
}
