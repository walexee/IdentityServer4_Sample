﻿CREATE TABLE [dbo].[ApiScopeClaims] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [ApiScopeId] INT            NOT NULL,
    [Type]       NVARCHAR (200) NOT NULL,
    CONSTRAINT [PK_ApiScopeClaims] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ApiScopeClaims_ApiScopes_ApiScopeId] FOREIGN KEY ([ApiScopeId]) REFERENCES [dbo].[ApiScopes] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ApiScopeClaims_ApiScopeId]
    ON [dbo].[ApiScopeClaims]([ApiScopeId] ASC);

