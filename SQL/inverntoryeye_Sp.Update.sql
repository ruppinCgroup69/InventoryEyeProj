-- Update

USE [igroup169_test2]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Yarden and Sharon>
-- Create date: <25/04/2024>
-- Description:	<Update User>
-- =============================================
Alter PROCEDURE [dbo].[SP_InEye_UpdateUser]
	  @role int,
    @lastSeen date,
    @fullName nvarchar(max),
    @birthDate date,
    @lat float(53),
    @lng float(53),
    @address nvarchar(max),
    @image nvarchar(max),
    @createdAt date,
    @searchRadius float,
    @emailAddress nvarchar(255)
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT [EmailAddress] FROM Users WHERE [EmailAddress] = @emailAddress)
    BEGIN
        RETURN 0
    END

    -- Update statements
    UPDATE Users 
    SET 
        [Role] = @role,
        [LastSeen] = @lastSeen,
        [FullName] = @fullName,
        [BirthDate] = @birthDate,
        [Lat] = @lat,
        [Lng] = @lng,
        [Address] = @address,
        [Image] = @image,
        [CreatedAt] = @createdAt,
        [SearchRadius] = @searchRadius
    WHERE [EmailAddress] = @emailAddress;

    RETURN 1;
	
END

-- =============================================

USE [igroup169_test2]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Yarden and Sharon>
-- Create date: <25/04/2024>
-- Description:	<Update Comments>
-- =============================================
Alter PROCEDURE [dbo].[SP_InEye_UpdateComments]

	@userId int,
	@createdAt date,
	@editedAt date,
	@content nvarchar(max),
	@inventoryEye date,
	@storeId int,
	@stockId int,
	@storeLocation nvarchar(max),
	@bought bit ,
	@boughtDate date,
	@productQuality int,
	@satisfaction int

AS
BEGIN
	SET NOCOUNT ON;

	if not exists (select [UserId] from Comments where [UserId] = @userId)
	begin
		return 0
	end

    -- update statements for procedure here
	update Comments set [EditedAt]=@editedAt,[Content]=@content,[InventoryEye]=@inventoryEye,[StoreId]=@storeId,[StockId]=@stockId,[StoreLocation]=@storeLocation,[Bought]=@bought,[BoughtDate]=@boughtDate,[ProductQuality]=@productQuality,[Satisfaction]=@satisfaction
	where [UserId] = @userId	   
	return 1
	
END

-- =============================================

USE [igroup169_test2]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Yarden and Sharon>
-- Create date: <25/04/2024>
-- Description:	<Update Post>
-- =============================================
Alter PROCEDURE [dbo].[SP_InEye_UpdatePost]
    @userId int,
    @createAt date,
    @editedAt datetime,
    @name nvarchar(max),
    @content nvarchar(max),
    @image nvarchar(max),
    @tags nvarchar(max),
    @category int,
    @pickUpFromUser nvarchar(max),
    @pickUpLat float(53),
    @picUpLng float(53),
    @pickUpAddress nvarchar(max)
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT [UserId] FROM Post WHERE [UserId] = @userId)
    BEGIN
        RETURN 0;
    END;

    -- Update statements for procedure here
    UPDATE Post
    SET [EditedAt] = @editedAt,
        [Name] = @name,
        [Content] = @content,
        [Image] = @image,
        [Tags] = @tags,
        [Category] = @category,
        [PickUpFromUser] = @pickUpFromUser,
        [PickUpLat] = @pickUpLat,
        [PicUpLng] = @picUpLng,
        [PickUpAddress] = @pickUpAddress
    WHERE [UserId] = @userId;

    RETURN 1;
END;

-- =============================================
-- Author:		<Yarden and Sharon>
-- Create date: <25/04/2024>
-- Description:	<Update Comment Score>
-- =============================================
Alter PROCEDURE [dbo].[SP_InEye_UpdateCommentScore]

	@commentId INT, 
	@publishedBy INT, 
	@ratedBy INT,
	@content NVARCHAR(MAX)

AS
BEGIN
    SET NOCOUNT ON;

    -- Update statements for procedure here
    UPDATE CommentScore SET [Content] = @content
    
    WHERE [PublishedBy] = @publishedBy and  [RatedBy]= @ratedBy and [CommentId] = @commentId;

    RETURN 1;
END;

------------------------------------------------
USE [igroup169_test2]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Yarden and Sharon>
-- Create date: <02/06/2024>
-- Description:	<Update Category>
-- =============================================
ALTER PROCEDURE [dbo].[SP_InEye_UpdateCategory]

	@id int,
	@categoryDesc nvarchar (max),
	@categoryImage varchar (max)

AS
BEGIN
    SET NOCOUNT ON;

    -- Update statements for procedure here
    UPDATE Category SET [CategoryDesc] = @categoryDesc, [CategoryImage]=@categoryImage

    WHERE [Id] = @id

    RETURN 1;
END;

