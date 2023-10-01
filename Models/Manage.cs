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
            Log.Models.LogEntity logEntity = new Log.Models.LogEntity {
                Ip_Address = ip_address,
                Action_Type = (int)entryType,
                Action_Type_Name = Enum.GetName(typeof(EntryType), entryType),
                Created_Date = DateTime.UtcNow.Date,
                Created_Time = DateTime.UtcNow.TimeOfDay,
            };        
        
            _dbContext.LogsEntity.Add(logEntity);
            _dbContext.SaveChanges();
        }
    }
}