-- Insert

USE [igroup169_test2]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Yarden and Sharon>
-- Create date: <24/05/2024>
-- Description:	<Insert User>
-- =============================================
Alter PROCEDURE [dbo].[SP_InEye_InsertUser]
@role int,
@lastSeen date,
@fullName nvarchar(max),
@emailAddress nvarchar(255),
@birthDate date ,
@lat float(53),
@lng float(53),
@address nvarchar(max) ,
@image nvarchar(max),
@createdAt date

AS
BEGIN
	SET NOCOUNT ON;

	if exists (select EmailAddress from Users where EmailAddress = @emailAddress)
	begin
		return 0
	end

INSERT INTO Users ([Role],[LastSeen],[FullName],[EmailAddress],[BirthDate],[Lat],[Lng],[Address],[Image],[CreatedAt])
VALUES (@role,@lastSeen,@fullName,@emailAddress,@birthDate,@lat,@lng,@address,@image,@createdAt);
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
-- Create date: <24/05/2024>
-- Description:	<Insert Comments>
-- =============================================
Alter PROCEDURE [dbo].[SP_InEye_InsertComments]

@userId int,
@createdAt date,
@editedAt date,
@content nvarchar(max),
@inventoryEye date,
@storeId int,
@stockId int,
@storeLocation nvarchar(max),
@bought nvarchar(max),
@boughtDate date,
@productQuality int

AS
BEGIN
	SET NOCOUNT ON;

INSERT INTO Comments ([UserId],[CreatedAt],[EditedAt],[Content],[InventoryEye],[StoreId],[StockId],[StoreLocation],[Bought],[BoughtDate],[ProductQuality])
VALUES (@userId,@createdAt,@editedAt,@content,@inventoryEye,@storeId,@stockId,@storeLocation,@bought,@boughtDate,@productQuality);
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
-- Create date: <24/05/2024>
-- Description:	<Insert CommentScore>
-- =============================================
Alter PROCEDURE [dbo].[SP_InEye_InsertCommentScore]

@generalScore int,
@credibility int,
@bought nvarchar(max),
@content nvarchar(max)

AS
BEGIN
	SET NOCOUNT ON;

INSERT INTO CommentScore ([GeneralScore],[Credibility],[Bought],[Content])
VALUES (@generalScore,@credibility,@bought,@content);
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
-- Create date: <24/05/2024>
-- Description:	<Insert Post>
-- =============================================
Alter PROCEDURE [dbo].[SP_InEye_InsertPost]

	@userId int,
	@createAt date ,
	@editedAt datetime,
	@name nvarchar(max),
	@content nvarchar(max) ,
	@image nvarchar(max) ,
	@tags nvarchar,
	@category int ,
	@pickUpFromUser nvarchar(max),
	@pickUpLat float(53),
	@picUpLng float(53),
	@pickUpAddress nvarchar(max)

AS
BEGIN
	SET NOCOUNT ON;

INSERT INTO Post ([UserId],[CreateAt],[EditedAt],[Name],[Content],[Image],[Tags],[Category],[PickUpFromUser],[PickUpLat],[PicUpLng],[PickUpAddress])
VALUES (@userId,@createAt,@editedAt,@name,@content,@image,@tags,@category,@pickUpFromUser,@pickUpLat,@picUpLng,@pickUpAddress);
return 1

END

