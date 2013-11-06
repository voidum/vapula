using System.Windows.Forms;
using TCM.Runtime;

namespace sample_xui
{
    public class Program
    {
        public ReturnCode Run(int function, Envelope envelope, Context context)
        {
            switch (function)
            {
                case 1:
                    //context.ReplyCtrlCode();
                    FrmA form = new FrmA();
                    form.ShowDialog();

                    break;
            }

            //string text = envelope.Read(1);
            //A obj = new A();
            //obj.Test(text);
            return ReturnCode.Normal;
        }
    }

    public class A
    {
        public void Test(string text)
        {
            MessageBox.Show(text);
        }
    }
}
