CREATE DATABASE SistemaDeEnvios;
GO

USE SistemaDeEnvios;
GO

CREATE TABLE Perfil (
    id_perfil INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL,
    activo BIT NOT NULL DEFAULT 1,
    dvh VARCHAR(255) NULL
);

CREATE TABLE Familia (
    id_familia INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL,
    activo BIT NOT NULL DEFAULT 1,
    dvh VARCHAR(255) NULL
);

CREATE TABLE Permiso (
    id_permiso INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL,
    dvh VARCHAR(255) NULL
);

CREATE TABLE Perfil_Familia (
    id_perfil INT,
    id_familia INT,
    dvh VARCHAR(255) NULL,

    PRIMARY KEY (id_perfil, id_familia),

    FOREIGN KEY (id_perfil) REFERENCES Perfil(id_perfil),
    FOREIGN KEY (id_familia) REFERENCES Familia(id_familia)
);

CREATE TABLE Perfil_Permiso (
    id_perfil INT,
    id_permiso INT,
    dvh VARCHAR(255) NULL,

    PRIMARY KEY (id_perfil, id_permiso),

    FOREIGN KEY (id_perfil) REFERENCES Perfil(id_perfil),
    FOREIGN KEY (id_permiso) REFERENCES Permiso(id_permiso)
);

CREATE TABLE Familia_Permiso (
    id_familia INT,
    id_permiso INT,
    dvh VARCHAR(255) NULL,

    PRIMARY KEY (id_familia, id_permiso),

    FOREIGN KEY (id_familia) REFERENCES Familia(id_familia),
    FOREIGN KEY (id_permiso) REFERENCES Permiso(id_permiso)
);

CREATE TABLE Familia_Familia (
    id_familia_padre INT,
    id_familia_hija INT,
    dvh VARCHAR(255) NULL,

    PRIMARY KEY (id_familia_padre, id_familia_hija),
    FOREIGN KEY (id_familia_padre) REFERENCES Familia(id_familia),
    FOREIGN KEY (id_familia_hija) REFERENCES Familia(id_familia),

    CHECK (id_familia_padre <> id_familia_hija)
);

CREATE TABLE Idioma (
    id_idioma INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(50),
    dvh VARCHAR(255) NULL
);

CREATE TABLE Usuario (
    dni INT PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL,
    apellido VARCHAR(50) NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL,
    password VARCHAR(255) NOT NULL,
    id_perfil INT NOT NULL,
    bloqueado BIT NOT NULL DEFAULT 0,
    estado BIT NOT NULL DEFAULT 1,
    intentos_fallidos INT NOT NULL DEFAULT 0,
    id_idioma INT NOT NULL DEFAULT 1,
    dvh VARCHAR(255) NULL,

    FOREIGN KEY (id_perfil) REFERENCES Perfil(id_perfil),
    FOREIGN KEY (id_idioma) REFERENCES Idioma(id_idioma)
);

CREATE TABLE Evento (
    id_evento INT IDENTITY(1,1) PRIMARY KEY,
    dni_usuario INT NOT NULL,
    fecha DATETIME NOT NULL DEFAULT GETDATE(),
    modulo VARCHAR(100) NOT NULL,
    evento VARCHAR(100) NOT NULL,
    criticidad INT,
    dvh VARCHAR(255) NULL,

    FOREIGN KEY (dni_usuario) REFERENCES Usuario(dni)
);

CREATE TABLE DVV
(
    tabla VARCHAR(100) PRIMARY KEY,
    dvv VARCHAR(255) NULL
);

INSERT INTO Idioma (nombre, dvh)
VALUES
('Español', 'dx8d9bOVRNwzpqYBCSojgXAEX23Il7bhpgN4kwEETis='),
('English', 'p11WWRjsIW623CneVGy5s/oUP38lvARbxMzTLzbS6j0='),
('Português', 'SSA/Hj1vinxhGZkeUnxluAJkk+Oj1xwVdzAlA7HF6Aw=');

