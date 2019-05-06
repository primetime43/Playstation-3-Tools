using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Call_of_Duty_World_at_War_Tool
{
    public partial class playermanageplayer : Form
    {
        private string p;
        private int Index;
        private Clients clients;

        public playermanageplayer()
        {
            InitializeComponent();
        }

        public playermanageplayer(string p, int Index, Clients clients)
        {
            // TODO: Complete member initialization
            this.p = p;
            this.Index = Index;
            this.clients = clients;
        }
    }
}
