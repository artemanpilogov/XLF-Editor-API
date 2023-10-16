using System.Net.Mail;
using System.Text;
using Log.Models;
using Setup.Models;
using Users.Models;

namespace Manage.Models
{
    public class Manages
    {
        private readonly DBContext _dbContext;

        public Manages(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void InsertLog(string ip_address, EntryType entryType)
        {
            SetupEntity setupEntity = _dbContext.SetupEntities.FirstOrDefault();

            LogEntity logEntity = new LogEntity
            {
                Ip_Address = ip_address,
                Action_Type = (int)entryType,
                Action_Type_Name = Enum.GetName(typeof(EntryType), entryType),
                Created_Date = DateTime.UtcNow.Date,
                Created_Time = DateTime.UtcNow.TimeOfDay,
            };

            switch (entryType)
            {
                case EntryType.ImportToCSV:
                    {
                        if (setupEntity.Import_To_CSV)
                            InsertLog(logEntity);
                        break;
                    }
                case EntryType.ExportFromCSV:
                    {
                        if (setupEntity.Export_From_CSV)
                            InsertLog(logEntity);
                        break;
                    }
                case EntryType.SaveChanges:
                    {
                        if (setupEntity.Save_Changes)
                            InsertLog(logEntity);
                        break;
                    }
                case EntryType.CreatedFile:
                    {
                        if (setupEntity.Created_File)
                            InsertLog(logEntity);
                        break;
                    }
            }
        }

        public string RegisterUser(UserInfo userInfo)
        {
            if (!IsValid(userInfo.Email))
                return "You entered not correct email";

            if (_dbContext.UsersEntities.Where(w => w.Email == userInfo.Email).Any())
                return "This email is registered. Try to register another email";

            _dbContext.UsersEntities.Add(new UsersEntity
            {
                Email = userInfo.Email,
                Password = EncryptPassword(userInfo.Password),
                Created_Date = DateTime.UtcNow.Date
            });
            _dbContext.SaveChanges();

            return "Account was created";
        }

        public bool Autorization(UserInfo userInfo)
        {
            if (!IsValid(userInfo.Email))
                return false;

            UsersEntity userEntity = _dbContext.UsersEntities.Where(w => w.Email == userInfo.Email).FirstOrDefault();

            if (userEntity == null)
                return false;
            if (userInfo.Password != DecryptPassword(userEntity.Password))
            {
                return  false;
            }
            return true;
        }

        private byte[] EncryptPassword(string password)
        {
            ASCIIEncoding encryptpwd = new ASCIIEncoding();
            return encryptpwd.GetBytes(password);
        }

        private string DecryptPassword(byte[] password)
        {
            ASCIIEncoding encryptpwd = new ASCIIEncoding();
            return encryptpwd.GetString(password);
        }

        private void InsertLog(LogEntity logEntity)
        {
            _dbContext.LogEntities.Add(logEntity);
            _dbContext.SaveChanges();
        }

        private static bool IsValid(string email)
        {
            var valid = true;

            try
            {
                var emailAddress = new MailAddress(email);
            }
            catch
            {
                valid = false;
            }

            return valid;
        }
    }
}