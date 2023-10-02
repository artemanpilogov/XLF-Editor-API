using Log.Models;
using Setup.Models;

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

            LogEntity logEntity = new LogEntity {
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

        private void InsertLog(LogEntity logEntity)
        {
            _dbContext.LogEntities.Add(logEntity);
            _dbContext.SaveChanges();
        }
    }
}