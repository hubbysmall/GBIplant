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


namespace GBIplantView
{
    public partial class FormGBIpieceOfArt__ingridient : Form
    {
  

       public GBIpieceofArt__ingridientViewModel Model { set { model = value; } get { return model; } }


        private GBIpieceofArt__ingridientViewModel model;

        public FormGBIpieceOfArt__ingridient()
        {
            InitializeComponent();
        }

        private void FormProductComponent_Load(object sender, EventArgs e)
        {
           
             try
            {
                var response = APIClient.GetRequest("api/GBIingridient/GetList");
                if (response.Result.IsSuccessStatusCode)
                {
                    comboBoxComponent.DisplayMember = "GBIingridientName";
                    comboBoxComponent.ValueMember = "Id";
                    comboBoxComponent.DataSource = APIClient.GetElement<List<GBIingridientViewModel>>(response);
                    comboBoxComponent.SelectedItem = null;
                }
                else
                {
                    throw new Exception(APIClient.GetError(response));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (model != null)
            {
                comboBoxComponent.Enabled = false;
                comboBoxComponent.SelectedValue = model.GBIingridientId;
                textBoxCount.Text = model.Count.ToString();
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxComponent.SelectedValue == null)
            {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (model == null)
                {
                    model = new GBIpieceofArt__ingridientViewModel
                    {
                        GBIingridientId = Convert.ToInt32(comboBoxComponent.SelectedValue),
                        GBIingridientName = comboBoxComponent.Text,
                        Count = Convert.ToInt32(textBoxCount.Text)
                    };
                }
                else
                {
                    model.Count = Convert.ToInt32(textBoxCount.Text);
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
