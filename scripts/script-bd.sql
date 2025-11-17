-- Criação da tabela Pontuacoes (caso não exista)
IF OBJECT_ID('Pontuacoes', 'U') IS NULL
BEGIN
    CREATE TABLE Pontuacoes (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Nome NVARCHAR(2000) NOT NULL,
        EcoCoins INT NOT NULL,
        NivelVerde INT NOT NULL,
        AtualizadoEm DATETIME2 NOT NULL
    );
END;

-- Criação da tabela Recompensas (caso não exista)
IF OBJECT_ID('Recompensas', 'U') IS NULL
BEGIN
    CREATE TABLE Recompensas (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Nome NVARCHAR(2000) NOT NULL,
        CustoEcoCoins INT NOT NULL,
        Tipo NVARCHAR(2000) NOT NULL
    );
END;