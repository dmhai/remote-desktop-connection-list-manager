using System;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Collections.Generic;

namespace Remote_Desktop_Connection_List_Manager
{
    public partial class ManagerForm : Form
    {
        public ManagerForm()
        {
            InitializeComponent();
            IntialSetup();
        }

        private void ManagerFormLoad(object sender, EventArgs e)
        {
            new ToolTip().SetToolTip(add, "Add");
            new ToolTip().SetToolTip(remove, "Remove selected");
            new ToolTip().SetToolTip(up, "Move selected up");
            new ToolTip().SetToolTip(down, "Move selected down");
        }

        private void IntialSetup()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Terminal Server Client\Default");
            if (key != null)
            {
                foreach (var computer in key.GetValueNames())
                {
                    computers.Items.Add(key.GetValue(computer));
                }
            }
            if (computers.Items.Count > 9)
            {
                add.Enabled = false;
            }
        }

        private void UpOnClick(object sender, EventArgs e)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Terminal Server Client\Default", true);
            ListBox.SelectedIndexCollection selectedIndices = computers.SelectedIndices;
            List<int> select = new List<int>();
            int total = selectedIndices.Count;

            if (total > 0 && selectedIndices[0] > 0)
            {
                for (int i = selectedIndices.Count - 1; i >= 0; i--)
                {
                    int place = selectedIndices[i];
                    string value = (string)computers.Items[place];
                    computers.Items.RemoveAt(place);
                    computers.Items.Insert(place - total, value);
                    select.Add(place - total + i);
                }

                foreach (var item in select)
                {
                    Console.WriteLine(item);
                    computers.SetSelected(item, true);
                }

                foreach (var computer in key.GetValueNames())
                {
                    key.DeleteValue(computer);
                }

                for (int i = 0; i < computers.Items.Count; i++)
                {
                    key.SetValue("MRU" + i, computers.Items[i]);
                }
            }
        }

        private void DownOnClick(object sender, EventArgs e)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Terminal Server Client\Default", true);
            ListBox.SelectedIndexCollection selectedIndices = computers.SelectedIndices;
            List<int> select = new List<int>();

            if (selectedIndices.Count > 0 && selectedIndices[selectedIndices.Count - 1] + 1 < computers.Items.Count)
            {
                for (int i = selectedIndices.Count - 1; i >= 0; i--) //
                {
                    int place = selectedIndices[i];
                    string value = (string)computers.Items[place];
                    computers.Items.RemoveAt(place);
                    computers.Items.Insert(place + 1, value);
                    select.Add(place + 1);
                }

                foreach (var item in select)
                {
                    Console.WriteLine(item);
                    computers.SetSelected(item, true);
                }

                foreach (var computer in key.GetValueNames())
                {
                    key.DeleteValue(computer);
                }

                for (int i = 0; i < computers.Items.Count; i++)
                {
                    key.SetValue("MRU" + i, computers.Items[i]);
                }
            }
        }

        private void AddInputKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                AddOnClick(sender, e);
            }
        }

        private void AddOnClick(object sender, EventArgs e)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Terminal Server Client\Default", true);

            if (addInput.Text != "" && key != null && computers.Items.Count < 10)
            {
                key.SetValue("MRU" + key.GetValueNames().Length, addInput.Text);
                computers.Items.Add(addInput.Text);
                addInput.Text = "";
                computers.ClearSelected();
                computers.SetSelected(computers.Items.Count - 1, true);
            }
            if (computers.Items.Count > 9)
            {
                add.Enabled = false;
            }
        }

        private void RemoveOnClick(object sender, EventArgs e)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Terminal Server Client\Default", true);
            ListBox.SelectedObjectCollection selectedItems = computers.SelectedItems;

            if (selectedItems.Count > 0 && key != null)
            {
                for (int i = selectedItems.Count - 1; i >= 0; i--)
                {
                    computers.Items.Remove(selectedItems[i]);
                }

                foreach (var computer in key.GetValueNames())
                {
                    key.DeleteValue(computer);
                }

                for (int i = 0; i < computers.Items.Count; i++)
                {
                    key.SetValue("MRU" + i, computers.Items[i]);
                }
            }
            if (computers.Items.Count < 10)
            {
                add.Enabled = true;
            }
        }

        private void titleText2_Click(object sender, EventArgs e)
        {

        }

        private void titleText1_Click(object sender, EventArgs e)
        {

        }
    }
}