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
    public partial class FormStoragesLoad : Form
    {
    
        public FormStoragesLoad()
        {
            InitializeComponent();
        }

        private void FormStoragesLoad_Load(object sender, EventArgs e)
        {
            try
            {
                var response = APIClient.GetRequest("api/Reporting/GetStocksLoad");
                if (response.Result.IsSuccessStatusCode)
                {
                    dataGridView1.Rows.Clear();
                    foreach (var elem in APIClient.GetElement<List<StorageLoadViewModel>>(response))
                    {
                        dataGridView1.Rows.Add(new object[] { elem.StorageName, "", "" });
                        foreach (var listElem in elem.GBIingridients)
                        {
                            dataGridView1.Rows.Add(new object[] { "", listElem.GBIingridientname, listElem.Count });
                        }
                        dataGridView1.Rows.Add(new object[] { "Итого", "", elem.TotalCount });
                        dataGridView1.Rows.Add(new object[] { });
                    }
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

        private void buttonSaveToExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "xls|*.xls|xlsx|*.xlsx"
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var response = APIClient.PostRequest("api/Reporting/SaveStocksLoad", new ReportingBindingModel
                    {
                        FileName = sfd.FileName
                    });
                    if (response.Result.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        }

   
    }
}
