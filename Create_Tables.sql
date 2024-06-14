CREATE SCHEMA [queue]

GO

CREATE TABLE [queue].[QueueStatus](
	[QueueStatusId] [int] IDENTITY(1,1) NOT NULL,
	[QueueStatusName] [nvarchar](10) NOT NULL
CONSTRAINT [PK_QueueStatus] PRIMARY KEY CLUSTERED 
(
	[QueueStatusId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
CONSTRAINT [UK_QueueStatus] UNIQUE NONCLUSTERED 
(
	[QueueStatusName] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [queue].[QueueLog](
	[QueueLogId] [int] IDENTITY(1,1) NOT NULL,
	[QueueMessage] [nvarchar](max) NOT NULL,
	[StatusName] [nvarchar](10) NOT NULL,
	[CreateDate] [DateTime] NOT NULL
CONSTRAINT [PK_QueueLog] PRIMARY KEY CLUSTERED 
(
	[QueueLogId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [queue].[QueueLog]  WITH CHECK ADD  CONSTRAINT [FK_QueueLog_QueueStatus] FOREIGN KEY([StatusName])
REFERENCES [queue].[QueueStatus] ([QueueStatusName])
GO
 
ALTER TABLE [queue].[QueueLog] CHECK CONSTRAINT [FK_QueueLog_QueueStatus]
GO

INSERT INTO [queue].[QueueStatus]
           ([QueueStatusName])
     VALUES
           ('Processed')
GO

INSERT INTO [queue].[QueueStatus]
           ([QueueStatusName])
     VALUES
           ('WithError')

GO

INSERT INTO [queue].[QueueStatus]
           ([QueueStatusName])
     VALUES
           ('Read')

GO

INSERT INTO [queue].[QueueStatus]
           ([QueueStatusName])
     VALUES
           ('ReadError')