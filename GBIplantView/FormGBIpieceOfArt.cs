using GBIplantService.BindingModels;
using GBIplantService.Interfaces;
using GBIplantService.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;

namespace GBIplantView
{
    public partial class FormGBIpieceOfArt : Form
    {
       [Dependency]
        public new IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly IGBIpieceOfArtService service;

        private int? id;

        private List<GBIpieceofArt__ingridientViewModel> productComponents;

        public FormGBIpieceOfArt(IGBIpieceOfArtService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void FormProduct_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    GBIpieceOfArtViewModel view = service.GetGBIpieceOfArt(id.Value);
                    if (view != null)
                    {
                        textBoxName.Text = view.GBIpieceOfArtName;
                        textBoxPrice.Text = view.Price.ToString();
                        productComponents = view.GBIpieceofArt__ingridients;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                productComponents = new List<GBIpieceofArt__ingridientViewModel>();
            }
        }

        private void LoadData()
        {
            try
            {
                if (productComponents != null)
                {
                    dataGridView.DataSource = null;
                    dataGridView.DataSource = productComponents;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].Visible = false;
                    dataGridView.Columns[2].Visible = false;
                    dataGridView.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormGBIpieceOfArt__ingridient>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if(form.Model != null)
                {
                    if(id.HasValue)
                    {
                        form.Model.GBIpieceofArtId = id.Value;
                    }
                    productComponents.Add(form.Model);
                }
                LoadData();
            }
        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<FormGBIpieceOfArt__ingridient>();
                form.Model = productComponents[dataGridView.SelectedRows[0].Cells[0].RowIndex];
                if (form.ShowDialog() == DialogResult.OK)
                {
                    productComponents[dataGridView.SelectedRows[0].Cells[0].RowIndex] = form.Model;
                    LoadData();
                }
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        productComponents.RemoveAt(dataGridView.SelectedRows[0].Cells[0].RowIndex);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }

        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (productComponents == null || productComponents.Count == 0)
            {
                MessageBox.Show("Заполните компоненты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                List<GBIpieceofArt__ingridientBindingModel> productComponentBM = new List<GBIpieceofArt__ingridientBindingModel>();
                for (int i = 0; i < productComponents.Count; ++i)
                {
                    productComponentBM.Add(new GBIpieceofArt__ingridientBindingModel
                    {
                        Id = productComponents[i].Id,
                        GBIpieceofArtId = productComponents[i].GBIpieceofArtId,
                        GBIingridientId = productComponents[i].GBIingridientId,
                        Count = productComponents[i].Count
                    });
                }
                if (id.HasValue)
                {
                    service.UpdGBIpieceOfArt(new GBIpieceOfArtBindingModel
                    {
                        Id = id.Value,
                        GBIpieceOfArtName = textBoxName.Text,
                        Price = Convert.ToInt32(textBoxPrice.Text),
                        GBIpieceofArt__ingridients = productComponentBM
                    });
                }
                else
                {
                    service.AddGBIpieceOfArt(new GBIpieceOfArtBindingModel
                    {
                        GBIpieceOfArtName = textBoxName.Text,
                        Price = Convert.ToInt32(textBoxPrice.Text),
                        GBIpieceofArt__ingridients = productComponentBM
                    });
                }
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
