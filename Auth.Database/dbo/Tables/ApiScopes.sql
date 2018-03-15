CREATE TABLE [dbo].[ApiScopes] (
    [Id]                      INT             IDENTITY (1, 1) NOT NULL,
    [ApiResourceId]           INT             NOT NULL,
    [Description]             NVARCHAR (1000) NULL,
    [DisplayName]             NVARCHAR (200)  NULL,
    [Emphasize]               BIT             NOT NULL,
    [Name]                    NVARCHAR (200)  NOT NULL,
    [Required]                BIT             NOT NULL,
    [ShowInDiscoveryDocument] BIT             NOT NULL,
    CONSTRAINT [PK_ApiScopes] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ApiScopes_ApiResources_ApiResourceId] FOREIGN KEY ([ApiResourceId]) REFERENCES [dbo].[ApiResources] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ApiScopes_ApiResourceId]
    ON [dbo].[ApiScopes]([ApiResourceId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_ApiScopes_Name]
    ON [dbo].[ApiScopes]([Name] ASC);

