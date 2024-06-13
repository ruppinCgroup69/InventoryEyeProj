USE [igroup169_test2]
GO
/****** Object:  StoredProcedure [dbo].[SP_InEye_Login]    Script Date: 6/13/2024 7:30:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Yarden And Sharon>
-- Create date: <Create Date,02/06/2024,>
-- Description:	<Description,SP_Login>
-- =============================================
ALTER PROCEDURE [dbo].[SP_InEye_Login]
	-- Add the parameters for the stored procedure here
@email nvarchar(max),
@password nvarchar(max)

AS
BEGIN

select * from dbo.LoginUser(@email, @password);
END
