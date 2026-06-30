CREATE DATABASE SistemaDeEnvios1;
GO

USE SistemaDeEnvios1;
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

INSERT INTO Idioma (nombre)
VALUES
('Español'),
('English'),
('Português');

INSERT INTO Perfil (nombre)
VALUES
('Administrador'),
('Recepcionista'),
('Gestor'),
('Repartidor'),
('Remitente/Destinatario');

INSERT INTO Permiso (nombre)
VALUES
('Cambiar contraseña'),
('Cambiar idioma'),
('Gestión de usuarios'),
('Bitácora de eventos'),
('Solicitar envío'),
('Registrar envío'),
('Consultar estado'),
('Modificar destino'),
('Cancelar envío'),
('Registrar pago'),
('Consultar comprobantes'),
('Generar rutas'),
('Asignar rutas'),
('Consultar rutas'),
('Registrar entrega'),
('Solicitar devolución'),
('Calificar repartidor'),
('Reportar inconveniente'),
('Reporte de envíos'),
('Reporte de entregas'),
('Reporte de inconvenientes'),
('Reporte de repartidores'),
('Reporte de pagos'),
('Reporte de analíticas operativas');

INSERT INTO DVV(tabla)
VALUES
('Perfil'),
('Familia'),
('Permiso'),
('Perfil_Familia'),
('Perfil_Permiso'),
('Familia_Permiso'),
('Familia_Familia'),
('Idioma'),
('Usuario'),
('Evento');

-- Administrador base
INSERT INTO Usuario (dni, nombre, apellido, email, password, id_perfil) VALUES (12345678, 'Administrador', 'Sistema', 'admin@sistema.com', '$2a$11$lVbVvtP4dYgekTqzbwg2zOpEOIoEKEkyuckdej7hbDo/vvOS69dMO', 1);
-- La contraseña en el programa es admin123
