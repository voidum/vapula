using System;
using System.Windows.Forms;

namespace Vapula.Designer.UI
{
    public partial class UctData : UserControl
    {
        public UctData()
        {
            InitializeComponent();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            int width = Width < 900 ? 900 : Width;
            int height = Height < 600 ? 600 : Height;
            LsvSource.Width = LsvTarget.Width = width / 2 - 30;
            LsvSource.Height = LsvTarget.Height = height - 60;
            LblSource.Left = LsvSource.Left = LsvTarget.Right + 10;
        }
    }
}
