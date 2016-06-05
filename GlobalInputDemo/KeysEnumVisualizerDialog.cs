using System;
using System.Collections;
using System.Text;
using System.Windows.Forms;
using GlobalInput.Keyboard;

namespace GlobalInputDemo
{
    public partial class KeysEnumVisualizerDialog : Form
    {
        public KeysEnumVisualizerDialog()
        {
            InitializeComponent();
        }

        private void hotkeyTextBox_TextChanged(object sender, EventArgs e)
        {
            int num = (int)hotkeyTextBox.Hotkey;
            StringBuilder SB = new StringBuilder();

            SB.AppendLine("All bits: ");
            byte[] bytes = BitConverter.GetBytes(num);
            BitArray ba = new BitArray(bytes);
            for (int i = 0; i < ba.Count; i++)
                SB.Append(ba[i] ? "1" : "0");

            SB.AppendLine().AppendLine();
            SB.AppendLine("Modifier bits: ");
            for (int i = ba.Count / 2; i < ba.Count; i++)
                SB.Append(ba[i] ? "1" : "0");

            SB.AppendLine().AppendLine();
            SB.AppendLine("KeyCode bits: ");
            for (int i = 0; i < ba.Count / 2; i++)
                SB.Append(ba[i] ? "1" : "0");

            SB.AppendLine().AppendLine();
            SB.AppendLine("KeyCode number: " + (int)hotkeyTextBox.Hotkey.GetKeyCode());

            textBoxOutput.Text = SB.ToString();
        }

        public static bool GetBit(byte b, int bitNumber)
        {
            return (b & (1 << bitNumber)) != 0;
        }
    }
}
