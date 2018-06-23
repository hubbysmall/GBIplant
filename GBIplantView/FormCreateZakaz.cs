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



namespace GBIplantView
{
    public partial class FormCreateZakaz : Form
    {
      

        public FormCreateZakaz()
        {
            InitializeComponent();
        }

        private void FormCreateOrder_Load(object sender, EventArgs e)
        {

            try
            {
                var responseC = APIClient.GetRequest("api/Buyer/GetList");
                if (responseC.Result.IsSuccessStatusCode)
                {
                    List<BuyerViewModel> list = APIClient.GetElement<List<BuyerViewModel>>(responseC);
                    if (list != null)
                    {
                        comboBoxClient.DisplayMember = "BuyerFIO";
                        comboBoxClient.ValueMember = "Id";
                        comboBoxClient.DataSource = list; // listC  ?????
                        comboBoxClient.SelectedItem = null;
                    }
                }
                else
                {
                    throw new Exception(APIClient.GetError(responseC));
                }
                var responseP = APIClient.GetRequest("api/GBIpieceOfArt/GetList");
                if (responseP.Result.IsSuccessStatusCode)
                {
                    List<GBIpieceOfArtViewModel> list = APIClient.GetElement<List<GBIpieceOfArtViewModel>>(responseP);
                    if (list != null)
                    {
                        comboBoxProduct.DisplayMember = "GBIpieceOfArtName";
                        comboBoxProduct.ValueMember = "Id";
                        comboBoxProduct.DataSource = list; // listP  ?????
                        comboBoxProduct.SelectedItem = null;
                    }
                }
                else
                {
                    throw new Exception(APIClient.GetError(responseP));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalcSum()
        {
            if (comboBoxProduct.SelectedValue != null && !string.IsNullOrEmpty(textBoxCount.Text))
            {
             
                 try
                {
                    int id = Convert.ToInt32(comboBoxProduct.SelectedValue);
                    var responseP = APIClient.GetRequest("api/GBIpieceOfArt/Get/" + id);
                    if (responseP.Result.IsSuccessStatusCode)
                    {
                        GBIpieceOfArtViewModel product = APIClient.GetElement<GBIpieceOfArtViewModel>(responseP);
                        int count = Convert.ToInt32(textBoxCount.Text);
                        textBoxSum.Text = (count * (int)product.Price).ToString();
                    }
                    else
                    {
                        throw new Exception(APIClient.GetError(responseP));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void textBoxCount_TextChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        private void comboBoxProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxClient.SelectedValue == null)
            {
                MessageBox.Show("Выберите клиента", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxProduct.SelectedValue == null)
            {
                MessageBox.Show("Выберите изделие", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                var response = APIClient.PostRequest("api/Main/CreateOrder", new ZakazBindingModel
                {
                    BuyerId = Convert.ToInt32(comboBoxClient.SelectedValue),
                    GBIpieceOfArtId = Convert.ToInt32(comboBoxProduct.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text),
                    Sum = Convert.ToInt32(textBoxSum.Text)
                });
                if (response.Result.IsSuccessStatusCode)
                {
                    MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
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
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
