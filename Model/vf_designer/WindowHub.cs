using System.Collections.Generic;
using System.Windows.Forms;

namespace Vapula.Designer
{
    public class WindowHub
    {
        public enum State
        {
            Visible = 0,
            Hidden = 1
        }

        private List<IWindow> _Windows
            = new List<IWindow>();

        public IWindow this[string id]
        {
            get
            {
                foreach (var window in _Windows)
                    if (window.Id == id)
                        return window;
                return null;
            }
        }

        public List<IWindow> Windows
        {
            get { return _Windows; }
        }

        public void Add(IWindow window)
        {
            var w = this[window.Id];
            if (w == null)
            {
                _Windows.Add(window);
                window.State = State.Hidden;
            }
        }

        public void Remove(string id)
        {
            var w = this[id];
            if (w != null)
            {
                (w as Form).Dispose();
                _Windows.Remove(w);
            }
        }

        public IWindow Show(string id)
        {
            var w = this[id];
            if (w == null)
                return null;
            w.State = State.Visible;
            return w;
        }

        public void Hide(string id)
        {
            var window = this[id];
            if (window == null)
                return;
            window.State = State.Hidden;
        }

        public void CloseAll()
        {
            foreach (var window in _Windows)
            {
                var dlg = window as Form;
                dlg.Dispose();
            }
        }
    }
}
