USE [StoreSample]
GO
/****** Object:  StoredProcedure [dbo].[AddNewOrder]    Script Date: 5/05/2023 3:44:22 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		CARLOS DE LA CRUZ
-- Create date: 2023-05-03
-- Description:	PERMITE CREAR LA ORDEN Y UN PRODUCTO DENTRO DE ELLA
-- =============================================
CREATE PROCEDURE [dbo].[AddNewOrder] 
	-- Add the parameters for the stored procedure here
	@empid INT
	,@productid INT
	,@shipperid INT
	,@qty SMALLINT
	,@discount numeric(4,3)
	,@unitprice money
	,@shipname nvarchar(40)
	,@shipaddress nvarchar(60)
	,@shipcity nvarchar(15)
	,@shipcountry nvarchar(15)
	,@orderdate datetime
	,@requireddate datetime
	,@shippeddate datetime
	,@freight money

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @orderid int;

    -- Insert statements for procedure here
	INSERT INTO [Sales].[Orders]
           ([empid]
           ,[orderdate]
           ,[requireddate]
           ,[shippeddate]
           ,[shipperid]
           ,[freight]
           ,[shipname]
           ,[shipaddress]
           ,[shipcity]
           ,[shipcountry])
     VALUES
           (@empid
           ,@orderdate
           ,@requireddate
           ,@shippeddate
		   ,@shipperid
           ,@freight
           ,@shipname
           ,@shipaddress
           ,@shipcity
           ,@shipcountry)
	
	set @orderid = SCOPE_IDENTITY();

	INSERT INTO [Sales].[OrderDetails]
           ([orderid]
           ,[productid]
           ,[unitprice]
           ,[qty]
           ,[discount])
     VALUES
           (@orderid
           ,@productid
           ,@unitprice
           ,@qty
           ,@discount)

	IF OBJECT_ID('tempdb..#A') IS NOT NULL DROP TABLE #A
	SELECT @orderid AS orderid
	into #A

	SELECT orderid FROM #A

END
GO
/****** Object:  StoredProcedure [dbo].[GetClientOrders]    Script Date: 5/05/2023 3:44:22 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetClientOrders]
	-- Add the parameters for the stored procedure here
	@custid int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
		A.[orderid]
		,A.[requireddate]
		,A.[shippeddate]
		,A.[shipname]
		,A.[shipaddress]
		,A.[shipcity]
	FROM [Sales].[Orders] AS A WITH(NOLOCK)
	WHERE [custid] = @custid

END
GO
/****** Object:  StoredProcedure [dbo].[GetEmployees]    Script Date: 5/05/2023 3:44:22 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetEmployees]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
	A.[empid]
	,CONCAT(A.[lastname],A.[lastname]) AS fullname
	FROM [HR].[Employees] AS A WITH(NOLOCK)
END
GO
/****** Object:  StoredProcedure [dbo].[GetProducts]    Script Date: 5/05/2023 3:44:22 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetProducts]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
		A.[productid]
		,A.[productname]
	FROM [Production].[Products] AS A WITH(NOLOCK)

END
GO
/****** Object:  StoredProcedure [dbo].[GetShippers]    Script Date: 5/05/2023 3:44:22 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetShippers]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
	A.[shipperid]
	,A.[companyname]
	FROM [Sales].[Shippers] AS A WITH(NOLOCK)

END
GO
/****** Object:  StoredProcedure [dbo].[SalesPredictedDate]    Script Date: 5/05/2023 3:44:22 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		CARLOS DE LA CRUZ
-- Create date: 2023-05-03
-- Description:	Listar clientes con fecha de ultima orden y fecha de posible orden
-- =============================================
CREATE PROCEDURE [dbo].[SalesPredictedDate] 
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF OBJECT_ID('tempdb..#A') IS NOT NULL DROP TABLE #A

		SELECT 
		[custid]
		,MAX([orderdate]) AS [maxdate]
		INTO #A
		FROM [Sales].[Orders] AS A WITH(NOLOCK)
		GROUP BY [custid]

	IF OBJECT_ID('tempdb..#B') IS NOT NULL DROP TABLE #B

		SELECT 
		[custid]
		,AVG(DATEPART(DAY, A.orderdate)) AS [avgorderdate]
		INTO #B
		FROM [Sales].[Orders] AS A WITH(NOLOCK)
		GROUP BY [custid]

	SELECT 
	A.custid
	,A.maxdate AS [lastorderdate]
	,DATEADD(DAY,b.avgorderdate, a.maxdate ) AS [nextpredictedorder]
	FROM #A AS A INNER JOIN #B AS B
	ON A.[custid] = B.[custid]

END
GO
