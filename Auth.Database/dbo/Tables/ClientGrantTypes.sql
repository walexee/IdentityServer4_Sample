﻿CREATE TABLE [dbo].[ClientGrantTypes] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [ClientId]  INT            NOT NULL,
    [GrantType] NVARCHAR (250) NOT NULL,
    CONSTRAINT [PK_ClientGrantTypes] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ClientGrantTypes_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Clients] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ClientGrantTypes_ClientId]
    ON [dbo].[ClientGrantTypes]([ClientId] ASC);

