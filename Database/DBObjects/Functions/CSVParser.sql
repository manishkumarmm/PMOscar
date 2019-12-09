USE [PMOscarN2NTestEvn]
GO

/****** Object:  UserDefinedFunction [dbo].[CSVParser]    Script Date: 12/9/2019 6:45:27 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[CSVParser]
(
  @s        VARCHAR(255),
  @idx      NUMERIC
)
RETURNS VARCHAR(12)
BEGIN
    DECLARE @comma int
    SET @comma = CHARINDEX(',', @s)
    WHILE 1=1
    BEGIN
        IF @comma=0
            IF @idx=1
                RETURN @s
            ELSE
                RETURN ''

        IF @idx=1
        BEGIN
            DECLARE @word VARCHAR(12)
            SET @word=LEFT(@s, @comma - 1)
            RETURN @word
        END

        SET @s = RIGHT(@s,LEN(@s)-@comma)
        SET @comma = CHARINDEX(',', @s)
        SET @idx = @idx - 1
    END
    RETURN 'not used'
END

GO


