using AppModel.BindingModels;
using DAL.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interface
{
    public interface IBOBackup
    {
        string TakeBackup();
        bool SetMail(SettingsBindingModel model);
        Mailing GetMail();
    }
}
