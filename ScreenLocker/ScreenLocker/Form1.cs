using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ScreenLocker.Models.Instructions;

namespace ScreenLocker
{
    public partial class Form1 : Form
    {
        public bool AllowedToClose { get; set; }

        public Form1()
        {
            InitializeComponent();
            Lock();
            PopulateInstructions();
        }

        private void Lock()
        {
            this.WindowState = FormWindowState.Maximized;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
        }

        private void PopulateInstructions()
        {
            if (TryLoadInstructions<RootInstructions>(out RootInstructions instructions))
            {
                lblMessage.Text = $"Computador bloqueado. {instructions.message}";
                lblTarget.Text = instructions.target;
            }
        }

        private bool TryLoadInstructions<T>(out T obj)
        {
            try
            {
                using (StreamReader fileConfig = new StreamReader("config.json"))
                {
                    var ContentConfig = fileConfig.ReadToEnd();
                    var objConfig = ((Models.config.Rootobject)JsonConvert.DeserializeObject<Models.config.Rootobject>(ContentConfig));

                    using (StreamReader file = new StreamReader(objConfig.instructionsPath))
                    {
                        var Content = file.ReadToEnd();
                        obj = ((T)JsonConvert.DeserializeObject<T>(Content));
                        return true;
                    }
                }
            }
            catch (Exception)
            { }

            obj = default(T);
            return false;
        }

        private void Liberar_Click(object sender, EventArgs e)
        {
            this.AllowedToClose = true;
            this.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (TryLoadInstructions<RootInstructions>(out RootInstructions instructions))
            {
                if (!instructions.IsCloseAllowed)
                {
                    MessageBox.Show("Só pode usar depois que o papai/tio Max liberar :) :P HAHAHAHAHA ");
                    e.Cancel = true;
                }
            }
        }
    }
}
