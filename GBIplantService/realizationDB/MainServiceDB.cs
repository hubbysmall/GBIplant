using GBIplantModel;
using GBIplantService.BindingModels;
using GBIplantService.Interfaces;
using GBIplantService.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Net.Mail;
using System.Configuration;
using System.Net;

namespace GBIplantService.realizationDB
{
    public class MainServiceDB : IMainService
    {
        private AbstractDbContext context;

        public MainServiceDB(AbstractDbContext context)
        {
            this.context = context;
        }
        public List<ZakazViewModel> GetList()
        {
            List<ZakazViewModel> result = context.Zakazes
                .Select(rec => new ZakazViewModel
                {
                    Id = rec.Id,
                    BuyerId = rec.BuyerId,
                    GBIpieceOfArtId = rec.GBIpieceofArtId,
                    ExecutorId = rec.ExecutorId,
                    DateCreate = SqlFunctions.DateName("dd", rec.DateCreate) + " " +
                                SqlFunctions.DateName("mm", rec.DateCreate) + " " + 
                                SqlFunctions.DateName("yyyy", rec.DateCreate),
                    DateExecute = rec.DateExecute == null ? "" :
                                        SqlFunctions.DateName("dd", rec.DateExecute.Value) + " " +
                                        SqlFunctions.DateName("mm", rec.DateExecute.Value) + " " +
                                        SqlFunctions.DateName("yyyy", rec.DateExecute.Value),
                    Status = rec.Status.ToString(),
                    Count = rec.Count,
                    Sum = rec.Sum,
                    BuyerFIO = rec.Buyer.BuyerFIO,
                    GBIpieceOfArtName = rec.GBIpieceOfArt.GBIpieceOfArtNAme,
                    ExecutorName = rec.Executor.ExecutorFIO
                })
                .ToList();
            return result;
        }

        public void CreateZakaz(ZakazBindingModel model)
        {
            var order = new Zakaz
            {
                BuyerId = model.BuyerId,
                GBIpieceofArtId = model.GBIpieceOfArtId,
                DateCreate = DateTime.Now,
                Count = model.Count,
                Sum = model.Sum,
                Status = ZakazStatus.taken
            };
            context.Zakazes.Add(order);
            context.SaveChanges();

            var client = context.Buyers.FirstOrDefault(x => x.Id == model.BuyerId);
            SendEmail(client.Mail, "Оповещение по заказам",
                string.Format("Заказ №{0} от {1} создан успешно", order.Id,
                order.DateCreate.ToShortDateString()));
        }

        public void TakeZakazInWork(ZakazBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {

                    Zakaz element = context.Zakazes.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    var productComponents = context.GBIpieceofArt__ingridients
                                               .Include(rec => rec.GBIindgridient) 
                                                .Where(rec => rec.GBIpieceOfArtId == element.GBIpieceofArtId);
                    // списываем
                    foreach (var productComponent in productComponents)
                    {
                        int countOnStocks = productComponent.Count * element.Count;
                        var stockComponents = context.Storage__GBIingridients
                                                    .Where(rec => rec.GBIingridientId == productComponent.GBIindgridientId);
                        foreach (var stockComponent in stockComponents)
                        {
                            // компонентов на одном слкаде может не хватать
                            if (stockComponent.Count >= countOnStocks)
                            {
                                stockComponent.Count -= countOnStocks;
                                countOnStocks = 0;
                                context.SaveChanges();
                                break;
                            }
                            else
                            {
                                countOnStocks -= stockComponent.Count;
                                stockComponent.Count = 0;
                                context.SaveChanges();
                            }
                        }
                        if (countOnStocks > 0)
                        {
                            throw new Exception("Не достаточно компонента " +
                                productComponent.GBIindgridient.GBIindgridientName + " требуется " +
                                productComponent.Count + ", не хватает " + countOnStocks);
                        }
                    }
                    element.ExecutorId = model.ExecutorId;
                    element.DateExecute = DateTime.Now;
                    element.Status = ZakazStatus.inProcess;
                    context.SaveChanges();
                    SendEmail(element.Buyer.Mail, "Оповещение по заказам",
                       string.Format("Заказ №{0} от {1} передеан в работу", element.Id, element.DateCreate.ToShortDateString()));
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void FinishZakaz(int id)
        {
            Zakaz element = context.Zakazes.FirstOrDefault(rec => rec.Id == id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.Status = ZakazStatus.ready;
            context.SaveChanges();
            SendEmail(element.Buyer.Mail, "Оповещение по заказам",
                string.Format("Заказ №{0} от {1} передан на оплату", element.Id,
                element.DateCreate.ToShortDateString()));
        }

        public void PayZakaz(int id)
        {
            Zakaz element = context.Zakazes.FirstOrDefault(rec => rec.Id == id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.Status = ZakazStatus.paid;
            context.SaveChanges();
            SendEmail(element.Buyer.Mail, "Оповещение по заказам",
                string.Format("Заказ №{0} от {1} оплачен успешно", element.Id, element.DateCreate.ToShortDateString()));
        }

        public void PutGBIingridientInStorage(Storage__GBIingridientBindingModel model)
        {
            Storage__GBIingridient element = context.Storage__GBIingridients
                                                .FirstOrDefault(rec => rec.StorageId == model.StorageId &&
                                                                    rec.GBIingridientId == model.GBIingridientId);
            if (element != null)
            {
                element.Count += model.Count;
            }
            else
            {
                context.Storage__GBIingridients.Add(new Storage__GBIingridient
                {
                    StorageId = model.StorageId,
                    GBIingridientId = model.GBIingridientId,
                    Count = model.Count
                });
            }
            context.SaveChanges();
        }

        private void SendEmail(string mailAddress, string subject, string text)
        {
            MailMessage objMailMessage = new MailMessage();
            SmtpClient objSmtpClient = null;

            try
            {
                objMailMessage.From = new MailAddress(ConfigurationManager.AppSettings["MailLogin"]);
                objMailMessage.To.Add(new MailAddress(mailAddress));
                objMailMessage.Subject = subject;
                objMailMessage.Body = text;
                objMailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
                objMailMessage.BodyEncoding = System.Text.Encoding.UTF8;

                objSmtpClient = new SmtpClient("smtp.gmail.com", 587);
                objSmtpClient.UseDefaultCredentials = false;
                objSmtpClient.EnableSsl = true;
                objSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                objSmtpClient.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["MailLogin"],
                    ConfigurationManager.AppSettings["MailPassword"]);

                objSmtpClient.Send(objMailMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objMailMessage = null;
                objSmtpClient = null;
            }
        }




    }
}