INSERT INTO Perfil (nombre, dvh)
VALUES
('Administrador', 'hx10p2cqZeRwfI57C1MTu+rBOtXIGuzXChhbE8KANEQ='),
('Recepcionista', 'tYXOx6u0bLtp3zY+XEspvDl/11j+lg6iJfEH+tlainc='),
('Gestor', '3PiXTLcpL/j8+cT0hGnYoZjdvYg+/EeMnI1v+L4AduM='),
('Repartidor', '6fHC2zzOl3yy6+1UeNSymO8B8OmPjlh8qw3pj9tVjF8='),
('Cajero', 'HdA/mao8rWuG/gUsbVoVeSzY11QHQxe+YD/WvKWUHuM=');

INSERT INTO Permiso (nombre, dvh)
VALUES
('Cambiar contraseña', 'H+E5DLBVNay4E6fW8dM0KkIvB+MVj+J/5UNTeZChMLE='),
('Cambiar idioma', 'teqn75buySzaZzNG4t3aRBbGjk0KRo8+biKNi1PJRO4='),
('Gestión de usuarios', 'hcEnHEEbo8nG6UE3SaEmnB4La8Gdl2XojHeGl3zkqI8='),
('Bitácora de eventos', 'tOq45Yiw61plHyoB2im738bOPLSS3lH/xMYyJQaEwTU='),
('Gestión de perfiles', 'EqMgaJJaX5y8OxHbHDpCIJomhn2GgSktb96SbLThEBY='),
('Gestión de backup', '6SfoesT4m9oReycKM7sRr0uVihB4vHRSknX0YSI3M2I='),
('Registro de clientes', 'WTP73QICBpHWJfdAVEcQIiw0tpLnD5sTDb0XSDLWGUE='),
('Registro de envíos', 'oLKIajiuH1DaHptu8x/sY2L1Kh6yRl5SVSOo8c9c9fA='),
('Cobro de envíos', 'LGvlWS/lAfJYn9VAsZSzgHMihHdMZNtaiGZNnQqv0Rk='),
('Gestión de facturas', 'DXinYIcd2tRiDk8vUd9efp5J0/Jjult/uxQCP5Jc2/U='),
('Creación de rutas', 'KBR3tvJNOP6aBn9MztoVBMsvfnJcNUWFZ7pO0SVR43k='),
('Asignación de rutas', 'Uw3LsHWOLdIl1LdM8DnHYFD9dCHeR5vY5+17RWrAfMA='),
('Administración de rutas', 'DzcwG/8hkkmZJTTqaG0GXcnCRfNgyb4daruigi6s2Tk='),
('Bitacora de cambios', 'jWn+dL/PMunVi5zC4P7z2rloO3J23mRQZWvAsIl3GDw='),
('Reporte inteligente', '5Tu6ltICd8GJuPCrQlh+YNad7aiOSLiXH68AR0anAMc=');

INSERT INTO DVV(tabla, dvv)
VALUES
('Perfil', 'VLzgKRkKR38U+rAvR+HHHUyyVQP+g4cdRrRzeCPqLW0='),
('Familia', ''),
('Permiso', 'c5KDD3K6K69kHIHgHi1e8SobzIF8SY4CY8pStq156GM='),
('Perfil_Familia', ''),
('Perfil_Permiso', 'DFGLgvmTrx0vtT0ospEn+qJ7HFpF/EKTvnlW8nUBZzE='),
('Familia_Permiso', ''),
('Familia_Familia', ''),
('Idioma', '11RGkmmGmKxFdtPT72JnyBSE4qBNhdFZBrTmw8mfZIM='),
('Usuario', 'V2cmMqAlpNq0WEf0eoVpn0p90+6Pa/gUqNz9mq2NT7g='),
('Evento', ''),
('Cliente', ''),
('Paquete', ''),
('Destino', ''),
('Envio', ''),
('Pago', ''),
('Factura', ''),
('Ruta', ''),
('Ruta_Envio', ''),
('AsignacionRuta', ''),
('IntentoEntrega', ''),
('Envio_C', '');

-- Administrador base
INSERT INTO Usuario (dni, nombre, apellido, email, password, id_perfil, dvh) VALUES (12345678, 'Administrador', 'Sistema', 'admin@sistema.com', '$2a$11$lVbVvtP4dYgekTqzbwg2zOpEOIoEKEkyuckdej7hbDo/vvOS69dMO', 1, 'ki7e9KBIii3mEKYZQUf+eoZKHC8n5Kd02xU7O7WHsVI=');
-- La contraseña en el programa es admin123

