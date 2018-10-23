using OpenTK.Input;
using System;

namespace Prosper.Camera
{
    internal class CameraControl
    {
        public bool AnyKeyDown => _pDown || _nDown;

        private readonly Key _p;
        private readonly Key _n;
        private readonly Action<double> _pAction;
        private readonly Action<double> _nAction;
        private bool _pDown;
        private bool _nDown;

        internal CameraControl(Key p, Key n, Action<double> pAction, Action<double> nAction)
        {
            _p = p;
            _n = n;
            _pAction = pAction;
            _nAction = nAction;
        }

        internal void OnKeyDown(Key k)
        {
            if (k == _p)
                _pDown = true;
            else if (k == _n)
                _nDown = true;
        }

        internal void OnKeyUp(Key k)
        {
            if (k == _p)
                _pDown = false;
            else if (k == _n)
                _nDown = false;
        }

        internal void OnUpdate(double dt)
        {
            if (_pDown)
                _pAction(dt);
            if (_nDown)
                _nAction(dt);
        }
    }
}
