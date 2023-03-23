CREATE TYPE [dbo].[cartDetailType] AS TABLE(
	[id] [uniqueidentifier] NULL,
	[cartid] [uniqueidentifier] NULL,
	[productid] [uniqueidentifier] NULL,
	[quantity] [decimal](10, 3) NULL
)
GO
CREATE TABLE [dbo].[cartDetails](
	[id] [uniqueidentifier] NOT NULL,
	[cartid] [uniqueidentifier] NULL,
	[productid] [uniqueidentifier] NULL,
	[quantity] [decimal](10, 3) NULL,
	[createdAt] [datetimeoffset](7) NULL,
	[isActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[carts](
	[id] [uniqueidentifier] NOT NULL,
	[customerid] [uniqueidentifier] NULL,
	[createdAt] [datetimeoffset](7) NULL,
	[isActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[category](
	[id] [uniqueidentifier] NOT NULL,
	[category] [nvarchar](100) NULL,
	[createdAt] [datetimeoffset](7) NULL,
	[isActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[customers](
	[id] [uniqueidentifier] NOT NULL,
	[name] [nvarchar](100) NULL,
	[address] [nvarchar](100) NULL,
	[email] [nvarchar](30) NULL,
	[phone] [nvarchar](20) NULL,
	[password] [nvarchar](100) NULL,
	[createdAt] [datetimeoffset](7) NULL,
	[isActive] [bit] NULL,
 CONSTRAINT [PK__customer__3213E83F465ECA05] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[ExceptionLogs](
	[id] [uniqueidentifier] NOT NULL,
	[requestId] [uniqueidentifier] NULL,
	[message] [text] NULL,
	[stackTrace] [text] NULL,
	[timeStamp] [datetimeoffset](7) NULL,
	[createdAt] [datetimeoffset](7) NULL,
	[isDeleted] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE TABLE [dbo].[products](
	[id] [uniqueidentifier] NOT NULL,
	[title] [nvarchar](100) NULL,
	[category] [uniqueidentifier] NULL,
	[price] [decimal](10, 3) NULL,
	[quantity] [decimal](10, 3) NULL,
	[metadata] [nvarchar](500) NULL,
	[createdAt] [datetimeoffset](7) NULL,
	[isActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[RefreshToken](
	[id] [uniqueidentifier] NOT NULL,
	[customerId] [uniqueidentifier] NOT NULL,
	[token] [nvarchar](max) NOT NULL,
	[jwtId] [nvarchar](max) NOT NULL,
	[isUsed] [bit] NOT NULL,
	[isRevoked] [bit] NOT NULL,
	[expiryDate] [datetime] NOT NULL,
	[createDate] [datetime] NOT NULL,
 CONSTRAINT [PK_RefreshToken] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE TABLE [dbo].[RequestLogs](
	[id] [uniqueidentifier] NOT NULL,
	[type] [varchar](100) NULL,
	[requestDomain] [varchar](100) NULL,
	[requestIp] [varchar](50) NULL,
	[requestUrl] [varchar](100) NULL,
	[requestQueryString] [text] NULL,
	[requestHeader] [text] NULL,
	[requestBody] [text] NULL,
	[responseHeader] [text] NULL,
	[responseBody] [text] NULL,
	[requestScheme] [varchar](10) NULL,
	[formData] [text] NULL,
	[routeData] [text] NULL,
	[httpMethod] [varchar](10) NULL,
	[statusCode] [varchar](10) NULL,
	[userAgent] [text] NULL,
	[timeStamp] [datetimeoffset](7) NULL,
	[createdAt] [datetimeoffset](7) NULL,
	[isDeleted] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[cartDetails] ADD  DEFAULT (sysdatetimeoffset()) FOR [createdAt]
GO
ALTER TABLE [dbo].[cartDetails] ADD  DEFAULT ((1)) FOR [isActive]
GO
ALTER TABLE [dbo].[carts] ADD  DEFAULT (sysdatetimeoffset()) FOR [createdAt]
GO
ALTER TABLE [dbo].[carts] ADD  DEFAULT ((1)) FOR [isActive]
GO
ALTER TABLE [dbo].[category] ADD  DEFAULT (sysdatetimeoffset()) FOR [createdAt]
GO
ALTER TABLE [dbo].[category] ADD  DEFAULT ((1)) FOR [isActive]
GO
ALTER TABLE [dbo].[customers] ADD  CONSTRAINT [DF__customers__creat__5BE2A6F2]  DEFAULT (sysdatetimeoffset()) FOR [createdAt]
GO
ALTER TABLE [dbo].[customers] ADD  CONSTRAINT [DF__customers__isAct__5CD6CB2B]  DEFAULT ((1)) FOR [isActive]
GO
ALTER TABLE [dbo].[products] ADD  DEFAULT (sysdatetimeoffset()) FOR [createdAt]
GO
ALTER TABLE [dbo].[products] ADD  DEFAULT ((1)) FOR [isActive]
GO
ALTER TABLE [dbo].[RefreshToken] ADD  CONSTRAINT [DF_RefreshToken_isUsed]  DEFAULT ((0)) FOR [isUsed]
GO
ALTER TABLE [dbo].[RefreshToken] ADD  CONSTRAINT [DF_RefreshToken_isRevoked]  DEFAULT ((0)) FOR [isRevoked]
GO
ALTER TABLE [dbo].[cartDetails]  WITH CHECK ADD FOREIGN KEY([cartid])
REFERENCES [dbo].[carts] ([id])
GO
ALTER TABLE [dbo].[cartDetails]  WITH CHECK ADD FOREIGN KEY([productid])
REFERENCES [dbo].[products] ([id])
GO
ALTER TABLE [dbo].[carts]  WITH CHECK ADD  CONSTRAINT [FK__carts__customeri__68487DD7] FOREIGN KEY([customerid])
REFERENCES [dbo].[customers] ([id])
GO
ALTER TABLE [dbo].[carts] CHECK CONSTRAINT [FK__carts__customeri__68487DD7]
GO
ALTER TABLE [dbo].[ExceptionLogs]  WITH CHECK ADD FOREIGN KEY([requestId])
REFERENCES [dbo].[RequestLogs] ([id])
GO
ALTER TABLE [dbo].[products]  WITH CHECK ADD FOREIGN KEY([category])
REFERENCES [dbo].[category] ([id])
GO
ALTER TABLE [dbo].[RefreshToken]  WITH CHECK ADD  CONSTRAINT [FK_RefreshToken_customers] FOREIGN KEY([customerId])
REFERENCES [dbo].[customers] ([id])
GO
ALTER TABLE [dbo].[RefreshToken] CHECK CONSTRAINT [FK_RefreshToken_customers]
GO
CREATE PROCEDURE [dbo].[RefreshTokenAdd]
@id uniqueidentifier,
@customerId uniqueIdentifier,
@token nvarchar(max),
@jwtId nvarchar(max),
@isUsed bit,
@isRevoked bit,
@expiryDate datetime,
@createDate datetime
AS
BEGIN
	INSERT INTO RefreshToken(id, customerId, token, jwtId, isUsed, isRevoked, expiryDate, createDate)
	VALUES (@id, @customerId, @token, @jwtId, @isUsed, @isRevoked, @expiryDate, @createDate);

	SELECT * FROM RefreshToken
	WHERE id=@id;
END
GO
CREATE PROCEDURE [dbo].[sp_authenticateCustomer]
	@email varchar(50),
	@password varchar(100)
AS
BEGIN

		SELECT [id]
			  ,[name]
			  ,[address]
			  ,[email]
			  ,[phone]
			  ,[createdAt]
		  FROM [dbo].[customers]
		WHERE 
		[email] = @email AND
		[password] = @password AND
		[isActive] = 1	
END
GO
CREATE PROCEDURE [dbo].[sp_fetchCart]
	@cartid UNIQUEIDENTIFIER,
	@customerid UNIQUEIDENTIFIER
AS
BEGIN
		SELECT
			id, 
			createdAt,
			customerid 
		FROM carts 
		WHERE 
		id = COALESCE(@cartid, id) AND 
		customerid = COALESCE(@customerid, customerid) AND
		isActive = 1
END
GO
CREATE PROCEDURE [dbo].[sp_fetchCartDetail]
	@id UNIQUEIDENTIFIER,
	@cartid UNIQUEIDENTIFIER
AS
BEGIN
		SELECT
			[id]
		  ,[cartid]
		  ,[quantity]
		  ,[createdAt]
		  ,[productid]

		FROM cartDetails
		WHERE 
		id = COALESCE(@id, id) AND 
		[cartid] = COALESCE(@cartid, [cartid]) AND
		isActive = 1

END
GO
CREATE PROCEDURE [dbo].[sp_fetchCustomer]
	@id UNIQUEIDENTIFIER
AS
BEGIN

		SELECT [id]
			  ,[name]
			  ,[address]
			  ,[email]
			  ,[phone]
			  ,[createdAt]
		  FROM [dbo].[customers]
		WHERE 
		[id] = COALEScE(@id, [id]) AND
		[isActive] = 1
END
GO
CREATE PROCEDURE [dbo].[sp_fetchProduct]
@id UNIQUEIDENTIFIER,
@category nvarchar(100)
AS
BEGIN

		SELECT p.[id]
			,p.[title]
			,c.[category]
			,p.[price]
			,p.[quantity]
			,p.[metadata]
			,p.[createdAt]
		FROM [dbo].[products] p
			INNER JOIN [category]  c
			ON p.category = c.id and c.isActive = 1 and p.isActive = 1
		WHERE 
			p.[id] = COALESCE(@id, p.[id]) AND
			c.category = COALESCE(@category, c.[category])
END
GO
CREATE PROCEDURE [dbo].[sp_fetchRequestLogs]

	@id UNIQUEIDENTIFIER = NULL,
	@startfrom datetimeoffset = NULL,
	@endat datetimeoffset = NULL,
	@statuscode varchar(100) = NULL,
	@type varchar(100) = NULL,
	@method varchar(100) = NULL,
	@url varchar(100) = NULL

AS
BEGIN
	IF(@id IS NOT NULL)
	BEGIN
		SELECT [id]
			  ,[type]
			  ,[requestDomain]
			  ,[requestIp]
			  ,[requestUrl]
			  ,[requestQueryString]
			  ,[requestHeader]
			  ,[requestBody]
			  ,[responseHeader]
			  ,[responseBody]
			  ,[requestScheme] as scheme
			  ,[formData]
			  ,[routeData]
			  ,[httpMethod] as method
			  ,[statusCode]
			  ,[userAgent]
			  ,[timeStamp]
			  ,[createdAt]
		  FROM [dbo].[RequestLogs] 
		  WHERE 
		  id = @id AND 
		  isDeleted = 0
	END
	ELSE
		BEGIN
			SELECT [id]
				  ,[type]
				  ,[requestDomain]
				  ,[requestIp]
				  ,[requestUrl]
				  ,[requestQueryString]
				  ,[requestHeader]
				  ,[requestBody]
				  ,[responseHeader]
				  ,[responseBody]
				  ,[requestScheme] as scheme
				  ,[formData]
				  ,[routeData]
				  ,[httpMethod] as method
				  ,[statusCode]
				  ,[userAgent]
				  ,[timeStamp]
				  ,[createdAt]
			  FROM [dbo].[RequestLogs] 
			  WHERE 
			  [timeStamp] >= COALESCE(@startfrom, [timeStamp]) AND
			  [timeStamp] <= COALESCE(@endat, [timeStamp]) AND 
			  [statusCode] = COALESCE(@statuscode, [statusCode]) AND
			  [type] = COALESCE(@type, [type]) AND
			  [httpMethod] = COALESCE(@method, httpMethod) AND
			  [requestUrl] = COALESCE(@url, requestUrl) AND
			  [isDeleted] = 0
		END
END
GO
CREATE PROCEDURE [dbo].[sp_logError]
@id UNIQUEIDENTIFIER,
@reqeustid UNIQUEIDENTIFIER,
@message text,
@stacktrace text,
@timestamp datetimeoffset
AS
BEGIN
	SET NOCOUNT ON;

		INSERT INTO [dbo].[ExceptionLogs]
           ([id]
           ,[requestId]
           ,[message]
           ,[stackTrace]
           ,[timeStamp]
           ,[createdAt]
           ,[isDeleted])

		 VALUES
			   (@id
			   ,@reqeustid
			   ,@message
			   ,@stacktrace
			   ,@timestamp
			   ,GETUTCDATE()
			   ,0)

		SELECT [id]
			  ,[requestId]
			  ,[message]
			  ,[stackTrace]
			  ,[timeStamp]
			  ,[createdAt]
			  ,[isDeleted]
		  FROM [dbo].[ExceptionLogs]
		  WHERE 
		  id = @id AND 
		  isDeleted = 0
END

GO
CREATE PROCEDURE [dbo].[sp_logRequest]
@id UNIQUEIDENTIFIER,
@type varchar(100),
@requestdomain varchar(100),
@requestip varchar(50),
@requesturl varchar(100),
@requestheader text,
@requestquerystring text,
@requestbody text,
@responseheader text,
@responsebody text,
@scheme varchar(10),
@formdata text,
@routedata text,
@httpmethod varchar(10),
@statuscode varchar(10),
@useragent text,
@timestamp datetimeoffset
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO [dbo].[RequestLogs]
			   ([id]
			   ,[type]
			   ,[requestDomain]
			   ,[requestIp]
			   ,[requestUrl]
			   ,[requestHeader]
			   ,[requestQueryString]
			   ,[requestBody]
			   ,[responseHeader]
			   ,[responseBody]
			   ,[requestScheme]
			   ,[formData]
			   ,[routeData]
			   ,[httpMethod]
			   ,[statusCode]
			   ,[userAgent]
			   ,[timeStamp]
			   ,[createdAt]
			   ,[isdeleted])
		 VALUES
			   (@id
			   ,@type
			   ,@requestDomain
			   ,@requestIp
			   ,@requestUrl
			   ,@requestHeader
			   ,@requestquerystring
			   ,@requestBody
			   ,@responseHeader
			   ,@responseBody
			   ,@scheme
			   ,@formdata
			   ,@routedata
			   ,@httpmethod
			   ,@statusCode
			   ,@userAgent
			   ,@timeStamp
			   ,GETUTCDATE()
			   ,0)

		SELECT [id]
			  ,[type]
			  ,[requestDomain]
			  ,[requestIp]
			  ,[requestUrl]
			  ,[requestQueryString]
			  ,[requestHeader]
			  ,[requestBody]
			  ,[responseHeader]
			  ,[responseBody]
			  ,[requestScheme]
			  ,[formData]
			  ,[routeData]
			  ,[httpMethod]
			  ,[statusCode]
			  ,[userAgent]
			  ,[timeStamp]
			  ,[createdAt]
			  ,[isDeleted]
		  FROM [dbo].[RequestLogs]
		  WHERE id = @id and isdeleted = 0 

END
GO

CREATE PROCEDURE [dbo].[sp_removeCart]
	@cartid UNIQUEIDENTIFIER
AS
BEGIN
		UPDATE 
			carts
		SET 
			isActive = 0
		WHERE
			id = COALESCE(@cartid, id)

		UPDATE 
			cartDetails
		SET 
			isActive = 0
		WHERE
			cartid = COALESCE(@cartid, id)
		IF(@@ROWCOUNT > 0)
		BEGIN
			SELECT TOP 1 
				id, 
				customerid, 
				createdAt 
			FROM carts 
			WHERE 
			id = @cartid and 
			isActive = 0
		END
END
GO
CREATE PROCEDURE [dbo].[sp_removeCustomer]
	@id UNIQUEIDENTIFIER
AS
BEGIN

		UPDATE [customers]
			SET
			   [isActive] = 0
			WHERE 
				[id] = @id AND 
				[isActive] = 1
		IF @@ROWCOUNT > 0
		BEGIN
			SELECT [id]
				  ,[name]
				  ,[address]
				  ,[email]
				  ,[phone]
				  ,[createdAt]
			  FROM [dbo].[customers]
			WHERE 
			[id] = @id AND
			[isActive] = 0
		END
END
GO
CREATE PROCEDURE [dbo].[sp_removeProduct]
@id UNIQUEIDENTIFIER
AS
BEGIN

	
		UPDATE [products]
			SET
			   [isActive] = 0
			WHERE 
				[id] = @id and 
				[isActive] = 1


	IF(@@ROWCOUNT > 0)
	BEGIN
		SELECT p.[id]
			,p.[title]
			,c.[category]
			,p.[price]
			,p.[quantity]
			,p.[metadata]
			,p.[createdAt]
		FROM [dbo].[products] p
			INNER JOIN [category]  c
			ON p.category = c.id and c.isActive = 1 and p.isActive = 0
		WHERE 
			p.[id] = @id
	END
END
GO
CREATE PROCEDURE [dbo].[sp_saveCart] 
	@cartid UNIQUEIDENTIFIER,
	@customerid UNIQUEIDENTIFIER
AS
BEGIN
	IF NOT EXISTS(SELECT TOP 1 id FROM carts where id = @cartid and isActive = 1)
		BEGIN
			INSERT INTO [dbo].[carts]
					   ([id]
					   ,[customerid])
				 VALUES
					   (@cartid,
					   @customerid)
		END

		SELECT TOP 1 
			id, 
			customerid, 
			createdAt 
		FROM carts 
		WHERE 
		id = @cartid and 
		isActive = 1
END
GO
CREATE PROCEDURE [dbo].[sp_saveCartDetail]
	@cartdetailid UNIQUEIDENTIFIER,
	@cartid UNIQUEIDENTIFIER,
	@productid UNIQUEIDENTIFIER,
	@quantity DECIMAL(10,3)
AS
BEGIN
	IF NOT EXISTS(SELECT TOP 1 id FROM cartDetails where id = @cartdetailid and isActive = 1)
		BEGIN

			INSERT INTO [dbo].[cartDetails]
			   ([id]
			   ,[cartid]
			   ,[productid]
			   ,[quantity])
			 VALUES
				   (@cartdetailid
				   ,@cartid
				   ,@productid
				   ,@quantity)
		END
	ELSE
		BEGIN
			UPDATE [cartDetails]
			SET
				quantity = @quantity
			WHERE
				id = @cartdetailid AND 
				isActive = 1
		END
	SELECT TOP 1 [id]
			   ,[cartid]
			   ,[productid]
			   ,[quantity]
			   ,[createdAt]
			   ,[isActive] FROM [cartDetails] where id = @cartid and isActive = 1
END
GO
CREATE PROCEDURE [dbo].[sp_saveCartWithDetail]
	@cartid UNIQUEIDENTIFIER,
	@customerid UNIQUEIDENTIFIER,
	@cartdetails cartDetailType READONLY
AS
BEGIN
	IF NOT EXISTS(SELECT TOP 1 id FROM carts where id = @cartid and isActive = 1)
		BEGIN
			INSERT INTO [dbo].[carts]
					   ([id]
					   ,[customerid])
				 VALUES
					   (@cartid,
					   @customerid)
		END


		MERGE 
			cartDetails AS TARGET 
			USING @cartdetails AS SOURCE
		ON 
			SOURCE.[id] = TARGET.[id] AND 
			TARGET.[isActive] = 1
		
		WHEN MATCHED THEN
			UPDATE
			SET
				quantity = SOURCE.quantity

		WHEN NOT MATCHED BY TARGET THEN
			INSERT 
			   ([id]
			   ,[cartid]
			   ,[productid]
			   ,[quantity])
			 VALUES
				   ([id]
				   ,cartid
				   ,productid
				   ,quantity)

		WHEN NOT MATCHED BY SOURCE THEN
			UPDATE
			SET
				TARGET.isActive = 0;


		SELECT TOP 1 
			id, 
			customerid, 
			createdAt 
		FROM carts 
		WHERE 
		id = @cartid and 
		isActive = 1
END
GO
CREATE PROCEDURE [dbo].[sp_saveCustomer]
	@id UNIQUEIDENTIFIER,
	@name nvarchar(100),
	@address nvarchar(100),
	@phone nvarchar(30),
	@email nvarchar(30),
	@password varchar(100)
AS
BEGIN
		DECLARE @customer TABLE 
		(
			[id] UNIQUEIDENTIFIER, 
			[name] NVARCHAR(100), 
			[address] NVARCHAR(100), 
			[phone] NVARCHAR(30), 
			[email] NVARCHAR(30)
		);

		INSERT INTO @customer (id, [name], [address], [phone], [email])
			Values (@id, @name, @address, @phone, @email)

		MERGE customers AS TARGET
		USING @customer AS SOURCE
		ON TARGET.id = SOURCE.id
		WHEN NOT MATCHED THEN
		INSERT 
			   ([id]
			   ,[name]
			   ,[address]
			   ,[email]
			   ,[phone]
			   ,[password])
		 VALUES
			   (@id
			   ,@name
			   ,@address
			   ,@email
			   ,@phone
			   ,@password
			   )

		WHEN MATCHED AND TARGET.isActive = 1 THEN
		UPDATE
			SET
				[name] = SOURCE.[name]
			   ,[address] = SOURCE.[address]
			   ,[email] = SOURCE.[email]
			   ,[phone] = SOURCE.[phone];

		SELECT [id]
			  ,[name]
			  ,[address]
			  ,[email]
			  ,[phone]
			  ,[createdAt]
		  FROM [dbo].[customers]
		WHERE 
		[id] = @id AND
		[isActive] = 1
END
GO
CREATE PROCEDURE [dbo].[sp_saveExceptionLogs]
@id UNIQUEIDENTIFIER,
@reqeustid UNIQUEIDENTIFIER,
@message text,
@stacktrace text,
@timestamp datetimeoffset
AS
BEGIN
	SET NOCOUNT ON;

		INSERT INTO [dbo].[ExceptionLogs]
           ([id]
           ,[requestId]
           ,[message]
           ,[stackTrace]
           ,[timeStamp]
           ,[createdAt]
           ,[isDeleted])

		 VALUES
			   (@id
			   ,@reqeustid
			   ,@message
			   ,@stacktrace
			   ,@timestamp
			   ,GETUTCDATE()
			   ,0)
	SELECT @id
END
GO
CREATE PROCEDURE [dbo].[sp_saveProduct]
@id UNIQUEIDENTIFIER,
@title nvarchar(100),
@category nvarchar(100),
@price decimal(10,3),
@quantity decimal(10,3),
@metadata nvarchar(200)
AS
BEGIN
	DECLARE @categoryid UNIQUEIDENTIFIER = (SELECT TOP 1 id from category where category = @category and isActive = 1)
	IF(@categoryid IS NULL)
	BEGIN
		SET @categoryid = NEWID();
		INSERT INTO [dbo].[category]
           ([id]
           ,[category])
		 VALUES
           (@categoryid,
		   @category)
	END

	DECLARE @product TABLE 
	(
		[id] UNIQUEIDENTIFIER,
		[title] nvarchar(100),
		[category] nvarchar(100),
		[price] decimal(10,3),
		[quantity] decimal(10,3),
		[metadata] nvarchar(200)
	);
	
	INSERT INTO @product
		([id]
		,[title]
		,[category]
		,[price]
		,[quantity]
		,[metadata])
		VALUES
		(@id
		,@title
		,@category
		,@price
		,@quantity
		,@metadata)

	MERGE products AS TARGET
	USING @product AS SOURCE
	
	ON TARGET.id = SOURCE.id
	
	WHEN NOT MATCHED THEN
		INSERT 
			   ([id]
			   ,[title]
			   ,[category]
			   ,[price]
			   ,[quantity]
			   ,[metadata])
		 VALUES
			   (id
			   ,[title]
			   ,@categoryid
			   ,[price]
			   ,[quantity]
			   ,[metadata])

	WHEN MATCHED AND TARGET.isActive = 1 THEN
	UPDATE
		SET
			[title] = SOURCE.[title]
			,[category] = @categoryid
			,[price] = SOURCE.[price]
			,[quantity] = SOURCE.[quantity]
			,[metadata] = SOURCE.[metadata];

	SELECT p.[id]
		,p.[title]
		,c.[category]
		,p.[price]
		,p.[quantity]
		,p.[metadata]
		,p.[createdAt]
	FROM [dbo].[products] p
		INNER JOIN [category]  c
		ON p.category = c.id and c.isActive = 1 and p.isActive = 1
	WHERE 
		p.[id] = @id
END
GO
CREATE PROCEDURE [dbo].[sp_saveRefreshToken]
@id uniqueidentifier,
@customerId uniqueIdentifier,
@token nvarchar(max),
@jwtId nvarchar(max),
@isUsed bit,
@isRevoked bit,
@expiryDate datetime,
@createDate datetime
AS
BEGIN
	INSERT INTO RefreshToken(id, customerId, token, jwtId, isUsed, isRevoked, expiryDate, createDate)
	VALUES (@id, @customerId, @token, @jwtId, @isUsed, @isRevoked, @expiryDate, @createDate);

	SELECT * FROM RefreshToken
	WHERE id=@id;
END
GO
CREATE PROCEDURE [dbo].[sp_saveRequestLogs]
@id UNIQUEIDENTIFIER,
@type varchar(100),
@requestdomain varchar(100),
@requestip varchar(50),
@requesturl varchar(100),
@requestheader text,
@requestquerystring text,
@requestbody text,
@responseheader text,
@responsebody text,
@scheme varchar(10),
@formdata text,
@routedata text,
@httpmethod varchar(10),
@statuscode varchar(10),
@useragent text,
@timestamp datetimeoffset
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO [dbo].[RequestLogs]
			   ([id]
			   ,[type]
			   ,[requestDomain]
			   ,[requestIp]
			   ,[requestUrl]
			   ,[requestHeader]
			   ,[requestQueryString]
			   ,[requestBody]
			   ,[responseHeader]
			   ,[responseBody]
			   ,[requestScheme]
			   ,[formData]
			   ,[routeData]
			   ,[httpMethod]
			   ,[statusCode]
			   ,[userAgent]
			   ,[timeStamp]
			   ,[createdAt]
			   ,[isdeleted])
		 VALUES
			   (@id
			   ,@type
			   ,@requestDomain
			   ,@requestIp
			   ,@requestUrl
			   ,@requestHeader
			   ,@requestquerystring
			   ,@requestBody
			   ,@responseHeader
			   ,@responseBody
			   ,@scheme
			   ,@formdata
			   ,@routedata
			   ,@httpmethod
			   ,@statusCode
			   ,@userAgent
			   ,@timeStamp
			   ,GETUTCDATE()
			   ,0)
	SELECT @id
END
GO