INSERT INTO Perfil_Permiso (id_perfil, id_permiso, dvh)
VALUES
(1, 1, 'T8grJq7LR9KGjE7741gXMqPny8xsLvsyBiwIFwoF7rg='),
(1, 2, 'a1HUMd9dfxQcvs7M957fPdhhw7QGnwsRZho+76y7qRg='),
(1, 3, 'P9ujXwTcjEYphsmSvPh1VGJXETByqQnBYvfkcOWB4ng='),
(1, 4, 'hSeokeIkE2lQ/zLKIStFvJP2n7uAHDsevtrFJ3X5nmE='),
(1, 5, '5in6ZZjXMnaPfHJrS2IShfnDuFMDkAqpEgF9t2F9i9s='),
(1, 6, 'sX720Zx6Wx7oO5B8WVUm3LHrBtuCJ9ZQ1d2gqfTOjNk='),
(1, 14, 'nx+dzjGcRwDvKOyMU708yOar5kxoOFR5q4khWAalvdY='),
(1, 15, 'KNrnyL3i88pgj4bQ4WohTe50x0vuARzf3Ua8BLZVvBQ=');

CREATE TABLE Cliente (
    dni INT PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL,
    apellido VARCHAR(50) NOT NULL,
    telefono VARCHAR(30) NOT NULL,
    email VARCHAR(100) NOT NULL,
    estado BIT NOT NULL DEFAULT 1,
    dvh VARCHAR(255) NULL
);

CREATE TABLE Paquete (
    id_paquete INT IDENTITY(1,1) PRIMARY KEY,
    descripcion VARCHAR(200) NOT NULL,
    peso DECIMAL(10,2) NOT NULL,
    alto DECIMAL(10,2) NOT NULL,
    ancho DECIMAL(10,2) NOT NULL,
    largo DECIMAL(10,2) NOT NULL,
    dvh VARCHAR(255) NULL
);

CREATE TABLE Destino (
    id_destino INT IDENTITY(1,1) PRIMARY KEY,

    direccion VARCHAR(200) NOT NULL,
    ciudad VARCHAR(100) NOT NULL,
    codigo_postal VARCHAR(10) NOT NULL,
    provincia VARCHAR(100) NOT NULL,

    dni_destinatario INT NOT NULL,
    nombre_destinatario VARCHAR(50) NOT NULL,
    apellido_destinatario VARCHAR(50) NOT NULL,
    telefono_destinatario VARCHAR(30) NOT NULL,
    email_destinatario VARCHAR(100) NOT NULL,

    dvh VARCHAR(255) NULL
);

CREATE TABLE Envio (
    id_envio INT IDENTITY(1,1) PRIMARY KEY,
    codigo_seguimiento VARCHAR(20) NULL UNIQUE,
    origen VARCHAR(200) NOT NULL,
    fecha_registro DATETIME NOT NULL,
    estado VARCHAR(50) NOT NULL,
    id_paquete INT NOT NULL,
    id_destino INT NOT NULL,
    id_remitente INT NOT NULL,
    dvh VARCHAR(255) NULL,

    CONSTRAINT FK_Envio_Paquete
        FOREIGN KEY (id_paquete)
        REFERENCES Paquete(id_paquete),

    CONSTRAINT FK_Envio_Destino
        FOREIGN KEY (id_destino)
        REFERENCES Destino(id_destino),

    CONSTRAINT FK_Envio_Remitente
        FOREIGN KEY (id_remitente)
        REFERENCES Cliente(dni)
);

CREATE TABLE Pago (
    id_pago INT IDENTITY(1,1) PRIMARY KEY,
    id_envio INT NOT NULL,
    dni_cliente INT NOT NULL,
    importe DECIMAL(10,2) NOT NULL,
    fecha_pago DATETIME NOT NULL,
    estado VARCHAR(30) NOT NULL,
    dvh VARCHAR(255) NULL,
    CONSTRAINT FK_Pago_Envio FOREIGN KEY (id_envio) REFERENCES Envio(id_envio),
    CONSTRAINT FK_Pago_Cliente FOREIGN KEY (dni_cliente) REFERENCES Cliente(dni)
);

