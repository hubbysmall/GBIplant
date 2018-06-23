﻿using GBIplantService.BindingModels;
using GBIplantService.Interfaces;
using GBIplantService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unity;
using Unity.Attributes;

namespace GBIplantWeb
{
    public partial class Storage : System.Web.UI.Page
    {
        private readonly IStorageService service = UnityConfig.Container.Resolve<IStorageService>();

        private int id;



        protected void Page_Load(object sender, EventArgs e)
        {
            if (Int32.TryParse((string)Session["id"], out id))
            {
                try
                {
                    StorageViewModel view = service.GetStorage(id);
                    if (view != null)
                    {
                        if (!Page.IsPostBack)
                        {
                            TextBoxName.Text = view.StorageName;
                            GridView1.DataSource = view.Storage__GBIingridients;
                            GridView1.DataBind();
                            GridView1.ShowHeaderWhenEmpty = true;
                        }
                        
                    }
                    Page.DataBind();
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
                }
            }
        }

        protected void save_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(TextBoxName.Text))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Заполните название');</script>");
                return;
            }
            try
            {
                if (Int32.TryParse((string)Session["id"], out id))
                {
                    service.UpdStorage(new StorageBindingModel
                    {
                        Id = id,
                        StorageName = TextBoxName.Text
                    });
                }
                else
                {
                    service.AddStorage(new StorageBindingModel
                    {
                        StorageName = TextBoxName.Text
                    });
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
                Response.Redirect("Storages.aspx");
            }
            Session["id"] = null;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Сохранение прошло успешно');</script>");
            Response.Redirect("Storages.aspx");
        }

        protected void cancel_Click(object sender, EventArgs e)
        {
            Session["id"] = null;
            Response.Redirect("Storages.aspx");
        }
    }
}