------------------------------------------------
USE [igroup169_test2]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Yarden and Sharon>
-- Create date: <02/06/2024>
-- Description:	<Update User Email>
-- =============================================
ALTER PROCEDURE [dbo].[SP_InEye_UpdateUserEmail]

	@id int,
	@emailAddress nvarchar (max)

AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT [Id] FROM Users WHERE [Id] = @id)
    BEGIN
        RETURN 0;
    END;

    -- Update statements for procedure here
    UPDATE Users SET [EmailAddress] = @emailAddress
    WHERE [Id] = @id

    RETURN 1;
END;

------------------------------------------------
USE [igroup169_test2]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Yarden and Sharon>
-- Create date: <02/06/2024>
-- Description:	<Update Stock Level>
-- =============================================
ALTER PROCEDURE [dbo].[SP_InEye_UpdateStockLevel]

	@id int,
	@stockDesc nvarchar (max)

AS
BEGIN
    SET NOCOUNT ON;

    -- Update statements for procedure here
    UPDATE StockLevel SET StockDesc = @stockDesc
    WHERE [Id] = @id

    RETURN 1;
END;

------------------------------------------------
USE [igroup169_test2]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Yarden and Sharon>
-- Create date: <02/06/2024>
-- Description:	<Update Store>
-- =============================================
Alter PROCEDURE [dbo].[SP_InEye_UpdateStore]

	@id int,
	@storeName nvarchar (max)

AS
BEGIN
    SET NOCOUNT ON;

    -- Update statements for procedure here
    UPDATE Store SET StoreName = @storeName
    WHERE [Id] = @id

    RETURN 1;
END;

------------------------------------------------
USE [igroup169_test2]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Yarden and Sharon>
-- Create date: <02/06/2024>
-- Description:	<Update StoreCategories>
-- =============================================
Alter PROCEDURE [dbo].[SP_InEye_UpdateStoreCategories]

@storeId int,
@categoryId int,
@isActive nvarchar (max)

AS
BEGIN
    SET NOCOUNT ON;

    -- Update statements for procedure here
    UPDATE StoreCategories SET IsActive = @isActive
    WHERE StoreId = @storeId and CategoryId=@categoryId

    RETURN 1;
END;

------------------------------------------------
USE [igroup169_test2]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Yarden and Sharon>
-- Create date: <02/06/2024>
-- Description:	<Update Weights>
-- =============================================
Alter PROCEDURE [dbo].[SP_InEye_UpdateWeights]

@generalWeight int,
@boughtWeight int,
@credibilityWeight int

AS
BEGIN
    SET NOCOUNT ON;

    -- Update statements for procedure here
    UPDATE Weights SET GeneralWeight=@generalWeight, BoughtWeight=@boughtWeight, CredibilityWeight=@credibilityWeight

    RETURN 1;
END;

------------------------------------------------
USE [igroup169_test2]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Yarden and Sharon>
-- Create date: <02/06/2024>
-- Description:	<Update User Password>
-- =============================================
ALTER PROCEDURE [dbo].[SP_InEye_UpdateUserPassword]

	@email nvarchar (max),
	@password nvarchar (max)

AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT [EmailAddress] FROM Users WHERE [EmailAddress] = @email)
    BEGIN
        RETURN 0;
    END;

    -- Update statements for procedure here
    UPDATE Users SET [Password] = @password
    WHERE [EmailAddress] = @email

    RETURN 1;
END;

-- =============================================

USE [igroup169_test2]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Yarden and Sharon>
-- Create date: <25/04/2024>
-- Description:	<Update Survey>
-- =============================================
Alter PROCEDURE [dbo].[SP_InEye_UpdateSurvey]
    @userId int,
	@favCategory int,
	@favStore int

	AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT [UserId] FROM Survey WHERE [UserId] = @userId)
    BEGIN
        RETURN 0;
    END;

    UPDATE Survey
    SET [FavCategory] = @favCategory,
        [FavStore] = @favStore
    WHERE [UserId] = @userId;

    RETURN 1;
END;

-- =============================================

USE [igroup169_test2]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Yarden and Sharon>
-- Create date: <25/04/2024>
-- Description:	<Update Bonus>
-- =============================================
ALTER PROCEDURE [dbo].[SP_InEye_UpdateBonus]
    @bonusId int,
    @userId int,
    @createAt date,
    @editedAt datetime,
    @name nvarchar(max),
    @description nvarchar(max),
    @image nvarchar(max),
    @minScore int,
    @numDownloads int,
    @category int
AS
BEGIN
    SET NOCOUNT ON;

    -- Check if the User exists
    IF NOT EXISTS (SELECT [UserId] FROM Bonus WHERE [UserId] = @userId)
    BEGIN
        RETURN 0;
    END;

    -- Update Bonus details
    UPDATE Bonus
    SET [EditedAt] = @editedAt,
        [Name] = @name,
        [Description] = @description,
        [Image] = @image,
        [Category] = @category,
        [MinScore] = @minScore,
        [NumDownloads] = @numDownloads
    WHERE [BonusId] = @bonusId;

    RETURN 1;
END;