CREATE TABLE Factura (
    id_factura INT IDENTITY(1,1) PRIMARY KEY,
    id_envio INT NOT NULL,
    id_pago INT NULL,
    dni_cliente INT NOT NULL,
    importe DECIMAL(10,2) NOT NULL,
    fecha DATETIME NOT NULL,
    dvh VARCHAR(255) NULL,
    CONSTRAINT FK_Factura_Envio FOREIGN KEY (id_envio) REFERENCES Envio(id_envio),
    CONSTRAINT FK_Factura_Pago FOREIGN KEY (id_pago) REFERENCES Pago(id_pago),
    CONSTRAINT UQ_Factura_Pago UNIQUE (id_pago)
);

CREATE TABLE Ruta (
    id_ruta INT IDENTITY(1,1) PRIMARY KEY,
    descripcion VARCHAR(150) NULL,
    fecha_creacion DATETIME NOT NULL DEFAULT GETDATE(),
    estado VARCHAR(20) NOT NULL DEFAULT 'Pendiente',
    dvh VARCHAR(64) NOT NULL DEFAULT ''
);

CREATE TABLE Ruta_Envio (
    id_ruta_envio INT IDENTITY(1,1) PRIMARY KEY,
    id_ruta INT NOT NULL,
    id_envio INT NOT NULL,
    orden_entrega INT NOT NULL,
    dvh VARCHAR(64) NOT NULL DEFAULT '',
    CONSTRAINT FK_RutaEnvio_Ruta FOREIGN KEY (id_ruta) REFERENCES Ruta(id_ruta),
    CONSTRAINT FK_RutaEnvio_Envio FOREIGN KEY (id_envio) REFERENCES Envio(id_envio),
    CONSTRAINT UQ_RutaEnvio_Orden UNIQUE (id_ruta, orden_entrega),
    CONSTRAINT CK_RutaEnvio_Orden CHECK (orden_entrega > 0)
);

CREATE TABLE AsignacionRuta (
    id_asignacion INT IDENTITY(1,1) PRIMARY KEY,
    id_ruta INT NOT NULL,
    dni_repartidor INT NOT NULL,
    fecha_asignacion DATETIME NOT NULL DEFAULT GETDATE(),
    fecha_inicio DATETIME NULL,
    fecha_fin DATETIME NULL,
    estado VARCHAR(20) NOT NULL DEFAULT 'Activa',
    motivo_cancelacion VARCHAR(250) NULL,
    dvh VARCHAR(64) NOT NULL DEFAULT '',
    CONSTRAINT FK_AsignacionRuta_Ruta FOREIGN KEY (id_ruta) REFERENCES Ruta(id_ruta),
    CONSTRAINT FK_AsignacionRuta_Repartidor FOREIGN KEY (dni_repartidor) REFERENCES Usuario(dni),
    CONSTRAINT CK_AsignacionRuta_Estado CHECK (estado IN ('Activa', 'Finalizada', 'Cancelada'))
);

CREATE TABLE IntentoEntrega (
    id_intento INT IDENTITY(1,1) PRIMARY KEY,
    id_ruta_envio INT NOT NULL,
    fecha_intento DATETIME NOT NULL DEFAULT GETDATE(),
    resultado VARCHAR(20) NOT NULL,
    tipo_inconveniente VARCHAR(150) NULL,
    observaciones VARCHAR(500) NULL,
    dvh VARCHAR(64) NOT NULL DEFAULT '',
    CONSTRAINT FK_IntentoEntrega_RutaEnvio FOREIGN KEY (id_ruta_envio) REFERENCES Ruta_Envio(id_ruta_envio),
    CONSTRAINT CK_IntentoEntrega_Resultado CHECK (resultado IN ('Entregado', 'Fallido'))
);

ALTER TABLE Envio
ADD intentos_fallidos INT NOT NULL
CONSTRAINT DF_Envio_IntentosFallidos DEFAULT 0;

