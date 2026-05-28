CREATE DATABASE SistemaDeEnvios;
GO;

USE SistemaDeEnvios;
GO;

CREATE TABLE Rol (
    id_rol INT IDENTITY(1,1) PRIMARY KEY,
    descripcion VARCHAR(50) NOT NULL
);

CREATE TABLE Usuario (
    dni INT PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL,
    apellido VARCHAR(50) NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL,
    password VARCHAR(255) NOT NULL,
    id_rol INT NOT NULL,
    bloqueado BIT NOT NULL DEFAULT 0,
    estado BIT NOT NULL DEFAULT 1,
    intentos_fallidos INT NOT NULL DEFAULT 0,

    FOREIGN KEY (id_rol) REFERENCES Rol(id_rol)
);

CREATE TABLE Envio (
    id_envio INT IDENTITY(1,1) PRIMARY KEY,
    codigo_seguimiento VARCHAR(20) UNIQUE NOT NULL,
    direccion_origen VARCHAR(150),
    direccion_destino VARCHAR(150),
    peso DECIMAL(10,2),
    dimensiones VARCHAR(50),
    costo DECIMAL(10,2),
    estado VARCHAR(30)
);

CREATE TABLE Pago (
    id_pago INT IDENTITY(1,1) PRIMARY KEY,
    id_envio INT NOT NULL,
    medio_pago VARCHAR(50),
    titular VARCHAR(100),
    monto DECIMAL(10,2),
    fecha_pago DATETIME DEFAULT GETDATE(),

    FOREIGN KEY (id_envio) REFERENCES Envio(id_envio)
);

CREATE TABLE Evento (
    id_evento INT IDENTITY(1,1) PRIMARY KEY,
    dni_usuario INT NOT NULL,
    fecha DATETIME NOT NULL DEFAULT GETDATE(),
    modulo VARCHAR(100) NOT NULL,
    evento VARCHAR(100) NOT NULL,
    criticidad INT,

    FOREIGN KEY (dni_usuario) REFERENCES Usuario(dni)
);

INSERT INTO Rol (descripcion)
VALUES
('Administrador'),
('Recepcionista'),
('Gestor'),
('Repartidor'),
('Remitente/Destinatario');

-- Administrador base
INSERT INTO Usuario VALUES (12345678, 'Administrador', 'Sistema', 'admin@sistema.com', '$2a$11$lVbVvtP4dYgekTqzbwg2zOpEOIoEKEkyuckdej7hbDo/vvOS69dMO', 1, 0 ,1);
-- La contraseña en el programa es admin123