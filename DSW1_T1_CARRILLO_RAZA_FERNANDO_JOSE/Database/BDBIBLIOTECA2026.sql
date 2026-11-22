CREATE DATABASE BDBIBLIOTECA2026;
GO

USE BDBIBLIOTECA2026;
GO

CREATE TABLE dbo.Libros
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    NombreLibro VARCHAR(120) NOT NULL,
    Autor VARCHAR(100) NOT NULL,
    NombreEditorial VARCHAR(100) NOT NULL,
    ISDN VARCHAR(20) NOT NULL
);
GO

CREATE OR ALTER PROCEDURE dbo.usp_ListarLibros
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        NombreLibro,
        Autor,
        NombreEditorial,
        ISDN
    FROM dbo.Libros
    ORDER BY Id;
END;
GO

CREATE OR ALTER PROCEDURE dbo.usp_RegistrarLibro
    @NombreLibro VARCHAR(120),
    @Autor VARCHAR(100),
    @NombreEditorial VARCHAR(100),
    @ISDN VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.Libros
    (
        NombreLibro,
        Autor,
        NombreEditorial,
        ISDN
    )
    VALUES
    (
        @NombreLibro,
        @Autor,
        @NombreEditorial,
        @ISDN
    );
END;
GO

CREATE OR ALTER PROCEDURE dbo.usp_ActualizarLibro
    @Id INT,
    @NombreLibro VARCHAR(120),
    @Autor VARCHAR(100),
    @NombreEditorial VARCHAR(100),
    @ISDN VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.Libros
    SET
        NombreLibro = @NombreLibro,
        Autor = @Autor,
        NombreEditorial = @NombreEditorial,
        ISDN = @ISDN
    WHERE Id = @Id;
END;
GO

CREATE OR ALTER PROCEDURE dbo.usp_EliminarLibro
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM dbo.Libros
    WHERE Id = @Id;
END;
GO

CREATE OR ALTER PROCEDURE dbo.usp_ListarLibrosPorAutor
    @Autor VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        NombreLibro,
        Autor,
        NombreEditorial,
        ISDN
    FROM dbo.Libros
    WHERE Autor LIKE '%' + @Autor + '%'
    ORDER BY Id;
END;
GO

CREATE OR ALTER PROCEDURE dbo.usp_ObtenerLibroPorId
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        NombreLibro,
        Autor,
        NombreEditorial,
        ISDN
    FROM dbo.Libros
    WHERE Id = @Id;
END;
GO