CREATE TABLE Envio_C (
    id_version INT IDENTITY(1,1) PRIMARY KEY,
    id_envio INT NOT NULL,
    codigo_seguimiento VARCHAR(20) NULL,
    origen VARCHAR(200) NOT NULL,
    fecha_registro DATETIME NOT NULL,
    id_paquete INT NOT NULL,
    id_destino INT NOT NULL,
    id_remitente INT NOT NULL,
    fecha_cambio DATETIME NOT NULL DEFAULT GETDATE(),
    intentos_fallidos INT NOT NULL DEFAULT 0,
    descripcion_paquete VARCHAR(200) NOT NULL,
    peso DECIMAL(10,2) NOT NULL,
    alto DECIMAL(10,2) NOT NULL,
    ancho DECIMAL(10,2) NOT NULL,
    largo DECIMAL(10,2) NOT NULL,
    direccion VARCHAR(200) NOT NULL,
    ciudad VARCHAR(100) NOT NULL,
    codigo_postal VARCHAR(10) NOT NULL,
    provincia VARCHAR(100) NOT NULL,
    dni_destinatario INT NOT NULL,
    nombre_destinatario VARCHAR(50) NOT NULL,
    apellido_destinatario VARCHAR(50) NOT NULL,
    telefono_destinatario VARCHAR(30) NOT NULL,
    email_destinatario VARCHAR(100) NOT NULL,
    Act BIT NOT NULL DEFAULT 1,
    dvh VARCHAR(255) NULL,
    CONSTRAINT FK_Envio_C_Envio FOREIGN KEY (id_envio) REFERENCES dbo.Envio(id_envio)
);

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET ANSI_PADDING ON;
SET ANSI_WARNINGS ON;
SET CONCAT_NULL_YIELDS_NULL ON;
SET ARITHABORT ON;
SET NUMERIC_ROUNDABORT OFF;
GO

CREATE UNIQUE INDEX UX_Envio_C_Act
ON Envio_C(id_envio)
WHERE Act = 1;

INSERT INTO Envio_C (
    id_envio, codigo_seguimiento, origen, fecha_registro, id_paquete, id_destino, id_remitente, intentos_fallidos,
    descripcion_paquete, peso, alto, ancho, largo, direccion, ciudad, codigo_postal, provincia,
    dni_destinatario, nombre_destinatario, apellido_destinatario, telefono_destinatario, email_destinatario, Act
)
SELECT
    e.id_envio, e.codigo_seguimiento, e.origen, e.fecha_registro, e.id_paquete, e.id_destino, e.id_remitente,
    e.intentos_fallidos, p.descripcion, p.peso, p.alto, p.ancho, p.largo, d.direccion, d.ciudad, d.codigo_postal,
    d.provincia, d.dni_destinatario, d.nombre_destinatario, d.apellido_destinatario, d.telefono_destinatario,
    d.email_destinatario, 1
FROM Envio e
INNER JOIN Paquete p ON e.id_paquete = p.id_paquete
INNER JOIN Destino d ON e.id_destino = d.id_destino
WHERE NOT EXISTS (SELECT 1 FROM Envio_C ec WHERE ec.id_envio = e.id_envio);
GO

CREATE OR ALTER TRIGGER TR_Envio_Auditoria
ON dbo.Envio
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF TRY_CONVERT(BIT, SESSION_CONTEXT(N'RestaurandoEnvio')) = 1 RETURN;

    DECLARE @EnviosARegistrar TABLE (id_envio INT PRIMARY KEY);

    INSERT INTO @EnviosARegistrar (id_envio)
    SELECT i.id_envio
    FROM inserted i
    LEFT JOIN deleted d ON d.id_envio = i.id_envio
    WHERE i.codigo_seguimiento IS NOT NULL
        AND (d.id_envio IS NULL OR EXISTS (
            SELECT i.codigo_seguimiento, i.origen, i.fecha_registro, i.id_paquete, i.id_destino, i.id_remitente, i.intentos_fallidos
            EXCEPT
            SELECT d.codigo_seguimiento, d.origen, d.fecha_registro, d.id_paquete, d.id_destino, d.id_remitente, d.intentos_fallidos
        ));

    UPDATE ec SET ec.Act = 0
    FROM Envio_C ec
    INNER JOIN @EnviosARegistrar a ON a.id_envio = ec.id_envio
    WHERE ec.Act = 1;

    INSERT INTO Envio_C (
        id_envio, codigo_seguimiento, origen, fecha_registro, id_paquete, id_destino, id_remitente, intentos_fallidos,
        descripcion_paquete, peso, alto, ancho, largo, direccion, ciudad, codigo_postal, provincia,
        dni_destinatario, nombre_destinatario, apellido_destinatario, telefono_destinatario, email_destinatario, Act
    )
    SELECT
        e.id_envio, e.codigo_seguimiento, e.origen, e.fecha_registro, e.id_paquete, e.id_destino, e.id_remitente,
        e.intentos_fallidos, p.descripcion, p.peso, p.alto, p.ancho, p.largo, d.direccion, d.ciudad, d.codigo_postal,
        d.provincia, d.dni_destinatario, d.nombre_destinatario, d.apellido_destinatario, d.telefono_destinatario,
        d.email_destinatario, 1
    FROM @EnviosARegistrar a
    INNER JOIN Envio e ON a.id_envio = e.id_envio
    INNER JOIN Paquete p ON e.id_paquete = p.id_paquete
    INNER JOIN Destino d ON e.id_destino = d.id_destino;
