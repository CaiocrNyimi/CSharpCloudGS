-- Criação da tabela Pontuacoes
CREATE TABLE Pontuacoes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(2000) NOT NULL,
    EcoCoins INT NOT NULL,
    NivelVerde INT NOT NULL,
    AtualizadoEm DATETIME2 NOT NULL
);

-- Criação da tabela Recompensas
CREATE TABLE Recompensas (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(2000) NOT NULL,
    CustoEcoCoins INT NOT NULL,
    Tipo NVARCHAR(2000) NOT NULL
);