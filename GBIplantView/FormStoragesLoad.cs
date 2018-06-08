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
                dataGridView1.Rows.Clear();
                foreach (var elem in Task.Run(() => APIClient.GetRequestData<List<StorageLoadViewModel>>("api/Reporting/GetStocksLoad")).Result)
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
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
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
                string fileName = sfd.FileName;
                Task task = Task.Run(() => APIClient.PostRequestData("api/Reporting/SaveStocksLoad", new ReportingBindingModel
                {
                    FileName = fileName
                }));

                task.ContinueWith((prevTask) => MessageBox.Show("Выполнено", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information),
                    TaskContinuationOptions.OnlyOnRanToCompletion);
                task.ContinueWith((prevTask) =>
                {
                    var ex = (Exception)prevTask.Exception;
                    while (ex.InnerException != null)
                    {
                        ex = ex.InnerException;
                    }
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }, TaskContinuationOptions.OnlyOnFaulted);
            }
        }

   
    }
}
