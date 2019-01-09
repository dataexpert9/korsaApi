using AppModel.BindingModels;
using BLL.Interface;
using Component.ResponseFormats;
using Component.Utility;
using DAL.DomainModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace BLL.Implementation
{
    public class BOBackup : IBOBackup
    {

        public DataContext _dbContext { get; set; }

        public BOBackup(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string TakeBackup()
        {
            try
            {


                System.IO.DirectoryInfo di = new DirectoryInfo(AppSettingsProvider.DatabaseBackUpUrl);

                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }

                System.IO.DirectoryInfo dir = new DirectoryInfo(AppSettingsProvider.DatabaseBackUpZipUrl);

                foreach (FileInfo file in dir.GetFiles())
                {
                    file.Delete();
                }


                var FileName = AppSettingsProvider.DatabaseBackUpUrl+"//DatabaseBackUp-" + DateTime.UtcNow.Date.ToString("dd-MM-yyyy") + ".bak";



                var Directory= AppSettingsProvider.DatabaseBackUpUrl;
                var FileNameZip = AppSettingsProvider.DatabaseBackUpZipUrl+"//DatabaseBackUpZip-" + DateTime.UtcNow.Date.ToString("dd-MM-yyyy") + ".zip";

                string commandText = $@"BACKUP DATABASE [" + AppSettingsProvider.DataBaseName + "] TO DISK = N'"+ FileName+"' WITH NOFORMAT, INIT, NAME = N'" + AppSettingsProvider.DataBaseName + "-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10";

                SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder
                {
                    DataSource = AppSettingsProvider.DataSource,
                    InitialCatalog = AppSettingsProvider.DataBaseName,
                    IntegratedSecurity = false,
                    UserID = AppSettingsProvider.DBUser,
                    Password = AppSettingsProvider.Password
                };
                using (SqlConnection connection = new SqlConnection(connectionStringBuilder.ConnectionString))
                {
                    connection.Open();
                    // connection.InfoMessage += Connection_InfoMessage;
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = commandText;
                        command.CommandType = CommandType.Text;
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
                ZipFile.CreateFromDirectory(Directory, FileNameZip);

                return FileNameZip;
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
                throw ex;
            }

        }

        public bool SetMail(SettingsBindingModel model)
        {

            if (model.Id > 0)
            {
                var Mail = _dbContext.Mailing.FirstOrDefault(x => x.Id == model.Id);

                Mail.Subject = model.Subject;
                Mail.Body = model.Body;
            }
            else
            {
                _dbContext.Mailing.Add(new Mailing {
                    Body=model.Body,
                    Subject=model.Subject,
                    Type=0
                });
            }
            _dbContext.SaveChanges();
            return true;
        }


        public Mailing GetMail()
        {
            return _dbContext.Mailing.FirstOrDefault();
        }


    }
}
