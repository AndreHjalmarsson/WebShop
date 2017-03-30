CREATE TABLE [dbo].[Products] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (MAX) NOT NULL,
    [Category] NVARCHAR (MAX) NOT NULL,
    [Price]    INT            NOT NULL,
    [Image]    NVARCHAR(MAX)          NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

