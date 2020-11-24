CREATE TABLE [dbo].[RegistrosPontos]
(
    [Id] INT NOT NULL identity,
    [Data] DATE NULL,
    [Hora] TIME(7) NULL,
    [NomeUsuario] NVARCHAR (50) NULL,
    [Tipo] NCHAR (10) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

