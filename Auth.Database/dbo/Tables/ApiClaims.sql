CREATE TABLE [dbo].[ApiClaims] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [ApiResourceId] INT            NOT NULL,
    [Type]          NVARCHAR (200) NOT NULL,
    CONSTRAINT [PK_ApiClaims] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ApiClaims_ApiResources_ApiResourceId] FOREIGN KEY ([ApiResourceId]) REFERENCES [dbo].[ApiResources] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ApiClaims_ApiResourceId]
    ON [dbo].[ApiClaims]([ApiResourceId] ASC);

