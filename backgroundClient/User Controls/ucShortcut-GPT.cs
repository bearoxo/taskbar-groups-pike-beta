using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using backgroundClient.Classes;

namespace backgroundClient.User_controls
{
    public partial class ucShortcut : UserControl
    {
        public ProgramShortcut Psc { get; set; }
        public frmMain MotherForm { get; set; }
        public Image bkgImage { get; set; }
        public Category loadedCategory { get; set; }
        private ContextMenuStrip contextMenu;

        public ucShortcut()
        {
            InitializeComponent();
            InitializeContextMenu();
        }

        private void ucShortcut_Load(object sender, EventArgs e)
        {
            this.Size = new Size(MotherForm.ucShortcutSize, MotherForm.ucShortcutSize);
            this.Show();
            this.BringToFront();
            this.BackColor = MotherForm.BackColor;
            picIcon.BackgroundImage = bkgImage;
            toolTip1.SetToolTip(picIcon, Psc.name);
            toolTip1.SetToolTip(this, Psc.name);
            picIcon.Location = new System.Drawing.Point(MotherForm.ucShortcutIconLocation, MotherForm.ucShortcutIconLocation);
            picIcon.Size = new System.Drawing.Size(MotherForm.ucShortcutIconSize, MotherForm.ucShortcutIconSize);
        }

        public void ucShortcut_Click(object sender, EventArgs e)
        {
            if (Psc.isWindowsApp)
            {
                Process p = new Process() {StartInfo = new ProcessStartInfo() { UseShellExecute = true, FileName = $@"shell:appsFolder\{Psc.FilePath}" }};
                p.Start();
            } else
            {
                if(Path.GetExtension(Psc.FilePath).ToLower() == ".lnk" && Psc.FilePath == Paths.exeString)

                {
                    MotherForm.OpenFile(Psc.Arguments, Psc.FilePath, Paths.path);
                } else
                {
                    MotherForm.OpenFile(Psc.Arguments, Psc.FilePath, Psc.WorkingDirectory);
                }
            }
        }

        public void ucShortcut_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = MotherForm.HoverColor;
        }

        public void ucShortcut_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.Transparent;
        }

        private void InitializeContextMenu()
        {
            // Create the context menu
            contextMenu = new ContextMenuStrip();

            // Add a menu item to show the file or executable path
            ToolStripMenuItem pathMenuItem = new ToolStripMenuItem("Show Path");
            pathMenuItem.Click += PathMenuItem_Click;
            contextMenu.Items.Add(pathMenuItem);

            // Attach the context menu to the ucShortcut control
            this.ContextMenuStrip = contextMenu;
        }

        private void PathMenuItem_Click(object sender, EventArgs e)
        {
            // Show the file or executable path in a message box
            MessageBox.Show($"File or Executable Path: {Psc.FilePath}");
        }

        // Other code...

        public void ucShortcut_RightClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // Show the context menu at the location of the right-click event
                contextMenu.Show(this, e.Location);
            }
        }
    }
}
