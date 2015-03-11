﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;

namespace QVManagerConnectorHost
{
    public partial class Form1 : Form
    {
        private WebServiceHost host;
        //http://localhost:8085/QVManagerApiConnectorService/ListarCALRaw

        public Form1()
        {
            InitializeComponent();
            host = new WebServiceHost(typeof(QVManagerConnectorService.QVManagerApiConnectorService));

            host.Open();
            btnStart.Enabled = false;
            btnStop.Enabled = true;
            lblMessage.Text = "Servicio iniciado";
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            host = new WebServiceHost(typeof(QVManagerConnectorService.QVManagerApiConnectorService));

            host.Open();
            btnStart.Enabled = false;
            btnStop.Enabled = true;
            lblMessage.Text = "Servicio iniciado";
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            host.Close();
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            lblMessage.Text = "Servicio detenido";
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            host.Close();
            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }
    }
}
