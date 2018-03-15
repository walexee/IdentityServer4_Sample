﻿CREATE TABLE [dbo].[ClientCorsOrigins] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [ClientId] INT            NOT NULL,
    [Origin]   NVARCHAR (150) NOT NULL,
    CONSTRAINT [PK_ClientCorsOrigins] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ClientCorsOrigins_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Clients] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ClientCorsOrigins_ClientId]
    ON [dbo].[ClientCorsOrigins]([ClientId] ASC);

