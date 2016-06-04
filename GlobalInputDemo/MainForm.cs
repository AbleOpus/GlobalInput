using System;
using System.Windows.Forms;
using GlobalInput.Keyboard;
using GlobalInput.Keyboard.KeyNaming;

namespace GlobalInputDemo
{
    public partial class MainForm : Form
    {
        private ListBox listBoxToClear;
        private readonly HotkeyHooker hotKeyHooker = new HotkeyHooker();

        /// <summary>
        /// Gets the currently selected hotkey.
        /// </summary>
        private Keys SelectedHotKey => listBoxBindings.SelectedIndex == -1 ?
            Keys.None : ((HotkeyBinding)listBoxBindings.SelectedItem).Hotkey;

        public MainForm()
        {
            InitializeComponent();

            // Setup keyboard hooks
            hotkeyTextBox.AllowSoloModifiers = true;
            listBoxBindings.DataSource = hotKeyHooker.HotkeyBindings;
            // Setup checkbox bindings
            BindCheckBoxValue(checkBoxInvokeEnabled, hotKeyHooker, nameof(hotKeyHooker.InvokeEnabled));
            BindCheckBoxValue(checkBoxKeyboardHookEnabled, keyboardHooker, nameof(keyboardHooker.Hooked));
            BindCheckBoxValue(checkBoxMouseHookerEnabled, mouseHooker, nameof(mouseHooker.Hooked));
            BindCheckBoxValue(checkBoxKeyCallNext, keyboardHooker, nameof(keyboardHooker.CallNextHook));
            BindCheckBoxValue(checkBoxMouseCallNext, mouseHooker, nameof(mouseHooker.CallNextHook));
        }

        /// <summary>
        /// Binds the Checked value of the specified checkbox to the specified data member.
        /// </summary>
        /// <param name="checkBox">The CheckBox to add the data-binding to.</param>
        /// <param name="dataSource">The source of the data.</param>
        /// <param name="dataSourceMemberName">The name of the data source member.</param>
        private static void BindCheckBoxValue(CheckBox checkBox, object dataSource, string dataSourceMemberName)
        {
            checkBox.DataBindings.Add("Checked", dataSource, dataSourceMemberName,
                false, DataSourceUpdateMode.OnPropertyChanged);
        }

        private static string MouseEventArgsToString(MouseEventArgs args, bool isDown)
        {
            string mode = isDown ? "Down" : "Up";
            return $"Button {args.Button} {mode} at {args.X} x {args.Y}";
        }

        private void buttonBind_Click(object sender, EventArgs e)
        {
            if (hotkeyTextBox.Hotkey == Keys.None)
            {
                ShowError("Input a key sequence before trying to bind.");
                return;
            }

            try
            {
                var key = hotkeyTextBox.Hotkey;
                hotKeyHooker.Hook(key, () => listBoxHKLog.Items.Insert(0, $@"""{KeyNaming.KeyDataToString(key)}"" detected."));
            }
            catch (HotkeyAlreadyBoundException ex)
            {
                ShowError(ex.Message);
            }
        }

        private static void ShowError(string message)
        {
            MessageBox.Show(message, Application.ProductName,
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void buttonUnbindAll_Click(object sender, EventArgs e)
        {
            hotKeyHooker.UnhookAll();
        }

        private void buttonUnbindSelected_Click(object sender, EventArgs e)
        {
            if (listBoxBindings.SelectedIndex == -1)
            {
                ShowError("No hotkey binding selected.");
            }
            else
            {
                hotKeyHooker.Unhook(SelectedHotKey);
            }
        }

        private void menuItemClear_Click(object sender, EventArgs e)
        {
            listBoxToClear?.Items.Clear();
        }

        private void listBox_MouseEnter(object sender, EventArgs e)
        {
            listBoxToClear = (ListBox)sender;
        }

        private void keyboardHooker_KeyDown(object sender, KeyEventArgs e)
        {
            listBoxKeyboardHook.Items.Insert(0, "Key down: " + e.KeyCode);
        }

        private void keyboardHooker_KeyUp(object sender, KeyEventArgs e)
        {
            listBoxKeyboardHook.Items.Insert(0, "Key up: " + e.KeyCode);
        }

        private void mouseHooker_MouseDown(object sender, MouseEventArgs e)
        {
            listBoxMousing.Items.Insert(0, MouseEventArgsToString(e, true));
        }

        private void mouseHooker_MouseMoved(object sender, MouseEventArgs e)
        {
            labelMousePos.Text = $@"Mouse Position: {e.X} x {e.Y}";
        }

        private void mouseHooker_MouseUp(object sender, MouseEventArgs e)
        {
            listBoxMousing.Items.Insert(0, MouseEventArgsToString(e, false));
        }

        private void mouseHooker_MouseWheel(object sender, MouseEventArgs e)
        {
            listBoxMousing.Items.Insert(0, "Wheeled " + e.Delta + " notches");
        }

        private async void buttonFindOccupied_Click(object sender, EventArgs e)
        {
            buttonFindOccupied.Enabled = false;
            var occupied = await HotkeyHooker.GetOccupiedHotkeysTaskAsync();
            listBoxOccupied.Items.Clear();

            foreach (var keys in occupied)
            {
                listBoxOccupied.Items.Add(KeyNaming.KeyDataToString(keys));
            }

            buttonFindOccupied.Enabled = true;
        }
    }
}
