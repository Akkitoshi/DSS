﻿using Controller;
using Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Unity;

namespace View
{
    public partial class ComplexitiesForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly ComplexityController service;

        public ComplexitiesForm(ComplexityController service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void ComplexitiesForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                List<ComplexityView> list = service.GetList();
                if (list != null)
                {
                    dataGridView1.DataSource = list;
                    dataGridView1.Columns[0].Visible = true;
                    dataGridView1.Columns[1].Visible = true;
                    dataGridView1.Columns[1].AutoSizeMode =
                        DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch
            {
                labelError.ForeColor = Color.Red;
                labelError.Text = "Ошибка";
            }
        }

        private void добавитьКатегориюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<AddComplexityForm>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
            LoadData();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            labelError.ForeColor = Color.White;
            if (textBoxDelId.Text.Length > 5)
            {
                labelError.ForeColor = Color.Red;
                labelError.Text = "Максимум символов 5";
            }
            if (!(new Regex(@"[\d!#h]")).Match(textBoxDelId.Text).Success)
            {
                labelError.ForeColor = Color.Red;
                labelError.Text = "Не цифровое значение";
            }
            else
            {
                try
                {
                    service.delElement(Convert.ToInt32(textBoxDelId.Text));
                    LoadData();
                    labelError.ForeColor = Color.Green;
                    labelError.Text = "Успешно";
                }
                catch
                {
                    labelError.ForeColor = Color.Red;
                    labelError.Text = "Ошибка";
                }
            }
        }

        private void добавитьСложностьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<AddComplexityForm>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
            LoadData();
        }
    }
}