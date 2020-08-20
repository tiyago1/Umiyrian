using InControl;
using UnityEngine;

namespace Umiyrian.Inputs
{
    public class PlayerActions : PlayerActionSet // PlayerActions olarak değiştirilebilir
    {
        public readonly PlayerTwoAxisAction Move;
        public readonly PlayerAction LeftMove;
        public readonly PlayerAction RightMove;
        public readonly PlayerAction UpMove;
        public readonly PlayerAction DownMove;

        public readonly PlayerTwoAxisAction Aim;
        public readonly PlayerAction LeftAim;
        public readonly PlayerAction RightAim;
        public readonly PlayerAction UpAim;
        public readonly PlayerAction DownAim;

        public readonly PlayerAction Shoot;
        public readonly PlayerAction Dash;

        public PlayerActions()
        {
            LeftMove = CreatePlayerAction("LeftMove");
            RightMove = CreatePlayerAction("RightMove");
            DownMove = CreatePlayerAction("DownMove");
            UpMove = CreatePlayerAction("UpMove");
            Move = CreateTwoAxisPlayerAction(LeftMove, RightMove, DownMove, UpMove);

            LeftAim = CreatePlayerAction("LeftAim");
            RightAim = CreatePlayerAction("RightAim");
            DownAim = CreatePlayerAction("DownAim");
            UpAim = CreatePlayerAction("UpAim");
            Aim = CreateTwoAxisPlayerAction(LeftAim, RightAim, DownAim, UpAim);

            Shoot = CreatePlayerAction("Shoot");
            Dash = CreatePlayerAction("Dash");
        }

        public static PlayerActions CreateWithDefaultBindings()
        {
            var playerActions = new PlayerActions();

            // How to set up mutually exclusive keyboard bindings with a modifier key.
            // playerActions.Back.AddDefaultBinding( Key.Shift, Key.Tab );
            // playerActions.Next.AddDefaultBinding( KeyCombo.With( Key.Tab ).AndNot( Key.Shift ) );
            #region KeyBoard&Mouse

            playerActions.UpMove.AddDefaultBinding(Key.UpArrow);
            playerActions.DownMove.AddDefaultBinding(Key.DownArrow);
            playerActions.LeftMove.AddDefaultBinding(Key.LeftArrow);
            playerActions.RightMove.AddDefaultBinding(Key.RightArrow);

            playerActions.UpMove.AddDefaultBinding(Key.W);
            playerActions.DownMove.AddDefaultBinding(Key.S);
            playerActions.LeftMove.AddDefaultBinding(Key.A);
            playerActions.RightMove.AddDefaultBinding(Key.D);

            playerActions.Shoot.AddDefaultBinding(Mouse.LeftButton);
            playerActions.Dash.AddDefaultBinding(Key.LeftShift);

            playerActions.UpAim.AddDefaultBinding(Mouse.PositiveY);
            playerActions.DownAim.AddDefaultBinding(Mouse.NegativeY);
            playerActions.LeftAim.AddDefaultBinding(Mouse.NegativeX);
            playerActions.RightAim.AddDefaultBinding(Mouse.PositiveX);

            #endregion

            #region GamePad

            playerActions.LeftMove.AddDefaultBinding(InputControlType.LeftStickLeft);
            playerActions.RightMove.AddDefaultBinding(InputControlType.LeftStickRight);
            playerActions.UpMove.AddDefaultBinding(InputControlType.LeftStickUp);
            playerActions.DownMove.AddDefaultBinding(InputControlType.LeftStickDown);

            playerActions.Shoot.AddDefaultBinding(InputControlType.RightTrigger);
            playerActions.Dash.AddDefaultBinding(InputControlType.LeftTrigger);

            playerActions.LeftAim.AddDefaultBinding(InputControlType.RightStickLeft);
            playerActions.RightAim.AddDefaultBinding(InputControlType.RightStickRight);
            playerActions.DownAim.AddDefaultBinding(InputControlType.RightStickDown);
            playerActions.UpAim.AddDefaultBinding(InputControlType.RightStickUp);

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