END;
GO

CREATE OR ALTER TRIGGER TR_Paquete_Auditoria
ON dbo.Paquete
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF TRY_CONVERT(BIT, SESSION_CONTEXT(N'ModificacionEnvio')) = 1 RETURN;
    IF TRY_CONVERT(BIT, SESSION_CONTEXT(N'RestaurandoEnvio')) = 1 RETURN;

    DECLARE @EnviosARegistrar TABLE (id_envio INT PRIMARY KEY);

    INSERT INTO @EnviosARegistrar (id_envio)
    SELECT DISTINCT e.id_envio
    FROM inserted i
    INNER JOIN deleted d ON i.id_paquete = d.id_paquete
    INNER JOIN Envio e ON e.id_paquete = i.id_paquete
    WHERE EXISTS (
        SELECT i.descripcion, i.peso, i.alto, i.ancho, i.largo
        EXCEPT
        SELECT d.descripcion, d.peso, d.alto, d.ancho, d.largo
    );

    UPDATE ec SET ec.Act = 0
    FROM Envio_C ec
    INNER JOIN @EnviosARegistrar a ON a.id_envio = ec.id_envio
    WHERE ec.Act = 1;

    INSERT INTO Envio_C (
        id_envio, codigo_seguimiento, origen, fecha_registro, id_paquete, id_destino, id_remitente, intentos_fallidos,
        descripcion_paquete, peso, alto, ancho, largo, direccion, ciudad, codigo_postal, provincia,
        dni_destinatario, nombre_destinatario, apellido_destinatario, telefono_destinatario, email_destinatario, Act
    )
    SELECT
        e.id_envio, e.codigo_seguimiento, e.origen, e.fecha_registro, e.id_paquete, e.id_destino, e.id_remitente,
        e.intentos_fallidos, p.descripcion, p.peso, p.alto, p.ancho, p.largo, d.direccion, d.ciudad, d.codigo_postal,
        d.provincia, d.dni_destinatario, d.nombre_destinatario, d.apellido_destinatario, d.telefono_destinatario,
        d.email_destinatario, 1
    FROM @EnviosARegistrar a
    INNER JOIN Envio e ON a.id_envio = e.id_envio
    INNER JOIN Paquete p ON e.id_paquete = p.id_paquete
    INNER JOIN Destino d ON e.id_destino = d.id_destino;
END;
GO

CREATE OR ALTER TRIGGER TR_Destino_Auditoria
ON dbo.Destino
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF TRY_CONVERT(BIT, SESSION_CONTEXT(N'ModificacionEnvio')) = 1 RETURN;
    IF TRY_CONVERT(BIT, SESSION_CONTEXT(N'RestaurandoEnvio')) = 1 RETURN;

    DECLARE @EnviosARegistrar TABLE (id_envio INT PRIMARY KEY);

    INSERT INTO @EnviosARegistrar (id_envio)
    SELECT DISTINCT e.id_envio
    FROM inserted i
    INNER JOIN deleted d ON i.id_destino = d.id_destino
    INNER JOIN Envio e ON e.id_destino = i.id_destino
    WHERE EXISTS (
        SELECT i.direccion, i.ciudad, i.codigo_postal, i.provincia, i.dni_destinatario, i.nombre_destinatario, i.apellido_destinatario, i.telefono_destinatario, i.email_destinatario
        EXCEPT
        SELECT d.direccion, d.ciudad, d.codigo_postal, d.provincia, d.dni_destinatario, d.nombre_destinatario, d.apellido_destinatario, d.telefono_destinatario, d.email_destinatario
    );

    UPDATE ec SET ec.Act = 0
    FROM Envio_C ec
    INNER JOIN @EnviosARegistrar a ON a.id_envio = ec.id_envio
    WHERE ec.Act = 1;

    INSERT INTO Envio_C (
        id_envio, codigo_seguimiento, origen, fecha_registro, id_paquete, id_destino, id_remitente, intentos_fallidos,
        descripcion_paquete, peso, alto, ancho, largo, direccion, ciudad, codigo_postal, provincia,
        dni_destinatario, nombre_destinatario, apellido_destinatario, telefono_destinatario, email_destinatario, Act
    )
    SELECT
        e.id_envio, e.codigo_seguimiento, e.origen, e.fecha_registro, e.id_paquete, e.id_destino, e.id_remitente,
        e.intentos_fallidos, p.descripcion, p.peso, p.alto, p.ancho, p.largo, d.direccion, d.ciudad, d.codigo_postal,
        d.provincia, d.dni_destinatario, d.nombre_destinatario, d.apellido_destinatario, d.telefono_destinatario,
        d.email_destinatario, 1
    FROM @EnviosARegistrar a
    INNER JOIN Envio e ON a.id_envio = e.id_envio
    INNER JOIN Paquete p ON e.id_paquete = p.id_paquete
    INNER JOIN Destino d ON e.id_destino = d.id_destino;
