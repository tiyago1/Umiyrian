using InControl;
using UnityEngine;

namespace Umiyrian.Inputs
{
    public class PlayerActions : PlayerActionSet // PlayerActions olarak değiştirilebilir
    {
        public readonly PlayerTwoAxisAction Move;
        public readonly PlayerAction Left;
        public readonly PlayerAction Right;
        public readonly PlayerAction Up;
        public readonly PlayerAction Down;
        public readonly PlayerAction Shoot;


        public PlayerActions()
        {
            Left = CreatePlayerAction("Move Left");
            Right = CreatePlayerAction("Move Right");
            Up = CreatePlayerAction("Move Up");
            Down = CreatePlayerAction("Move Down");
            Move = CreateTwoAxisPlayerAction(Left, Right, Down, Up);
            Shoot = CreatePlayerAction("Shoot");
        }

        public static PlayerActions CreateWithDefaultBindings()
        {
            var playerActions = new PlayerActions();

            // How to set up mutually exclusive keyboard bindings with a modifier key.
            // playerActions.Back.AddDefaultBinding( Key.Shift, Key.Tab );
            // playerActions.Next.AddDefaultBinding( KeyCombo.With( Key.Tab ).AndNot( Key.Shift ) );
            #region KeyBoard&Mouse

            playerActions.Up.AddDefaultBinding(Key.UpArrow);
            playerActions.Down.AddDefaultBinding(Key.DownArrow);
            playerActions.Left.AddDefaultBinding(Key.LeftArrow);
            playerActions.Right.AddDefaultBinding(Key.RightArrow);

            playerActions.Up.AddDefaultBinding(Key.W);
            playerActions.Down.AddDefaultBinding(Key.S);
            playerActions.Left.AddDefaultBinding(Key.A);
            playerActions.Right.AddDefaultBinding(Key.D);

            playerActions.Shoot.AddDefaultBinding(Mouse.LeftButton);

            #endregion

            #region GamePad

            playerActions.Left.AddDefaultBinding(InputControlType.LeftStickLeft);
            playerActions.Right.AddDefaultBinding(InputControlType.LeftStickRight);
            playerActions.Up.AddDefaultBinding(InputControlType.LeftStickUp);
            playerActions.Down.AddDefaultBinding(InputControlType.LeftStickDown);

            playerActions.Shoot.AddDefaultBinding(InputControlType.RightTrigger);

            #endregion

            playerActions.ListenOptions.IncludeUnknownControllers = true;
            playerActions.ListenOptions.MaxAllowedBindings = 4;
            //playerActions.ListenOptions.MaxAllowedBindingsPerType = 1;
            //playerActions.ListenOptions.AllowDuplicateBindingsPerSet = true;
            playerActions.ListenOptions.UnsetDuplicateBindingsOnSet = true;
            //playerActions.ListenOptions.IncludeMouseButtons = true;
            //playerActions.ListenOptions.IncludeModifiersAsFirstClassKeys = true;
            //playerActions.ListenOptions.IncludeMouseScrollWheel = true;

            playerActions.ListenOptions.OnBindingFound = (action, binding) =>
            {
                if (binding == new KeyBindingSource(Key.Escape))
                {
                    action.StopListeningForBinding();
                    return false;
                }

                return true;
            };

            playerActions.ListenOptions.OnBindingAdded += (action, binding) => { Debug.Log("Binding added... " + binding.DeviceName + ": " + binding.Name); };

            playerActions.ListenOptions.OnBindingRejected += (action, binding, reason) => { Debug.Log("Binding rejected... " + reason); };

            return playerActions;
        }
    }
}
