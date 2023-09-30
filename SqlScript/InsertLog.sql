insert into [xlfeditor].[dbo].[Log] (
       [ip_address]
      ,[created_date]
      ,[created_time]
      ,[action_type]
)
 VALUES
(
    '192.168.0.1',
    GETUTCDATE(),
    CONVERT(time, GETUTCDATE()),
    1
)