END;
GO

CREATE OR ALTER TRIGGER [dbo].[TR_Envio_C_Restaurar]
ON [dbo].[Envio_C]
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (
        SELECT 1
        FROM inserted i
        INNER JOIN deleted d ON i.id_version = d.id_version
        WHERE i.Act = 1 AND d.Act = 0
    )
        RETURN;

    EXEC sys.sp_set_session_context @key = N'RestaurandoEnvio', @value = 1;

    BEGIN TRY
        -- Restaurar los datos propios del envío.
        UPDATE e SET
            e.codigo_seguimiento = i.codigo_seguimiento, e.origen = i.origen, e.fecha_registro = i.fecha_registro,
            e.estado = CASE WHEN e.estado = 'Cancelado' THEN 'Registrado' ELSE e.estado END,
            e.id_paquete = i.id_paquete, e.id_destino = i.id_destino, e.id_remitente = i.id_remitente,
            e.intentos_fallidos = i.intentos_fallidos
        FROM Envio e
        INNER JOIN inserted i ON e.id_envio = i.id_envio
        INNER JOIN deleted d ON i.id_version = d.id_version
        WHERE i.Act = 1 AND d.Act = 0;

        -- Restaurar los datos del paquete.
        UPDATE p SET
            p.descripcion = i.descripcion_paquete, p.peso = i.peso, p.alto = i.alto, p.ancho = i.ancho, p.largo = i.largo
        FROM Paquete p
        INNER JOIN inserted i ON p.id_paquete = i.id_paquete
        INNER JOIN deleted d ON i.id_version = d.id_version
        WHERE i.Act = 1 AND d.Act = 0;

        -- Restaurar los datos del destino y destinatario.
        UPDATE d SET
            d.direccion = i.direccion, d.ciudad = i.ciudad, d.codigo_postal = i.codigo_postal, d.provincia = i.provincia,
            d.dni_destinatario = i.dni_destinatario, d.nombre_destinatario = i.nombre_destinatario,
            d.apellido_destinatario = i.apellido_destinatario, d.telefono_destinatario = i.telefono_destinatario,
            d.email_destinatario = i.email_destinatario
        FROM Destino d
        INNER JOIN inserted i ON d.id_destino = i.id_destino
        INNER JOIN deleted dc ON i.id_version = dc.id_version
        WHERE i.Act = 1 AND dc.Act = 0;

        EXEC sys.sp_set_session_context @key = N'RestaurandoEnvio', @value = NULL;
    END TRY
    BEGIN CATCH
        EXEC sys.sp_set_session_context @key = N'RestaurandoEnvio', @value = NULL;
        THROW;
    END CATCH
END;
GO

CREATE OR ALTER TRIGGER TR_Envio_Cancelado
ON dbo.Envio
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE EC
    SET Act = 0
    FROM dbo.Envio_C EC
    INNER JOIN inserted I ON EC.id_envio = I.id_envio
    INNER JOIN deleted D ON I.id_envio = D.id_envio
    WHERE I.estado = 'Cancelado'
      AND D.estado <> 'Cancelado'
      AND EC.Act = 1;
END;
GO