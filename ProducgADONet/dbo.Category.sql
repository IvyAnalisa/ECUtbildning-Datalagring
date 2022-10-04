CREATE TABLE [dbo].[Category] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [CategoryName] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Category_Product] FOREIGN KEY ([Id]) REFERENCES [dbo].[Product] ([Id]),
    
);

